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
    public class SceneInfo
    {
        public VertexBufferArray vertexBufferArray;
        public VertexBuffer vertexDataBuffer;
        public VertexBuffer colourDataBuffer;
        OpenGL gl;
        public SceneInfo(OpenGL _gl)
        {
            gl = _gl;
            CreateVerticesForSquare_FirstInit();
        }
        public void CreateVerticesForSquare_FirstInit()
        {
            //  Create the vertex array object.
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);

            //  Create a vertex buffer for the vertex data.
            vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);

            //  Now do the same for the colour data.
            colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
        }
        public void CreateVerticesForSquare(ref DataForDraw Data)
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
