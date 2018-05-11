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

        public DataForDraw_without_angles Main;
        public DataForDraw_angled Secondary;
        public DataForDraw_angled TemporalList;
        public DataForDraw_angled ExplosionList;
        public DataForDraw_angled SunAndMoon;

        public ShaderedScene(OpenGL gl)
        {
            Main = new DataForDraw_Main(gl);
            Secondary = new DataForDraw_Secondary(gl);
            TemporalList = new DataForDraw_TemporalList(gl);
            ExplosionList = new DataForDraw_ExplodingList(gl);
            SunAndMoon = new DataForDraw_SunAndMoon(gl);

        }
        
        Random Rand = new Random();
        
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
