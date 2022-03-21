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

using System;
using System.Collections.Generic;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using DiffPlex.Model;

namespace SCompareText
{
    /// <summary>
    ///Code to work with the Diffplex library. Most of this is boilerplate code from their examples.
    ///Example: https://github.com/mmanela/diffplex/blob/master/DiffPlex/DiffBuilder/SideBySideDiffBuilder.cs
    /// </summary>
    public class SideBySideDiffBuilder : ISideBySideDiffBuilder
    {
        private readonly IDiffer differ;

        private delegate void PieceBuilder(string oldText, string newText, List<DiffPiece> oldPieces, List<DiffPiece> newPieces);

        public static readonly char[] WordSeparaters = { ' ', '\t', '.', '(', ')', '{', '}', ',', '!' };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="differ"></param>
        public SideBySideDiffBuilder(IDiffer differ)
        {
            //?? returns the value of its left-hand operand if it isn't null;
            //otherwise, it evaluates the right-hand operand and returns its result.
            //The ?? operator doesn't evaluate its right-hand operand
            //if the left-hand operand evaluates to non-null.
            this.differ = differ ?? throw new ArgumentNullException(nameof(differ));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldText"></param>
        /// <param name="newText"></param>
        /// <returns></returns>
        [Obsolete]
        public SideBySideDiffModel BuildDiffModel(string oldText, string newText)
        {
            return BuildLineDiff(
                oldText ?? throw new ArgumentNullException(nameof(oldText)),
                newText ?? throw new ArgumentNullException(nameof(newText)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldText"></param>
        /// <param name="newText"></param>
        /// <returns></returns>
        [Obsolete]
        private SideBySideDiffModel BuildLineDiff(string oldText, string newText)
        {
            var model = new SideBySideDiffModel();
            var diffResult = differ.CreateLineDiffs(oldText, newText, ignoreWhitespace: true);
            BuildDiffPieces(diffResult, model.OldText.Lines, model.NewText.Lines, BuildWordDiffPieces);
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oldText"></param>
        /// <param name="newText"></param>
        /// <param name="oldPieces"></param>
        /// <param name="newPieces"></param>
        [Obsolete]
        private void BuildWordDiffPieces(string oldText, string newText, List<DiffPiece> oldPieces, List<DiffPiece> newPieces)
        {
            var diffResult = differ.CreateWordDiffs(oldText, newText, false, WordSeparaters);
            BuildDiffPieces(diffResult, oldPieces, newPieces, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="diffResult"></param>
        /// <param name="oldPieces"></param>
        /// <param name="newPieces"></param>
        /// <param name="subPieceBuilder"></param>
        private static void BuildDiffPieces(DiffResult diffResult, List<DiffPiece> oldPieces, List<DiffPiece> newPieces, PieceBuilder subPieceBuilder)
        {
            int aPos = 0;
            int bPos = 0;

            foreach (var diffBlock in diffResult.DiffBlocks)
            {
                while (bPos < diffBlock.InsertStartB && aPos < diffBlock.DeleteStartA)
                {
                    oldPieces.Add(new DiffPiece(diffResult.PiecesOld[aPos], ChangeType.Unchanged, aPos + 1));
                    newPieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));
                    aPos++;
                    bPos++;
                }

                int i = 0;
                for (; i < Math.Min(diffBlock.DeleteCountA, diffBlock.InsertCountB); i++)
                {
                    var oldPiece = new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted, aPos + 1);
                    var newPiece = new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1);

                    if (subPieceBuilder != null)
                    {
                        subPieceBuilder(diffResult.PiecesOld[aPos], diffResult.PiecesNew[bPos], oldPiece.SubPieces, newPiece.SubPieces);
                        newPiece.Type = oldPiece.Type = ChangeType.Modified;
                    }

                    oldPieces.Add(oldPiece);
                    newPieces.Add(newPiece);
                    aPos++;
                    bPos++;
                }

                if (diffBlock.DeleteCountA > diffBlock.InsertCountB)
                {
                    for (; i < diffBlock.DeleteCountA; i++)
                    {
                        oldPieces.Add(new DiffPiece(diffResult.PiecesOld[i + diffBlock.DeleteStartA], ChangeType.Deleted, aPos + 1));
                        newPieces.Add(new DiffPiece());
                        aPos++;
                    }
                }
                else
                {
                    for (; i < diffBlock.InsertCountB; i++)
                    {
                        newPieces.Add(new DiffPiece(diffResult.PiecesNew[i + diffBlock.InsertStartB], ChangeType.Inserted, bPos + 1));
                        oldPieces.Add(new DiffPiece());
                        bPos++;
                    }
                }
            }

            while (bPos < diffResult.PiecesNew.Length && aPos < diffResult.PiecesOld.Length)
            {
                oldPieces.Add(new DiffPiece(diffResult.PiecesOld[aPos], ChangeType.Unchanged, aPos + 1));
                newPieces.Add(new DiffPiece(diffResult.PiecesNew[bPos], ChangeType.Unchanged, bPos + 1));
                aPos++;
                bPos++;
            }
        }
    }
}
