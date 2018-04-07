using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Keyboard
    {
        public static void Wrapped_KeyPressed_Reaction(char KeyChar)
        {
            switch (KeyChar)
            {
                case 'm': Interface.SaySomeQuote(); break;

                default: break;
            }
        }
    }
}
