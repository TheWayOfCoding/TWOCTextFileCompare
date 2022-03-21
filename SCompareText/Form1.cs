/*
 * TWOC Text File Compare
 * Copyright 2022, Scott Waldron
 * TheWayOfCoding.com
 * 
 * Official source location: 
 * https://github.com/TheWayOfCoding/TWOCTextFileCompare
 * 
 * This file is part of TWOC Text File Compare.
 * 
 * TWOC Text File Compare is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.
 * 
 * TWOC Text File Compare is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License along with TWOC Text File Compare. If not, see <https://www.gnu.org/licenses/>.
 * 
 * *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *
 * This software relies on open source libraries (through NuGet):
 * Look at the libraries to see what is allowed under their license terms.
 * 
 * DiffPlex by Matthew Manela
 * License: Apache-2.0 License
 * Released with version 1.7.1
 * https://github.com/mmanela/diffplex/blob/master/License.txt
 * https://github.com/mmanela/diffplex
 * 
 * Fast Colored TextBox by Pavel Torgashov
 * License: GNU Lesser General Public License (LGPLv3)
 * Released with version 2.16.24
 * https://github.com/PavelTorgashov/FastColoredTextBox/blob/master/license.txt
 * https://github.com/PavelTorgashov/FastColoredTextBox
 * https://www.codeproject.com/Articles/161871/Fast-Colored-TextBox-for-syntax-highlighting-2
 * 
 * *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  *  
 */

using DiffPlex.DiffBuilder.Model;
using FastColoredTextBoxNS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCompareText
{
    public partial class Form1 : Form
    {
        int updatingDualScroll = 0;

        /// <summary>
        /// constructor before form load
        /// </summary>
        public Form1()
        {
            InitializeComponent();

            Globals.allDifferences[0].Columns.Add("position");
            Globals.allDifferences[0].Columns.Add("type");
            Globals.allDifferences[0].Columns.Add("text");
            Globals.allDifferences[1].Columns.Add("position");
            Globals.allDifferences[1].Columns.Add("type");
            Globals.allDifferences[1].Columns.Add("text");
        }

        /// <summary>
        /// the primary function to process the two selected source files
        /// </summary>
        private void VerifyFilesSelectedAndLoad()
        {
            //make sure that two files are selected and they exist
            if ((Globals.fileLocation[0].Trim() != string.Empty && File.Exists(Globals.fileLocation[0]))
                && (Globals.fileLocation[1].Trim() != string.Empty && File.Exists(Globals.fileLocation[1])))
            {
                //initiate the diff processing with a worker object
                if (!bwSideBySideDiffScreen.IsBusy)
                {
                    PbLoading.Visible = true;

                    //not making sure these listboxes are unlinked is important
                    //if they are linked then it would slow the entire loading process
                    LbDiff0.DataSource = new DataTable();
                    LbDiff0.DisplayMember = "";
                    LbDiff0.ValueMember = "";
                    LbDiff1.DataSource = new DataTable();
                    LbDiff1.DisplayMember = "";
                    LbDiff1.ValueMember = "";

                    //make sure no files exist
                    FctbFile0.CloseBindingFile();
                    FctbFile1.CloseBindingFile();
                    FctbFile0.Clear();
                    FctbFile1.Clear();

                    //make sure any temporary files are deleted (used in lazy loading)
                    //this has to be done after the display screen are unbound from the files if they exist
                    if ((Globals.tempFileLocation[0] != null
                        || Globals.tempFileLocation[0] != string.Empty)
                        && File.Exists(Globals.tempFileLocation[0]))
                    {
                        File.Delete(Globals.tempFileLocation[0]);
                    }
                    if ((Globals.tempFileLocation[1] != null
                        || Globals.tempFileLocation[1] != string.Empty)
                        && File.Exists(Globals.tempFileLocation[1]))
                    {
                        File.Delete(Globals.tempFileLocation[1]);
                    }

                    //start the background worker to compare text
                    bwSideBySideDiffScreen.RunWorkerAsync();
                }

            }
        }

        /// <summary>
        /// processes a single line of comparison so it can be displayed to the user 
        /// </summary>
        /// <param name="activeLine"></param>
        /// <param name="fileNumber"></param>
        /// <param name="fileLocation"></param>
        /// <remarks>this should not be called when activeLine.Type == ChangeType.Unchanged</remarks>
        private void ProcessDiffLine(DiffPiece activeLine, int fileNumber, long fileLocation)
        {
            string textualType = "";

            //translate the library's difference state to this program's listboxes
            switch (activeLine.Type)
            {
                case ChangeType.Deleted:
                    textualType = "DEL";
                    break;
                case ChangeType.Imaginary:
                    textualType = "BLANK";
                    break;
                case ChangeType.Inserted:
                    textualType = "INS";
                    break;
                case ChangeType.Modified:
                    textualType = "MOD";
                    break;
            }

            //add all lines that are not unchanged
            //position, type, text
            Globals.allDifferences[fileNumber].Rows.Add(
                fileLocation,
                activeLine.Type,
                (fileLocation + 1) +
                    ", " + textualType +
                    ", " + activeLine.Text);
        }

        /// <summary>
        /// when one of the textboxes moves we need to process a refresh
        /// </summary>
        /// <param name="tb"></param>
        /// <param name="vPos"></param>
        /// <param name="curLine"></param>
        void UpdateScroll(FastColoredTextBox tb, int vPos, int curLine)
        {
            //prevent further processing if there is something in the queue
            if (updatingDualScroll > 0)
            {
                return;
            }

            BeginUpdateDualScroll();

            if (vPos <= tb.VerticalScroll.Maximum)
            {
                tb.VerticalScroll.Value = vPos;
                tb.UpdateScrollbars();
            }

            if (curLine < tb.LinesCount)
            {
                tb.Selection = new Range(tb, 0, curLine, 0, curLine);
            }

            EndUpdateDualScroll();
        }

        /// <summary>
        /// part of the dual textbox display system
        /// </summary>
        private void EndUpdateDualScroll()
        {
            updatingDualScroll--;
        }

        /// <summary>
        /// part of the dual textbox display system
        /// </summary>
        private void BeginUpdateDualScroll()
        {
            updatingDualScroll++;
        }

        /// <summary>
        /// do initial heavy processing in a thread seperate from the UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private void BwSideBySideDiffScreen_DoWork(object sender, DoWorkEventArgs e)
        {
            //if this isn't the first time we need to reset our listbox difference datasets
            Globals.allDifferences[0].Rows.Clear();
            Globals.allDifferences[1].Rows.Clear();

            //load both selected files into memory and pass them to the diff processor
            Globals.diffModel = Globals.diffScreen.BuildDiffModel(File.ReadAllText(Globals.fileLocation[0]),
                File.ReadAllText(Globals.fileLocation[1]));

            //select out the objects that show differences between the documents
            Globals.diffText[0] = Globals.diffModel.OldText;
            Globals.diffText[1] = Globals.diffModel.NewText;

            //make certain there is valid data in both diff objects
            if (Globals.diffText[0] != null && Globals.diffText[1] != null)
            {
                //get a temporary location for two files we will generate
                string tempDirectory = Path.GetTempPath();

                //we need to find a temporary filename to push data into
                //in this case we increment a number afterward until no file exists
                //there is the potential that the user is running multiple
                //instances of this program, which means we can't use two set filenames
                bool[] noTempFileFound = new bool[2] { false, false };
                int availableFileNumber = 0;
                for (int faIndex = 0; faIndex < 2; ++faIndex) //do this for both selected source files
                {
                    //keep trying until an available filename is found
                    while (!noTempFileFound[faIndex])
                    {
                        string currentPotentialFilenameAndPath = Path.Combine(tempDirectory,
                            "ncompare-f" + availableFileNumber + ".txt");

                        //see if a file already exists
                        if (!File.Exists(currentPotentialFilenameAndPath))
                        {
                            //assign the empty filename to our current global file+path variable
                            Globals.tempFileLocation[faIndex] = currentPotentialFilenameAndPath;
                            noTempFileFound[faIndex] = true;
                        }

                        ++availableFileNumber;
                    }
                }

                //write a diff parsed version of the first file loaded
                //we have to write to disk because of the need for "lazy loading"
                long filePosition0 = 0;
                StringBuilder sbText0 = new StringBuilder();
                foreach (DiffPiece diffLine in Globals.diffText[0].Lines)
                {
                    //pull out all lines one at a time
                    //this will be written to a temporary file
                    //The display screens will "lazy load" from that
                    sbText0.AppendLine(diffLine.Text);

                    //save all differences in this document to a data table object that
                    //will be linked with a listbox for the user to jump to different areas
                    if (diffLine.Type != ChangeType.Unchanged)
                    {
                        ProcessDiffLine(diffLine, 0, filePosition0);
                    }

                    ++filePosition0;
                }
                using (StreamWriter sw = new StreamWriter(Globals.tempFileLocation[0], false, Encoding.UTF8, 65536))
                {
                    sw.Write(sbText0.ToString()); //push everything at once to a file
                }

                //write the second diff parsed file
                StringBuilder sbText1 = new StringBuilder();
                long filePosition1 = 0;
                foreach (DiffPiece diffLine in Globals.diffText[1].Lines)
                {
                    sbText1.AppendLine(diffLine.Text); //pull out all lines one at a time

                    //save all differences in this document to a data table object that
                    //will be linked with a listbox for the user to jump to different areas
                    if (diffLine.Type != ChangeType.Unchanged)
                    {
                        ProcessDiffLine(diffLine, 1, filePosition1);
                    }

                    ++filePosition1;
                }
                using (StreamWriter sw = new StreamWriter(Globals.tempFileLocation[1], false, Encoding.UTF8, 65536))
                {
                    sw.Write(sbText1.ToString()); //push everything at once to a file
                }

                //bind the two created files to the two custom control displays
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    if (File.Exists(Globals.tempFileLocation[0]))
                    {
                        try
                        {
                            //bind the two files to their respective controls
                            FctbFile0.OpenBindingFile(Globals.tempFileLocation[0], Encoding.UTF8);
                            FctbFile0.IsChanged = false;
                            FctbFile0.ClearUndo();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("There was an issue with the file's encoding.");
                        }
                    }
                    //I'm not sure this is needed...
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                }));

                this.BeginInvoke(new MethodInvoker(delegate
                {
                    if (File.Exists(Globals.tempFileLocation[1]))
                    {
                        try
                        {
                            FctbFile1.OpenBindingFile(Globals.tempFileLocation[1], Encoding.UTF8);
                            FctbFile1.IsChanged = false;
                            FctbFile1.ClearUndo();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("There was an issue with the file's encoding.");
                        }
                    }
                    //I'm not sure this is needed...
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                }));
            }
        }

        /// <summary>
        /// highlight only visible area of text
        /// </summary>
        /// <param name="currentRange"></param>
        /// <param name="fileNumber"></param>
        /// <param name="activeTextBox"></param>
        private void HTMLSyntaxHighlight(Range currentRange, int fileNumber, FastColoredTextBox activeTextBox)
        {
            if (Globals.diffText[fileNumber] != null)
            {
                //extract out lines from the current display range
                IEnumerable<Range> singleLines = currentRange.GetRanges(".+",
                    System.Text.RegularExpressions.RegexOptions.IgnoreCase); //\r\n|\r|\n

                foreach (Range singleDisplayLine in singleLines)
                {
                    //singleDisplayLine.SetStyle(Globals.nsStyleImaginary);
                    singleDisplayLine.ClearStyle(StyleIndex.All);

                    int currentScreenLine = singleDisplayLine.ToLine;

                    //make sure we are withing bounds of the diffplex object
                    if (currentScreenLine < Globals.diffText[fileNumber].Lines.Count)
                    {
                        //extract out a diffplex piece to see if it needs coloring
                        DiffPiece singleDiffPiece = Globals.diffText[fileNumber].Lines[currentScreenLine];

                        //color each line based on type
                        switch (singleDiffPiece.Type)
                        {
                            case ChangeType.Deleted:
                                singleDisplayLine.SetStyle(Globals.nsStyleDeleted);
                                break;

                            case ChangeType.Imaginary:
                                singleDisplayLine.SetStyle(Globals.nsStyleImaginary);
                                break;

                            case ChangeType.Inserted:
                                singleDisplayLine.SetStyle(Globals.nsStyleInserted);
                                break;

                            case ChangeType.Modified:
                                singleDisplayLine.SetStyle(Globals.nsStyleModified);

                                //for modified lines we have to go character by character
                                //sections of the line will have different styles
                                int textLengthTracker = 0;
                                for (int sci = 0; sci < singleDiffPiece.SubPieces.Count; ++sci)
                                {
                                    DiffPiece singleDPiece = singleDiffPiece.SubPieces[sci];

                                    if (singleDPiece.Type == ChangeType.Imaginary) { continue; }
                                    switch (singleDPiece.Type)
                                    {
                                        case ChangeType.Inserted:
                                            Range subRangeInserted = new Range(activeTextBox,
                                                textLengthTracker,
                                                currentScreenLine,
                                                textLengthTracker + singleDPiece.Text.Length,
                                                currentScreenLine);
                                            subRangeInserted.SetStyle(Globals.nsStyleInserted);
                                            break;
                                        case ChangeType.Deleted:
                                            Range subRangeDeleted = new Range(activeTextBox,
                                                textLengthTracker,
                                                currentScreenLine,
                                                textLengthTracker + singleDPiece.Text.Length,
                                                currentScreenLine);
                                            subRangeDeleted.SetStyle(Globals.nsStyleDeletedCharacter);
                                            break;
                                    }

                                    if (singleDPiece.Text != null)
                                    {
                                        textLengthTracker += singleDPiece.Text.Length;
                                    }
                                }
                                break;

                            case ChangeType.Unchanged:
                                singleDisplayLine.SetStyle(Globals.nsStyleUnchanged);
                                break;

                            default:
                                singleDisplayLine.SetStyle(Globals.nsStyleUnexpected);
                                break;
                        } //switch
                    } //if
                } //foreach
            } //if
        }

        //--------------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [Obsolete]
        private void bwSideBySideDiffScreen_DoWork_1(object sender, DoWorkEventArgs e)
        {
            Globals.allDifferences[0].Rows.Clear();
            Globals.allDifferences[1].Rows.Clear();

            //load both selected files into memory and pass them to the diff processor
            Globals.diffModel = Globals.diffScreen.BuildDiffModel(File.ReadAllText(Globals.fileLocation[0]),
                File.ReadAllText(Globals.fileLocation[1]));

            //select out the objects that show differences between the documents
            Globals.diffText[0] = Globals.diffModel.OldText;
            Globals.diffText[1] = Globals.diffModel.NewText;

            //make certain there is valid data in both diff objects
            if (Globals.diffText[0] != null && Globals.diffText[1] != null)
            {
                //get a temporary location for two files we will generate
                string tempDirectory = Path.GetTempPath();

                //we need to find a temporary filename to push data into
                //in this case we increment a number afterward until no file exists
                //there is the potential that the user is running multiple
                //instances of this program, which means we can't use two set filenames
                bool[] noTempFileFound = new bool[2] { false, false };
                int availableFileNumber = 0;
                for (int faIndex = 0; faIndex < 2; ++faIndex) //do this for both selected source files
                {
                    //keep trying until an available filename is found
                    while (!noTempFileFound[faIndex])
                    {
                        string currentPotentialFilenameAndPath = Path.Combine(tempDirectory,
                            "ncompare-f" + availableFileNumber + ".txt");

                        //see if a file already exists
                        if (!File.Exists(currentPotentialFilenameAndPath))
                        {
                            //assign the empty filename to our current global file+path variable
                            Globals.tempFileLocation[faIndex] = currentPotentialFilenameAndPath;
                            noTempFileFound[faIndex] = true;
                        }

                        ++availableFileNumber;
                    }
                }

                //write a diff parsed version of the first file loaded
                //we have to write to disk because of the need for "lazy loading"
                long filePosition0 = 0;
                StringBuilder sbText0 = new StringBuilder();
                foreach (DiffPiece diffLine in Globals.diffText[0].Lines)
                {
                    //pull out all lines one at a time
                    //this will be written to a temporary file
                    //The display screens will "lazy load" from that
                    sbText0.AppendLine(diffLine.Text);

                    //save all differences in this document to a data table object that
                    //will be linked with a listbox for the user to jump to different areas
                    if (diffLine.Type != ChangeType.Unchanged)
                    {
                        ProcessDiffLine(diffLine, 0, filePosition0);
                    }

                    ++filePosition0;
                }
                using (StreamWriter sw = new StreamWriter(Globals.tempFileLocation[0], false, Encoding.UTF8, 65536))
                {
                    sw.Write(sbText0.ToString()); //push everything at once to a file
                }

                //write the second diff parsed file
                StringBuilder sbText1 = new StringBuilder();
                long filePosition1 = 0;
                foreach (DiffPiece diffLine in Globals.diffText[1].Lines)
                {
                    sbText1.AppendLine(diffLine.Text); //pull out all lines one at a time

                    //save all differences in this document to a data table object that
                    //will be linked with a listbox for the user to jump to different areas
                    if (diffLine.Type != ChangeType.Unchanged)
                    {
                        ProcessDiffLine(diffLine, 1, filePosition1);
                    }

                    ++filePosition1;
                }
                using (StreamWriter sw = new StreamWriter(Globals.tempFileLocation[1], false, Encoding.UTF8, 65536))
                {
                    sw.Write(sbText1.ToString()); //push everything at once to a file
                }

                //bind the two created files to the two custom control displays
                this.BeginInvoke(new MethodInvoker(delegate
                {
                    if (File.Exists(Globals.tempFileLocation[0]))
                    {
                        try
                        {
                            //bind the two files to their respective controls
                            FctbFile0.OpenBindingFile(Globals.tempFileLocation[0], Encoding.UTF8);
                            FctbFile0.IsChanged = false;
                            FctbFile0.ClearUndo();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("There was an issue with the file's encoding.");
                        }
                    }
                    //I'm not sure this is needed...
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                }));

                this.BeginInvoke(new MethodInvoker(delegate
                {
                    if (File.Exists(Globals.tempFileLocation[1]))
                    {
                        try
                        {
                            FctbFile1.OpenBindingFile(Globals.tempFileLocation[1], Encoding.UTF8);
                            FctbFile1.IsChanged = false;
                            FctbFile1.ClearUndo();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("There was an issue with the file's encoding.");
                        }
                    }
                    //I'm not sure this is needed...
                    //GC.Collect();
                    //GC.GetTotalMemory(true);
                }));
            }
        }

        /// <summary>
        /// finish off processing of the differences by linking data with controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bwSideBySideDiffScreen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //link the list of changes to the related listbox
            if (Globals.allDifferences[0].Rows.Count > 0)
            {
                LbDiff0.DataSource = Globals.allDifferences[0];
                LbDiff0.DisplayMember = "text";
                LbDiff0.ValueMember = "position";
            }
            else
            {
                LbDiff0.DataSource = new DataTable();
                LbDiff0.DisplayMember = "";
                LbDiff0.ValueMember = "";
            }

            //link the list of changes to the related listbox
            if (Globals.allDifferences[1].Rows.Count > 0)
            {
                LbDiff1.DataSource = Globals.allDifferences[1];
                LbDiff1.DisplayMember = "text";
                LbDiff1.ValueMember = "position";
            }
            else
            {
                LbDiff1.DataSource = new DataTable();
                LbDiff1.DisplayMember = "";
                LbDiff1.ValueMember = "";
            }

            //visually finish loading for the use by removing the loading spinner
            PbLoading.Visible = false;
        }

        /// <summary>
        /// let the user reload the two selected files if they want
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReloadMenuItem_Click(object sender, EventArgs e)
        {
            VerifyFilesSelectedAndLoad();
        }

        /// <summary>
        /// allow the user to select one file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadFile0_Click(object sender, EventArgs e)
        {
            DialogResult selectedFile = ofdMain.ShowDialog();

            //make sure the user selected a file and it exists
            if (selectedFile == DialogResult.OK && File.Exists(ofdMain.FileName))
            {
                Globals.fileLocation[0] = ofdMain.FileName;
                TxtSelectFile0.Text = ofdMain.FileName;
                VerifyFilesSelectedAndLoad();
            }
        }

        /// <summary>
        /// allow the user to select one file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLoadFile1_Click(object sender, EventArgs e)
        {
            DialogResult selectedFile = ofdMain.ShowDialog();

            //make sure the user selected a file and it exists
            if (selectedFile == DialogResult.OK && File.Exists(ofdMain.FileName))
            {
                Globals.fileLocation[1] = ofdMain.FileName;
                TxtSelectFile1.Text = ofdMain.FileName;
                VerifyFilesSelectedAndLoad();
            }
        }

        /// <summary>
        /// perform a few necessary tasks before the application closes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //make certain that the two displays are not binded to files
            FctbFile0.CloseBindingFile();
            FctbFile1.CloseBindingFile();

            //make sure any temporary files are deleted (used in lazy loading)
            if (File.Exists(Globals.tempFileLocation[0]))
            {
                File.Delete(Globals.tempFileLocation[0]);
            }
            if (File.Exists(Globals.tempFileLocation[1]))
            {
                File.Delete(Globals.tempFileLocation[1]);
            }

            //clear any special objects
            Globals.allDifferences[0].Dispose();
            Globals.allDifferences[1].Dispose();
        }

        /// <summary>
        /// process changes when the user scrolls this textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FctbFile0_VisibleRangeChanged(object sender, EventArgs e)
        {
            //prevent further processing if there is something in the queue
            if (updatingDualScroll > 0)
            {
                return;
            }

            int vPos = (sender as FastColoredTextBox).VerticalScroll.Value;
            int curLine = (sender as FastColoredTextBox).Selection.Start.iLine;

            if (sender == FctbFile1)
            {
                UpdateScroll(FctbFile0, vPos, curLine);
            }
            else
            {
                UpdateScroll(FctbFile1, vPos, curLine);
            }

            //highlight only visible area of text
            HTMLSyntaxHighlight(FctbFile0.VisibleRange, 0, FctbFile0);
            HTMLSyntaxHighlight(FctbFile1.VisibleRange, 1, FctbFile1);

            FctbFile0.Refresh();
            FctbFile1.Refresh();
        }

        /// <summary>
        /// process changes when the user scrolls this textbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FctbFile1_VisibleRangeChanged(object sender, EventArgs e)
        {
            //prevent further processing if there is something in the queue
            if (updatingDualScroll > 0)
            {
                return;
            }

            int vPos = (sender as FastColoredTextBox).VerticalScroll.Value;
            int curLine = (sender as FastColoredTextBox).Selection.Start.iLine;

            if (sender == FctbFile1)
            {
                UpdateScroll(FctbFile0, vPos, curLine);
            }
            else
            {
                UpdateScroll(FctbFile1, vPos, curLine);
            }

            //highlight only visible area of text
            HTMLSyntaxHighlight(FctbFile0.VisibleRange, 0, FctbFile0);
            HTMLSyntaxHighlight(FctbFile1.VisibleRange, 1, FctbFile1);

            FctbFile1.Refresh();
            FctbFile0.Refresh();
        }

        /// <summary>
        /// move the position shown in the textboxes based on which
        /// item is selected in the differences listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDiff0_Click(object sender, EventArgs e)
        {
            if (LbDiff0.SelectedIndex > -1 && LbDiff0.SelectedIndex < Globals.allDifferences[0].Rows.Count)
            {
                DataRow selectedRecord = Globals.allDifferences[0].Rows[LbDiff0.SelectedIndex];

                int valuePosition = 0;
                if (int.TryParse(selectedRecord["position"].ToString(), out valuePosition))
                {
                    //fctbFile0
                    FctbFile0.Navigate(valuePosition);
                }
            }
        }

        /// <summary>
        /// move the position shown in the textboxes based on which
        /// item is selected in the differences listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDiff1_Click(object sender, EventArgs e)
        {
            if (LbDiff1.SelectedIndex > -1 && LbDiff1.SelectedIndex < Globals.allDifferences[1].Rows.Count)
            {
                DataRow selectedRecord = Globals.allDifferences[1].Rows[LbDiff1.SelectedIndex];

                int valuePosition = 0;
                if (int.TryParse(selectedRecord["position"].ToString(), out valuePosition))
                {
                    //fctbFile0
                    FctbFile1.Navigate(valuePosition);
                }
            }
        }

        /// <summary>
        /// move the position shown in the textboxes based on which
        /// item is selected in the differences listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDiff0_KeyUp(object sender, KeyEventArgs e)
        {
            if (LbDiff0.SelectedIndex > -1 && LbDiff0.SelectedIndex < Globals.allDifferences[0].Rows.Count)
            {
                DataRow selectedRecord = Globals.allDifferences[0].Rows[LbDiff0.SelectedIndex];

                int valuePosition = 0;
                if (int.TryParse(selectedRecord["position"].ToString(), out valuePosition))
                {
                    //fctbFile0
                    FctbFile0.Navigate(valuePosition);
                }
            }
        }

        /// <summary>
        /// move the position shown in the textboxes based on which
        /// item is selected in the differences listbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LbDiff1_KeyUp(object sender, KeyEventArgs e)
        {
            if (LbDiff1.SelectedIndex > -1 && LbDiff1.SelectedIndex < Globals.allDifferences[1].Rows.Count)
            {
                DataRow selectedRecord = Globals.allDifferences[1].Rows[LbDiff1.SelectedIndex];

                int valuePosition = 0;
                if (int.TryParse(selectedRecord["position"].ToString(), out valuePosition))
                {
                    //fctbFile0
                    FctbFile1.Navigate(valuePosition);
                }
            }
        }

        /// <summary>
        /// reset the UI of any existing data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearAllMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult verifyUserAction = MessageBox.Show("Are you sure?", "Verify Action", MessageBoxButtons.OKCancel);

            if (verifyUserAction == DialogResult.OK)
            {
                //make certain that the two displays are not binded to files
                FctbFile0.CloseBindingFile();
                FctbFile1.CloseBindingFile();
                FctbFile0.Clear();
                FctbFile1.Clear();

                Globals.diffModel = null;

                //make sure any temporary files are deleted (used in lazy loading)
                if (File.Exists(Globals.tempFileLocation[0]))
                {
                    File.Delete(Globals.tempFileLocation[0]);
                }
                if (File.Exists(Globals.tempFileLocation[1]))
                {
                    File.Delete(Globals.tempFileLocation[1]);
                }

                //clear any special objects
                Globals.allDifferences[0].Rows.Clear();
                Globals.allDifferences[1].Rows.Clear();
                LbDiff0.DataSource = new DataTable();
                LbDiff0.DisplayMember = "";
                LbDiff0.ValueMember = "";
                LbDiff1.DataSource = new DataTable();
                LbDiff1.DisplayMember = "";
                LbDiff1.ValueMember = "";

                Globals.fileLocation[0] = string.Empty;
                Globals.fileLocation[1] = string.Empty;

                Globals.tempFileLocation[0] = string.Empty;
                Globals.tempFileLocation[1] = string.Empty;

                TxtSelectFile0.Text = string.Empty;
                TxtSelectFile1.Text = string.Empty;
            }
        }

        /// <summary>
        /// main entry point for the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            //fill the title bar with the application's current version
            Version version = Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Format("Text Compare v{0}.{1}.{2}, R{3}",
                version.Major, version.Minor, version.Build, version.Revision);
        }

        private void splitContainer3_SplitterMoved(object sender, SplitterEventArgs e)
        {

        }

        /// <summary>
        /// happens with the main dual text view split container changes size
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scDualTextView_SizeChanged(object sender, EventArgs e)
        {
            //keep the file selection table the same width as the dual text view width
            tlpSelectFiles.Width = (sender as SplitContainer).Width;
        }

        /// <summary>
        /// copy the entire old change list to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyOldFileDiff_Click(object sender, EventArgs e)
        {
            StringBuilder listToConvert = new StringBuilder();

            if(LbDiff0.Items.Count > 0)
            {
                //go through the entire listbox
                foreach(DataRowView singleItem in LbDiff0.Items)
                {
                    //extract one property from the dataset that's linked to the listbox
                    listToConvert.AppendLine(singleItem["text"].ToString());
                }

                //push the text data to the clipboard
                Clipboard.SetText(listToConvert.ToString());
            }
        }

        /// <summary>
        /// copy the entire new change list to the clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyNewFileDiff_Click(object sender, EventArgs e)
        {
            StringBuilder listToConvert = new StringBuilder();

            if (LbDiff1.Items.Count > 0)
            {
                //go through the entire listbox
                foreach (DataRowView singleItem in LbDiff1.Items)
                {
                    //extract one property from the dataset that's linked to the listbox
                    listToConvert.AppendLine(singleItem["text"].ToString());
                }

                //push the text data to the clipboard
                Clipboard.SetText(listToConvert.ToString());
            }


        }
    }
}
