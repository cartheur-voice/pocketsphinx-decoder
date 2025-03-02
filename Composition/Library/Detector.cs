using Cartheur.Presents.Interfaces;
using Cartheur.Presents;
using System.Threading.Tasks;

namespace SpeechDetection
{
    public class Detector
    {
        private readonly IRecorder _recorder;
        //private readonly IPlayer _player;
        public Detector()
        {
            _recorder = new Recorder();
            //_player = new Player();
        }
        public async Task Record(string fileName, int duration)
        {
            await _recorder.Record(fileName, duration);
        }
    }
}