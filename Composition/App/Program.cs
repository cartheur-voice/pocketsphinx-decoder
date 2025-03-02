using Cartheur.Presents;
using Cartheur.Animals.Utilities;
using Cartheur.Animals.Core;

namespace App
{
    class Program
    {
        public static Recorder VoiceRecorder { get; private set; }
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
            FileName = _thisAeon.GlobalSettings.GrabSetting("filename");
            UseFile = Convert.ToBoolean(_thisAeon.GlobalSettings.GrabSetting("usefile"));
            // Create an instance of the Recorder.
            VoiceRecorder = new Recorder();
            // Notify, perhaps the tonals from david are good here.
            Console.WriteLine("Started recording...");
            // Set the recording duration
            RecordingDuration = 1000;
            await VoiceRecorder.Record(ReturnRecordingFilePath("recorded"), RecordingDuration);
            VoiceRecorder.RecordingFinished += RecordingEvent;
            Console.WriteLine("File recorded.");
            // Run pocketsphinx to detect what was spoken.
            var output = PocketSphinx.StartBashProcess("pocketsphinx single" + ReturnRecordingFilePath("recorded"));
             // Display the classification result
            Console.WriteLine("Output: " + output.StandardOutput.ReadToEnd());
            Console.ReadLine();
            

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
