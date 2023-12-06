/**
    Author: Eric Tatchell & Brendan Doyle
    Date: October & November 2023
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

/***********************************************************************************************************************************************************
 *
 * File: Program.cs
 *
 * Purpose: This is a Wave Analyzer Application. It is a versatile audio tool designed for in-depth analysis and editing of wave files. 
 * It enables users to open, create, and save wave files, alongside offering advanced functionalities like graphical visualization, header information viewing, 
 * and basic editing operations such as cut, copy, and paste. Users can play back the audio, zoom in for detailed analysis, and choose between 8-bit and 
 * 16-bit audio in mono or stereo formats. The application supports multiple sample rates, offers various threading options for performance benchmarking, 
 * and includes three windowing styles for nuanced analysis. Additionally, it features a Discrete Fourier Transform (DFT) function with filter applications, 
 * making it a comprehensive solution for both simple and complex audio processing tasks.
 *
 ***********************************************************************************************************************************************************/

namespace Wave3931
{
    internal static class Program
    {
        /*
        * The main entry point for the application.
        */
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new Form1());
        }
    }
}
