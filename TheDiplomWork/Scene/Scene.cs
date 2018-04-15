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
    /// <summary>
    /// Represents the Scene for this sample.
    /// </summary>
    /// <remarks>
    /// This code is based on work from the OpenGL 4.x Swiftless tutorials, please see:
    /// http://www.swiftless.com/opengl4tuts.html
    /// </remarks>
    public class Scene
    {
        //Console Hider
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        /// <summary>
        /// It's Mine
        /// </summary>
        public static ShaderedScene SS = new ShaderedScene();
        public static LearningAsync LA = new LearningAsync();

        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        mat4 viewMatrix;
        mat4 modelMatrix;
        mat4 rotMatrix;

        //  Constants that specify the attribute indexes.
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        //  The vertex buffer array which contains the vertex and colour buffers.
        static VertexBufferArray vertexBufferArray;
        //VertexBufferArray vertexBufferArray2;

        //  The shader program for our vertex and fragment shader.
        private ShaderProgram shaderProgram;

        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="width">The width of the screen.</param>
        /// <param name="height">The height of the screen.</param>
        static OpenGL _gl;
        public async void Initialise(OpenGL gl, float width, float height)
        {
            _gl = gl;
            Console.WriteLine("Starting My");
            newThread = new Thread(Scene.DoWork);
            newThread.Start(42);

            while (newThread.IsAlive) { }
            Console.WriteLine("Finished My");
            SS.CopyToReady();

            //  Set a blue clear colour.
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.0f);

            //gl.Hint(OpenGL.WGL_CONTEXT_DEBUG_BIT_ARB, OpenGL.GL_TRUE);
            //  Create the shader program.
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile("Shaders\\Shader.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("Shaders\\Shader.frag");
            shaderProgram = new ShaderProgram();
            shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
            shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
            shaderProgram.AssertValid(gl);

            //  Create a perspective projection matrix.
            const float rads = (60.0f / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, width / height, 0.1f, 100.0f);

            //  Create a model matrix to make the model a little bigger.
            modelMatrix = glm.scale(new mat4(1.0f), new vec3(Environment.SizeView));

            //  Now create the geometry for the square.
            CreateVerticesForSquare();

            var handle = GetConsoleWindow();
            ShowWindow(handle, SW_HIDE);

            Scene.SS.env.player.coords.Player_precise_position.TryLoad("PlayerPosition");
            Scene.SS.env.player.coords.Player_rotational_view.TryLoad("PlayerRotationalView");
        }

        Thread newThread;
        /// <summary>
        /// Draws the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// 
        public void Draw(OpenGL gl)
        {
            if (SS.env.player.coords.RangeReloader && !newThread.IsAlive)
            {
                if (!SS.CopiedLastResult)
                {
                    vertexBufferArray.Delete(gl);
                    SS.CopyToReady();
                    CreateVerticesForSquare();
                    GC.Collect();
                }

                if (SS.env.player.coords.Player_chunk_position.x >= 0 && SS.env.player.coords.Player_chunk_position.x < CubicalMemory.World.Quantity_of_chunks_in_root
                    && SS.env.player.coords.Player_chunk_position.z >= 0 && SS.env.player.coords.Player_chunk_position.z < CubicalMemory.World.Quantity_of_chunks_in_root)
                if (Math.Abs(SS.env.player.coords.Player_chunk_position.x - SS.env.player.coords.Player_chunk_position_OLD.x) > (SS.env.player.coords.RangeOfView / 2 - 1)
                || Math.Abs(SS.env.player.coords.Player_chunk_position.z - SS.env.player.coords.Player_chunk_position_OLD.z) > (SS.env.player.coords.RangeOfView / 2 - 1))
                {
                    newThread = new Thread(Scene.DoWork);
                    newThread.Start(42);
                    SS.env.player.coords.Player_chunk_position_OLD.x = SS.env.player.coords.Player_chunk_position.x;
                    SS.env.player.coords.Player_chunk_position_OLD.z = SS.env.player.coords.Player_chunk_position.z;
                    Console.WriteLine("Inting My");
                }
            }
            else
            {

            }

            if (SS.FirstInitialization) Draw_Wrapped(gl);
        }
        public static void DoWork(object data)
        {
            SS.Initialization();
            return;
        }

        public void Draw_Wrapped(OpenGL gl)
        {
            //  Create a view matrix to move us back a bit.
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(-SS.env.player.coords.Player_precise_position.x,
                -SS.env.player.coords.Player_precise_position.y,
                -SS.env.player.coords.Player_precise_position.z));

            rotMatrix = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate(-SS.env.player.coords.Player_rotational_view.y, new vec3(1.0f, 0.0f, 0.0f)) * glm.rotate(-SS.env.player.coords.Player_rotational_view.x, new vec3(0.0f, 1.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f));

            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "rotMatrix", rotMatrix.to_array());

            //  Bind the out vertex array.
            vertexBufferArray.Bind(gl);

            //  Draw the square.
            //gl.DrawArrays(OpenGL.GL_QUADS, 0, SS.Quantity());

            //  Unbind our vertex array and shader.
            vertexBufferArray.Unbind(gl);
            shaderProgram.Unbind(gl);

            SS.OpenGLDraw(gl, rotMatrix * viewMatrix);//projectionMatrix * rotMatrix * viewMatrix * modelMatrix);
        }
        /// <summary>
        /// Creates the geometry for the square, also creating the vertex buffer array.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        public static void CreateVerticesForSquare()
        {
            OpenGL gl = _gl;
            //  Create the vertex array object.
            vertexBufferArray = new VertexBufferArray();
            vertexBufferArray.Create(gl);
            vertexBufferArray.Bind(gl);

            //  Create a vertex buffer for the vertex data.
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, ShaderedScene.vertices.ToArray(), false, 3);

            //  Now do the same for the colour data.
            var colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, SS.colors.ToArray(), false, 3);

            //  Unbind the vertex array, we've finished specifying data for it.
            vertexBufferArray.Unbind(gl);
        }
    }
}
