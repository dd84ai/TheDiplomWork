using System;
using System.Windows.Forms;
using SharpGL;
namespace TheDiplomWork
{
    /// <summary>
    /// A form to render the scene.
    /// </summary>
    public partial class FormModernOpenGLSample : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormModernOpenGLSample"/> class.
        /// </summary>
        public FormModernOpenGLSample()
        {
            InitializeComponent();
            CF = new CalculatorFont(openGLControl);

            //if (Interface.IsReadyToPlay() && StaticSettings.S.MusicIsEnabled)

            Music.Initialize();
            //Keyboard.Wrapped_SINGLE_KeyPressed_Reaction('m');
        }

        /// <summary>
        /// Handles the OpenGLInitialized event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Initialise the scene.

                scene.Initialise(openGLControl.OpenGL, Width, Height);

        }

        /// <summary>
        /// Handles the OpenGLDraw event of the openGLControl1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="RenderEventArgs"/> instance containing the event data.</param>
        
        
        
        private void openGLControl_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL_Draw_ReWrapped();
            //defaulttriangle();
        }
        public void OpenGL_Draw_ReWrapped()
        {
            if (Scene.ShadersInitializated)
                openGL_Draw_Wrapped();
            else
            {
                Application.Exit();

                //  Clear the scene.
                openGLControl.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

                CF.Ultimate_DrawText(20, openGLControl.Height / 2 + 60, System.Drawing.Color.Red, 14, "WARNING!", 2.0f, openGLControl);

                //Center = OpenGLcontrol.Width/2 - (string.Length)*step/2
                CF.Ultimate_DrawText(20, openGLControl.Height/2 + 20, System.Drawing.Color.Red, 14, scene.ShadersWereNotInitializatedMessage, 2.0f, openGLControl);
                CF.Ultimate_DrawText(20, openGLControl.Height / 2 - 20, System.Drawing.Color.Red, 14, "Please, check compatibility with OpenGL 2.1", 2.0f, openGLControl);
                CF.Ultimate_DrawText(20, openGLControl.Height / 2 - 60, System.Drawing.Color.Red, 14, "Press any key to exit", 2.0f, openGLControl);
            }
        }
        public void openGL_Draw_Wrapped()
        {
            if (this.ActiveControl.Focused)
            {
                if ((Control.ModifierKeys & Keys.Shift) != 0)
                    Keyboard.DoSpecificAction('z');

                if ((Control.ModifierKeys & Keys.Control) != 0)
                    Keyboard.Ctrl_is_pressed = true;
                else Keyboard.Ctrl_is_pressed = false;

                if ((Control.ModifierKeys & Keys.Alt) != 0)
                    Keyboard.Alt_is_pressed = true;
                else Keyboard.Alt_is_pressed = false;

                Keyboard.DoAction();

                if (Mouse.MouseIsActive)
                {
                    Mouse.DoMouse(Control.MousePosition);
                    Cursor.Position = Mouse.ReturnToCenter();
                }

                Scene.SS.env.player.coords.Player_recalculate_extra_positions();
            }
            //  Draw the scene.
            scene.Draw(openGLControl.OpenGL);

            //if (StaticSettings.S.GhostCubeBool)
            for (int i = GraphicalOverlap.Start_Shift; i <= GraphicalOverlap.End_Max; i++)
                GraphicalOverlap.draw_GO_square(openGLControl, 20 + i*30, 20,20, GeneralProgrammingStuff.ColorSwitch(i-GraphicalOverlap.Start_Shift));

            CF.Ultimate_DrawText(20 +GraphicalOverlap.GO_interface_item_choice*30, 50, System.Drawing.Color.White, 14, GraphicalOverlap.GO_interface_item_choice.ToString(), 3.0f);
            
            CF.Ultimate_DrawText(openGLControl.Width/2 - FontSizeTargetPointer/4, openGLControl.Height/2- FontSizeTargetPointer/2, System.Drawing.Color.White, FontSizeTargetPointer, "+",4.0f);

            if (StaticSettings.S.HelpInfoForPlayer)
            {
                label1_InfoTable.Visible = false;
                int place = 1;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * 1, System.Drawing.Color.Gold, 10, "|F| - Fly Mod: " + StaticSettings.S.FlyMod, 2.0f);
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Gold, 10, "|M| - Music: " + Music.wmp_player.PlayerTime() + " " + Music.wmp_player.PlayerSongName(), 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Cyan, 10, "|N| - Next Song: ", 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.LimeGreen, 10, "|P| - Phantom Mod: " + StaticSettings.S.PhantomMod, 2.0f);  place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Yellow, 10, "|Y| - Sun Disabled: " + StaticSettings.S.SunStatus.x, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.WhiteSmoke, 10, "|T| - Time Speed: " + Time.time.Time_Speed, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.SandyBrown, 10, "|F| - Falling Cube: " + StaticSettings.S.FallingCube, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Red, 10, "|E| - Exploding Mod: " + StaticSettings.S.ExplosionMod, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "Range of View: " + StaticSettings.S.RangeOfView, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Time: " + Time.time.GetDayTime(), 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Ctrl: " + Keyboard.Ctrl_is_pressed, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Alt: " + Keyboard.Alt_is_pressed, 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Time: " + Time.time.GetTotalSeconds(), 2.0f); place++;
            }
            else
            {
                label1_InfoTable.Visible = true;
                label1_InfoTable.Text = info_table();
            }
        }
        public string info_table()
        {
            return "PrecisePosition:" + Scene.SS.env.player.coords.Player_precise_position.ToString() + "\r\n"
                + "ChunkPosition:" + Scene.SS.env.player.coords.Player_chunk_position.ToString() + "\r\n"
                + "CubicalPosition:" + Scene.SS.env.player.coords.Player_cubical_position.ToString() + "\r\n"
             + "PreciseLook:" + Scene.SS.env.player.coords.Player_precise_lookforcube.ToString() + "\r\n"
             + "ChunkLook:" + Scene.SS.env.player.coords.Player_chunk_lookforcube.ToString() + "\r\n"
             + "CubicalLook:" + Scene.SS.env.player.coords.Player_cubical_lookforcube.ToString() + "\r\n"
            + "Step_multiplier:" + Keyboard.step_multiplier.ToString("F2") + "\r\n"
            + "Step_multiplier:" + Scene.SS.env.player.coords.Player_precise_stepback.ToString();
        }
        static int FontSizeTargetPointer = 40;
        CalculatorFont CF;
        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  TODO: Set the projection matrix here.

            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);

            //  Load the identity.
            gl.LoadIdentity();

            //  Create a perspective transformation.
            //gl.Perspective(60.0f, (double)openGLControl.Size.Width / (double)openGLControl.Size.Height, 0.1f, 10000.0);

            //gl.LookAt(0, 0, 5, 0, 0, 0, 0, -1, 0);
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, -1, 0);
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);
            
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            //Cursor.Position.X - Location.X - openGLControl.Location.X - 8, -Cursor.Position.Y + Location.Y + Size.Height + openGLControl.Location.Y - 30
            SetMouseCenter();
            Cursor.Position = Mouse.ReturnToCenter();
        }
        public void SetMouseCenter()
        {
            Mouse.SetCenterCursor(new System.Drawing.Point(this.Location.X - openGLControl.Location.X - 28 + openGLControl.Width / 2, this.Location.Y + Size.Height - openGLControl.Height / 2));
        }
        float rotation = 0.0f;
        private void defaulttriangle()
        {
            
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Clear the color and depth buffer.
            //gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

            //  Load the identity matrix.
            gl.LoadIdentity();

            //  Rotate around the Y axis.
            gl.Rotate(rotation, 0.0f, 1.0f, 0.0f);

            //  Draw a coloured pyramid.
            gl.Begin(OpenGL.GL_TRIANGLES);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(1.0f, -1.0f, 1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(1.0f, 0.0f, 0.0f);
            gl.Vertex(0.0f, 1.0f, 0.0f);
            gl.Color(0.0f, 0.0f, 1.0f);
            gl.Vertex(-1.0f, -1.0f, -1.0f);
            gl.Color(0.0f, 1.0f, 0.0f);
            gl.Vertex(-1.0f, -1.0f, 1.0f);
            gl.End();

            //  Nudge the rotation.
            rotation += 3.0f;
        }
        /// <summary>
        /// The scene that we are rendering.
        /// </summary>
        private readonly Scene scene = new Scene();


        private void openGLControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
        public static bool AnyKeyPressed = false;
        private void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (!Scene.ShadersInitializated) Application.Exit();

            if (e.Control) Keyboard.Ctrl_RUN_IS_ACTIVATED = 4;

            char item = ((char)e.KeyValue).ToString().ToLower()[0];
            if (!Keyboard.KeysActive.Contains(item))
                Keyboard.KeysActive.Add(item);


            if (!AnyKeyPressed) Keyboard.Wrapped_SINGLE_KeyPressed_Reaction(item);
            AnyKeyPressed = true;
        }

        private void openGLControl_KeyUp(object sender, KeyEventArgs e)
        {
            char item = ((char)e.KeyValue).ToString().ToLower()[0];
            Keyboard.KeysActive.Remove(item);

            if (Keyboard.KeysActive.Count == 0 || !e.Control)
            {
                Keyboard.Ctrl_RUN_IS_ACTIVATED = 1;
            }
            AnyKeyPressed = false;
            if (item == 't')
            {
                Music.wav_player.SaySoundEffect("Blorp");
                Time.time.Time_Speed = 1.0;
            }
        }

        private void openGLControl_MouseEnter(object sender, EventArgs e)
        {
            Mouse.MouseIsActive = true;
            Mouse.SetOldPisition(Control.MousePosition);
            Cursor.Hide();
        }

        private void openGLControl_MouseLeave(object sender, EventArgs e)
        {
            Mouse.MouseIsActive = false;
            Cursor.Show();
        }

        private void openGLControl_Click(object sender, EventArgs e)
        {
        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (StaticSettings.S.GhostCube_Add_in_Data_For_Draw)
                {
                    if (e.Button.ToString() == "Right")
                    {
                        Music.wav_player.SaySoundEffect("BlockPlacement");

                        
                        int y = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
                        bool found = false;
                        if (StaticSettings.S.FallingCube)
                        {
                            for (; y >= 0; y--)
                                if (!Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled
                                || Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsTakenForExplosion)
                                { found = true; }
                                else break;

                            if (found)
                                y++;
                            else y = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
                        }

                        {
                            float x1 = 0, y1 = 0, z1 = 0;
                            ShaderedScene.CalculateFromMaptoGraphical(Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].xz, Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].xyz, ref x1, ref y1, ref z1);
                            Explosion.exp.SetBombLocation(x1, y1, z1);
                        }
                        if (!StaticSettings.S.ExplosionMod)
                        {
                            Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled = true;

                            Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsTakenForExplosion = false;

                            Scene.SS.env.cub_mem.world.World_as_Whole
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                                [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                                [y]
                                [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].color = GraphicalOverlap.GO_color;
                        }
                        Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].FallingFromHeight = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;

                        Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].FallingStartingTime = (float)Time.time.GetTotalSeconds();

                        Explosion.exp.StartingTime = (float)Time.time.GetTotalSeconds();

                        Explosion.exp.ExplosionCenter = new CubicalMemory.Chunk_and_Cube_link(Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z],
                            Scene.SS.env.cub_mem.world.World_as_Whole
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                        [y]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.z]
                            );

                        DataForDraw_TemporalList.TemporalList.Add(new CubicalMemory.Chunk_and_Cube_link(Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z],
                            Scene.SS.env.cub_mem.world.World_as_Whole
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                        [y]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.z]
                            ));

                        scene.Reloader_TemporalList();

                        float TookTime = (float)Math.Sqrt((Scene.SS.env.player.coords.Player_cubical_lookforcube.y - y) * 2 / 9.8);

                        if (Time.time.GetTotalSeconds() + TookTime > Time.time.TimeWaitForFallingCubes)
                        Time.time.TimeWaitForFallingCubes = Time.time.GetTotalSeconds() + TookTime;
                    }
                    else if (e.Button.ToString() == "Left")
                    {
                        Music.wav_player.SaySoundEffect("BlockRemovement");
                        Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled = false;
                        Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].color = Scene.SS.env.cub_mem.world.World_as_Whole
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                            [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].color_default;

                        StaticSettings.S.RealoderCauseOfBuildingBlocks = true;
                    }
                }
            }
            catch (Exception Ouch)
            {
                Console.WriteLine(Ouch.Message);
            }
        }

        private void FormModernOpenGLSample_LocationChanged(object sender, EventArgs e)
        {
            SetMouseCenter();
        }

        private void openGLControl_MouseScroller(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                GraphicalOverlap.GO_interface_item_choice++;
            else
                GraphicalOverlap.GO_interface_item_choice--;

            GraphicalOverlap.Graphical_OverLap_Logic(GraphicalOverlap.GO_interface_item_choice);
        }
        private void FormModernOpenGLSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            scene.Scene_Form_Closing(openGLControl.OpenGL);
            Music.wmp_player.Player.close();
        }
    }
}
