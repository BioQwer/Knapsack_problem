using System;
using System.Globalization;

namespace Рюкзак_1_мерный
{
    class element
    {
        int number; //порядковый номер
        int c;  //ценность предмета
        int a;  //вес элемента
        double lambda;  //удельное значение
        double result_double;
        int result;

        public element(int number, int c, int a, double lambda)
        {
            this.number = number;
            this.c = c;
            this.a = a;
            this.lambda = lambda;
        }
        public int get_number() { return number; }
        public int get_c() { return c; }

        public int get_a() { return a; }

        public double get_lambda() { return lambda; }
        public double get_result_double() { return result_double; }

        public int get_result() { return result; }
        public void set_result_double(double result_double) { this.result_double = result_double; }

        public void set_result(int result) { this.result = result; }

        public override String ToString()
        {
            return "Element " + number + " Cost = " + c +
                " Weight = " + a;
                //" Lambda = " + lambda.ToString("0.0", CultureInfo.InvariantCulture) + " resD = " + result_double.ToString("0.00", CultureInfo.InvariantCulture) + " resB = " + result.ToString();
        }

    }

    class knap
    {
        element[] elements;
        int R;              //максимальный вес
        double weight, F_Dan;  //текущий вес , текущий данцигНеЦелый
        int F;  //значение только что выполненого алгоритма с целыми

        public void Dancig()
        {
            quickSort(elements, 0, elements.Length - 1);
            int n = elements.Length;
            for (int i = 0; i < n; i++)
                elements[i].set_result_double(0.0);

            weight = 0;
            F_Dan = 0;
            for (int i = 0; i < n; i++)
            {
                if (weight <= R)
                {
                    weight = weight + elements[i].get_a();
                    if (weight < R)
                    {
                        F_Dan = F_Dan + elements[i].get_c();
                        elements[i].set_result_double(1.0);
                    }
                    else
                    {
                        weight = weight - elements[i].get_a();
                        double temp = (R - weight);
                        temp = temp / elements[i].get_a();
                        elements[i].set_result_double(temp);
                        F_Dan += temp * elements[i].get_c();
                        weight += temp * elements[i].get_a();
                    }
                }
            }
            quickSort_number(ref elements, 0, elements.Length - 1);
        }

        public void Dancig_bool()
        {
            quickSort(elements, 0, elements.Length - 1);
            int n = elements.Length;
            for (int i = 0; i < n; i++)
                elements[i].set_result(0);

            weight = 0;
            F = 0;
            for (int i = 0; i < n; i++)
            {
                if (weight < R)
                {
                    weight = weight + elements[i].get_a();
                    if (weight <= R)
                    {
                        F = F + elements[i].get_c();
                        elements[i].set_result(1);
                    }
                    else
                    {
                        weight = weight - elements[i].get_a();
                        elements[i].set_result(0);
                    }
                }
            }
            quickSort_number(ref elements, 0, elements.Length - 1);
        }

        public void Rule_2()
        {
            quickSort_cost(ref elements, 0, elements.Length - 1);
            int n = elements.Length;
            for (int i = 0; i < n; i++)
                elements[i].set_result(0);

            weight = 0;
            F = 0;
            for (int i = 0; i < n; i++)
            {
                if (weight < R)
                {
                    weight = weight + elements[i].get_a();
                    if (weight <= R)
                    {
                        F = F + elements[i].get_c();
                        elements[i].set_result(1);
                    }
                    else
                    {
                        weight = weight - elements[i].get_a();
                        elements[i].set_result(0);
                    }
                }
            }
            quickSort_number(ref elements, 0, elements.Length - 1);
        }

        public void Algo_mod()  //Алгоритм модификации начального решения
        {
            Dancig_bool();
            int n = elements.Length;


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (elements[i].get_result() != elements[j].get_result())
                    {
                        if (elements[i].get_result() > elements[j].get_result())
                        {
                            if (elements[i].get_c() < elements[j].get_c())
                            {
                                weight -= elements[i].get_a();
                                weight += elements[j].get_a();
                                if (weight <= R)
                                {
                                    elements[i].set_result(0);
                                    elements[j].set_result(1);
                                    F = F - elements[i].get_c() + elements[j].get_c();
                                }
                                else
                                {
                                    weight += elements[i].get_a();
                                    weight -= elements[j].get_a();
                                }
                            }
                        }
                        if (elements[i].get_result() < elements[j].get_result())
                        {
                            if (elements[i].get_c() > elements[j].get_c())
                            {
                                weight += elements[i].get_a();
                                weight -= elements[j].get_a();
                                if (weight <= R)
                                {
                                    elements[i].set_result(1);
                                    elements[j].set_result(0);
                                    F = F + elements[i].get_c() - elements[j].get_c();
                                }
                                else
                                {
                                    weight -= elements[i].get_a();
                                    weight += elements[j].get_a();
                                }
                            }
                        }
                    }
                }
            }
        }

        public void Rule_2_double()
        {
            quickSort_cost_vozrast(ref elements, 0, elements.Length - 1);
            int n = elements.Length;
            weight = 0;
            F = 0;
            for (int i = 0; i < n; i++)
            {
                elements[i].set_result(1);
                weight += elements[i].get_a();
                F += elements[i].get_c();
            }

            for (int i = 0; i < n; i++)
            {
                if (weight > R)
                {

                    if (weight >= R)
                    {
                        weight = weight - elements[i].get_a();
                        F = F - elements[i].get_c();
                        elements[i].set_result(0);
                    }
                    else
                    {
                        weight = weight + elements[i].get_a();
                    }
                }
            }
            quickSort_number(ref elements, 0, elements.Length - 1);
        }

        public void print_elements()
        {
            Console.WriteLine(Environment.NewLine + "Элементы рюкзака : МАкс вес " + R);
            for (int i = 0; i < elements.Length; i++)
            {
                Console.WriteLine(elements[i].ToString());
            }
            
            Console.Write(Environment.NewLine + "Решение рюкзака : Целевая =" + F +
                Environment.NewLine + "{");
            for (int i = 0; i < elements.Length; i++)
            {
                Console.Write(elements[i].get_result() + " , ");
                if (i == elements.Length - 1)
                    Console.Write("}" + Environment.NewLine);
            }
        }

        public void quickSort(element[] a, int l, int r)
        {
            element temp;
            double x = a[l + (r - l) / 2].get_lambda();
            //запись эквивалентна (l+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (a[i].get_lambda() > x) i++;
                while (a[j].get_lambda() < x) j--;
                if (i <= j)
                {
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                quickSort(a, i, r);

            if (l < j)
                quickSort(a, l, j);
        }

        public void quickSort_cost(ref element[] a, int l, int r)
        {
            element temp;
            double x = a[l + (r - l) / 2].get_c();
            //запись эквивалентна (l+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (a[i].get_c() > x) i++;
                while (a[j].get_c() < x) j--;
                if (i <= j)
                {
                    if (a[i].get_c() == a[j].get_c())
                    { }
                    else
                    {
                        temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }

                    i++;
                    j--;
                }
            }
            if (i < r)
                quickSort_cost(ref a, i, r);

            if (l < j)
                quickSort_cost(ref a, l, j);
        }

        public void quickSort_cost_vozrast(ref element[] a, int l, int r)
        {
            element temp;
            double x = a[l + (r - l) / 2].get_c();
            //запись эквивалентна (l+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (a[i].get_c() < x) i++;
                while (a[j].get_c() > x) j--;
                if (i <= j)
                {
                    if (a[i].get_c() == a[j].get_c())
                    { }
                    else
                    {

                        temp = a[i];
                        a[i] = a[j];
                        a[j] = temp;
                    }

                    i++;
                    j--;
                }
            }
            if (i < r)
                quickSort_cost_vozrast(ref a, i, r);

            if (l < j)
                quickSort_cost_vozrast(ref a, l, j);
        }

        public void quickSort_number(ref element[] a, int l, int r)
        {
            element temp;
            double x = a[l + (r - l) / 2].get_number();
            //запись эквивалентна (l+r)/2, 
            //но не вызввает переполнения на больших данных
            int i = l;
            int j = r;
            //код в while обычно выносят в процедуру particle
            while (i <= j)
            {
                while (a[i].get_number() < x) i++;
                while (a[j].get_number() > x) j--;
                if (i <= j)
                {
                    temp = a[i];
                    a[i] = a[j];
                    a[j] = temp;
                    i++;
                    j--;
                }
            }
            if (i < r)
                quickSort_number(ref a, i, r);
            if (l < j)
                quickSort_number(ref a, l, j);
        }

        public knap(int[] weight, int[] cost, int R)
        {
            set_knap(weight, cost, R);
        }

        public void set_knap(int[] weight, int[] cost, int R)
        {
            this.R = R;

            elements = new element[weight.Length];
            for (int i = 0; i < weight.Length; i++)
            {
                double c = cost[i];
                c = c / weight[i];
                elements[i] = new element(i, cost[i], weight[i], c);
            }
        }

        public int get_Dancig_bool_F()
        {
            Dancig_bool();
            return F;
        }

        public double get_Dancig_F()
        {
            Dancig();
            return F_Dan;
        }

        public int get_Rule_2()
        {
            Rule_2();
            return F;
        }

        public int get_Rule_2_double()
        {
            Rule_2_double();
            return F;
        }
        public int get_AlgoMod()
        {
            Algo_mod();
            return F;
        }
    }

    class Program
    {
        public static void toFile(double DancigF, int Dancig_boolF, int Rule_2F, int Rule_2_doubleF, int Algo_modF)
        {
            string path = "f:\\documents\\visual studio 2013\\Projects\\Рюкзак 1 мерный\\Рюкзак 1 мерный\\data\\";  //Путь   

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + path + "Dancig.txt", true))  //Второй параметр означает будет ли полность перезаписан файл
                file.WriteLine(DancigF);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + path + "Dancig_bool.txt", true))
                file.WriteLine(Dancig_boolF);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + path + "Rule_2.txt", true))
                file.WriteLine(Rule_2F);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + path + "Rule_2_double.txt", true))
                file.WriteLine(Rule_2_doubleF);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"" + path + "Algo_mod.txt", true))
                file.WriteLine(Algo_modF);
        }
        static void Main()
        {
            Random random = new Random();
            //костыльный сбор статистики с помощью так же названных переменных
            //и простых методов getЦелевая функция алгоритма
            int max_k = 15; //сколько итераций при одной размерности массива
            int max_i = 50, max_j = 10; //max_i*max_j  =  максимальная размерность массива       max_j - увеличение на каждом шаге
            for (int i = 1; i < max_i; i++)
            {
                double Dancig = 0;
                int Dancig_bool = 0, Rule_2 = 0, Rule_2_double = 0, Algo_mod = 0;
                for (int k = 0; k < max_k; k++)
                {
                    int[] w_arr = new int[i * max_j];
                    int[] c_arr = new int[i * max_j];
                    int R = 0;
                    for (int j = 0; j < i * max_j; j++)
                    {
                        w_arr[j] = random.Next(1, i * max_j);
                        R += w_arr[j];
                        c_arr[j] = random.Next(1, max_j);
                    }
                    knap test = new knap(w_arr, c_arr, R / 2);

                    Dancig += test.get_Dancig_F();
                    Dancig_bool += test.get_Dancig_bool_F();
                    Rule_2 += test.get_Rule_2();
                    Rule_2_double += test.get_Rule_2_double();
                    Algo_mod += test.get_AlgoMod();
                }
                toFile(Dancig / max_k, Dancig_bool / max_k, Rule_2 / max_k, Rule_2_double / max_k, Algo_mod / max_k);
            }
            Console.WriteLine("Программа выполнена =)");



            Console.ReadKey();
        }
    }
}

