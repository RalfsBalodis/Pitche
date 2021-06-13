using System;
using System.Collections.Generic;
using Equations;

namespace ConstructionFoundationSideLenghtProcesor
{
    class Program
    {
        static void Main(string[] args)
        {
            /******** (1) Inputs ********/
           
            List<DataSet> DataSets = new List<DataSet>();
            DataSets.Add(new DataSet(0.5, 1.1, 25, 20, 10, 10, 10, 10, 100, 1.5, 1 / 6D));//Izejas dati no python koda piemēra!

            DataSets.Add(new DataSet(0,0,0,0,0,0,0,0,0,0,0));
            DataSets.Add(new DataSet(12,3,4,5,6,7,8,9,10,11,12));
            DataSets.Add(new DataSet(13,21,33,44,56,67,87,98,09,100,151));
            DataSets.Add(new DataSet(1,1,1,1,1,0,0,1,1,1,1));
            DataSets.Add(new DataSet(3,1.5,25,20,10000,10000,10000,15000,20000,1.5,1/6D));


            double k;//Malu attiecību koeficents
            double M1;//max Moments
            double M2;//min Moments
            double L1;//Garākā mala
            double L2;//īsākā mala

            /******** (2) Maksimālā un minimālā rezultējošā lieces momenta noteikšana ********/

            foreach (var D in DataSets)
            {
                if ((D.Mx + D.d * D.Hy) >= (D.My + D.d * D.Hx))
                {
                    M1 = D.Mx +D.d *D.Hy;
                    M2 = D.My +D.d * D.Hx;
                }
                else
                {
                    M1 = D.My + D.d * D.Hx;
                    M2 = D.Mx + D.d * D.Hy;
                }

                /******** (3) Pamata malu garumu attiecibas aprēķins ********/
                if (M1 == 0)
                {
                    Console.WriteLine("Pamatu malu attiecība nevar būt 0! \n M1 = 0");
                    Console.WriteLine("****************************************************************");

                    continue;
                }
                else if (M2 == 0)
                {
                    Console.WriteLine("Rezultejošais lieces moments M2 bedrīkst būt 0!");
                    Console.WriteLine("****************************************************************");

                    continue;
                }
                else
                {
                    k = M1 / M2;
                }
                /******** (4-1) Ja k<=kmax (1.5) ********/
                if (k <= D.kmax)

                /******** (4-1-1) Pamata garākās malas (L1) aprēķins ********/

                {
                    double a1 = (-D.d / k * D.Gc) - (D.z / k * D.Gs);
                    double c1 = -D.N;
                    double d1 = (1 / D.emax) * M1;

                    L1 = CubicEquations.GetRealRootReduced(a1, c1, d1);
                    if (Double.IsNaN(L1))
                    {
                        Console.WriteLine("Malas garumu L1 nevar aprēķināt!");
                        Console.WriteLine("****************************************************************");

                        continue;
                    }
                    /******** (4-1-2) Pamata isakas malas (L2) aprekins ********/

                    double a2 = (-D.d * k * D.Gc) - (D.z * k * D.Gs);
                    double c2 = -D.N;
                    double d2 = 1 / D.emax * (M2);

                    L2 = CubicEquations.GetRealRootReduced(a2, c2, d2);

                    if (Double.IsNaN(L2))
                    {
                        Console.WriteLine("Malas garumu L2 nevar aprēķināt!");
                        Console.WriteLine("****************************************************************");

                        continue;
                    }
                }

                /******** (4-1) Ja k>kmax (1.5) ********/
                /******** 4-2-1) Aprekina L2cr - fiktivo pamata isakas malas garumu, pie kuras izpildas M2 ekscentritates prasiba, pie pamatu malu attiecibas kmax ********/
                else
                {
                    double a2 = (-D.d / D.kmax * D.Gc) - (D.z / D.kmax * D.Gs);
                    double c2 = -D.N;
                    double d2 = (1 / D.emax) * M2;

                    double L2cr = CubicEquations.GetRealRootReduced(a2, c2, d2);
                    if (Double.IsNaN(L2cr))
                    {
                        Console.WriteLine("Fiktīvo malas garumu L2cr nevar aprēķināt!");
                        Console.WriteLine("****************************************************************");

                        continue;
                    }

                    /******** (4-2-2) Aprekina L1cr - fiktivo pamata garakas malas garumu, zinot L1cr un pamata malu attiecibu k=kmax ********/
                    double L1cr = L2cr * D.kmax;

                    /******** (4-2-3) Aprekina M1cr - Kritisko lieces momentu, kuru spej uznemt kritiskais pamata garakas malas garums L1cr, pie noteiktas maksimalas ekscentritates emax ********/
                    double M1cr = Math.Abs(((-D.d / k * D.Gc) - (D.z / k * D.Gs * Math.Pow(L1cr, 3)) - (D.N * L1cr)) / (1 / D.emax));

                    /******** (4-2-4) Aprekina reala lieces momenta M1 un M1cr attiecibu kM ********/

                    double km = M1 / M1cr;

                    /******** (4-2-5) Aprekina pamata malu L1cr un L2cr palielinajuma koeficientu (kL),lai izpilditos reala lieces momenta ekscentritates prasiba (emax),
                     * pie reala un fiktiva lieces momentu attiecibas koeficienta kM ********/
                    double a1 = (Math.Pow(L1cr, 3) * (-D.d / D.kmax * D.Gc)) - ((D.z / D.kmax) * D.Gs);
                    double c1 = -D.N;
                    double d1 = (1 / D.emax) * M1 * km;

                    double kL = CubicEquations.GetRealRootReduced(a1, c1, d1);
                    if (Double.IsNaN(kL))
                    {
                        Console.WriteLine("Palielinājuma koeficentu kl nevar aprēķināt!");
                        Console.WriteLine("****************************************************************");

                        continue;
                    }

                    /******** (4-2-6) Aprekina pamata malas, nemot vera pamata malu palielinajuma koeficientu kL (scale factor) ********/

                    L1 = L1cr * kL;
                    L2 = L2cr * kL;
                }
                /******** (5) Output ********/
                Console.WriteLine($"L1 = {Math.Round(L1, 2)}, Pamta pēdas garākās malas garums (m).");
                Console.WriteLine($"L2 = {Math.Round(L2, 2)}, Pamta pēdas īsākās malas garums (m).");
                Console.WriteLine($"k = {Math.Round(L1 / L2, 2)}, Pamta malu garumu attiecība.");
                Console.WriteLine("****************************************************************");
            }
        }
    }
}
