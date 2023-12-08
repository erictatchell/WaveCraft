#pragma once
#ifdef __cplusplus
#define EXPORT extern "C" __declspec (dllexport)
#else
#define EXPORT __declspec (dllexport)
#endif
#include <windows.h>
#include <mmsystem.h>
#include "RESOURCE.H"
TCHAR szAppName[] = TEXT("Record1");

typedef struct {
    BOOL bRecording, bPlaying, bPaused, bEnding, bTerminating;
    DWORD dwDataLength, dwRepetitions;
    HWAVEIN hWaveIn;
    HWAVEOUT hWaveOut;
    PBYTE pBuffer1, pBuffer2, pSaveBuffer, pNewBuffer;
    PWAVEHDR pWaveHdr1, pWaveHdr2;
    WAVEFORMATEX waveform;
    USHORT CHANNELS;
    UINT SR;
    WORD BLOCKALIGN;
} AUDIO_CONFIG;

EXPORT void CALLBACK InitializeAudio(unsigned int SampleRate, int channels);
EXPORT void CALLBACK SetPBuffer(PBYTE pb);
EXPORT PBYTE GetPBuffer();
EXPORT DWORD GetDataLength();
//EXPORT void CALLBACK Box(HWND hwnd);
EXPORT void SetPSaveBuffer(const double* data, int length);
BOOL CALLBACK DlgProc(HWND hwnd, UINT message, WPARAM wParam, LPARAM lParam);

