using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;

namespace TheDiplomWork
{
    public class Speech
    {
        public class LS
        {
            public SpeechSynthesizer synthesizer = new SpeechSynthesizer();
            public LS()
            {
                synthesizer.Volume = 100;  // 0...100
                synthesizer.Rate = -2;     // -10...10
            }

            //// Synchronous
            //synthesizer.Speak("Hello World");

            //// Asynchronous
            //synthesizer.SpeakAsync("Hello World");
        }
        public static LS ls = new LS();
    }
    
}
