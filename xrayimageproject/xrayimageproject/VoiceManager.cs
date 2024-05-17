using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xrayimageproject
{
    internal class VoiceManager
    {
        public VoiceManager()
        {

        }

        public void Import(string path)
        {

        }

        public void Record()
        {
            using (var waveIn = new WaveInEvent())
            {
                waveIn.WaveFormat = new WaveFormat(44100, 1);
                WaveFileWriter waveFileWriter = null;
                waveIn.DataAvailable += (sender, e) =>
                {
                    if (waveFileWriter == null)
                    {
                        waveFileWriter = new WaveFileWriter("C:\\Users\\number one\\source\\repos\\Xrayimageproject\\xrayimageproject\\xrayimageproject\\Patients\\NewFolder\\output.wav", waveIn.WaveFormat);
                    }
                    waveFileWriter.Write(e.Buffer, 0, e.BytesRecorded);
                };

                waveIn.StartRecording();
                Console.WriteLine("Recording. Press any key to stop...");
                Console.ReadKey();
                waveIn.StopRecording();
                waveFileWriter?.Dispose();
                Console.WriteLine("Recording stopped. Audio saved to: " + "output.wav");
            }
        }
    }
}
