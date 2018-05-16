using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
using System.Windows.Forms;
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
        public DataForDraw_angled ProjectileList;

        public ShaderedScene(OpenGL gl)
        {
            Main = new DataForDraw_Main(gl);
            Secondary = new DataForDraw_Secondary(gl);
            TemporalList = new DataForDraw_TemporalList(gl);
            ExplosionList = new DataForDraw_ExplodingList(gl);
            SunAndMoon = new DataForDraw_SunAndMoon(gl);
            ProjectileList = new DataForDraw_Projectile(gl);

        }
        
        Random Rand = new Random();

        public static void CalculateFromMaptoGraphical(CubicalMemory.Cube cube, ref vec3 inp)
        {
            inp.x = cube.xz.x * CubicalMemory.Chunk.Width + cube.xyz.x;
            inp.y = cube.xyz.y;
            inp.z = cube.xz.z * CubicalMemory.Chunk.Length + cube.xyz.z;

            inp.x *= (CubicalMemory.Cube.rangeOfTheEdge);
            inp.y *= (CubicalMemory.Cube.rangeOfTheEdge);
            inp.z *= (CubicalMemory.Cube.rangeOfTheEdge);
        }
        public static void CalculateFromMaptoGraphical(CubicalMemory.Cube cube, ref float x, ref float y, ref float z)
        {
            try
            {
                x = cube.xz.x * CubicalMemory.Chunk.Width + cube.xyz.x;
                y = cube.xyz.y;
                z = cube.xz.z * CubicalMemory.Chunk.Length + cube.xyz.z;

                x *= (CubicalMemory.Cube.rangeOfTheEdge);
                y *= (CubicalMemory.Cube.rangeOfTheEdge);
                z *= (CubicalMemory.Cube.rangeOfTheEdge);
            }
            catch (Exception)
            {
                MessageBox.Show("CalculateFromMaptoGraphical Error");   
            }
        }
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
