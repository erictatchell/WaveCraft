using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
/***********************************************************************************************************************************************************
 *
 * File: Externals.cs
 *
 * Purpose: Contains the external method declarations for interacting with the WaveDLL.dll and other libraries for audio processing.
 *
 ***********************************************************************************************************************************************************/


namespace Wave3931
{
    /*
    * Provides external method declarations for interacting with the WaveDLL.dll and other libraries for audio processing.
    */
    public class Externals
    {

        // WaveDLL.dll method declarations.

        /*
         * Initializes the audio with specified sample rate and channels.
         * 
         * @param SampleRate The sample rate of the audio.
         * @param channels The number of channels of the audio.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void InitializeAudio(uint SampleRate, int channels);

        /*
         * Sets the pointer to the buffer used for processing audio data.
         * 
         * @param pb The pointer to the buffer.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void SetPBuffer(IntPtr pb);

        /*
         * Retrieves the length of the data in the current audio buffer.
         */
        [DllImport("WaveDLL.dll")]
        public static extern int GetDataLength();

        /*
         * Retrieves a pointer to the current audio processing buffer.
         */
        [DllImport("WaveDLL.dll")]
        public static extern IntPtr GetPBuffer();

        /*
         * Cleans up and releases resources used by the audio system.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void CleanupAudio();

        /*
        * Starts audio recording, directing output to the specified window handle.
        * 
        * @param hwnd The window handle to direct output to.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void StartRecording(IntPtr hwnd);

        /*
        * Stops the ongoing audio recording.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void StopRecording();

        /*
         * Begins playback of the currently loaded audio data.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void StartPlaying();

        /*
         *  Initializes the audio playback system.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void Init();

        /*
         * Stops audio playback.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void StopPlaying();

        /*
         * Pauses the current audio playback.
         */
        [DllImport("WaveDLL.dll")]
        public static extern void PausePlaying();

        /*
        * Sets the pointer to the buffer used for saving audio data.
        * 
        * @param ps The pointer to the buffer.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetPSaveBuffer(IntPtr ps);

        /*
        * Sets the length of the data in the save buffer.
        * 
        * @param dl The length of the data in the save buffer.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetDWDataLength(uint dl);

        /*
        * Updates the save buffer with new data.
        * 
        * @param ps The pointer to the save buffer.
        * @param dl The length of the data in the save buffer.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void UpdatePSaveBuffer(IntPtr ps, int dl);

        /*
        * Retrieves the header data of the current wave audio.
        */
        [DllImport("WaveDLL.dll")]
        public static extern IntPtr GetWaveHdrLPData();

        /*
         * Retrieves the buffer length of the current wave audio header.
         */
        [DllImport("WaveDLL.dll")]
        public static extern int GetWaveHdrBUFLEN();

        /*
        * Sets the wave header information.
        * 
        * @param lpData The pointer to the wave header data.
        * @param dwBufferLength The length of the wave header data.
        * @param dwBytesRecorded The number of bytes recorded.
        * @param dwUser The user data.
        * @param dwFlags The flags.
        * @param dwLoops The number of loops.
        * @param reserved Reserved.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetWaveHdr(IntPtr lpData, int dwBufferLength, uint dwBytesRecorded, uint dwUser, uint dwFlags, uint dwLoops, uint reserved);

        /*
        * Configures the wave format for audio data.
        * 
        * @param wFormatTag The format tag.
        * @param nChannels The number of channels.
        * @param nSamplesPerSec The number of samples per second.
        * @param nAvgBytesPerSec The average number of bytes per second.
        * @param nBlockAlign The block alignment.
        * @param wBitsPerSample The number of bits per sample.
        * @param cbSize The size of the buffer.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetWaveformData(int wFormatTag, uint nChannels, uint nSamplesPerSec, uint nAvgBytesPerSec, uint nBlockAlign, uint wBitsPerSample, uint cbSize);

        /*
        * Retrieves the wave format data.
        */
        [DllImport("WaveDLL.dll")]
        public static extern ushort GET_wFormatTag();

        /*
         * Retrieves the number of channels.
         */
        [DllImport("WaveDLL.dll")]
        public static extern ushort GET_nChannels();

        /*
         * Retrieves the number of samples per second.
         */
        [DllImport("WaveDLL.dll")]
        public static extern uint GET_nSamplesPerSec();

        /*
         * Retrieves the average number of bytes per second.
         */
        [DllImport("WaveDLL.dll")]
        public static extern uint GET_nAvgBytesPerSec();

         /*
         * Retrieves the number of block alignments 
         */
        [DllImport("WaveDLL.dll")]
        public static extern uint GET_nBlockAlign();

        /*
         * Retrieves bits per sample  
         */
        [DllImport("WaveDLL.dll")]
        public static extern ushort GET_wBitsPerSample();

        /*
         * Retrieves the size of the buffer.
         */
        [DllImport("WaveDLL.dll")]
        public static extern uint GET_cbSize();

        /*
         * Retrieves the number of bytes recorded
         */
        [DllImport("WaveDLL.dll")]
        public static extern int GetSampleRate();

        /*
        *  Sets the sample rate for audio processing.
        *  
        * @param sr The sample rate.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetSampleRate(uint sr);

        /*
        *  Sets the number of audio channels.
        *  
        * @param sr The number of channels.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetChannels(uint sr);

        /*
        * Sets the number of bits per sample for audio data.
        * 
        * @param bps The number of bits per sample.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetBitsPerSample(ushort bps);

        /*
        * Updates the save buffer with stereo audio data.
        * 
        * @param leftChannelData The pointer to the left channel data.
        * @param rightChannelData The pointer to the right channel data.
        * @param leftChannelDataLength The length of the left channel data.
        * @param rightChannelDataLength The length of the right channel data.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void UpdatePSaveBufferStereo(IntPtr leftChannelData, IntPtr rightChannelData, int leftChannelDataLength,
                                                int rightChannelDataLength);

        /*
        * Sets the block alignment for audio data.
        * 
        * @param ba The block alignment.
        */
        [DllImport("WaveDLL.dll")]
        public static extern void SetBlockAlign(uint ba);

        // user32.dll method declarations.

        /*
        * Retrieves a handle to the top-level window whose class name and window name match the specified strings.
        * 
        * @param lpClassName The class name.
        * @param lpWindowName The window name.
        */
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /*
        * Sends the specified message to a window or windows.
        * 
        * @param hWnd The window handle.
        * @param Msg The message to send.
        * @param wParam The first message parameter.
        * @param lParam The second message parameter.
        */
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        /*
        * Performs a convolution operation on the provided audio data using assembly language optimizations, typically for signal processing or filtering.
        * 
        * @param N The length of the audio data.
        * @param WN The length of the convolution data.
        * @param convolutionData The convolution data.
        * @param audioData The audio data.
        * @param newSignal The new signal.
        */
        [DllImport("WaveASMDLLFR.dll")]
        public static extern void convolveInASM(int N, int WN, double[] convolutionData, double[] audioData, double[] newSignal);

        /*
        *  Loads the specified dynamic-link library into the calling process's address space.
        *  
        * @param dllToLoad The name of the DLL to load.
        */
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibrary(string dllToLoad);

        /*
        * Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count.
        * 
        * @param hModule A handle to the loaded library module.
        */
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr hModule);

        /*
        * Retrieves the address of an exported function or variable from the specified dynamic-link library (DLL).
        */
        [DllImport("WaveDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr getConfig();

        /*
        * Method for testing the audio system.
        * @param hwnd The window handle.
        */
        [DllImport("WaveDLL.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void Box(IntPtr hwnd);

        /*
        * Structure defining the audio configuration.
        */
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct AUDIO_CONFIG
        {
            [MarshalAs(UnmanagedType.Bool)]
            public bool bRecording, bPlaying, bPaused, bEnding, bTerminating;
            public uint dwDataLength, dwRepetitions;
            public IntPtr hWaveIn;
            public IntPtr hWaveOut;
            public IntPtr pBuffer1, pBuffer2, pSaveBuffer, pNewBuffer;
            public IntPtr pWaveHdr1, pWaveHdr2;
            public WAVEFORMATEX waveform;
            public ushort CHANNELS;
            public uint SR;
            public ushort BLOCKALIGN;
        }

        /*
         * Structure defining the wave format.
         */
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
    }
}