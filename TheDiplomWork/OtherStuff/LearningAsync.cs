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
                Task<bool> thing = My();

                int i = 0;
                while (!thing.IsCompleted)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine($"* {i * 1000}");
                    i++;
                }
            }
            //Console.ReadLine();
        }

        static async Task<bool> My()
        {
            string message = await GetMessage(3000);
            Console.WriteLine(message);

            return true;
        }

        static Task<string> GetMessage(int time)
        {
            return Task.Run(() => {
                Thread.Sleep(time);
                return $"zxzxz {time.ToString()}";
            });
        }
    }
}
