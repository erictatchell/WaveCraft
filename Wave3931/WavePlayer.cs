using System;
using System.Runtime.InteropServices;
using System.Threading;
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
        public static extern void StopPlaying();
        [DllImport("WaveAnalyserDLL.dll")]

        public static extern void PausePlaying();



        [DllImport("winmm.dll")]
        public static extern int waveOutOpen(out IntPtr hWaveOut, uint uDeviceID, ref WAVEFORMATEX lpFormat, WaveCallback dwCallback, IntPtr dwInstance, uint dwFlags);

        [DllImport("winmm.dll")]
        public static extern int waveOutPrepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutWrite(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);

        [DllImport("winmm.dll")]
        public static extern int waveOutReset(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutClose(IntPtr hWaveOut);

        [DllImport("winmm.dll")]
        public static extern int waveOutUnprepareHeader(IntPtr hWaveOut, ref WAVEHDR lpWaveOutHdr, uint uSize);
/*        [DllImport("winmm.dll")]
*         public static extern int waveInOpen(out IntPtr hWaveIn, int uDeviceID, ref WAVEFORMATEX lpFormat, WaveInProc dwCallback, int dwInstance, int dwFlags);
*/
        [DllImport("winmm.dll")]
        public static extern int waveInStart(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInStop(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInClose(IntPtr hWaveIn);

        [DllImport("winmm.dll")]
        public static extern int waveInAddBuffer(IntPtr hWaveIn, ref WAVEHDR pWaveHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInPrepareHeader(IntPtr hWaveIn, ref WAVEHDR pWaveHdr, int uSize);

        [DllImport("winmm.dll")]
        public static extern int waveInUnprepareHeader(IntPtr hWaveIn, ref WAVEHDR pWaveHdr, int uSize);
/*        [DllImport("winmm.dll")]
*//*        public static extern int waveInOpen(out IntPtr hWaveIn, int uDeviceID, ref WAVEFORMATEX lpFormat, WaveInProc dwCallback, int dwInstance, int dwFlags);
*/        public delegate void WaveCallback(IntPtr hWaveOut, uint uMsg, IntPtr dwInstance, IntPtr dwParam1, IntPtr dwParam2);

        // Define the WAVEFORMATEX structure
        [StructLayout(LayoutKind.Sequential)]
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