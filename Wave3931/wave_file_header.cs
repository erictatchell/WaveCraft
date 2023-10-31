using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wave3931
{
    public class wave_file_header
    {
            [DllImport("winmm.dll")]
            public static extern int mmioStringToFOURCC([MarshalAs(UnmanagedType.LPStr)] String s, int flags);
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
            public void initialize(decimal sampUpDown)
            {
                clear();

                ChunkID = mmioStringToFOURCC("RIFF", 0);
                ChunkSize = 0;
                Format = mmioStringToFOURCC("WAVE", 0);
                SubChunk1ID = mmioStringToFOURCC("fmt ", 0);
                SubChunk1Size = 16;
                AudioFormat = 1;
                NumChannels = 1; 
                SampleRate = (uint)sampUpDown; 
                BitsPerSample = 16;
                ByteRate = (uint)(SampleRate * (BitsPerSample / 4)); 
                BlockAlign = (ushort)(BitsPerSample / 8);
                SubChunk2ID = mmioStringToFOURCC("data", 0);
                SubChunk2Size = (int)(sampUpDown * BlockAlign);

                ChunkSize = (int)(SubChunk2Size + 44);
            }
        }
    }