using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
namespace TheDiplomWork
{
    public class ShaderedScene
    {
        public Environment env = new Environment();

        public ShaderedScene()
        {
            //Initialization();
            //CopyToReady();
        }
        
        Random Rand = new Random();

        public DataForDraw_without_angles Main = new DataForDraw_Main();
        public DataForDraw_angled Secondary = new DataForDraw_Secondary();
        public DataForDraw_angled TemporalList = new DataForDraw_TemporalList();
        public DataForDraw_angled SunAndMoon = new DataForDraw_SunAndMoon();
        public static void CalculateFromMaptoGraphical(GeneralProgrammingStuff.Point2Int XYworld, GeneralProgrammingStuff.Point3Int XYZcube, ref float x, ref float y, ref float z)
        {
            x = XYworld.x * CubicalMemory.Chunk.Width + XYZcube.x;
            y = XYZcube.y;
            z = XYworld.z * CubicalMemory.Chunk.Length + XYZcube.z;

            x *= (CubicalMemory.Cube.rangeOfTheEdge);
            y *= (CubicalMemory.Cube.rangeOfTheEdge);
            z *= (CubicalMemory.Cube.rangeOfTheEdge);
        }
        
    }
}
