using Cartheur.Presents;
using Cartheur.Animals.Utilities;
using Cartheur.Animals.Core;
using SpeechDetection;

namespace App
{
    class Program
    {
        public static SpeechDetection.Detector VoiceRecorder { get; private set; }
        public static int RecordingDuration { get; private set; }
        public static LoaderPaths Configuration;
        private static Aeon _thisAeon;
        public static string TrainingDataFiles { get; private set; }
        public static string FileName { get; private set; }
        public static bool UseFile { get; private set; }

        static async Task Main(string[] args)
{
            // Create the app with settings.
            Configuration = new LoaderPaths("Debug");
            _thisAeon = new Aeon("hug");
            // Load the given application configuration.
            _thisAeon.LoadSettings(Configuration.PathToSettings);
            TrainingDataFiles = _thisAeon.GlobalSettings.GrabSetting("trainingdatafiles");
            FileName = _thisAeon.GlobalSettings.GrabSetting("filename");
            UseFile = Convert.ToBoolean(_thisAeon.GlobalSettings.GrabSetting("usefile"));
            // Create an instance of the Detector
            VoiceRecorder = new Detector();
            // Set the recording duration
            RecordingDuration = 1000;
            await VoiceRecorder.Record(ReturnRecordingFilePath("recorded"), RecordingDuration);
            Console.WriteLine("Started recording...");
            VoiceRecorder.RecordingFinished += RecordingEvent;
            Console.WriteLine("File recorded.");
            // Run pocketsphinx to detect what was spoken.
            //string audioFilePath = ReturnRecordingFilePath("recorded");
            // Send over for the file for analysis and return the approximated emotion.
            try
            {
                //DetectEmotion = new Boagaphish.Analytics.DetectEmotion(ReturnRecordingFilePath(OutputDetectionFileName), "linux");
            }
            catch (Exception ex)
            {

            }

            //Thread.Sleep(2000);
            // Load the data

            // Train the model
            //classifier.TrainModel();
            // Predict the emotion
            //var emotion = classifier.PredictEmotion(FileName);

            // Display the classification result
            

            static string ReturnRecordingFilePath(string filename)
            {
                return $"/home/cartheur/ame/aiventure/aiventure-github/voice/voice-emotion-detector/Composition/App/recordings/{filename}.wav";
            }
        }

        private static void RecordingEvent(object source, EventArgs e)
        {
            Console.WriteLine("Recording has completed.");
        }
    }
}
