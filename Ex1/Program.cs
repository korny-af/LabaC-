
using System.Numerics;
//https://ru.wikihow.com/решать-кубические-уравнения


public class Ex1
{
    static void Main()
    {
       
        try
        {
            double[] result = Kardano(6, -5, -5, 4);
            //double[] result = Kardano(1, -3, 3, -1);
            if (result.Length > 0) Console.WriteLine("Result with kardano method:\nRoot 1: {0}", result[0]);
            if (result.Length > 1) Console.WriteLine("Root 2: {0}", result[1]);
            if (result.Length > 2) Console.WriteLine("Root 3: {0}", result[2]);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }


        double[] result2 = Cube(6, -5, -5, 4);
        // double[] result2 = SolveCube(1, -3, 3, -1);
        if (result2.Length > 0) Console.WriteLine("\n\nResult with alternative method:\nRoot 1: {0}", result2[0]);
        if (result2.Length > 1) Console.WriteLine("Root 2: {0}", result2[1]);
        if (result2.Length > 2) Console.WriteLine("Root 3: {0}", result2[2]);


        Console.ReadLine();
    }




    static double[] Kardano(double a, double b, double c, double d, double epsilon = 1e-14)
    {


        if (epsilon <= 0)
        {
            throw new ArgumentException("Epsilon must be lower or eq then 0");
        }


        var p = (3 * a * c - Math.Pow(b, 2)) / (3 * Math.Pow(a, 2));
        var q = (2 * Math.Pow(b, 3) - 9 * a * b * c + 27 * Math.Pow(a, 2) * d) / (27 * Math.Pow(a, 3));
        var Q = Math.Pow(p / 3.0, 3) + Math.Pow(q/2.0, 2);

        if (Math.Abs(Q) < epsilon)
        {
            if(Math.Abs(q) < epsilon)
            {
                double root123 = -b / (3 * a);
                return new double[]
                {
                    root123, root123, root123
                };
            }
            else
            {
                double u = (q<0?-1:1) * Math.Exp(Math.Log(Math.Abs(q) / 2) / 3);
                double root1 = -2 * u - b / (3 * a);
                double root23 = u - b / (3 * a);
                return new double[]
                {
                    root1, root23, root23
                };
            }
            
        }
        else if (Q < 0)
        {
            double fi;
            if (Math.Abs(q) < epsilon)
                 fi = Math.PI / 2; 
            else
            {
                fi = Math.Atan(Math.Sqrt(-Q) / (-q / 2));
                if(q>=0) fi+=Math.PI;
            }
            var root1 = 2 * Math.Sqrt(-p / 3) * Math.Cos(fi / 3) - b / (3 * a);
            var root2 = 2 * Math.Sqrt(-p / 3) * Math.Cos((fi + 2 * Math.PI) / 3) - b / (3 * a);
            var root3 = 2 * Math.Sqrt(-p / 3) * Math.Cos((fi + 4 * Math.PI) / 3) - b / (3 * a);
            return new double []
            {
                root1,
                root2,
                root3
            };
        }
        else if (Q > 0)
        {
            double u = Math.Exp(Math.Log(-q / 2 + Math.Sqrt(Q))/3);
            var part = u - p / (3 * u);
            var root1 = part - b / (3 * a);
            var root2 = -part / 2 - b / (3 * a);
            var root3 = Math.Sqrt(3) / 2 * (u + p / (3 * u));

            return new[]
            {
                root1,
                root2,
                root3
            };
        }
       
        
        throw new ArgumentException("Invalid arguments");
    }


    static double[] Cube(double a, double b, double c, double d)
    {
       

        if (Math.Abs(a) < 1e-10)
        {
            return Discriminant(b, c, d);
        }

        b /= a;
        c /= a;
        d /= a;


        double q = (3 * c - b * b) / 9;
        double r = (9 * b * c - 27 * d - 2 * b * b * b) / 54;
        double discriminant = q * q * q + r * r;

        if (discriminant > 0)
        {
            double s = Math.Sign(r + Math.Sqrt(discriminant)) * Math.Pow(Math.Abs(r + Math.Sqrt(discriminant)), 1.0 / 3.0);
            double t = Math.Sign(r - Math.Sqrt(discriminant)) * Math.Pow(Math.Abs(r - Math.Sqrt(discriminant)), 1.0 / 3.0);
            double root1 = -b / 3 + (s + t);
            double root2 = (-b / 3 - (s + t) / 2) + Math.Sqrt(3) * (s - t) / 2;
            double root3 = root2 - Math.Sqrt(3) * (s - t) / 2;
            return new double[]
            {
                root1,root2,root3
            };
        }
        else if (discriminant == 0)
        {
            double r13 = Math.Pow(Math.Abs(r), 1.0 / 3.0);
            double root1 = -b / 3 + 2 * r13;
            double root2 = -b / 3 - r13;
            double root3 = root1;
            return new double[]
            {
                root1,root2,root3
            };
        }
        else
        {
            double theta = Math.Acos(r / Math.Sqrt(-(q * q * q)));
            double root1 = -b / 3 + 2 * Math.Sqrt(-q) * Math.Cos(theta / 3);
            double root2 = -b / 3 + 2 * Math.Sqrt(-q) * Math.Cos((theta + 2 * Math.PI) / 3);
            double root3 = -b / 3 + 2 * Math.Sqrt(-q) * Math.Cos((theta + 4 * Math.PI) / 3);
            return new double[]
            {
                root1,root2,root3
            };
        }
    }

    public static double[] Discriminant(double a, double b, double c, double epsilon=1e-10)
    {
      

        double discriminant = b * b - 4 * a * c;

        if (discriminant > epsilon)
        {
            double root1 = (-b + Math.Sqrt(discriminant)) / (2 * a);
            double root2 = (-b - Math.Sqrt(discriminant)) / (2 * a);
            return new[]
            {
                root1,root2
            };
        }
        else if (discriminant == 0)
        {
            double root1 = -b / (2 * a);
            double root2 = root1;
            return new[]
            {
                root1,root2
            };
        }
        else return new double [] { }; 
    }

}
