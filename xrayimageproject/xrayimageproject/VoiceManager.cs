using NAudio.Wave;
using System;
using System.Windows.Forms;

namespace xrayimageproject
{
    internal class VoiceManager
    {
        private WaveInEvent waveIn;
        private WaveFileWriter waveFileWriter;
        private string tempFilePath = Path.GetTempFileName(); // Temporary file path for recording
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
                // Initialize the WaveFileWriter with the temporary file path
                waveFileWriter = new WaveFileWriter(tempFilePath, waveIn.WaveFormat);
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
            if (waveFileWriter != null)
            {
                waveFileWriter.Dispose();
                waveFileWriter = null;

                // After recording is stopped, let the user choose where to save the file
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.InitialDirectory = "..\\..\\..\\";
                    saveFileDialog.Filter = "WAV files (*.wav)|*.wav";
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Get the path where the user wants to save the file
                        string outputPath = saveFileDialog.FileName;

                        // Move the temporary file to the user-selected location
                        File.Move(tempFilePath, outputPath);

                        MessageBox.Show("Audio saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                // Clean up the temporary file
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }
    }
}
