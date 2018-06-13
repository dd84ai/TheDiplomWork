using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheDiplomWork
{
    public abstract class ODE
    {

        private int numEqns;
        private double[] q;
        private double s;

        public ODE(int numEqns)
        {
            this.numEqns = numEqns;
            q = new double[numEqns];
        }

        public int getNumEqns()
        {
            return numEqns;
        }
        public double getS()
        {
            return s;
        }
        public double getQ(int index)
        {
            return q[index];
        }
        public double[] getAllQ()
        {
            return q;
        }

        public void setS(double value)
        {
            s = value;
            return;
        }
        public void setQ(double value, int index)
        {
            q[index] = value;
            return;
        }
        public abstract double[] getRightHandSide(double s,
        double[] q, double[] deltaQ, double ds, double qScale);
    }

    public class ODESolver
    {

        public static void rungeKutta4(ODE ode, double ds)
        {
            int j;
            int numEqns = ode.getNumEqns();
            double s;
            double[] q;
            double[] dq1 = new double[numEqns];
            double[] dq2 = new double[numEqns];
            double[] dq3 = new double[numEqns];
            double[] dq4 = new double[numEqns];

            s = ode.getS();
            q = ode.getAllQ();

            dq1 = ode.getRightHandSide(s, q, q, ds, 0.0);
            dq2 = ode.getRightHandSide(s + 0.5 * ds, q, dq1, ds, 0.5);
            dq3 = ode.getRightHandSide(s + 0.5 * ds, q, dq2, ds, 0.5);
            dq4 = ode.getRightHandSide(s + ds, q, dq3, ds, 1.0);

            ode.setS(s + ds);
            for (j = 0; j < numEqns; ++j)
            {
                q[j] = q[j] + (dq1[j] + 2.0 * dq2[j] + 2.0 * dq3[j] + dq4[j]) / 6.0;
                ode.setQ(q[j], j);
            }
            return;
        }
    }
}
