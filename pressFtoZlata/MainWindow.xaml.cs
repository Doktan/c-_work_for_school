using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace pressFtoZlata
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TextBox[] boxes = new TextBox[9]; //поменять потом размер до 9
        float[] answers = new float[9];
        string fifth_answ = "";
        Clock clock = new Clock();

        public MainWindow()
        {

            InitializeComponent();
            Generate_Task();
            boxes[0] = first_a_answ;
            boxes[1] = first_b_asnw;
            boxes[2] = first_c_answ;
            boxes[3] = second_a_answ;
            boxes[4] = second_b_answ;
            boxes[5] = second_c_answ;
            boxes[6] = third_answ;
            boxes[7] = forth_answ;
            boxes[8] = fith_answ_num;

            Thread myThread = new Thread(new ThreadStart(Timer));
            myThread.Start();
        }

        private void Timer()
        {
            /*int count = 0;
            while(count != 10)
            {
                //time.Content = count.ToString();
                Application.Current.Dispatcher.Invoke(new Action(() => { time.Content = count.ToString(); }));
                Thread.Sleep(1000);
                count++;
            }*/
            while(clock.GetTime() != 356400)
            {
                Thread.Sleep(1000);
                clock.IncTime();
                Application.Current.Dispatcher.Invoke(new Action(() => { time.Content = clock.hours + ":" + clock.minutes + ":" + clock.seconds; }));
            }

        }

        // 0 - not prime
        // 1 - prime
        private static int Check_Prime(int number)
        {
            int i;
            for (i = 2; i <= number - 1; i++)
            {
                if (number % i == 0)
                {
                    return 0;
                }
            }
            if (i == number)
            {
                return 1;
            }
            return 0;
        }

        private static int Nod(int n, int d)
        {
            n = Math.Abs(n);
            d = Math.Abs(d);
            while (d != 0 && n != 0)
            {
                if (n % d > 0)
                {
                    int temp = n;
                    n = d;
                    d = temp % d;
                }
                else break;
            }
            if (d != 0 && n != 0) return d;
            return 0;
        }


        // квадрат
        public class Square
        {
            public int a;

            public Square(int a)
            {
                this.a = a;
            }

            public int Sqr()
            {
                return a * a;
            }

            public int Perimeter()
            {
                return a * 4;
            }

        }

        // прямоугольник
        public class Rectangle
        {
            public int a;
            public int b;

            public Rectangle(int a, int b) // удалить
            {
                this.a = a;
                this.b = b;
            }

            // метод который подойдет для решения задачи 5, создает прямоугольник, чья площадь равняется площади квадрата
            public Rectangle()
            {
                Random rnd = new Random();
                // сначала генерим число от 5 - 13 включительно
                // возводим её в квадрат - это наша площадь
                // а затем нужно из этого числа получить два, которые его получают при перемножение, но они не равны друг другу
                int temp = rnd.Next(5, 21);
                //int square = temp * temp;
                // можно получить все делители числа
                // выбрать случайный из них
                // второй получить при помощи square / a
                // a != sqrt(square)
                
                while(Check_Prime(temp) == 1)
                {
                    temp = rnd.Next(5, 21);
                }

                int square = temp * temp;

                List<int> list = new List<int>(); // список делителей
                for (int i = 1; i < square; i++)
                {
                    if (square % i == 0)
                    {
                        if (i != temp && i != 1)
                        {
                            list.Add(i);
                        }
                    }
                }

                a = list[rnd.Next(0, list.Count - 1)];
                b = square / a;
            }

            public int Square()
            {
                return a * b;
            }

            public int Perimeter()
            {
                return (a + b) * 2;
            }

        }

        private class Drob
        {
            public int n;  // numerator
            public int d;  // denumerator

            public Drob(int n, int d)
            {
                this.n = n;
                this.d = d;
            }

            public Drob Sum(Drob drob)
            {
                int n = (this.n * drob.d) + (drob.n * this.d);
                int d = this.d * drob.d;
                Drob test = new Drob(n, d);
                return test;
            }

            public Drob Subtract(Drob drob)
            {
                int n = (this.n * drob.d) - (drob.n * this.d);
                int d = this.d * drob.d;
                Drob res = new Drob(n, d);
                return res;
            }

            public Drob()
            {
                Random rnd = new Random();
                n = rnd.Next(2, 8);
                d = n;
                while (d <= n && d % n == 0)
                {
                    d = rnd.Next(n, 13);
                }
            }

            public void Simplified()
            {
                int nod = Nod(n, d);
                if (nod != 0)
                {
                    n /= nod;
                    d /= nod;
                }
            }
        }

        private void Generate_First_Questrion()
        {

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 1 пример
            int[] sign = { -1, 1 };
            Random rnd = new Random();
            float[] text = new float[5];
            string[] ex = new string[3];

            for (int i = 0; i < 4; i++)
            {
                if (i % 2 == 0)
                {
                    text[i] = sign[rnd.Next(0, 2)] * rnd.Next(10, 40);
                }
                else
                {
                    text[i] = text[i - 1] < 0 ? rnd.Next(10, 40) : -1 * rnd.Next(10, 40);

                }
            }

            text[4] = sign[rnd.Next(0, 2)] * 10;

            ex[0] = "(" + text[0].ToString();
            ex[0] += text[1] < 0 ? (text[1].ToString() + ")") : ("+" + text[1].ToString() + ")");

            ex[1] = "(" + text[2].ToString();
            ex[1] += text[3] < 0 ? (text[3].ToString() + ")") : ("+" + text[3].ToString() + ")");

            ex[2] = text[4] < 0 ? "(" + text[4].ToString() + ")" : text[4].ToString();

            first_a_ex.Content = ex[0] + "*" + ex[1] + ":" + ex[2];

            answers[0] = (text[0] + text[1]) * (text[2] + text[3]) / text[4];
            answers[0] = answers[0] == -0 ? 0 : answers[0];

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 1 пример

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 2 пример
            string a = rnd.Next(0, 15).ToString() + "," + rnd.Next(1, 10);
            text[0] = float.Parse(a);

            float ideal = rnd.Next(1, 11);
            text[1] = ideal - float.Parse(rnd.Next(0, 10).ToString() + "," + rnd.Next(1, 10));
            text[1] = Convert.ToSingle(Math.Round(text[1], 3));
            answers[1] = text[0];
            int cur_sign = rnd.Next(0, 2);

            text[2] = cur_sign == 1 ? ideal - text[1] : text[1] - ideal;
            text[2] = Convert.ToSingle(Math.Round(text[2], 3));

            string znak = cur_sign == 0 ? "-" : "+";
            answers[1] *= cur_sign == 0 ? text[1] - text[2] : text[1] + text[2];
            answers[1] = Convert.ToSingle(Math.Round(answers[1], 3));
            ex[0] = text[1] < 0 ? text[0].ToString() + "*(" + text[1].ToString() + ")" :
                text[0].ToString() + "*" + text[1].ToString();

            ex[1] = text[2] < 0 ? text[0].ToString() + "*(" + text[2].ToString() + ")" :
                text[0].ToString() + "*" + text[2].ToString();

            first_b_ex.Content = ex[0] + $"{znak}" + ex[1];
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 2 пример

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 3 пример
            Drob first = new Drob();
            Drob second = new Drob();
            first.Simplified();
            second.Simplified();

            int d_sign = rnd.Next(0, 2);
            string d_znak = d_sign == 0 ? "-" : "+";
            Drob res = d_sign == 0 ? first.Subtract(second) : first.Sum(second);

            // начальное приближение

            float y = float.Parse(rnd.Next(0, 10).ToString() + "," + rnd.Next(1, 10));
            float x = Convert.ToSingle(Math.Round(y * res.d, 3));

            first_c_ex.Content = "(" + $"{x}" + " : " + $"{y}" + ")*(" + $"{first.n}" + "/" + $"{first.d}" + " " + d_znak + " " + $"{second.n}" + "/" + $"{second.d}" + ")";
            answers[2] = res.n;
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 3 пример
        }

        private void Generate_Second_Qestion()
        {
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 1 пример
            Random rnd = new Random();
            int num = rnd.Next(10, 80) * 10;
            //  0 - 5  1 - 10
            float percent = rnd.Next(0, 2) == 0 ? rnd.Next(1, 6) * 5 : rnd.Next(1, 6) * 10;

            // 0  - понижение  1 - повышение
            int znak = rnd.Next(0, 2);

            string plot_1_znak = znak == 0 ? "понижения" : "повышения";
            string plot_2_znak = znak == 0 ? "уменьшили" : "увеличили";

            string plot_1 = "Товар стоил " + $"{num}" + " рублей. Определите его стоиомсть после " + plot_1_znak + " цены на " + $"{percent}" + "%";
            string plot_2 = "Число " + $"{num}" + " " + plot_2_znak + " на " + $"{percent}" + "%. Найдите новое число.";

            second_a_ex.Content = rnd.Next(0, 2) == 0 ? plot_1 : plot_2;

            answers[3] = znak == 0 ? num * (1 - (percent / 100)) : num * (1 + (percent / 100));
            answers[3] = Convert.ToInt32(answers[3]);
            //second_a_answ.Text = answers[3].ToString();
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 1 пример

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 2 пример
            int up = rnd.Next(1, 8); // числитель
            int down = up; // знаменатль
            while (down <= up && down % up == 0)
                down = rnd.Next(up, 13);

            int nod = Nod(up, down);
            if (nod != 0)
            {
                up /= nod;
                down /= nod;
            }
            num = up * rnd.Next(2, 11);
            answers[4] = num * down / up;
            answers[4] = Convert.ToSingle(Math.Round(answers[4], 3));
            //second_b_answ.Text = answers[4].ToString();
            second_b_ex.Content = "Найдите число, " + $"{up}" + "/" + $"{down}" + " которого равны " + $"{num}";
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 2 пример

            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 3 пример
            // идем в обратную сторону
            // генерим число от 2 до 10
            // генерим красивый процент для него
            // находим числа по этому проценту
            float num_2 = 1.1f;
            num = rnd.Next(2, 20);
            while (num_2 != Math.Truncate(num_2) || num_2 == num)
            {
                num = rnd.Next(1, 50);
                percent = rnd.Next(1, 210);
                answers[5] = percent;
                num_2 = num * percent / 100;
            }

            //second_c_answ.Text = answers[5].ToString();
            second_c_ex.Content = "Сколько процентов составляет " + $"{num_2}" + " от " + num.ToString();
            // ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~ 3 пример
        }

        private void Generate_Third_Task()
        {
            //решите уравнение
            float[] digits = new float[4];
            int[] d_znak = { -1, 1 };
            Random rnd = new Random();
            string znak = rnd.Next(0, 2) == 0 ? "-" : "+";

            third_ex.Content = ""; //для отладки

            for (int i = 0; i < digits.Length; i++)
            {
                digits[i] = i switch
                {
                    1 => 0.5f * d_znak[rnd.Next(0, 2)],
                    2 => 1 * d_znak[rnd.Next(0, 2)],
                    3 => rnd.Next(10, 20),
                    _ => rnd.Next(2, 10),
                };

            }

            // функция из нашего уравнения f(x) = 0
            static float F(float[] digits, string znak, float x)
            {
                return znak == "-" ? digits[0] * (x + digits[1]) - (x + digits[2]) - digits[3]
                    : digits[0] * (x + digits[1]) + (x + digits[2]) - digits[3];
            }

            //теперь нужно найти корень уравнения
            //будет искать методом хорд
            float eps = 0.001f;
            float a = -10, b = 10;
            while (Math.Abs(b - a) > eps)
            {
                a = b - (b - a) * F(digits, znak, b) / (F(digits, znak, b) - F(digits, znak, a));
                b = a - (a - b) * F(digits, znak, a) / (F(digits, znak, a) - F(digits, znak, b));
            }

            // c# округляет в большую сторону начиная с 5

            string[] eq = new string[2];
            eq[0] = digits[1] < 0 ? digits[0].ToString() + "(x - " + $"{Math.Abs(digits[1])}" + ")" : digits[0].ToString() + "(x + " + $"{digits[1]}" + ")";
            eq[1] = "(x";
            eq[1] += digits[2] < 0 ? " - " + $"{Math.Abs(digits[2])}" : " + " + digits[2].ToString();
            eq[1] += ")";

            third_ex.Content = eq[0] + " " + znak + " " + eq[1] + " = " + digits[3].ToString();

            answers[6] = Convert.ToSingle(Math.Round(b));

        }

        private void Generate_Fourth_Task()
        {
            Random rnd = new Random();
            float s_speed = rnd.Next(10, 17);
            float stream = rnd.Next(1, 8);
            float hours_up = rnd.Next(4, 10);
            float hours_down = Convert.ToSingle(Math.Round((s_speed + stream) * hours_up / (s_speed - stream)));

            string plot_1 = "Катер, имеющий собственную скорость " + $"{s_speed}" + " км/ч, проплыл " + $"{hours_up}" + "\nчасов по течению реки. На обратный путь он затратил " + $"{hours_down}" + " часов.\nКакова скорость течения реки?";
            string plot_2 = "На путь по течению реки катер затратил " + $"{hours_up}" + " часов, а на обратный\nпуть " + $"{hours_down}" + " часов. Какова скорость течения реки, если собственная\nскорость катера " + $"{s_speed}" + " км/ч?";

            test_ideal.Content = "* Ответ округлить до целого числа";
            forth_ex.Content = rnd.Next(0, 2) == 0 ? plot_1 : plot_2;
            answers[7] = stream;
        }

        private void Generate_Fith_Task()
        {
       
            Rectangle rect = new Rectangle();
            Square kvadr = new Square(Convert.ToInt32(Math.Sqrt(rect.Square())));
            string plot_1 = "Дачник решил огородить два учатска земли. Один участок прямоугольный\n";
            plot_1 += "размерами " + rect.a.ToString() + " м и " + rect.b.ToString() + " м, а другой квадратный, имебщий ту же площадь, что и\n";
            plot_1 += "прямоугольный. Для какого участка ограда будет длиннее и насколько?";

            string plot_2 = "Какая комната потребует больше потолочного плинтуса и на сколько: прямо-\n";
            plot_2 += "угольная размерами " + rect.a.ToString() + " м и " + rect.b.ToString() + " м или квадратная, имеющая ту же площадь?";

            Random rnd = new Random();

            fith_ex.Content = rnd.Next(0, 2) == 0 ? plot_1 : plot_2;

            if (rect.Perimeter() > kvadr.Perimeter())
            {
                fifth_answ = "Прямоугольный";
                answers[8] = rect.Perimeter() - kvadr.Perimeter();
            }
            else
            {
                fifth_answ = "Квадратный";
                answers[8] = kvadr.Perimeter() - rect.Perimeter();
            }  
        }

        private void Generate_Task()
        {
            Generate_First_Questrion();
            Generate_Second_Qestion();
            Generate_Third_Task();
            Generate_Fourth_Task();
            Generate_Fith_Task();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            float num;
            bool test;
            for (int i = 0; i < boxes.Length; i++)
            {
                test = float.TryParse(boxes[i].Text, out num);
                boxes[i].BorderBrush = answers[i] == num && test ? Brushes.Green : Brushes.Red;
            }

            check_answ.Content = fifth_answ == fifth_answ_list.Text.ToString() ? "Правильно!": "Неправильно!";
            check_answ.Foreground = fifth_answ == fifth_answ_list.Text.ToString() ? Brushes.Green : Brushes.Red;
        }

        private void New_variant_b_Click(object sender, RoutedEventArgs e)
        {
            Generate_Task();
   
            foreach (TextBox box in boxes)
            {
                box.BorderBrush = Brushes.Gray;
                box.Text = "";
            }

            fifth_answ_list.SelectedIndex = 0;
            check_answ.Content = "";
            check_answ.Foreground = Brushes.Gray;

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }
    }
}

//цвет обычной рамки #FFABADB3
//цвет неправильного ответа #FFFF0000
//correct #FF0DA00D