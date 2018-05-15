﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using GlmNet;
namespace TheDiplomWork
{
    class Projectile : GeneralProgrammingStuff
    {
        public static Localized_projectile jp = new Localized_projectile();
        public class Localized_projectile
        {
            public bool Launched = false;

            class Starting_Data
            {
                public vec3 starting_velocity = new vec3(0, 0, 0);
            }
            Starting_Data sd = new Starting_Data();

            public bool RotatingStartingVelocity = false;

            float StartingVelocity = 20f;

            Point3D Player_rotational_view_OLD = new Point3D(0, 0, 0);
            Point3D Player_rotational_view_Result = new Point3D(0, 0, 0);
            public void SetStartingPlayerView()
            {
                Player_rotational_view_OLD.x = Scene.SS.env.player.coords.Player_rotational_view.x;
                Player_rotational_view_OLD.y = Scene.SS.env.player.coords.Player_rotational_view.y;
                Player_rotational_view_OLD.z = Scene.SS.env.player.coords.Player_rotational_view.z;
            }
            public void SetEndingPlayerView()
            {
                Player_rotational_view_Result.x = Scene.SS.env.player.coords.Player_rotational_view.x - Player_rotational_view_OLD.x;
                Player_rotational_view_Result.y = Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.y;
                Player_rotational_view_Result.z = Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z;
            }
            public void ActivateStartingData()
            {
                if (!Launched)
                {
                    sd.starting_velocity.x = 0;
                    sd.starting_velocity.y = StartingVelocity;
                    sd.starting_velocity.z = 0;

                    sd.starting_velocity = Rotate(sd.starting_velocity, PlayerAngles());

                    StartingTimeToFly = Time.time.GetGameTotalSeconds();
                }
            }

            vec3 Rotate(vec3 SomethingToRotate, vec3 ang)
            {
                mat3 RotateX = new mat3(new vec3(1, 0, 0),
                new vec3(0, (float)Math.Cos(ang.x), -(float)Math.Sin(ang.x)),
                new vec3(0, (float)Math.Sin(ang.x), (float)Math.Cos(ang.x)));

                mat3 RotateY = new mat3(new vec3((float)Math.Cos(ang.y), 0, (float)Math.Sin(ang.y)),
                new vec3(0, 1, 0),
                new vec3(-(float)Math.Sin(ang.y), 0, (float)Math.Cos(ang.y)));

                mat3 RotateZ = new mat3(new vec3((float)Math.Cos(ang.z), -(float)Math.Sin(ang.z), 0),
                new vec3((float)Math.Sin(ang.z), (float)Math.Cos(ang.z), 0),
                new vec3(0, 0, 1));
                mat3 Rotator = RotateX * RotateY * RotateZ;

                vec3 temp = Rotator * SomethingToRotate;
                return temp;
            }

            CubicalMemory.Cube hpos1 { get; }
            CubicalMemory.Cube hpos2 { get; }
            CubicalMemory.Cube center { get; }

            vec3 os_x = new vec3(1, 0, 0);
            vec3 os_y = new vec3(1, 0, 0);

            vec3 angles = new vec3(0, 0, 0);
            vec3 PlayerAngles()
            {
                if (RotatingStartingVelocity)
                {
                    angles.x = Scene.SS.env.player.coords.Player_rotational_view.x - Player_rotational_view_OLD.x;
                    angles.y = Scene.SS.env.player.coords.Player_rotational_view.y - Player_rotational_view_OLD.y;
                    angles.z = Scene.SS.env.player.coords.Player_rotational_view.z - Player_rotational_view_OLD.z;
                }
                else
                {
                    angles.x = Player_rotational_view_Result.x;
                    angles.y = Player_rotational_view_Result.y;
                    angles.z = Player_rotational_view_Result.z;
                }

                return angles;
            }
            vec3 Angles(bool deactivated)
            {
                vec3 nv = glm.normalize(Velocity());

                angles.x = (float)Math.Atan(nv.y / nv.x);
                angles.y = (float)Math.Atan(Math.Sqrt(nv.x * nv.x + nv.y + nv.y) / nv.z);
                angles.z = 0;

                return angles;
            }

            float step = 0.01f;
            vec3 Velocity()
            {
                return (CoordinatesAtTime(TimeOfFlight() + step) - CoordinatesAtTime(TimeOfFlight()))/step;
            }

            double StartingTimeToFly = 0;
            float TimeOfFlight()
            {
                if (StartingTimeToFly < Time.time.GetGameTotalSeconds()) return 0;
                else return (float)(Time.time.GetGameTotalSeconds() - StartingTimeToFly);
            }

            vec3 coordinates = new vec3(0, 0, 0);
            vec3 Coordinates()
            {
                return CoordinatesAtTime(TimeOfFlight());
            }
            vec3 CoordinatesAtTime(float time)
            {
                coordinates.x = sd.starting_velocity.x* time;
                coordinates.y = sd.starting_velocity.y * time - (9.8f / 2f) * time * time;
                coordinates.z = sd.starting_velocity.z * time;

                return coordinates;
            }
            mat3 GetProjectileMatrix()
            {
                //Первый вектор. Координаты
                //Второй вектор.... Угол поворота
                //Третий вектор. Системные данные и просто доп.

                return new mat3(Coordinates(),
                    PlayerAngles(),
                    new vec3(0, 0, 0)
                    );
            }
        }
    }
}
