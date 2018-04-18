using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
namespace TheDiplomWork
{
    /// <summary>
    /// OpenGL User Interface
    /// </summary>
    class GraphicalOverlap
    {
        public static int size = 20;
        public static System.Drawing.Color GO_color = System.Drawing.Color.Aqua;
        public static int GO_interface_item_choice = 0;
        public static bool Rebuilding_is_required_cause_of_GO_color_changed_color = false;
        public static void draw_GO_square(OpenGLControl openGLControl, int x_from, int y_from, System.Drawing.Color _color)
        {
            //x_from -= mouse.ShiftedPosition.x;
            //y_from += mouse.ShiftedPosition.y;
            int x_to = x_from;
            int y_to = y_from;

            x_to += size;
            y_to += size;

            OpenGL gl = openGLControl.OpenGL;
            //  Clear the color and depth buffer.
            //  Load the identity matrix.
            gl.LoadIdentity();

            gl.Color((float)_color.R/255, (float)_color.G/255, (float)_color.B/255, 1.0f); //Must have, weirdness!
            gl.Begin(OpenGL.GL_QUADS);

            Single Line_Height = -0.4f;

            gl.Vertex(x_to, y_to, Line_Height);
            gl.Vertex(x_to, y_from, Line_Height);
            gl.Vertex(x_from, y_from, Line_Height);
            gl.Vertex(x_from, y_to, Line_Height);


            gl.End();
        }
        class GO_initer
        {
            public GO_initer()
            {
                GO_color = System.Drawing.Color.Aqua;
            }
        }
        static GO_initer go_initer = new GO_initer();
    }
}
