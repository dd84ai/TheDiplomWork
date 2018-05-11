using System;
using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
namespace TheDiplomWork
{
    public class SceneInfo_Main : SceneInfo
    {
        DataForDraw_without_angles Data;
        public SceneInfo_Main(OpenGL _gl, DataForDraw_without_angles _Data) : base(_gl)
        {
            Data = _Data;
        }

        public override void CreateVerticesForSquare(ref DataForDraw Data)
        {
            Console.WriteLine("FATAL ERROR! DO NOT USE IT! USE CreateVerticesForSquare_angled!");
            Console.ReadKey();
        }
        public void CreateVerticesForSquare_not_angled()
        {
            //  Create the vertex array object.
            vertexBufferArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, Data.vertices_arrayed, false, 3);

            //  Now do the same for the colour data.
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, Data.colours_arrayed, false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
            vertexDataBuffer.Unbind(gl);
            colourDataBuffer.Unbind(gl);
        }
    }
}
