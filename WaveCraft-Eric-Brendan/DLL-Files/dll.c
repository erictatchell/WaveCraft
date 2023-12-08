/*
    
    dll.c by Eric Tatchell - 11-03-23
    Record audio in a C# application
    Adaptation of Record1.c by Charles Petzold

*/
#include "dll.h"

#define INP_BUFFER_SIZE 16384

HINSTANCE hInst;
BOOL APIENTRY DllMain(HMODULE hModule,
    DWORD  ul_reason_for_call,
    LPVOID lpReserved
)
{
    hInst = hModule;
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}




/*
    
    Globals used for general audio processing; they were initially local to DlgProc in Record1.c

    COMMENTARY:
    I tried to move these globals into an AUDIO_DATA struct as seen in dll.h to no avail,
    I kept getting stuck on "pWaveHdr1 is 0xFFFFFFFFF..." and the waveIn was not opening
    Therefore, only one practical instance of RecordDialog can be used. Moving forward I'd like to
    explore this further as this is a pretty bad bottleneck of WaveCraft, perhaps store structs in an
    array of size x (x = max instances allowed), and I'd be curious as to what is causing the pWaveHdr1
    errors.

*/
BOOL         bRecording, bPlaying, bPaused,
bEnding, bTerminating;
DWORD        dwDataLength, dwRepetitions = 1;
HWAVEIN      hWaveIn;
HWAVEOUT     hWaveOut;
PBYTE        pBuffer1, pBuffer2, pSaveBuffer, pNewBuffer;
PWAVEHDR     pWaveHdr1, pWaveHdr2;
TCHAR        szOpenError[] = TEXT("Error opening waveform audio!");
TCHAR        szMemError[] = TEXT("Error allocating memory!");
WAVEFORMATEX waveform;




/*

    Globals for advanced audio processing customization, adjustable by the User in WaveCraft.exe
    
*/
USHORT CHANNELS = 1;
UINT SR = 11025;
WORD BITSPERSAMPLE = 8;
WORD BLOCKALIGN = 1;



/*

    Invisible dialog box with winmm powered record/playback buttons. The WaveCraft C# sends messages to this window
    (IDC_RECORD_BEG etc) to 'press' these buttons.

    COMMENTARY:
    Initially, I attempted to convert DlgProc's WM_COMMAND cases into individual functions that could be called from the WaveCraft C#.
    For various reasons, this was not immediately possible with how limited both my knowledge of Win32 and threading was at the time.
    Still, I'm not sure that approach would work without some major overhauls to this entire application. In its current state, the
    dialog box is still there, but partially invisible to the user; that is, focus still occasionally gets applied to this invisible
    window as a side effect to this workaround.

    Param: HWND hwnd; the C# window handle

*/
EXPORT void CALLBACK RecordDialog(HWND hwnd) {
    if (-1 == CreateDialog(hInst, TEXT("Record"), hwnd, DlgProc))
    {
        MessageBox(NULL, TEXT("This program requires Windows NT!"),
            szAppName, MB_ICONERROR);
    }
}



// Getter for the pSaveBuffer
EXPORT PBYTE GetPBuffer() {
    return pSaveBuffer;
}

// Getter for the length of the buffer
EXPORT DWORD GetDataLength() {
    return dwDataLength;
}

// Setter for the sample rate
EXPORT void SetSampleRate(unsigned int sr) {    
    SR = sr;
}

// Setter for channels, also sets block align. Bad logic, but will not refactor
EXPORT void SetChannels(unsigned int ch) {
    CHANNELS = ch;
    BLOCKALIGN = ch;
}

// Setter for block align
EXPORT void SetBlockAlign(unsigned int ba) {
    BLOCKALIGN = ba;
}

// Setter for # bits per sample, and updates the affected attributes
EXPORT void SetBitsPerSample(unsigned short bps) {
    BITSPERSAMPLE = bps;
    BLOCKALIGN = CHANNELS * bps / 8;
    waveform.nAvgBytesPerSec = SR * BLOCKALIGN * CHANNELS;
}

// Seter for the pSaveBuffer
EXPORT void SetPSaveBuffer(PBYTE ps)
{
    pSaveBuffer = ps;
}

// Setter for buffer length
EXPORT void SetDWDataLength(unsigned int dl) {
    dwDataLength = dl;
}

// Update pSaveBuffer when edits to its order/length are made. Logic from Charles Petzold's WIM_DATA implementation
EXPORT void UpdatePSaveBuffer(PBYTE audioData, int audioDataLength) {
    PBYTE newBuffer = realloc(pSaveBuffer, dwDataLength + audioDataLength);

    pSaveBuffer = newBuffer;
    CopyMemory(pSaveBuffer, audioData, audioDataLength);

    dwDataLength = audioDataLength;
}

// Getter for lpData, unused
EXPORT LPSTR GetWaveHdrLPData() {
    return pWaveHdr1->lpData;
}

// Getter for Buffer length, unused
EXPORT DWORD GetWaveHdrBUFLEN() {
    return pWaveHdr1->dwBufferLength;
}

// Getter for the current waveform struct
EXPORT WAVEFORMATEX GetWaveform() {
    return waveform;
}

/*
* wFormatTag;       WORD 
* nChannels;        WORD 
* nSamplesPerSec;   DWORD
* nAvgBytesPerSec;  DWORD
* nBlockAlign;      WORD 
* wBitsPerSample;   WORD 
* cbSize;           WORD 
*/
// Setter for the waveform struct, mainly used when playing opened files that have not been created in this context
EXPORT void SetWaveformData(WORD wFormatTag, WORD nChannels, DWORD nSamplesPerSec, DWORD nAvgBytesPerSec,
    WORD nBlockAlign, WORD wBitsPerSample, WORD cbSize) {
    waveform.wFormatTag = wFormatTag;
    waveform.nChannels = nChannels;
    waveform.nSamplesPerSec = nSamplesPerSec;
    waveform.nAvgBytesPerSec = nAvgBytesPerSec;
    waveform.nBlockAlign = nBlockAlign;
    waveform.wBitsPerSample = wBitsPerSample;
    waveform.cbSize = cbSize;

}

/*
LPSTR       lpData;
DWORD       dwBufferLength;
DWORD       dwBytesRecorded;
DWORD_PTR   dwUser;
DWORD       dwFlags;
DWORD       dwLoops;
struct wavehdr_tag FAR *lpNext;
DWORD_PTR   reserved;
*/
// Setter for the wavehdr struct, mainly used when playing opened files that have not been created in this context
EXPORT void SetWaveHdr(LPSTR lpD, int dwDL, DWORD dwBytesRecorded, DWORD_PTR dwUser, DWORD dwFlags, DWORD dwLoops,
    DWORD_PTR reserved) {
    pWaveHdr1->lpData = lpD;
    pWaveHdr1->dwBufferLength = dwDL;
    pWaveHdr1->dwBytesRecorded = dwBytesRecorded;
    pWaveHdr1->dwUser = dwUser;
    pWaveHdr1->dwFlags = dwFlags;
    pWaveHdr1->dwLoops = dwLoops;
    pWaveHdr1->reserved = reserved;
}

/*

    Getters for individual waveform attributes.
    Better solution is just creating a waveform struct in C#, but this is due tonight lol

*/
EXPORT DWORD GET_wFormatTag() { return waveform.wFormatTag; }
EXPORT DWORD GET_nChannels() { return waveform.nChannels; }
EXPORT DWORD GET_nSamplesPerSec() { return waveform.nSamplesPerSec; }
EXPORT DWORD GET_nAvgBytesPerSec() { return waveform.nAvgBytesPerSec; }
EXPORT DWORD GET_nBlockAlign() { return waveform.nBlockAlign; }
EXPORT DWORD GET_wBitsPerSample() { return waveform.wBitsPerSample; }
EXPORT DWORD GET_cbSize() { return waveform.cbSize; }

// Getter for sample rate
EXPORT DWORD GetSampleRate() {
    return waveform.nSamplesPerSec;
}

// Getter for the wavehdr
PWAVEHDR GetWaveHdr() {
    return pWaveHdr1;
}


/*

    Charles Petzold's DlgProc
    Changelog:
        Removed IDC_PLAY_SPEED, unnecessary for this application
        Exchanged magic numbers for user defined constants as seen above (SR BLOCKALIGN etc)

*/
BOOL CALLBACK DlgProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam)
{
    switch (message)
    {
    case WM_INITDIALOG:
        // Allocate memory for wave header

        pWaveHdr1 = malloc(sizeof(WAVEHDR));
        pWaveHdr2 = malloc(sizeof(WAVEHDR));

        // Allocate memory for save buffer

        pSaveBuffer = malloc(1);
        return TRUE;

    case WM_COMMAND:
        switch (LOWORD(wParam))
        {
        case IDC_RECORD_BEG:
            // Allocate buffer memory
            pBuffer1 = malloc(INP_BUFFER_SIZE);
            pBuffer2 = malloc(INP_BUFFER_SIZE);

            if (!pBuffer1 || !pBuffer2)
            {
                if (pBuffer1) free(pBuffer1);
                if (pBuffer2) free(pBuffer2);

                MessageBeep(MB_ICONEXCLAMATION);
                return TRUE;
            }

            // Open waveform audio for input

            waveform.wFormatTag = WAVE_FORMAT_PCM;
            waveform.nChannels = CHANNELS;
            waveform.nSamplesPerSec = SR;
            waveform.nAvgBytesPerSec = waveform.nSamplesPerSec * waveform.nBlockAlign * waveform.nChannels;
            waveform.nBlockAlign = BLOCKALIGN;
            waveform.wBitsPerSample = BITSPERSAMPLE;
            waveform.cbSize = 0;
            MMRESULT result = waveInOpen(&hWaveIn, WAVE_MAPPER, &waveform,
                (DWORD)hwnd, 0, CALLBACK_WINDOW);
            if (result == 5)
            {
                free(pBuffer1);
                free(pBuffer2);
                MessageBeep(MB_ICONEXCLAMATION);
            }
            // Set up headers and prepare them

            pWaveHdr1->lpData = pBuffer1;
            pWaveHdr1->dwBufferLength = INP_BUFFER_SIZE;
            pWaveHdr1->dwBytesRecorded = 0;
            pWaveHdr1->dwUser = 0;
            pWaveHdr1->dwFlags = 0;
            pWaveHdr1->dwLoops = 1;
            pWaveHdr1->lpNext = NULL;
            pWaveHdr1->reserved = 0;

            waveInPrepareHeader(hWaveIn, pWaveHdr1, sizeof(WAVEHDR));

            pWaveHdr2->lpData = pBuffer2;
            pWaveHdr2->dwBufferLength = INP_BUFFER_SIZE;
            pWaveHdr2->dwBytesRecorded = 0;
            pWaveHdr2->dwUser = 0;
            pWaveHdr2->dwFlags = 0;
            pWaveHdr2->dwLoops = 1;
            pWaveHdr2->lpNext = NULL;
            pWaveHdr2->reserved = 0;

            waveInPrepareHeader(hWaveIn, pWaveHdr2, sizeof(WAVEHDR));

            return TRUE;

        case IDC_RECORD_END:
            // Reset input to return last buffer
            bEnding = TRUE;
            waveInReset(hWaveIn);
            return TRUE;

        case IDC_PLAY_BEG:
            // Open waveform audio for output
            waveform = GetWaveform();
            waveform.wFormatTag = WAVE_FORMAT_PCM;
            waveform.nChannels = CHANNELS;
            waveform.nSamplesPerSec = SR;
            waveform.nAvgBytesPerSec = SR * BLOCKALIGN * CHANNELS;
            waveform.nBlockAlign = BLOCKALIGN;
            waveform.wBitsPerSample = BITSPERSAMPLE;
            waveform.cbSize = 0;
            MMRESULT result2 = waveOutOpen(&hWaveOut, WAVE_MAPPER, &waveform,
                (DWORD)hwnd, 0, CALLBACK_WINDOW);
            if (result2 != 0)
            {
                MessageBeep(MB_ICONEXCLAMATION);
                MessageBox(hwnd, szOpenError, szAppName,
                    MB_ICONEXCLAMATION | MB_OK);
            }
            return TRUE;

        case IDC_PLAY_PAUSE:
            // Pause or restart output

            if (!bPaused)
            {
                waveOutPause(hWaveOut);
                SetDlgItemText(hwnd, IDC_PLAY_PAUSE, TEXT("Resume"));
                bPaused = TRUE;
            }
            else
            {
                waveOutRestart(hWaveOut);
                SetDlgItemText(hwnd, IDC_PLAY_PAUSE, TEXT("Pause"));
                bPaused = FALSE;
            }
            return TRUE;

        case IDC_PLAY_END:
            // Reset output for close preparation

            bEnding = TRUE;
            waveOutReset(hWaveOut);
            return TRUE;

        case IDC_PLAY_REP:
            // Set infinite repetitions and play

            dwRepetitions = -1;
            SendMessage(hwnd, WM_COMMAND, IDC_PLAY_BEG, 0);
            return TRUE;

        }
        break;

    case MM_WIM_OPEN:
        // Shrink down the save buffer

        pSaveBuffer = realloc(pSaveBuffer, 1);

        // Enable and disable Buttons

        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_BEG), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_END), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_BEG), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_PAUSE), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_END), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REV), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REP), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_SPEED), FALSE);
        SetFocus(GetDlgItem(hwnd, IDC_RECORD_END));

        // Add the buffers

        waveInAddBuffer(hWaveIn, pWaveHdr1, sizeof(WAVEHDR));
        waveInAddBuffer(hWaveIn, pWaveHdr2, sizeof(WAVEHDR));

        // Begin sampling

        bRecording = TRUE;
        bEnding = FALSE;
        dwDataLength = 0;
        waveInStart(hWaveIn);
        return TRUE;

    case MM_WIM_DATA:

        // Reallocate save buffer memory

        pNewBuffer = realloc(pSaveBuffer, dwDataLength +
            ((PWAVEHDR)lParam)->dwBytesRecorded);

        if (pNewBuffer == NULL)
        {
            waveInClose(hWaveIn);
            MessageBeep(MB_ICONEXCLAMATION);
            MessageBox(hwnd, szMemError, szAppName,
                MB_ICONEXCLAMATION | MB_OK);
            return TRUE;
        }

        pSaveBuffer = pNewBuffer;
        CopyMemory(pSaveBuffer + dwDataLength, ((PWAVEHDR)lParam)->lpData,
            ((PWAVEHDR)lParam)->dwBytesRecorded);

        dwDataLength += ((PWAVEHDR)lParam)->dwBytesRecorded;

        if (bEnding)
        {
            waveInClose(hWaveIn);
            return TRUE;
        }

        // Send out a new buffer

        waveInAddBuffer(hWaveIn, (PWAVEHDR)lParam, sizeof(WAVEHDR));
        return TRUE;

    case MM_WIM_CLOSE:
        // Free the buffer memory

        waveInUnprepareHeader(hWaveIn, pWaveHdr1, sizeof(WAVEHDR));
        waveInUnprepareHeader(hWaveIn, pWaveHdr2, sizeof(WAVEHDR));

        free(pBuffer1);
        free(pBuffer2);

        // Enable and disable buttons

        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_BEG), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_END), FALSE);
        SetFocus(GetDlgItem(hwnd, IDC_RECORD_BEG));

        if (dwDataLength > 0)
        {
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_BEG), TRUE);
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_PAUSE), FALSE);
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_END), FALSE);
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REP), TRUE);
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REV), TRUE);
            EnableWindow(GetDlgItem(hwnd, IDC_PLAY_SPEED), TRUE);
            SetFocus(GetDlgItem(hwnd, IDC_PLAY_BEG));
        }
        bRecording = FALSE;

        if (bTerminating)
            SendMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0L);

        return TRUE;

    case MM_WOM_OPEN:
        // Enable and disable buttons

        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_BEG), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_END), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_BEG), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_PAUSE), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_END), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REP), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REV), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_SPEED), FALSE);
        SetFocus(GetDlgItem(hwnd, IDC_PLAY_END));

        // Set up header
        pWaveHdr1 = GetWaveHdr();
        pWaveHdr1->lpData = pSaveBuffer;
        pWaveHdr1->dwBufferLength = dwDataLength;
        pWaveHdr1->dwBytesRecorded = 0;
        pWaveHdr1->dwUser = 0;
        pWaveHdr1->dwFlags = WHDR_BEGINLOOP | WHDR_ENDLOOP;
        pWaveHdr1->dwLoops = dwRepetitions;
        pWaveHdr1->lpNext = NULL;
        pWaveHdr1->reserved = 0;

        // Prepare and write

        waveOutPrepareHeader(hWaveOut, pWaveHdr1, sizeof(WAVEHDR));
        waveOutWrite(hWaveOut, pWaveHdr1, sizeof(WAVEHDR));

        bEnding = FALSE;
        bPlaying = TRUE;
        return TRUE;

    case MM_WOM_DONE:
        waveOutUnprepareHeader(hWaveOut, pWaveHdr1, sizeof(WAVEHDR));
        waveOutClose(hWaveOut);
        return TRUE;

    case MM_WOM_CLOSE:
        // Enable and disable buttons

        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_BEG), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_RECORD_END), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_BEG), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_PAUSE), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_END), FALSE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REV), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_REP), TRUE);
        EnableWindow(GetDlgItem(hwnd, IDC_PLAY_SPEED), TRUE);
        SetFocus(GetDlgItem(hwnd, IDC_PLAY_BEG));

        SetDlgItemText(hwnd, IDC_PLAY_PAUSE, TEXT("Pause"));
        bPaused = FALSE;
        dwRepetitions = 1;
        bPlaying = FALSE;

        if (bTerminating)
            SendMessage(hwnd, WM_SYSCOMMAND, SC_CLOSE, 0L);

        return TRUE;

    case WM_SYSCOMMAND:
        switch (LOWORD(wParam))
        {
        case SC_CLOSE:
            if (bRecording)
            {
                bTerminating = TRUE;
                bEnding = TRUE;
                waveInReset(hWaveIn);
                return TRUE;
            }

            if (bPlaying)
            {
                bTerminating = TRUE;
                bEnding = TRUE;
                waveOutReset(hWaveOut);
                return TRUE;
            }

            free(pWaveHdr1);
            free(pWaveHdr2);
            free(pSaveBuffer);
            EndDialog(hwnd, 0);
            return TRUE;
        }
        break;
    }
    return FALSE;
}