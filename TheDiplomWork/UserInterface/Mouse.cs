﻿using System;
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
        static float MouseRotateDecreaser = 0.01f;
        public static void DoMouse(System.Drawing.Point Position)
        {
            Scene.SS.env.player.coords.Player_rotational_view.x += (float)(Position.X - OldPosition.X) * MouseRotateDecreaser;
            if (Scene.SS.env.player.coords.Player_rotational_view.y - (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser > -3.14f/2.0f
                && Scene.SS.env.player.coords.Player_rotational_view.y - (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser < 3.14f / 2.0f)
            Scene.SS.env.player.coords.Player_rotational_view.y -= (float)(Position.Y - OldPosition.Y) * MouseRotateDecreaser;

            OldPosition = CenterCursor;
        }
        public static System.Drawing.Point ReturnToCenter()
        {
            return CenterCursor;
        }
    }
}