using System;
using System.Speech.Recognition;
using VoiceManagedShop.VoiceRecognition;
using VoiceManagedShop.Drivers;
using VoiceManagedShop.Data;
using VoiceManagedShop.Adapters;

namespace VoiceManagedShop
{
    class Program
    {
        private const string url = @"https://localhost:44351/";

        static void Main(string[] args)
        {
            using var webDriver = new ChromeWebDriver(url);
            VoiceRecognitionAdapter.webDriver = webDriver;
            while (Console.ReadLine() != "start")
            {
            }

            //var recognizer = new VoiceRecognizer(speechRecognized);
            //recognizer.StartRecognition();
            using SpeechRecognitionEngine recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

            // Create and load a dictation grammar.  
            recognizer.LoadGrammar(new DictationGrammar());

            // Add a handler for the speech recognized event.  
            recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(VoiceRecognitionAdapter.speech_Recognized);
            //recognizer.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizedEvent);

            // Configure input to the speech recognizer.  
            recognizer.SetInputToDefaultAudioDevice();

            // Start asynchronous, continuous speech recognition.  
            recognizer.RecognizeAsync(RecognizeMode.Multiple);
            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}