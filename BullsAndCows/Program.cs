using System;

namespace Bulls_Cows
{
    class Program
    {
        static void Main(string[] args)
        {
            uint N;
            int bulls, cows;
            string user_number, computer_number;
            Console.WriteLine("\t===== ПРАВИЛА =====\n  Добро пожаловать в игру 'Быки и Коровы'\n  Правила очень просты.\n  В начале вам надо будет задать длину числа, которое вы хотели бы отгадывать.\n  Затем вы пытаетесь угадать число, которое рандомно генерирует программа.\n  Если цифра была отгадана верно, но она стоит не на своём месте, то это 'корова'.\n  Если же цифра была угадана верно и она была на своём месте, то это 'бык'.\n  Приятной игры!\n");
            ChangeColor("Нажмите любую клавишу, чтобы начать игру...", ConsoleColor.DarkGray);
            Console.ReadKey();
            // Продолжаем игру, пока кол-во быков не будет равно N.
            do
            {
                Console.Clear();
                Console.Write("Задайте длину числа: ");
                // Проверяем корректность ввода.
                while (!uint.TryParse(Console.ReadLine(), out N) || (N > 10) || N == 0)
                {
                    ChangeColor("Невозможно создать такое число, попробуйте снова", ConsoleColor.Red);
                    Console.Write("Задайте длину числа: ");
                }
                // Генерируем случайное N значное число.
                computer_number = GenerateRandomNumber(N);
                ChangeColor($"{N} значное число было успешно сгенерированно!", ConsoleColor.Green);
                Console.Write("Введите " + N + " значное число: ");
                do
                {
                    user_number = InPut(N);
                    bulls = BullsComp(user_number, computer_number);
                    cows = CowsComp(user_number, computer_number);
                    Console.WriteLine("Кол-во быков: " + bulls);
                    Console.WriteLine("Кол-во коров: " + cows);
                } while (bulls != N);
                // Поздравляем победителя, потому что он молодец.
                ChangeColor("Ура! Вы отгадали число!", ConsoleColor.Green);
                Console.WriteLine("Хотите сыграть ещё раз?");
                ChangeColor("Нажмите любую клавишу, чтобы продолжить или 'Enter', чтобы закончить\n", ConsoleColor.DarkGray);
                //  Предлагаем сыграть повторно или выйти с помощью клавиши 'Enter'.
            } while (Console.ReadKey(true).Key != ConsoleKey.Enter);
        }
        /// <summary>
        /// Проверяем ввод на корректность.
        /// </summary>
        /// <param name="N">Текущая размерность числа.</param>
        /// <returns>Число введённое пользователем.</returns>
        public static string InPut(uint N)
        {
            string user_num;
            int u_n;
            bool cond1;
            bool cond2;
            do
            {
                user_num = Console.ReadLine();
                cond1 = user_num.Length == N;
                cond2 = int.TryParse(user_num, out u_n);
                if (!cond1)
                {
                    ChangeColor("Ой! Кажется, вы ввели не " + N + " значное число", ConsoleColor.Red);
                }
                else if (!cond2)
                {
                    ChangeColor("*ОШИБОЧКА* Вы ввели не число", ConsoleColor.Red);
                }
            } while (!(cond1 && cond2));
            return user_num;
        }
        /// <summary>
        /// Метод, изменяющий цвет вывода в консоль.
        /// </summary>
        /// <param name="sentence">Предложение, которому надо поменять цвет.</param>
        /// <param name="color">Выбранный цвет.</param>
        public static void ChangeColor(string sentence, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(sentence);
            // Возвращаем исходный цвет.
            Console.ResetColor();
        }
        /// <summary>
        /// Генерируем рандомное число из N цифр и не начинающиеся с нуля.
        /// </summary>
        /// <param name="N">Текущая размерность числа.</param>
        /// <returns>Сгенерированное компьютером число.</returns>
        public static string GenerateRandomNumber(uint N)
        {
            string[] array = new string[N];
            Random randomic = new Random();
            array[0] = Convert.ToString(randomic.Next(1, 10));
            // Проверяем, что числа не повторяются.
            int k;
            string num;
            for (int i = 1; i < N; i++)
            {
                do
                {
                    num = Convert.ToString(randomic.Next(0, 10));
                    k = 0;
                    for (int j = 0; j < i; j++)
                    {
                        if (array[j] == num)
                        {
                            k += 1;
                        }
                    }
                } while (k != 0);
                array[i] = num;
            }
            return String.Join("", array);
        }
        /// <summary>
        /// Считаем кол-во быков.
        /// </summary>
        /// <param name="user_number">Число, введённое пользователем.</param>
        /// <param name="computer_number">Число, сгенерированное компьютером.</param>
        /// <returns>Количество быков.</returns>
        public static int BullsComp(string user_number, string computer_number)
        {
            int N = user_number.Length;
            int bulls = 0;
            for (int i = 0; i < N; i++)
            {
                if (user_number[i] == computer_number[i])
                {
                    bulls += 1;
                }
            }
            return bulls;
        }
        // Считаем кол-во коров.
        /// <summary>
        /// Считаем кол-во коров.
        /// </summary>
        /// <param name="user_number">Число, введённое пользователем.</param>
        /// <param name="computer_number">Число, сгенерированное компьютером.</param>
        /// <returns>Количнство Коров.</returns>
        public static int CowsComp(string user_number, string computer_number)
        {
            int N = user_number.Length;
            int cows = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if ((user_number[i] == computer_number[j]) && (i != j))
                    {
                        cows += 1;
                    }
                }
            }
            return cows;
        }
    }
}
