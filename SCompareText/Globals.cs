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

using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCompareText
{
    class Globals
    {
        //management, storage, and processing objects
        public static string[] fileLocation = new string[2] { string.Empty, string.Empty };
        public static SideBySideDiffBuilder diffScreen = new SideBySideDiffBuilder(new Differ());
        public static SideBySideDiffModel diffModel = null;
        public static DiffPaneModel[] diffText = new DiffPaneModel[2] { null, null };
        public static string[] tempFileLocation = new string[2] { string.Empty, string.Empty };

        public static DataTable[] allDifferences = new DataTable[2] { new DataTable(), new DataTable() };

        //definte the coloration of between types of differences in the two files
        //it would be nice to offer the user the ability to change this...
        public static FastColoredTextBoxNS.Style nsStyleInserted =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFFF00")
                ));

        public static FastColoredTextBoxNS.Style nsStyleModified =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#DCDCFF")
                ));

        public static FastColoredTextBoxNS.Style nsStyleDeleted =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFC864")
                ));

        public static FastColoredTextBoxNS.Style nsStyleUnchanged =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")
                ));

        public static FastColoredTextBoxNS.Style nsStyleImaginary =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#C8C8C8")
                ));

        public static FastColoredTextBoxNS.Style nsStyleInsertedCharacter =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#FFFF96")
                ));

        public static FastColoredTextBoxNS.Style nsStyleDeletedCharacter =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#E88484")
                ));

        public static FastColoredTextBoxNS.Style nsStyleModifiedCharacterChange =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#E8E464")
        ));

        public static FastColoredTextBoxNS.Style nsStyleUnexpected =
            new FastColoredTextBoxNS.MarkerStyle(
            new SolidBrush(System.Drawing.ColorTranslator.FromHtml("#EE0000")
                ));
    }
}
