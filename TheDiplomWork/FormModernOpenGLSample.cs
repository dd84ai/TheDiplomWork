﻿using System;
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
            openGL_Draw_Wrapped();
            //defaulttriangle();
        }
        public void OpenGL_Draw_ReWrapped()
        {
            openGL_Draw_Wrapped();
        }
        public void openGL_Draw_Wrapped()
        {
            if ((Control.ModifierKeys & Keys.Shift) != 0)
                Keyboard.DoSpecificAction('z');

            Keyboard.DoAction();

            if (this.ActiveControl.Focused && Mouse.MouseIsActive)
            {
                Mouse.DoMouse(Control.MousePosition);
                Cursor.Position = Mouse.ReturnToCenter();
            }

            Scene.SS.env.player.coords.Player_recalculate_extra_positions();

            //  Draw the scene.
            scene.Draw(openGLControl.OpenGL);

            label1_InfoTable.Text =
                "PrecisePosition:" + Scene.SS.env.player.coords.Player_precise_position.ToString() + "\r\n"
                + "ChunkPosition:" + Scene.SS.env.player.coords.Player_chunk_position.ToString() + "\r\n"
                + "CubicalPosition:" + Scene.SS.env.player.coords.Player_cubical_position.ToString() + "\r\n"
             + "PreciseLook:" + Scene.SS.env.player.coords.Player_precise_lookforcube.ToString() + "\r\n"
             + "ChunkLook:" + Scene.SS.env.player.coords.Player_chunk_lookforcube.ToString() + "\r\n"
             + "CubicalLook:" + Scene.SS.env.player.coords.Player_cubical_lookforcube.ToString() + "\r\n"
            + "Step_multiplier:" + Keyboard.step_multiplier.ToString("F2");
        }
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
            gl.Perspective(60.0f, (double)openGLControl.Size.Width / (double)openGLControl.Size.Height, 0.1f, 10000.0);

            gl.LookAt(0, 0, 5, 0, 0, 0, 0, -1, 0);
            //gl.LookAt(-5, 5, -5, 0, 0, 0, 0, -1, 0);
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            //Cursor.Position.X - Location.X - openGLControl.Location.X - 8, -Cursor.Position.Y + Location.Y + Size.Height + openGLControl.Location.Y - 30
            SetMouseCenter();
            label_CursorPlus.Location = Mouse.ReturnToCenter();
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

        private void openGLControl_KeyDown(object sender, KeyEventArgs e)
        {
            char item = ((char)e.KeyValue).ToString().ToLower()[0];
            if (!Keyboard.KeysActive.Contains(item))
                Keyboard.KeysActive.Add(item);

            Keyboard.Wrapped_SINGLE_KeyPressed_Reaction(item);
        }

        private void openGLControl_KeyUp(object sender, KeyEventArgs e)
        {
            char item = ((char)e.KeyValue).ToString().ToLower()[0];
            Keyboard.KeysActive.Remove(item);
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
                if (e.Button.ToString() == "Right")
                {
                    Scene.SS.env.cub_mem.world.World_as_Whole
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled = true;
                }
                else if (e.Button.ToString() == "Left")
                {
                    Scene.SS.env.cub_mem.world.World_as_Whole
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].IsFilled = false;
                }
                Scene.SS.env.cub_mem.world.World_as_Whole
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_chunk_lookforcube.z].cubes
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.x]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.y]
                        [Scene.SS.env.player.coords.Player_cubical_lookforcube.z].Changed = true;
                StaticSettings.S.RealoderCauseOfBuildingBlocks = true;
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
    }
}
