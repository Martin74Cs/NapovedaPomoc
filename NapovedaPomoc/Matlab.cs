using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
namespace NapovedaPomoc
{
    public class Matlab
    {
        static void Main(string[] args)
        {
            //Nuget
            //MathNet.Numerics
            //Jedná se o fnkcce matlab

            //https://numerics.mathdotnet.com/
            Console.WriteLine("Lineární soustava!");

          //lineární soustavy rovnic
            //https://numerics.mathdotnet.com/LinearEquations

          //definice matatice
            var A = Matrix<double>.Build.DenseOfArray(new double[,] {
            { 3, 2, -1 },
            { 2, -2, 10 },
            { -1, 0.5, -1 }
            });
          
          //definice vektoru
            var b = Vector<double>.Build.Dense(new double[] { 1, -2, 0 });

          //výpočet
            var x = A.Solve(b);

          //Vypiš výsledek
            Console.WriteLine("Vysledek {0}", x.ToString());
            Console.ReadKey();
        }
    }
}
