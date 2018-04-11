using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TheDiplomWork
{
    public class ShaderedScene
    {
        Environment env = new Environment();

        protected float[] vertices = null;
        protected float[] colors = null;
        public ShaderedScene()
        {
            
        }
        void Initialization()
        {
            for (int i = 0; i < CubicalMemory.Chunk.Height; i++)
                for (int j = 0; j < CubicalMemory.Chunk.Width; j++)
                    for (int k = 0; k < CubicalMemory.Chunk.Length; k++)
                    {
                        env.cub_mem.cubes[i][j][k].IsFilled = true;
                        cubes[i][j][k].color = ColorSwitch(Rand.Next(10));
                    }
        }
    }
}
