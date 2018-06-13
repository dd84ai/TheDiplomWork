using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class WindProjectile : DragProjectile
    {
        private double windVx;
        private double windVy;

        public WindProjectile(double x0, double y0, double z0,
    double vx0, double vy0, double vz0, double time,
    double mass, double area, double density, double Cd,
    double windVx, double windVy) : base(x0, y0, z0,
    vx0, vy0, vz0, time,
    mass, area, density, Cd)
        {

            this.windVx = windVx;
            this.windVy = windVy;
        }
        void Reignite_wind(double mass, double area, double density, double Cd,
double windVx, double windVy)
        {
            Reignite_drag(mass, area, density, Cd);
            this.windVx = windVx;
            this.windVy = windVy;
        }
        public void Reignite_wind_from_static_info()
        {
            Reignite_wind(
                Projectile.settings.mass,
                Projectile.settings.area,
                Projectile.settings.density,
                Projectile.settings.cd,
                Projectile.settings.windVx,
                Projectile.settings.windVy);
        }

        public double getWindVx()
        {
            return windVx;
        }
        public double getWindVy()
        {
            return windVy;
        }

        public new void updateLocationAndVelocity(double dt)
        {
            ODESolver.rungeKutta4(this, dt);
        }

        public override double[] getRightHandSide(double s, double[] q,
    double[] deltaQ, double ds,
    double qScale)
        {
            double[] dQ = new double[6];
            double[] newQ = new double[6];


            for (int i = 0; i < 6; ++i)
            {
                newQ[i] = q[i] + qScale * deltaQ[i];
            }

            double vx = newQ[0];
            double vy = newQ[2];
            double vz = newQ[4];

            double vax = vx - windVx;
            double vay = vy - windVy;
            double vaz = vz;

            double va = Math.Sqrt(vax * vax + vay * vay + vaz * vaz) + 1.0e-8;

            double Fd = 0.5 * getDensity() * getArea() * getCd() * va * va;

            dQ[0] = -ds * Fd * vax / (getMass() * va);
            dQ[1] = ds * vx;
            dQ[2] = -ds * Fd * vay / (getMass() * va);
            dQ[3] = ds * vy;
            dQ[4] = ds * (G - Fd * vaz / (getMass() * va));
            dQ[5] = ds * vz;
            return dQ;
        }
    }
}
