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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCompareText
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
