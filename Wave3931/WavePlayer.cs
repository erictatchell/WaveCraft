using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Wave3931
{
    public class WavePlayer
    {
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void InitializeAudio(uint SampleRate, int channels);
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void SetPBuffer(IntPtr pb);
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern int GetDataLength();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern IntPtr GetPBuffer();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void CleanupAudio();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void StartRecording();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void StopRecording();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void StartPlaying();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void Init();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void StopPlaying();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void PausePlaying();
        [DllImport("WaveAnalyserDLL.dll")]
        public static extern void Box();
        public struct WAVEFORMATEX
        {
            public ushort wFormatTag;
            public ushort nChannels;
            public uint nSamplesPerSec;
            public uint nAvgBytesPerSec;
            public ushort nBlockAlign;
            public ushort wBitsPerSample;
            public ushort cbSize;
        }

        // Define the WAVEHDR structure
        [StructLayout(LayoutKind.Sequential)]
        public struct WAVEHDR
        {
            public IntPtr lpData;
            public uint dwBufferLength;
            public uint dwBytesRecorded;
            public IntPtr dwUser;
            public uint dwFlags;
            public uint dwLoops;
            public IntPtr lpNext;
            public IntPtr reserved;
        }

    }
}