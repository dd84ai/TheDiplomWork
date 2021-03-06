﻿using System;
using GlmNet;
using SharpGL;
using SharpGL.Shaders;
using SharpGL.VertexBuffers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
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
        public static ShaderedScene SS;
        public static LearningAsync LA = new LearningAsync();

        //  The projection, view and model matrices.
        mat4 projectionMatrix;
        public mat4 viewMatrix;
        mat4 modelMatrix;
        public mat4 rotMatrix;
        mat3 playerMatrix;
        mat3 sunMatrix;
        mat3 zeroMatrix = new mat3(new vec3(0),
                    new vec3(0),
                    new vec3(0));
        //  Constants that specify the attribute indexes.
        const uint attributeIndexPosition = 0;
        const uint attributeIndexColour = 1;

        //  The vertex buffer array which contains the vertex and colour buffers.

        //  The shader program for our vertex and fragment shader.
        private ModifiedShaderProgram shaderProgram;
        private ModifiedShaderProgram shaderProgram_secondary;
        private ModifiedShaderProgram shaderProgram_projectile;
        //private ModifiedShaderProgram shaderProgram_ghost;
        /// <summary>
        /// Initialises the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// <param name="width">The width of the screen.</param>
        /// <param name="height">The height of the screen.</param>
        static OpenGL _gl;
        Thread newThread;
        Thread newThread_ghost;
        public static bool ShadersInitializated = true;
        public string ShadersWereNotInitializatedMessage = "";
        //System.Collections.Generic.Dictionary<uint, string> dict = new System.Collections.Generic.Dictionary<uint,string>();

            float width, height;
        //Создание шейдеров более in Old Fashioned Way чтобы использовать Geomtry Shader
        //https://www.codeproject.com/Articles/1167387/OpenGL-with-OpenTK-in-Csharp-Part-Compiling-Shader
        public void Initialise(OpenGL gl, float width, float height)
        {
            this.width = width; this.height = height;
            try
            {
                _gl = gl;
                SS =  new ShaderedScene(gl);
                newThread_ghost = new Thread(Scene.DoWork_ghost);

            Console.WriteLine("Starting My");
            newThread = new Thread(Scene.DoWork);
            newThread.Start(42);

            while (newThread.IsAlive) { }
            Console.WriteLine("Finished My");
            SS.Main.CopyToReady();

            //  Set a blue clear colour.
            gl.ClearColor(0.4f, 0.6f, 0.9f, 0.0f);
                

                //gl.Hint(OpenGL.WGL_CONTEXT_DEBUG_BIT_ARB, OpenGL.GL_TRUE);
                //  Create the shader program.

                var Header = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Header.vert");
                var Cuter = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Cuter.vert");
                var Sun = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Sun.vert");
                var Rotator = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Rotator.vert");
                var Translation = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Translation.vert");
                var Sizer = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Sizer.vert");
                var Explosion = ManifestResourceLoader.LoadTextFile(@"Shaders\VertexModules\Explosion.vert");

                var Main = ManifestResourceLoader.LoadTextFile(@"Shaders\Main\Main.vert");
                var Adv_main = ManifestResourceLoader.LoadTextFile(@"Shaders\Main\Adv_main.vert");
                var Projectile_main = ManifestResourceLoader.LoadTextFile(@"Shaders\Main\Projectile_main.vert");


                var vertexShaderSource = 
                    Header +
                    Rotator +
                    Cuter +
                    Sun +
                    Main;

                var FragmentalShader = ManifestResourceLoader.LoadTextFile(@"Shaders\OtherShaders\FragmentalShader.frag");
                var GeometryShader = ManifestResourceLoader.LoadTextFile(@"Shaders\OtherShaders\GeometryShader.geom");
                shaderProgram = new ModifiedShaderProgram();
                shaderProgram.Create(gl, vertexShaderSource, FragmentalShader, GeometryShader, null);
                shaderProgram.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram.AssertValid(gl);

                var vertexShaderSource2 = 
                    Header +
                    Rotator +
                    Cuter + 
                    Sun +
                    Translation + 
                    Sizer +
                    Explosion +
                    Adv_main;

                shaderProgram_secondary = new ModifiedShaderProgram();
                shaderProgram_secondary.Create(gl, vertexShaderSource2, FragmentalShader, GeometryShader, null);
                shaderProgram_secondary.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram_secondary.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram_secondary.BindAttributeLocation(gl, 2, "in_Center");
                shaderProgram_secondary.BindAttributeLocation(gl, 3, "in_Angles");
                shaderProgram_secondary.BindAttributeLocation(gl, 4, "in_Size");
                shaderProgram_secondary.AssertValid(gl);

                var vertexShaderSource3 =
                    Header +
                    Rotator +
                    Cuter +
                    Sun +
                    Translation +
                    Sizer +
                    Explosion +
                    Projectile_main;

                shaderProgram_projectile = new ModifiedShaderProgram();
                shaderProgram_projectile.Create(gl, vertexShaderSource3, FragmentalShader, GeometryShader, null);
                shaderProgram_projectile.BindAttributeLocation(gl, attributeIndexPosition, "in_Position");
                shaderProgram_projectile.BindAttributeLocation(gl, attributeIndexColour, "in_Color");
                shaderProgram_projectile.BindAttributeLocation(gl, 2, "in_Center");
                shaderProgram_projectile.BindAttributeLocation(gl, 3, "in_Angles");
                shaderProgram_projectile.BindAttributeLocation(gl, 4, "in_Size");
                shaderProgram_projectile.AssertValid(gl);
            }
            catch (ShaderCompilationException ShadersMessageError)
            {
                ShadersInitializated = false;
                ShadersWereNotInitializatedMessage = ShadersMessageError.CompilerOutput;
                MessageBox.Show(ShadersWereNotInitializatedMessage);
                //Environment.Exit(123);
            }

            if (ShadersInitializated)
            {
                //  Create a perspective projection matrix.

                //  Create a model matrix to make the model a little bigger.
                modelMatrix = glm.scale(new mat4(1.0f), new vec3(Environment.SizeView));

                //  Now create the geometry for the square.
                SS.Main.scene_info.CreateVerticesForSquare_not_angled();

                var handle = GetConsoleWindow();
                if (!StaticSettings.S.ConsoleIsEnabled) ShowWindow(handle, SW_HIDE);

                Scene.SS.env.player.coords.Player_precise_position.TryLoad("PlayerPosition");
                Scene.SS.env.player.coords.Player_rotational_view.TryLoad("PlayerRotationalView");
                SaveAndLoad.Load("default");

                Reloader_SunAndMoon();

                gl.Enable(OpenGL.GL_BLEND);
                gl.BlendFunc(OpenGL.GL_SRC_ALPHA, OpenGL.GL_ONE_MINUS_SRC_ALPHA);
            }

            if (StaticSettings.S.LoadProjectile) Projectile.jp.LoadFromFile();
        }
        public void Reloader_SunAndMoon()
        {
            SS.SunAndMoon.initialization();
            SS.SunAndMoon.CopyToReady();
            SS.SunAndMoon.scene_info.CreateVerticesForSquare_angled();
        }
        public void Reloader_TemporalList()
        {
            SS.FreshlyPlacedList.initialization();
            SS.FreshlyPlacedList.CopyToReady();
            SS.FreshlyPlacedList.scene_info.CreateVerticesForSquare_angled();
        }
        public static void Reloader_ExplosionList()
        {
            SS.ExplosionList.initialization();
            SS.ExplosionList.CopyToReady();
            SS.ExplosionList.scene_info.CreateVerticesForSquare_angled();
        }

        
        public void Reloader_Ghost()
        {
            if (!SS.Ghost.CopiedLastResult)
            {
                SS.Ghost.CopyToReady();
                SS.Ghost.scene_info.CreateVerticesForSquare_angled();
            }
            //Призрачным куб.
            if (StaticSettings.S.Secondary_SceneInfo_is_Activated &&
                (
                (StaticSettings.S.GhostCube_Add_in_Data_For_Draw &&
                Scene.SS.env.player.coords.Player_cubical_lookforcube !=
                Scene.SS.env.player.coords.Player_cubical_lookforcube_OLD)
                || GraphicalOverlap.Rebuilding_is_required_cause_of_GO_color_changed_color
                || ExtraAction
                )
                )
            {

                if (!newThread_ghost.IsAlive && !DoWork_ghost_IsAlive)
                {
                    Scene.SS.env.player.coords.Player_cubical_lookforcube_OLD.x = Scene.SS.env.player.coords.Player_cubical_lookforcube.x;
                    Scene.SS.env.player.coords.Player_cubical_lookforcube_OLD.y = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
                    Scene.SS.env.player.coords.Player_cubical_lookforcube_OLD.z = Scene.SS.env.player.coords.Player_cubical_lookforcube.z;
                    newThread_ghost = new Thread(Scene.DoWork_ghost);
                    newThread_ghost.Start(46);
                }
                GraphicalOverlap.Rebuilding_is_required_cause_of_GO_color_changed_color = false;
            }
        }
        /// TimeSpan timeItTook = (DateTime.Now - start);
        
        public void Reloader_Main(OpenGL gl)
        {
            if (StaticSettings.S.RequiredReloader && !newThread.IsAlive && !DoWork_IsAlive)
            {
                if (!SS.Main.CopiedLastResult)
                {
                    SS.Main.scene_info.vertexBufferArray.Delete(gl);
                    SS.Main.CopyToReady();
                    SS.Main.scene_info.CreateVerticesForSquare_not_angled();
                    DataForDraw_FreshlyPlacedCubes.TemporalList.Clear();
                    Reloader_TemporalList();
                }

                float scalar = GeneralProgrammingStuff.vec3_scalar(Scene.SS.env.player.coords.LastPlayerLook, Scene.SS.env.player.coords.NormalizedLook);

                if (SS.env.player.coords.Player_chunk_position.x >= 0 && SS.env.player.coords.Player_chunk_position.x < CubicalMemory.World.Quantity_of_chunks_in_root
                    && SS.env.player.coords.Player_chunk_position.z >= 0 && SS.env.player.coords.Player_chunk_position.z < CubicalMemory.World.Quantity_of_chunks_in_root)

                    if ((StaticSettings.S.ReloaderCauseOfChunkRare && ((float)Math.Abs(SS.env.player.coords.Player_chunk_position.x - SS.env.player.coords.Player_chunk_position_OLD.x) > ((float)StaticSettings.S.RangeOfView / 2 - 1)
                    || (float)Math.Abs(SS.env.player.coords.Player_chunk_position.z - SS.env.player.coords.Player_chunk_position_OLD.z) > ((float)StaticSettings.S.RangeOfView / 2 - 1)))
                            || (StaticSettings.S.ReloaderCauseOfChangingChunk && SS.env.player.coords.Player_chunk_position != SS.env.player.coords.Player_chunk_position_OLD)
                        || (StaticSettings.S.RealoderCauseOfPointOfView && scalar < StaticSettings.S.PointOfViewCoefOfDifference)
                        || (StaticSettings.S.RealoderCauseOfSunSided && SS.env.player.coords.Player_cubical_position.y != SS.env.player.coords.Player_cubical_position_OLD.y)
                        || StaticSettings.S.RealoderCauseOfBuildingBlocks
                        || StaticSettings.S.RangeOfView_Old != StaticSettings.S.RangeOfView)
                    {
                        StaticSettings.S.RangeOfView_Old = StaticSettings.S.RangeOfView;

                        newThread = new Thread(Scene.DoWork);
                        newThread.Start(42);
                        SS.env.player.coords.Player_chunk_position_OLD.x = SS.env.player.coords.Player_chunk_position.x;
                        SS.env.player.coords.Player_chunk_position_OLD.z = SS.env.player.coords.Player_chunk_position.z;
                        SS.env.player.coords.Player_cubical_position_OLD.x = SS.env.player.coords.Player_cubical_position.x;
                        SS.env.player.coords.Player_cubical_position_OLD.y = SS.env.player.coords.Player_cubical_position.y;
                        SS.env.player.coords.Player_cubical_position_OLD.z = SS.env.player.coords.Player_cubical_position.z;
                        Console.WriteLine($"Inting My CoThread #{CounterMyCoThread++}");
                        StaticSettings.S.RealoderCauseOfBuildingBlocks = false;
                    }
            }
            else
            {

            }
        }
        /// <summary>
        /// Draws the scene.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>
        /// TimeSpan timeItTook = (DateTime.Now - start);
        static DateTime start = DateTime.Now;
        static DateTime start_independent = DateTime.Now;
        public bool Every10SecondsAction = true;
        public int TimeRange = 3;
        public int TimeCount = 0;
        public static  bool ExtraAction = false;
        public void Draw(OpenGL gl)
        {
            TimeSpan timeItTook = (DateTime.Now - start);
            Time.time.SetTotalSeconds((DateTime.Now - start_independent).TotalSeconds);
            Time.time.TimeIncrease(timeItTook.TotalMilliseconds * Time.time.Time_Speed);
            start = DateTime.Now;

            if (Every10SecondsAction)
            {
                if (StaticSettings.S.MusicIsEnabled)
                    Music.wmp_player.PlayTheMusic_Checker();
            }

            if (Time.time.GetTotalSeconds() > TimeRange * TimeCount)
            {
                //start = DateTime.Now;
                Every10SecondsAction = true;
                TimeCount++;
            }
            else Every10SecondsAction = false;

            Reloader_Ghost();

            if (Time.time.GetGameTotalSeconds() > Time.time.TimeWaitForFallingCubes)
            Reloader_Main(gl);

            if (SS.Main.FirstInitialization)
            {
                StaticSettings.S.RealoderCauseOfBuildingBlocks = true;
            }
            else Draw_Wrapped(gl);
        }
        public static int CounterMyCoThread = 0;
        public static bool DoWork_IsAlive = false;
        public static void DoWork(object data)
        {
            DateTime Main_start = DateTime.Now;
            DoWork_IsAlive = true;
            Scene.SS.env.player.coords.LastPlayerLook.x = Scene.SS.env.player.coords.NormalizedLook.x;
            Scene.SS.env.player.coords.LastPlayerLook.y = Scene.SS.env.player.coords.NormalizedLook.y;
            Scene.SS.env.player.coords.LastPlayerLook.z = Scene.SS.env.player.coords.NormalizedLook.z;
            SS.Main.initialization();
            DoWork_IsAlive = false;
            //GC.Collect();

            TimeSpan timeItTook = (DateTime.Now - Main_start);
            Time.time.AverageRebuildingTime = timeItTook.TotalSeconds;

            //Main_start = DateTime.Now;
            return;
        }
        public static bool DoWork_ghost_IsAlive = false;
        public static void DoWork_ghost(object data)
        {
            DoWork_ghost_IsAlive = true;
            SS.Ghost.initialization();
            DoWork_ghost_IsAlive = false;
            if (!ExtraAction) ExtraAction = true; else ExtraAction = false;
            return;
        }
        public vec3[] playerMatrix_veced = new vec3[3];

        public void Draw_Wrapped(OpenGL gl)
        {
            //shaderProgram.SetUniform1

            Projectile.jp.ProcessStartingData();

            float rads = (StaticSettings.S.AngleOfView / 360.0f) * (float)Math.PI * 2.0f;
            projectionMatrix = glm.perspective(rads, width / height, 0.1f, 1000.0f);

            //  Create a view matrix to move us back a bit.
            viewMatrix = glm.translate(new mat4(1.0f), new vec3(-SS.env.player.coords.Player_precise_position.x,
                -SS.env.player.coords.Player_precise_position.y,
                -SS.env.player.coords.Player_precise_position.z));

            rotMatrix = glm.scale(new mat4(1.0f), new vec3(1.0f)) * glm.rotate(-SS.env.player.coords.Player_rotational_view.y, new vec3(1.0f, 0.0f, 0.0f)) * glm.rotate(-SS.env.player.coords.Player_rotational_view.x, new vec3(0.0f, 1.0f, 0.0f)) * glm.rotate(0, new vec3(0.0f, 0.0f, 1.0f));

            playerMatrix_veced[0] = Sun.S.player_pos;
            playerMatrix_veced[1] = Sun.S.player_stepback;
            playerMatrix_veced[2] = Sun.S.player_stepback - Sun.S.player_look;

            StaticShadow.Sh.ShadowProtocol();


            playerMatrix = new mat3(playerMatrix_veced);

            //  Clear the scene.
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);
            Time.time.GetSkyColor(gl);

            if (StaticSettings.S.Compass)
                StaticCompass.C.Draw_Compas(gl, (int)StaticAccess.FMOS.openGLControl.Width/2, (int)(0 + StaticCompass.C.r * 2 + 2));

            //  Bind the shader, set the matrices.
            shaderProgram.Bind(gl);
            shaderProgram.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());

            shaderProgram.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
            shaderProgram.SetUniformMatrix4(gl, "rotMatrix", rotMatrix.to_array());

            shaderProgram.SetUniform3(gl, "SunPosition", StaticShadow.Sh.SunPosition.x, StaticShadow.Sh.SunPosition.y, StaticShadow.Sh.SunPosition.z);

            shaderProgram.SetUniformMatrix3(gl, "playerMatrix", playerMatrix.to_array());

            sunMatrix = new mat3(new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0),
                    new vec3(0, DataForDraw.localed_range * Sun.LocalSun.Sun_Height, 0),//new vec3(0, (float)+DataForDraw.localed_range * 100, 0),
                    new vec3(StaticSettings.S.SunStatus.x,0,StaticSettings.S.PointOfViewCuterEnabled));

            shaderProgram.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());
            shaderProgram.SetUniform1(gl, "settingsTransparency", 1.0f);
            shaderProgram.SetUniform1(gl, "TimeTotalSeconds", (float)Time.time.GetTotalSeconds());
            shaderProgram.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 0.0f);
            shaderProgram.SetUniform3(gl, "viewparameters", 
                (float)(StaticSettings.S.RangeOfView_Old - 1)*16,
                StaticSettings.S.PointOfViewCoefOfDifference,
                StaticSettings.S.SunSidedCoef
                );

            //  Bind the out vertex array.
            SS.Main.scene_info.vertexBufferArray.Bind(gl);
            //  Draw the square.
            gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.Main.Quantity()/3);
            //  Unbind our vertex array and shader.
            SS.Main.scene_info.vertexBufferArray.Unbind(gl);

            shaderProgram.Unbind(gl);
            if (StaticSettings.S.Secondary_SceneInfo_is_Activated)
            {
                shaderProgram_secondary.Bind(gl);
                shaderProgram_secondary.SetUniform3(gl, "SunPosition", StaticShadow.Sh.SunPosition.x, StaticShadow.Sh.SunPosition.y, StaticShadow.Sh.SunPosition.z);

                shaderProgram_secondary.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
                shaderProgram_secondary.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
                shaderProgram_secondary.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
                shaderProgram_secondary.SetUniformMatrix4(gl, "rotMatrix", rotMatrix.to_array());
                shaderProgram_secondary.SetUniformMatrix3(gl, "playerMatrix", playerMatrix.to_array());

                sunMatrix = new mat3(new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0),
                    new vec3(0, DataForDraw.localed_range * Sun.LocalSun.Sun_Height, 0),//new vec3(0, (float)+DataForDraw.localed_range * 100, 0),
                    new vec3(StaticSettings.S.SunStatus.x, 1.0f, 0.0f));
                shaderProgram_secondary.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());
                shaderProgram_secondary.SetUniform1(gl, "settingsTransparency", 1.0f);
                shaderProgram_secondary.SetUniform1(gl, "TimeTotalSeconds", (float)Time.time.GetGameTotalSeconds());
                shaderProgram_secondary.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 0.0f);
                //shaderProgram_secondary.SetUniform1(gl, "TimePauseForExplosion", Projectile.jp.TimePauseUntilExplosion);

                shaderProgram_secondary.SetUniform3(gl, "viewparameters",
               (float)(StaticSettings.S.RangeOfView_Old - 1) * 16,
               StaticSettings.S.PointOfViewCoefOfDifference,
               StaticSettings.S.SunSidedCoef
               );

                SS.FreshlyPlacedList.scene_info.vertexBufferArray.Bind(gl);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.FreshlyPlacedList.Quantity() / 3);
                SS.FreshlyPlacedList.scene_info.vertexBufferArray.Unbind(gl);

                if (!newThread.IsAlive && !DoWork_IsAlive)
                {
                    if (Explosion.exp.StartingFirst)
                    {
                        Music.wav_player.SaySoundEffect("Explosion");

                        //Music.wav_player.SaySoundEffect("Explosion");
                        //Interface.Time_pause(900);
                        //TimeSpan timeItTook = (DateTime.Now - start);
                        //Time.time.SetTotalSeconds((DateTime.Now - start_independent).TotalSeconds);
                        //Time.time.TimeIncrease(timeItTook.TotalMilliseconds * Time.time.Time_Speed);
                        //start = DateTime.Now;

                        Explosion.exp.StartingFirst = false;
                        Explosion.exp.StartingFirstStarted = true;
                        Explosion.exp.StartingShiftForLoeading = (float)Time.time.GetGameTotalSeconds() - Explosion.exp.StartingTime;
                    }
                }

                if (Explosion.exp.StartingFirstStarted)
                {
                    shaderProgram_secondary.SetUniform1(gl, "TimeTotalSeconds", (float)Time.time.GetGameTotalSeconds() - (Explosion.exp.StartingShiftForLoeading));// - Explosion.exp.StartingShiftForLoeading);
                    shaderProgram_secondary.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 1.0f);
                    SS.ExplosionList.scene_info.vertexBufferArray.Bind(gl);
                    gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.ExplosionList.Quantity() / 3);
                    SS.ExplosionList.scene_info.vertexBufferArray.Unbind(gl);
                    shaderProgram_secondary.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 0.0f);
                }
                

                shaderProgram_secondary.SetUniform1(gl, "TimeTotalSeconds", (float)Time.time.GetGameTotalSeconds());
                shaderProgram_secondary.SetUniform1(gl, "settingsTransparency", (float)(0.4 + 0.2 * Math.Abs(Math.Sin(Time.time.GetTotalRadianTime()*200.0))));
                SS.Ghost.scene_info.vertexBufferArray.Bind(gl);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.Ghost.Quantity() / 3);
                SS.Ghost.scene_info.vertexBufferArray.Unbind(gl);

                sunMatrix = new mat3(new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0),
                    new vec3(0, DataForDraw.localed_range * Sun.LocalSun.Sun_Height, 0),//new vec3(0, (float)+DataForDraw.localed_range * 100, 0),
                    new vec3(1.0f, 1.0f, 0.0f));
                shaderProgram_secondary.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());

                if (Projectile.jp.Loaded && !Projectile.jp.Launched)
                {
                    shaderProgram_secondary.SetUniform1(gl, "settingsTransparency", 0.3f);

                    Projectile.jp.NotEveryTimeRealoder();

                    SS.TrajectoryPath.scene_info.vertexBufferArray.Bind(gl);
                    gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.TrajectoryPath.Quantity() / 3);
                    SS.TrajectoryPath.scene_info.vertexBufferArray.Unbind(gl);
                }

                sunMatrix = new mat3(new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0),
                    new vec3(0, DataForDraw.localed_range * Sun.LocalSun.Sun_Height, 0),//new vec3(0, (float)+DataForDraw.localed_range * 100, 0),
                    new vec3(1.0f, 1.0f, 0.25f));
                //Второе значений 3 строки отключает Point Of view если больше 0.5 в геом шейдере.
                //Третье пусть отключит вращение.
                shaderProgram_secondary.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());
                shaderProgram_secondary.SetUniform1(gl, "settingsTransparency", 1.0f);
                //shaderProgram_sunandmoon.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());

                SS.SunAndMoon.scene_info.vertexBufferArray.Bind(gl);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.SunAndMoon.Quantity() / 3);
                SS.SunAndMoon.scene_info.vertexBufferArray.Unbind(gl);
                //shaderProgram_sunandmoon.Unbind(gl);

                shaderProgram_secondary.Unbind(gl);

            }

            if (Projectile.jp.Loaded)
            {
                shaderProgram_projectile.Bind(gl);
                shaderProgram_projectile.SetUniform3(gl, "SunPosition", StaticShadow.Sh.SunPosition.x, StaticShadow.Sh.SunPosition.y, StaticShadow.Sh.SunPosition.z);

                shaderProgram_projectile.SetUniformMatrix4(gl, "projectionMatrix", projectionMatrix.to_array());
                shaderProgram_projectile.SetUniformMatrix4(gl, "modelMatrix", modelMatrix.to_array());
                shaderProgram_projectile.SetUniformMatrix4(gl, "viewMatrix", viewMatrix.to_array());
                shaderProgram_projectile.SetUniformMatrix4(gl, "rotMatrix", rotMatrix.to_array());
                shaderProgram_projectile.SetUniformMatrix3(gl, "playerMatrix", playerMatrix.to_array());

                sunMatrix = new mat3(new vec3(-(float)Time.time.GetTotalRadianTime(), 0, 0),
                    new vec3(0, DataForDraw.localed_range * Sun.LocalSun.Sun_Height, 0),//new vec3(0, (float)+DataForDraw.localed_range * 100, 0),
                    new vec3(StaticSettings.S.SunStatus.x, 1.0f, 0.0f));
                shaderProgram_projectile.SetUniformMatrix3(gl, "sunMatrix", sunMatrix.to_array());
                shaderProgram_projectile.SetUniform1(gl, "settingsTransparency", 1.0f);
                shaderProgram_projectile.SetUniform1(gl, "TimeTotalSeconds", (float)Time.time.GetGameTotalSeconds() - (Explosion.exp.StartingShiftForLoeading));// - Explosion.exp.StartingShiftForLoeading);
                shaderProgram_projectile.SetUniform3(gl, "viewparameters",
               (float)(StaticSettings.S.RangeOfView_Old - 1) * 16,
               StaticSettings.S.PointOfViewCoefOfDifference,
               StaticSettings.S.SunSidedCoef
               );

                if (Explosion.exp.StartingFirstStarted)
                    shaderProgram_projectile.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 1.0f);
                else
                    shaderProgram_projectile.SetUniform1(gl, "settingsTHIS_IS_EXPLOSION", 0.5f);
                //shaderProgram_projectile.SetUniform1(gl, "TimePauseForExplosion", Projectile.jp.TimePauseUntilExplosion);

                shaderProgram_projectile.SetUniformMatrix3(gl, "projectileMatrix", Projectile.jp.GetProjectileMatrix().to_array());

                SS.ProjectileList.scene_info.vertexBufferArray.Bind(gl);
                gl.DrawArrays(OpenGL.GL_POINTS, 0, SS.ProjectileList.Quantity() / 3);
                SS.ProjectileList.scene_info.vertexBufferArray.Unbind(gl);

                shaderProgram_projectile.Unbind(gl);
            }
            
            //SS.OpenGLDraw(gl, modelMatrix * rotMatrix * viewMatrix);//projectionMatrix * rotMatrix * viewMatrix * );
        }
        /// <summary>
        /// Creates the geometry for the square, also creating the vertex buffer array.
        /// </summary>
        /// <param name="gl">The OpenGL instance.</param>

        public void Scene_Form_Closing(OpenGL gl)
        {
            
        }
    }
}
