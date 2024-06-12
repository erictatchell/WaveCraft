# README for the Wave3931 Application

## Overview
Wave3931 is an audio processing application designed for in-depth analysis and editing of wave files. It allows users to open, create, save, and analyze wave files through graphical visualization, offering functionalities like header information viewing, and basic editing operations including cut, copy, and paste.

## Features
- **File Operations:** Open, create, and save wave files.
- **Audio Playback:** Play back audio with controls to pause and stop.
- **Graphical Visualization:** Visualize audio data in real-time with zoom capabilities for detailed analysis.
- **Editing Tools:** Basic audio editing functionalities such as cut, copy, and paste.
- **Audio Analysis:** Supports multiple sample rates and bit depths, offers DFT (Discrete Fourier Transform) functionalities with filter applications.
- **Multi-Threaded Analysis:** Utilize multiple threads for performance optimization during DFT calculations.
- **Windowing Functions:** Choose between Rectangle, Triangle, or Hamming windows for DFT analysis.
- **Recording:** Record audio directly within the application with configurable settings.

## Getting Started
1. **Installation:**
   - Download the latest release from the GitHub repository.
   - Extract the files and run the installer.
   - Follow the on-screen instructions to complete the installation.

2. **Opening a File:**
   - Launch Wave3931.
   - Navigate to `File > Open` or click the "OPEN FILE" button on the main screen.
   - Select the wave file you wish to analyze.

3. **Recording Audio:**
   - Click the "RECORD" button to start recording.
   - Press "STOP" to end the recording session.
   - The recorded audio will automatically load for analysis.

4. **Performing DFT Analysis:**
   - Load an audio file or record audio.
   - Select the DFT function from the options panel.
   - Choose your desired windowing function and the number of threads.
   - Click the "DFT" button to perform the analysis.
   - Visualization of the frequency spectrum will be displayed.

5. **Editing Audio:**
   - Use the cursor to select a portion of the waveform in the graphical display.
   - Cut, copy, or paste using the options under the `Edit` menu.

6. **Saving Files:**
   - After making your edits or analysis, choose `File > Save` to save your changes.

## Configuration Options
- **Sample Rate:** Choose from 11025 Hz, 22050 Hz, or 44100 Hz.
- **Bits Per Sample:** Select either 8 bits or 16 bits for recording and playback resolution.
- **DFT Threads:** Configure the number of threads for faster DFT processing.
