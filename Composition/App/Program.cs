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
            Configuration = new LoaderPaths("Debug");
            FileName = "recorded";
            // Notify, perhaps the tonals from david are good here.
            Console.WriteLine("Started recording...");
            // Set the recording duration
            RecordingDuration = 7000;
            var path = Path.Combine(LoaderPaths.ActiveRuntime, LoaderPaths.SavePath, FileName + ".wav");
            Console.WriteLine(PocketSphinx.StartBashProcess("arecord -vv -r 16000 -c 1 -f S16_LE -d 2 " + path));
            //VoiceRecorder.Record(ReturnRecordingFilePath("recorded"), RecordingDuration);
            //VoiceRecorder.RecordingFinished += RecordingEvent;
            Console.WriteLine("File recorded.");
            // Run pocketsphinx to detect what was spoken.
            var output = PocketSphinx.StartBashProcess("pocketsphinx single" + ReturnRecordingFilePath(FileName));
             // Display the classification result
            Console.WriteLine("Output: " + output.StandardOutput.ReadToEnd());
            Console.ReadLine();

            await Task.CompletedTask;
        }

        static string ReturnRecordingFilePath(string filename)
        {
            return Path.Combine(LoaderPaths.ActiveRuntime, LoaderPaths.SavePath, filename + ".wav");
        }
        static void RecordingEvent(object source, EventArgs e)
        {
            Console.WriteLine("Recording has completed.");
        }
    }
}
