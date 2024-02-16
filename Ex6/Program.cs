


class Ex6
{


    public delegate double mathFunc_Delegate(double x);
    static void Main()
    {

        mathFunc_Delegate mathFunc_delegate1 = new mathFunc_Delegate(mathFunc);

        //пример на кубическом уравнении
        try
        {
            Console.WriteLine("Root 1: {0}", dichotomy(-500, 500, 1e-5, 5, mathFunc_delegate1));
            Console.WriteLine("Root 2: {0}", dichotomy(-500, 0, 1e-5, 5, mathFunc_delegate1));
            Console.WriteLine("Root 3: {0}", dichotomy(-20, 0, 1e-5, 5, mathFunc_delegate1));
        }
        catch(ArgumentException e)
        {
            Console.WriteLine(e.Message);
        }
        Console.ReadLine();
    }


    static double mathFunc(double x)
    {
        double a = 2, b = 43, c = 5, d = -4;
        return a*Math.Pow(x, 3) + b * Math.Pow(x, 2) + c * x + d;
       
    }


    static double dichotomy(double from, double to, double accuracy_epsilon, int accuracy_round, mathFunc_Delegate dlg)
    {

        if (from > to)
        {
            throw new ArgumentException("To must be more then From");
        }
        if (accuracy_round<1)
        {
            throw new ArgumentException("Accuracy round value must be more then 1");
        }
        if (dlg==null)
        {
            throw new ArgumentException("Delegate must be initializated");
        }

        //забыл проверять разные знаки функции
        //свойства непрерывности   - нужно проверять что функция непрерывна
        //желательно чтобы был только один корень на отрезке

        while (Math.Abs(to - from) > accuracy_epsilon)
        {
            double center = (from + to) / 2;
            double centerValue = dlg(center);

            if (centerValue == 0) return center;//epsilon
            else if (dlg(from) * centerValue < 0) //epsilon
            {
                to = center;
            }
            else
            {
                from = center;
            }
        }
       
        return Math.Round((from + to) / 2, accuracy_round);
    }

}