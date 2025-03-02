//
// This autonomous intelligent system software is the property of Cartheur Research B.V. Copyright 2025, all rights reserved.
//
using Cartheur.Presents;

namespace App
{
    class Program
    {
        public static Recorder VoiceRecorder { get; private set; }
        public static int RecordingDuration { get; private set; }
        public static LoaderPaths Configuration;
        public static string TrainingDataFiles { get; private set; }
        public static string FileName { get; private set; }
        public static bool UseFile { get; private set; }
        // Aeon's status.
        static bool SettingsLoaded { get; set; }

        static async Task Main(string[] args)
{
            // Create an instance of the Recorder.
            VoiceRecorder = new Recorder();
            // Notify, perhaps the tonals from david are good here.
            Console.WriteLine("Started recording...");
            // Set the recording duration
            RecordingDuration = 7000;
            VoiceRecorder.Record(ReturnRecordingFilePath("recorded"), RecordingDuration);
            VoiceRecorder.RecordingFinished += RecordingEvent;
            Console.WriteLine("File recorded.");
            // Run pocketsphinx to detect what was spoken.
            var output = PocketSphinx.StartBashProcess("pocketsphinx single" + ReturnRecordingFilePath("recorded"));
             // Display the classification result
            Console.WriteLine("Output: " + output.StandardOutput.ReadToEnd());
            Console.ReadLine();
            

            static string ReturnRecordingFilePath(string filename)
            {
                return Path.Combine(LoaderPaths.ActiveRuntime, LoaderPaths.SavePath, filename + ".wav");
            }
        }

        private static void RecordingEvent(object source, EventArgs e)
        {
            Console.WriteLine("Recording has completed.");
        }
    }
}
