using System;
using System.Runtime.InteropServices;

/***********************************************************************************************************************************************************
 *
 * File: wave_fileheader.cs
 *
 * Purpose: This file contains the definition of the wave file headers and the methods to manipulate them.
 *
 ***********************************************************************************************************************************************************/


namespace Wave3931
{
    /* 
    * This class represents the header of a WAVE audio file and provides methods to manipulate its data.
    */
    public class wave_file_header
    {
        /* 
        * DllImport of the 'mmioStringToFOURCC' function from 'winmm.dll'.
        * Converts a string into a four-character code used in multimedia files.
        */
        [DllImport("winmm.dll")]
        public static extern int mmioStringToFOURCC([MarshalAs(UnmanagedType.LPStr)] String s, int flags);

        // Fields representing different parts of the WAVE file header.
        public int ChunkID;
        public int ChunkSize;
        public int Format;
        public int SubChunk1ID;
        public int SubChunk1Size;
        public ushort AudioFormat;
        public ushort NumChannels;
        public uint SampleRate;
        public uint ByteRate;
        public ushort BlockAlign;
        public ushort BitsPerSample;
        public int SubChunk2ID;
        public int SubChunk2Size;

        /* 
        * Method to clear or reset all header fields to their default (zero) values.
        */
        public void clear()
        {
            ChunkID = 0;
            ChunkSize = 0;
            Format = 0;
            SubChunk1ID = 0;
            SubChunk1Size = 0;
            AudioFormat = 0;
            NumChannels = 0;
            SampleRate = 0;
            ByteRate = 0;
            BlockAlign = 0;
            BitsPerSample = 0;
            SubChunk2ID = 0;
            SubChunk2Size = 0;
        }

        /* 
        * Method to initialize the WAVE file header with specific audio settings.
        * @param sampUpDown       Sample rate for the audio file.
        * @param channels         Number of audio channels.
        * @param bitspersample    Number of bits per sample.
        */
        public void initialize(uint sampUpDown, int channels, ushort bitspersample)
        {
            clear();

            // Initialize fields with specific audio settings
            ChunkID = mmioStringToFOURCC("RIFF", 0);
            ChunkSize = 0;
            Format = mmioStringToFOURCC("WAVE", 0);
            SubChunk1ID = mmioStringToFOURCC("fmt ", 0);
            SubChunk1Size = 16;
            AudioFormat = 1;
            NumChannels = (ushort)channels;
            SampleRate = sampUpDown;
            BitsPerSample = bitspersample;
            ByteRate = (uint)(SampleRate * NumChannels * (BitsPerSample / 8));
            BlockAlign = (ushort)(NumChannels * (BitsPerSample / 8));
            SubChunk2ID = mmioStringToFOURCC("data", 0);
            SubChunk2Size = (int)(sampUpDown * BlockAlign);

            ChunkSize = SubChunk2Size + 44;
        }

        /* 
        * Method to change the sample rate of the WAVE file header.
        * @param sampUpDown New sample rate to be set.
        */
        public void changeSR(uint sampUpDown)
        {
            // Update the sample rate and dependent fields
            SampleRate = sampUpDown;
            ByteRate = (uint)(SampleRate * NumChannels * (BitsPerSample / 8));
            SubChunk2Size = (int)(sampUpDown * BlockAlign);
            ChunkSize = SubChunk2Size + 44;
        }
    }
}
