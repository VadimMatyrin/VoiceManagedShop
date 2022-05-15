using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VoiceManagedShop.Drivers;

namespace VoiceManagedShop.Adapters
{
    public class VoiceRecognitionAdapter
    {
        public static ChromeWebDriver webDriver;
        // Handle the SpeechRecognized event.  
        public static void speech_Recognized(object sender, SpeechRecognizedEventArgs e)
        {
            string text = e.Result.Text.Replace(" into", "");
            Console.WriteLine("Recognized text: " + text);

            string[] commands = text.Split(' ');
            string type = commands.ElementAtOrDefault(0);
            string textRecognized = commands.ElementAtOrDefault(1);
            string? inputId = commands.ElementAtOrDefault(2);
            bool isButton = text.Contains("button", StringComparison.OrdinalIgnoreCase);
            int indexToClick = 0;
            if (Regex.IsMatch(text, @"\d+"))
            {
                indexToClick = int.Parse(Regex.Match(text, @"\d+").Value) - 1;
            }

            if (isButton)
            {
                switch (type)
                {
                    case "click":
                        if (webDriver.TryClickByXPath($"//button[contains(@class, '{textRecognized}')]", indexToClick))
                        {
                            Console.WriteLine($"Successfully clicked - {textRecognized}");
                        }
                        else
                        {
                            Console.WriteLine("Nothing to click");
                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognized command type");
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case "click":
                        if (textRecognized != null && textRecognized.Length > 0 && webDriver.TryClickByXPath($"//*[text()[contains(.,'{textRecognized.Remove(0, 1)}')]]", indexToClick))
                        {
                            Console.WriteLine($"Successfully clicked - {textRecognized}");
                        }
                        else
                        {
                            Console.WriteLine("Nothing to click");
                        }
                        break;
                    case "input":
                        if (webDriver.TryInputById(inputId, textRecognized))
                        {

                        }
                        break;
                    default:
                        Console.WriteLine("Unrecognized command type");
                        break;
                }
            }
        }
    }
}
