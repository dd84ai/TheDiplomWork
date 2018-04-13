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
            if ((Control.ModifierKeys & Keys.Shift) != 0)
                Keyboard.DoSpecificAction('z');

            Keyboard.DoAction();

            if (this.ActiveControl.Focused && Mouse.MouseIsActive)
            {
                Mouse.DoMouse(Control.MousePosition);
                Cursor.Position = Mouse.ReturnToCenter();
            }

            //  Draw the scene.
            scene.Draw(openGLControl.OpenGL);

            Scene.SS.env.player.coords.Player_recalculate_extra_positions();

            label1_InfoTable.Text =
                Scene.SS.env.player.coords.Player_precise_position.ToString() + "\r\n"
                + Scene.SS.env.player.coords.Player_chunk_position.ToString() + "\r\n"
                + Scene.SS.env.player.coords.Player_cubical_position.ToString() + "\r\n";
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

            gl.Perspective(60.0f, (double)openGLControl.Size.Width / (double)openGLControl.Size.Height, 0.0, 5000.0);

            gl.LookAt(0, 0, 0, 0, 0, 0, 0, -1, 0);
            //  Set the modelview matrix.
            gl.MatrixMode(OpenGL.GL_MODELVIEW);

            Mouse.SetCenterCursor(new System.Drawing.Point(openGLControl.Width / 2, openGLControl.Height / 2));

            label_CursorPlus.Location = Mouse.ReturnToCenter();
            Cursor.Position = Mouse.ReturnToCenter();
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
    }
}
