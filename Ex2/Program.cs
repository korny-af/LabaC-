using System;

public class Program
{
    static public void Main()
    {
        Console.WriteLine(CalculateE.range(1e-10)); //1e-5
        Console.Read();
    }
}

public class Helpers
{
    public static double Factorial(int f)
    {
        if (f == 0) return 1;

        return f * Factorial(f - 1);
    }
}

public static class CalculateE
{
    public static double equation(double accur)
    {
        return Math.Exp(1);
    }

    public static double range(double accur)
    {
        int n = 1;
        double currentApproximation = 1, previousApproximation = 0;

        while (Math.Abs(currentApproximation - previousApproximation) >= accur)
        {
            previousApproximation = currentApproximation;
            currentApproximation = Math.Pow(1 + 1.0/n, n);
            n++;
        }

        return currentApproximation;
    }

    public static double lim(double accur)
    {
        double x = 0;

        //берем в предел тк чем меньше accur
        //тем больше 1.0/accur
        // соотв больше значение предела
        //соотв больше точность

        //изначально: (перевернули дробь 2 раза)
        //accur/=1.0
        //Pow(1.0+1.0/accur , accur)
        x = Math.Pow(1.0 + accur, 1.0 / accur);
        return x;
    }
}

public class CalculateP
{

    public static double equation(double accur)
    {
        return Math.Acos(-1);
    }

    public static double range(double accur)
    {
        double x = 0, current = accur + 1;

        for (int i = 1; Math.Abs(current) > accur; i++)
        {
            current = (i % 2 == 0 ? -1.0 : 1.0) / (2.0 * i - 1.0);
            x += current;
        }
        return 4 * x;
    }

    //public static double lim(double accur)
    //{

    //    accur = (1.0 / accur);

    //    double x = Math.Pow(4, accur) * Math.Pow(Helpers.Factorial((int)accur), 2);
    //    x /= Math.Sqrt(accur) * Helpers.Factorial(2*(int)accur);

    //    //double x = Math.Pow(Math.Pow(2, accur) * Helpers.Factorial((int)accur), 4);
    //    //x = x/( accur * Math.Pow(Helpers.Factorial(2*((int)(accur))), 2));
    //    return x;
    //}
}

public static class CalculateLN2
{


    public static double equation(double accur)
    {
        return Math.Log(Math.E, 2);
    }

    public static double range(double accur)
    {
       
        double x = 0, current = accur + 1;

        for (int i = 1; Math.Abs(current)>accur; i++)
        {
            current = ((i % 2 == 0 ? 1.0 : -1.0)) / i;
            x += current;
        }
        return x;
    }

    //public static double lim(double accur)
    //{
    //    double x = 0;
    //    x = Math.Pow(1.0 + accur, 1.0 / accur);
    //    return x;
    //}


}




public static class CalculateSQRT2
{


    public static double equation(double accur)
    {
        return Math.Sqrt(2);
    }

    public static double range(double accur)
    {

        double x = 0, current = accur + 1;

        for (int i = 1; Math.Abs(current) > accur; i++)
        {
            current = ((i % 2 == 0 ? 1.0 : -1.0)) / i;
            x += current;
        }
        return x;
    }

    //public static double lim(double accur)
    //{
    //    double x = 0;
    //    x = Math.Pow(1.0 + accur, 1.0 / accur);
    //    return x;
    //}


}