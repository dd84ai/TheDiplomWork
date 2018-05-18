using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    class Mouse
    {
        public static bool MouseIsActive = false;

        static System.Drawing.Point OldPosition = new System.Drawing.Point(0, 0);
        static System.Drawing.Point CenterCursor = new System.Drawing.Point(0, 0);
        public static void SetOldPisition(System.Drawing.Point Position)
        {
            OldPosition = Position;
        }
        public static void SetCenterCursor(System.Drawing.Point Position)
        {
            CenterCursor = Position;
        }
        static float MouseRotateDecreaser = 0.004f;
        public static void DoMouse(System.Drawing.Point Position, string button)
        {
            Scene.SS.env.player.coords.Player_rotational_view.x += (float)(Position.X - OldPosition.X) * MouseRotateDecreaser;
            if (Scene.SS.env.player.coords.Player_rotational_view.y - (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser > -3.14f/2.0f
                && Scene.SS.env.player.coords.Player_rotational_view.y - (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser < 3.14f / 2.0f)
            Scene.SS.env.player.coords.Player_rotational_view.y -= (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser;

            //if (button == "Middle")
            //Projectile.jp.sd.ChangeStartingVelocity((float)(Position.Y - OldPosition.Y));
            if (button == "Right" && GraphicalOverlap.GO_interface_item_choice == 0 && Projectile.jp.Loaded && !Projectile.jp.Launched)
            {
                Projectile.jp.SetStartingPlayerView();
                //Projectile.jp.SetEndingPlayerView();
            }

            OldPosition = CenterCursor;
        }
        public static System.Drawing.Point ReturnToCenter()
        {
            return CenterCursor;
        }
    }
}
