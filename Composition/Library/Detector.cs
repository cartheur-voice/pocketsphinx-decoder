using Cartheur.Presents.Interfaces;
using Cartheur.Presents;
using System.Threading.Tasks;
using System;

namespace SpeechDetection
{
    public class Detector
    {
        private readonly IRecorder _recorder;
        private readonly IPlayer _player;
        public event EventHandler RecordingFinished;
        public Detector()
        {
            _recorder = new Recorder();
            _player = new Player();
        }
        public async Task Record(string fileName, int duration)
        {
            await _recorder.Record(fileName, duration);
        }
        private void OnRecordingFinished(object sender, EventArgs e)
        {
            RecordingFinished?.Invoke(this, e);
        }
    }
}