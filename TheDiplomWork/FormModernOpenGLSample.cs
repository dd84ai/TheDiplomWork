using System;
using System.Windows.Forms;
using SharpGL;
using GlmNet;
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
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //SetStyle(ControlStyles.ResizeRedraw, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            //this.BackColor = System.Drawing.Color.Transparent;
            //this.TransparencyKey = System.Drawing.Color.Transparent;
            //table_Menu_main.Parent = openGLControl;

            //this.Focus();
            //openGLControl.Focus();
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
            {
                openGL_Draw_Wrapped();
                if (table_Menu_main.Visible)
                    CF.Ultimate_DrawText(20, openGLControl.Height - 80, System.Drawing.Color.Red, 40, "PAUSE", 2.0f, openGLControl);
                else
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20, System.Drawing.Color.Red, 12, "Press Esc for opening menu", 2.0f, openGLControl);

            }
            else
            {
                Application.Exit();

                //  Clear the scene.
                openGLControl.OpenGL.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT | OpenGL.GL_STENCIL_BUFFER_BIT);

                CF.Ultimate_DrawText(20, openGLControl.Height / 2 + 60, System.Drawing.Color.Red, 14, "WARNING!", 2.0f, openGLControl);

                //Center = OpenGLcontrol.Width/2 - (string.Length)*step/2
                CF.Ultimate_DrawText(20, openGLControl.Height / 2 + 20, System.Drawing.Color.Red, 14, scene.ShadersWereNotInitializatedMessage, 2.0f, openGLControl);
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
                {
                    
                }
                else
                {
                    Keyboard.Alt_is_pressed = false;
                }

                Keyboard.DoAction();

                if (Mouse.MouseIsActive && (StaticAccess.FMOS!=null && !StaticAccess.FMOS.table_Menu_main.Visible))
                {
                    Mouse.DoMouse(Control.MousePosition, Control.MouseButtons.ToString());
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
                int place = 1;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * 1, System.Drawing.Color.Gold, 10, "|F| - Fly Mod: " + StaticSettings.S.FlyMod, 2.0f);
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Gold, 10, "|M| - Music: " + Music.wmp_player.PlayerTime() + " " + Music.wmp_player.PlayerSongName(), 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Cyan, 10, "|N| - Next Song: ", 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.LimeGreen, 10, "|P| - Phantom Mod: " + StaticSettings.S.PhantomMod, 2.0f);  place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Yellow, 10, "|Y| - Sun Disabled: " + StaticSettings.S.SunStatus.x, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.WhiteSmoke, 10, "|T| - Time Speed: " + Time.time.Time_Speed, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.SandyBrown, 10, "|F| - Falling Cube: " + StaticSettings.S.FallingCube, 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Red, 10, "|E| - Exploding Mod: " + StaticSettings.S.ExplosionMod, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Purple, 10, "|J| - Rotational Mod: " + Projectile.jp.RotatingStartingVelocity, 2.0f); place++;

                if (StaticSettings.S.ShowPlayerPosition)
                {
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "PrecisePosition:" + Scene.SS.env.player.coords.Player_precise_position.ToString(), 2.0f); place++;
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "ChunkPosition:" + Scene.SS.env.player.coords.Player_chunk_position.ToString(), 2.0f); place++;
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "CubicalPosition:" + Scene.SS.env.player.coords.Player_cubical_position.ToString(), 2.0f); place++;
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "PreciseLook:" + Scene.SS.env.player.coords.Player_precise_lookforcube.ToString(), 2.0f); place++;
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "ChunkLook:" + Scene.SS.env.player.coords.Player_chunk_lookforcube.ToString(), 2.0f); place++;
                    CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "CubicalLook:" + Scene.SS.env.player.coords.Player_cubical_lookforcube.ToString(), 2.0f); place++;
                }

                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Blue, 10, "Range of View: " + StaticSettings.S.RangeOfView, 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Time: " + Time.time.GetDayTime(), 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "Starting Velocity: " + vec3things.ToString(Projectile.jp.sd.Get_Starting_velocity()), 2.0f); place++;
                CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "Total Velocity: " + Projectile.jp.sd.StartingVelocity.ToString("G2"), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "X: " + StaticCompass.C.Xangle.ToString("G2"), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "Z: " + StaticCompass.C.Zangle.ToString("G2"), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "Z: " + (StaticCompass.C.Xangle / Math.PI * 180.0f).ToString("G3"), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Crimson, 10, "Z: " + (StaticCompass.C.Zangle / Math.PI * 180.0f).ToString("G3"), 2.0f); place++;

                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Ctrl: " + Keyboard.Ctrl_is_pressed, 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Alt: " + Keyboard.Alt_is_pressed, 2.0f); place++;

                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "-------------------", 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Position Old: " + vec3things.ToString(Projectile.jp.Coordinates()), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Position New: " + vec3things.ToString(Projectile.jp.Coordinates(true)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Velocity Old: " + vec3things.ToString(Projectile.jp.Velocity()), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Velocity New: " + vec3things.ToString(Projectile.jp.Velocity(true)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 Old: " + vec3things.ToString(Projectile.jp.AbsoluteLocationAtTime(1.0f)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 New: " + vec3things.ToString(Projectile.jp.AbsoluteLocationAtTime(1.0f,true)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 Old: " + vec3things.ToString(Projectile.jp.CoordinatesAtTimeAtHighPart(1.0f)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 New: " + vec3things.ToString(Projectile.jp.CoordinatesAtTimeAtHighPart(1.0f, true)), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 Old: " + vec3things.ToString(Projectile.jp.AbsoluteEstimatedLocation()), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Predicted After 1 New: " + vec3things.ToString(Projectile.jp.AbsoluteEstimatedLocation(true)), 2.0f); place++;


                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Time.time.GetGameTotalSeconds: " + Time.time.GetGameTotalSeconds(), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "starting velocity: " + Projectile.jp.GetStringedVec3(Projectile.jp.sd.starting_velocity), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Projectile.jp.Coordinates() " + Projectile.jp.GetStringedVec3(Projectile.jp.Coordinates()), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Projectile.jp.Coordinates() " + Projectile.jp.GetStringedVec3(Projectile.jp.center), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Bomb Location " + Explosion.exp.Bomb_precise_position.ToString(), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Rocket Angle: " + Projectile.jp.AngleBetweenStartingAndCurrentVelocity(), 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Average Rebuilding Main Time: " + Time.time.AverageRebuildingTime, 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "TimeWhenSecondZero: " + Projectile.jp.TimeWhenSecondZero(), 2.0f); place++;

                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Explosion.exp.StartingTime: " + Explosion.exp.StartingTime, 2.0f); place++;
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Explosion.exp.StartingShift: " + Explosion.exp.StartingShiftForLoeading, 2.0f); place++;
                //
                //CF.Ultimate_DrawText(20, openGLControl.Height - 20 * place, System.Drawing.Color.Orange, 10, "Time: " + Time.time.GetTotalSeconds(), 2.0f); place++;
            }
        }
        static int FontSizeTargetPointer = 40;
        public CalculatorFont CF;
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

            if (((char)e.KeyValue == 'j' || (char)e.KeyValue == 'J')) Projectile.jp.SetStartingPlayerView();
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
            if (item == 't' || item == 'u')
            {
                Music.wav_player.SaySoundEffect("Blorp");
                Time.time.Time_Speed = 1.0;
            }

            Projectile.jp.SetEndingPlayerView();
        }

        private void openGLControl_MouseEnter(object sender, EventArgs e)
        {
            Mouse.MouseIsActive = true;
            Mouse.SetOldPisition(Control.MousePosition);
            // if (!table_Menu_main.Visible)
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
                    CubicalMemory.Cube cube = Cube_Selection.Decide_Position_To_Place_Cube(StaticSettings.S.FallingCube);


                    if (e.Button.ToString() == "Right")
                    {
                        if (GraphicalOverlap.GO_interface_item_choice == 0 && Projectile.jp.Loaded && !Projectile.jp.Launched)
                        {
                            Projectile.jp.SetStartingPlayerView();
                            //Projectile.jp.SetEndingPlayerView();
                        }
                        Music.wav_player.SaySoundEffect("BlockPlacement");

                        Explosion.exp.PlaceTheBombAt(cube);

                        //if (!StaticSettings.S.ExplosionMod)
                        //{
                            cube.IsFilled = true;
                            cube.IsTakenForExplosion = false;
                            cube.color = GraphicalOverlap.GO_color;
                        //}
                        //else
                        //{
                            //Explosion.exp.StartingTime = (float)Time.time.GetGameTotalSeconds();
                            //Explosion.exp.ExplosionCenter = cube;
                        //}

                        cube.FallingFromHeight = Scene.SS.env.player.coords.Player_cubical_lookforcube.y;
                        cube.FallingStartingTime = (float)Time.time.GetGameTotalSeconds();

                        DataForDraw_FreshlyPlacedCubes.TemporalList.Add(cube);
                        scene.Reloader_TemporalList();

                        float TookTime = (float)Math.Sqrt((Scene.SS.env.player.coords.Player_cubical_lookforcube.y - cube.xyz.y) * 2 / 9.8);

                        if (Time.time.GetGameTotalSeconds() + TookTime > Time.time.TimeWaitForFallingCubes)
                        Time.time.TimeWaitForFallingCubes = Time.time.GetGameTotalSeconds() + TookTime;
                    }
                    else if (e.Button.ToString() == "Left")
                    {
                        Music.wav_player.SaySoundEffect("BlockRemovement");
                        cube.IsFilled = false;
                        cube.color = cube.color_default;

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
            if (e.Delta > 0) GraphicalOverlap.GO_interface_item_choice++;
            else GraphicalOverlap.GO_interface_item_choice--;

            GraphicalOverlap.Graphical_OverLap_Logic(GraphicalOverlap.GO_interface_item_choice);
        }
        private void FormModernOpenGLSample_FormClosing(object sender, FormClosingEventArgs e)
        {
            scene.Scene_Form_Closing(openGLControl.OpenGL);
            Music.wmp_player.Player.close();
        }

        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            Projectile.jp.SetEndingPlayerView();
        }

        private void button_Return_Click(object sender, EventArgs e)
        {
            Keyboard.Wrapped_SINGLE_KeyPressed_Reaction((char)27);
            
        }

        private void button_SaveAndExit_Click(object sender, EventArgs e)
        {
            Keyboard.Wrapped_SINGLE_KeyPressed_Reaction('q');
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button_About_Click(object sender, EventArgs e)
        {
            if (StaticAccess.AB == null || (StaticAccess.AB != null && StaticAccess.AB.IsDisposed))
            {
                if (StaticAccess.AB == null ||
                    (StaticAccess.AB != null && StaticAccess.AB.IsDisposed))
                StaticAccess.AB = new AboutBox1();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (StaticAccess.FAQ_Controls == null || (StaticAccess.FAQ_Controls != null && StaticAccess.FAQ_Controls.IsDisposed))
            {
                if (StaticAccess.FAQ_Controls == null ||
                    (StaticAccess.FAQ_Controls != null && StaticAccess.FAQ_Controls.IsDisposed))
                    StaticAccess.FAQ_Controls = new Form_FAQ_Controls();
            }
        }

        private void button_Settings_Click(object sender, EventArgs e)
        {
            if (StaticAccess.Form_Settings == null || (StaticAccess.Form_Settings != null && StaticAccess.Form_Settings.IsDisposed))
            {
                if (StaticAccess.Form_Settings == null ||
                    (StaticAccess.Form_Settings != null && StaticAccess.Form_Settings.IsDisposed))
                    StaticAccess.Form_Settings = new Form_ProjectileSettings();
            }
        }
    }
}
