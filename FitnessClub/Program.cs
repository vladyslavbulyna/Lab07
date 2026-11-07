using System;
using System.Text;

namespace FitnessClub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            bool running = true;
            while (running)
            {
                Console.WriteLine("=== Фітнес-клуб ===");
                Console.WriteLine("1 — про програму");
                Console.WriteLine("2 — розрахунок вартості тренувань");
                Console.WriteLine("3 — завантаження тренерів за тиждень");
                Console.WriteLine("0 — вихід");
                Console.Write("Ваш вибір: ");

                string? choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;
                    case "2":
                        ShowCostTable();
                        Pause();
                        break;
                    case "3":
                        ShowWorkloadTable();
                        Pause();
                        break;
                    case "0":
                        running = false;
                        Console.WriteLine("До побачення!");
                        break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            if (!Console.IsInputRedirected)
            {
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        static void ShowAbout()
        {
            Console.WriteLine("=== Про програму ===");
            Console.WriteLine("Тема 3: Фітнес-клуб");
            Console.WriteLine("Сутність: Абонемент");
            Console.WriteLine("Кількість полів: 7\n");
            Console.WriteLine("Поля сутності «Абонемент»:");
            Console.WriteLine("1. int номер — номер абонемента");
            Console.WriteLine("2. string прізвище клієнта — прізвище відвідувача");
            Console.WriteLine("3. int код — тип абонемента з довідника");
            Console.WriteLine("4. DateTime дата початку — дата активації абонемента");
            Console.WriteLine("5. int кількість місяців — тривалість дії абонемента");
            Console.WriteLine("6. double ціна — вартість абонемента в грн");
            Console.WriteLine("7. bool чи активний — статус дії абонемента\n");
            Console.WriteLine("Підсумкова величина: абонементи, що добігають кінця протягом тижня.");
        }

        static void ShowCostTable()
        {
            Console.WriteLine("=== Таблиця вартості абонемента / тренувань ===");
            Console.WriteLine("Залежність вартості від тривалості занять (місяці / блоки):");

            double from = 1.0;
            double to = 5.5;
            double step = 0.5;

            Console.WriteLine($"{"Період (міс)",14}{"Вартість (грн)",16}");
            Console.WriteLine(new string('-', 30));

            // додаємо 1e-9 щоб не зникав останній рядок через неточність double
            for (double t = from; t <= to + 1e-9; t += step)
            {
                double cost = 800.0 * t + 150.0 * Math.Sqrt(t);
                Console.WriteLine($"{t,14:F1}{cost,16:F2}");
            }
        }

        static void ShowWorkloadTable()
        {
            Console.WriteLine("=== Завантаження тренерів за тиждень ===");

            Console.Write("Введіть кількість тренерів (рядків): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0)
            {
                Console.WriteLine("Помилка: потрібно ввести додатне число.");
                return;
            }

            Console.Write("Введіть кількість днів (стовпців): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0)
            {
                Console.WriteLine("Помилка: потрібно ввести додатне число.");
                return;
            }

            Console.WriteLine();

            // масив підсумків по стовпцях
            int[] colSums = new int[cols];

            // шапка таблиці
            Console.Write($"{"Тренер",-12}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{"День " + col,10}");
            }
            Console.WriteLine($"{"Разом",10}");
            Console.WriteLine(new string('-', 12 + cols * 10 + 10));

            // рядки таблиці
            for (int row = 1; row <= rows; row++)
            {
                int rowSum = 0;
                Console.Write($"{"Тренер " + row,-12}");

                for (int col = 1; col <= cols; col++)
                {
                    int val = ((row * 3 + col * 2 + 1) % 6) + 2;
                    rowSum += val;
                    colSums[col - 1] += val;
                    Console.Write($"{val,10}");
                }

                Console.WriteLine($"{rowSum,10}");
            }

            // підсумок по стовпцях
            Console.WriteLine(new string('-', 12 + cols * 10 + 10));
            Console.Write($"{"Всього:",-12}");

            int grandTotal = 0;
            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colSums[col],10}");
                grandTotal += colSums[col];
            }

            Console.WriteLine($"{grandTotal,10}");
        }
    }
}
