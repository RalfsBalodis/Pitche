using System;

namespace Equations
{
    public static class CubicEquations
    {

        /******** ja a!=0 un b = 0, reducēta 3. pakāpes vienādojuma īstās saknes aprēķina formula  ********/
        /******** pieņemot, ka input dati doti reāli, vienādojumam ir viena reāla sakne ********/
        public static double GetRealRootReduced(double a, double c, double d)
        {
            return Math.Cbrt(-d / a / 2 + Math.Sqrt(Math.Pow(d / a / 2, 2) + (Math.Pow(c / a / 3, 3)))) +
                   Math.Cbrt(-d / a / 2 - Math.Sqrt(Math.Pow(d / a / 2, 2) + (Math.Pow(c / a / 3, 3))));
        }
    }
}
