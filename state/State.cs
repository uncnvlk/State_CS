using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace state
{
    class state
    {
        public string[,] str = new string[1, 6];

        public state() // конструктор
        {
            string line;
            string[] temp_line = new string[6];

            try
            {
                //чтение файла
                StreamReader sr = new StreamReader($@"{Directory.GetCurrentDirectory()}/state1.txt",Encoding.UTF8);

                //чтение первой строчки
                line = sr.ReadLine();

                //разделение первой строчки на ячейки
                temp_line = line.Split(';');

                //добавление строки в массив
                str = add(str, temp_line);

                //чтение следующей строки
                line = sr.ReadLine();

                //продолжаем читать строки файла до конца файла
                while (line != null)
                {
                    //добавение строки в массив
                    temp_line = line.Split(';');
                    str = add(str, temp_line);
                    line = sr.ReadLine();
                }

                //закрытие файла
                //sr.Close();
            }

            //ловит ошибки
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }


            finally
            {
                Console.WriteLine("Чтение файла закончено. Массив заполнен.");
            }
        }

        //добавление строчки в массив
        private string[,] add(string[,] str, string[] temp_line)
        {
            string[,] temp_str = new string[str.GetLength(0) + 1, str.GetLength(1)];

            for (int i = 0; i < str.GetLength(0); i++)
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    temp_str[i, j] = str[i, j];
                }

            for (int i = 0; i < str.GetLength(1); i++)
            {
                temp_str[str.GetLength(0), i] = temp_line[i];
            }

            return temp_str;
        }

        //вывод массива
        public void show()
        {
            Console.WriteLine();

            for (int i = 1; i < str.GetLength(0); i++)
            {
                for (int j = 0; j < str.GetLength(1); j++)
                {
                    Console.Write($"{str[i, j],-27}");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        //поиск столицы по названию страны
        public string search_of_capital(string name_of_country)
        {
            string str_inf = "Такой страны в списке нет!";
            if (search_of_string_by_country(name_of_country) != -1)
            {
                return str[search_of_string_by_country(name_of_country), 1];
            }
            else return str_inf;
        }


        //сортировка по численности
        public void sort_by_population()
        {
            string[] temp = new string[6];
            for (int i = 2; i < str.GetLength(0) + 1; i++)
            {
                for (int j = 2; j < str.GetLength(0) - i + 1; j++)
                {
                    if (long.Parse((str[j, 3])) > long.Parse(str[j + 1, 3]))
                    {
                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            temp[k] = str[j, k];
                        }

                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            str[j, k] = str[j + 1, k];
                        }

                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            str[j + 1, k] = temp[k];
                        }
                    }
                }
            }
        }

        //сортировка по названию стран
        public void sort_by_alf()
        {
            string[] temp = new string[6];
            for (int i = 2; i < str.GetLength(0); i++)
            {

                for (int j = 2; j < str.GetLength(0) - 1; j++)
                {
                    if (needToReOrder(str[j, 0], str[j + 1, 0]))
                    {
                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            temp[k] = str[j, k];
                        }

                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            str[j, k] = str[j + 1, k];
                        }

                        for (int k = 0; k < str.GetLength(1); k++)
                        {
                            str[j + 1, k] = temp[k];
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        //вспомогательная функция для сортировки по алфавиту
        protected static bool needToReOrder(string s1, string s2)
        {
            for (int i = 0; i < (s1.Length > s2.Length ? s2.Length : s1.Length); i++)
            {
                if (s1.ToCharArray()[i] < s2.ToCharArray()[i]) return false;
                if (s1.ToCharArray()[i] > s2.ToCharArray()[i]) return true;
            }
            return false;
        }

        //конвертация денежных единиц
        public double search_convert_to(string str_1, string str_2, double count)
        {
            int k = 2;
            double x1, x2, x3;

            while (this.str[k, 4] != str_1)
            {
                k++;
            }
            x2 = double.Parse(this.str[k, 5]);

            k = 2;

            while (this.str[k, 4] != str_2)
            {
                k++;
            }
            x3 = double.Parse(this.str[k, 5]);

            x1 = count;

            return Math.Round((convert_to(x1, x2, x3)), 3);
        }

        //вспомогательная для конвертации
        protected static double convert_to(double x1, double x2, double x3)
        {
            return (x1 * x2) / x3;
        }

        public void edit_list(string name_of_country)
        {
            string inf;
            if (search_of_string_by_country(name_of_country) != -1)
            {
                int k = search_of_string_by_country(name_of_country);
                Console.WriteLine("Какую информацию вы хотите изменить?");
                inf = Console.ReadLine();

                if (inf == str[1, 0])
                {
                    Console.WriteLine("Введите новое название страны");
                    str[k, 0] = Console.ReadLine();
                }
                else if (inf == str[1, 1])
                {
                    Console.WriteLine("Введите новое название столицы");
                    str[k, 1] = Console.ReadLine();
                }
                else if (inf == str[1, 2])
                {
                    Console.WriteLine("Введите новый государственный язык");
                    str[k, 2] = Console.ReadLine();
                }
                else if (inf == str[1, 3])
                {
                    Console.WriteLine("Введите новую численность населения");
                    str[k, 3] = Console.ReadLine();
                }
                else if (inf == str[1, 4])
                {
                    Console.WriteLine("Введите новую денежную единицу");
                    str[k, 4] = Console.ReadLine();
                }
                else if (inf == str[1, 5])
                {
                    Console.WriteLine("Введите новый курс относительно евро");
                    str[k, 5] = Console.ReadLine();
                }
            }
            else Console.WriteLine("Такой страны в списке нет!");
        }

        public void info_about_country(string name_of_country)
        {
            if (search_of_string_by_country(name_of_country) != -1)
            {
                int k = search_of_string_by_country(name_of_country);
                for (int i = 0; i < 6; i++)
                {
                    Console.WriteLine(str[k, i]);
                }
            }
            else Console.WriteLine("Такой страны в списке нет!");
        }

        private int search_of_string_by_country(string name_of_country)
        {
            int k = 0;
            int n = 0;
            

            while ((k < str.GetLength(0))&&(n == 0))
            {
                if (str[k, 0] == name_of_country) { n++; }
                k++;
            }
            if (n != 0) return k - 1;
            else return -1;
        }
        public void del(string name_of_country)
        {
            if (search_of_string_by_country(name_of_country) != -1)
            {
                int k = search_of_string_by_country(name_of_country);
                string[,] temp = new string[str.GetLength(0) - 1, 6];

                for (int i = 0; i < k; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        temp[i, j] = str[i, j];
                    }
                }
                for (int i = k + 1; i < str.GetLength(0); i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        temp[i - 1, j] = str[i, j];
                    }
                }
                this.str = temp;
            }
            else Console.WriteLine("Такой страны в списке нет!");
            
        }

        public void add_c()
        {
            string[] s = new string [6];
            Console.WriteLine("Введите название страны");
            s[0] = Console.ReadLine();
            Console.WriteLine("Введите название столицы");
            s[1] = Console.ReadLine();
            Console.WriteLine("Введите государственный язык");
            s[2] = Console.ReadLine();
            Console.WriteLine("Введите численность населения");
            s[3] = Console.ReadLine();
            Console.WriteLine("Введите денежную единицу");
            s[4] = Console.ReadLine();
            Console.WriteLine("Введите курс относительно евро");
            s[5] = Console.ReadLine();
            str = add(str, s);
        }

        public void write()
        {
            StreamWriter sw = new StreamWriter($@"{Directory.GetCurrentDirectory()}/state.txt");
            for (int i = 1; i < str.GetLength(0); i++)
            {
                string s = str[i, 0];
                for (int j = 1; j < 6; j++)
                {
                    s = s + ';' + str[i, j];
                }
                sw.WriteLine(s);
            }
            sw.Close();
        }
    }

    class MainClass
    {
        static void Main(string[] args)
        {
            int m;
            state obj = new state();
            Console.WriteLine("—————————————————————————————————————");
            Console.WriteLine("|          МАИ Институт №12         |");
            Console.WriteLine("|         Группа Т12О-209Б-19       |");
            Console.WriteLine("|        Сидоренкова Елизавета      |");
            Console.WriteLine("|           Курсовая работа         |");
            Console.WriteLine("—————————————————————————————————————");
            Console.WriteLine();

            do {
                Console.WriteLine();
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("1. Вывести данные на экран.");
                Console.WriteLine("2. Вывести данные определенной страны");
                Console.WriteLine("3. Вывести столицу определенной страны.");
                Console.WriteLine("4. Отсортировать список стран по алфавиту.");
                Console.WriteLine("5. Отсортировать список стран по возврастанию численности населения.");
                Console.WriteLine("6. Перевести из одной валюты в другую.");
                Console.WriteLine("7. Редактирование информации о существующей стране.");
                Console.WriteLine("8. Удаление информации о существующей стране.");
                Console.WriteLine("9. Добавление информации о новой стране.");
                Console.WriteLine("10. Выход.");

                bool Tr;
                do
                {
                    Console.Write("Введите номер пункта: ");
                    Tr = Int32.TryParse(Console.ReadLine(), out m);
                    if (Tr != true || m < 1 || m > 10)
                        Console.WriteLine("Неправильное значение, оно должно быть в пределах диапозона 1...10");
                } while (Tr != true || m < 1 || m > 10);

           
                if (m == 1) { obj.show(); }
                else if (m == 2)
                {
                    Console.WriteLine("Введите название страны");
                    string s = Console.ReadLine();
                    obj.info_about_country(s);
                }
                else if (m == 3)
                {
                    Console.WriteLine("Введите название страны");
                    string s = Console.ReadLine();
                    Console.WriteLine(obj.search_of_capital(s));
                }
                else if (m == 4)
                {
                    obj.sort_by_alf();
                    obj.show();
                    }
                else if (m == 5)
                {
                    obj.sort_by_population();
                    obj.show();
                }
                else if (m == 6)
                {
                    Console.WriteLine("Введите валюту, из которой нужно перевести");
                    string str_1 = Console.ReadLine();
                    Console.WriteLine("Введите количество " + str_1);
                    int count = int.Parse(Console.ReadLine());
                    Console.WriteLine("Введите валюту, в которую нужно перевести");
                    string str_2 = Console.ReadLine();
                    Console.WriteLine(obj.search_convert_to(str_1, str_2, count));
                }
                else if (m == 7)
                {
                    Console.WriteLine("Введите название страны");
                    string s = Console.ReadLine();
                    obj.edit_list(s);
                }
                else if (m == 8)
                {
                    Console.WriteLine("Введите название страны");
                    string s = Console.ReadLine();
                    obj.del(s);
                }
                else if (m == 9)
                {
                    obj.add_c();
                }
            } while (m != 10);

            obj.write();
            Environment.Exit(0);
        }
    }
}