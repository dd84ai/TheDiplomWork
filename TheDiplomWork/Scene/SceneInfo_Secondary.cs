using System;
using System.Collections.Generic;
using System.Linq;
using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
namespace TheDiplomWork
{
    public class SceneInfo_Secondary : SceneInfo
    {
        public VertexBuffer shiftedcenterDataBuffer;
        public VertexBuffer sizeDataBuffer;
        DataForDraw_angled Data;
        public SceneInfo_Secondary(OpenGL _gl, DataForDraw_angled _Data) : base(_gl)
        {
            Data = _Data;
        }
        public override void CreateVerticesForSquare_FirstInit()
        {
            CreateVerticesForSquare_FirstInit_wrapped();

            //  Now do the same for the colour data.
            shiftedcenterDataBuffer = new VertexBuffer();
            shiftedcenterDataBuffer.Create(gl);

            //  Now do the same for the colour data.
            sizeDataBuffer = new VertexBuffer();
            sizeDataBuffer.Create(gl);
            
        }
        public override void CreateVerticesForSquare(ref DataForDraw Data)
        {
            Console.WriteLine("FATAL ERROR! DO NOT USE IT! USE CreateVerticesForSquare_angled!");
            Console.ReadKey();
        }
        public void CreateVerticesForSquare_angled()
        {
            //  Create the vertex array object.
            vertexBufferArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, Data.vertices_arrayed, false, 3);

            //  Now do the same for the colour data.
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, Data.colours_arrayed, false, 3);

            shiftedcenterDataBuffer.Bind(gl);
            shiftedcenterDataBuffer.SetData(gl, 2, Data.center_arrayed, false, 3);

            sizeDataBuffer.Bind(gl);
            sizeDataBuffer.SetData(gl, 3, Data.size_arrayed, false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
            vertexDataBuffer.Unbind(gl);
            colourDataBuffer.Unbind(gl);
            shiftedcenterDataBuffer.Unbind(gl);
            sizeDataBuffer.Unbind(gl);
        }
    }
}
