using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstructionFoundationSideLenghtProcesor
{
    class DataSet
    {
        public double d;//Pamata pedas biezums
        public double z;//Pamata pedas iedzilinajums (lidz pamata pedas augsai)
        public double Gc;//Pamata pedas materiala tilpumsvars
        public double Gs;//Virs pamata pedas esosas grunts tilpumsvars

        public double Mx;//Lieces moments ap X asi
        public double My;//Lieces moments ap Y asi
        public double Hx;//Horizontalais speks X ass virziena
        public double Hy;//Horizontalais speks Y ass virziena

        public double N;//Vertikalais speks

        public double kmax;//Pamata pedas maksimālā garuma un platuma attieciba
        public double emax;//Limitejosas slodzes maksimala ekscentritate

        public DataSet(double d, double z, double gc, double gs, double mx, double my, double hx, double hy, double n, double kmax, double emax)
        {
            this.d = d;
            this.z = z;
            Gc = gc;
            Gs = gs;
            Mx = mx;
            My = my;
            Hx = hx;
            Hy = hy;
            N = n;
            this.kmax = kmax;
            this.emax = emax;
        }
    }
}
