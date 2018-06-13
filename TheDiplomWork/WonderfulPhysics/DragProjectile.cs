using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public class DragProjectile : SimpleProjectile
    {
        private double mass;
        private double area;
        private double density;
        private double Cd;

        public DragProjectile(double x0, double y0, double z0,
    double vx0, double vy0, double vz0, double time,
    double mass, double area, double density, double Cd)
                : base(x0, y0, z0, vx0, vy0, vz0, time)
        {
            this.mass = mass;
            this.area = area;
            this.density = density;
            this.Cd = Cd;
        }
        protected void Reignite_drag(double mass, double area, double density, double Cd)
        {
            this.mass = mass;
            this.area = area;
            this.density = density;
            this.Cd = Cd;
        }
        public double getMass()
        {
            return mass;
        }
        public double getArea()
        {
            return area;
        }
        public double getDensity()
        {
            return density;
        }
        public double getCd()
        {
            return Cd;
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

            double v = Math.Sqrt(vx * vx + vy * vy + vz * vz) + 1.0e-8;

            double Fd = 0.5 * density * area * Cd * v * v;

            dQ[0] = -ds * Fd * vx / (mass * v);
            dQ[1] = ds * vx;
            dQ[2] = -ds * Fd * vy / (mass * v);
            dQ[3] = ds * vy;
            dQ[4] = ds * (G - Fd * vz / (mass * v));
            dQ[5] = ds * vz;
            return dQ;
        }
    }
}
