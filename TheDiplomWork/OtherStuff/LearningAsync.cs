using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace TheDiplomWork
{
    public class LearningAsync
    {
        public LearningAsync()
        {
            if (false)
            {
                // Start a thread that calls a parameterized static method.
                Thread newThread = new Thread(LearningAsync.DoWork);
                newThread.Start(42);

                while (newThread.IsAlive) { }
                // Start a thread that calls a parameterized instance method.

                newThread = new Thread(DoMoreWork);
                newThread.Start("The answer.");

                Console.ReadKey();
            }
        }

        public static void DoWork(object data)
        {
            for (int i = 0; i < 100;i++)
            Console.WriteLine("Static thread procedure. Data='{0}'",
                i);
            return;
        }

        public void DoMoreWork(object data)
        {
            Console.WriteLine("Instance thread procedure. Data='{0}'",
                data);
        }
    }
}
