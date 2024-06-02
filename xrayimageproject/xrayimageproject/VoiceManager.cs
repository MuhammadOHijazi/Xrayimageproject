using NAudio.Wave;
using System;

namespace xrayimageproject
{
    internal class VoiceManager
    {
        private WaveInEvent waveIn;
        private WaveFileWriter waveFileWriter;

        public VoiceManager()
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += WaveIn_DataAvailable;
        }

        private void WaveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFileWriter == null)
            {
                string path = Properties.Settings.Default.searchPath;
                waveFileWriter = new WaveFileWriter(Path.Combine(path, "hi.wav"), waveIn.WaveFormat);
            }
            waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }

        public void StartRecording()
        {
            waveIn.StartRecording();
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
            waveFileWriter?.Dispose();
            waveFileWriter = null;
        }

    }
}
