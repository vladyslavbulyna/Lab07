using System;
using System.Text;

namespace FitnessClub
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // UTF-8, щоб українські літери в консолі не перетворювалися на знаки питання
            Console.OutputEncoding = Encoding.UTF8;

            bool running = true;

            // Головний цикл меню: працює, доки користувач не обере пункт 0
            while (running)
            {
                ShowMenu();
                string? choice = Console.ReadLine();
                Console.WriteLine();

                // У Main лишилися тільки виклики: що саме робиться — видно з назв методів
                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": ShowCostTable(1.0, 5.5, 0.5); Pause(); break;
                    case "3": ShowWorkloadTable(); Pause(); break;
                    case "0": running = false; Console.WriteLine("До побачення!"); break;
                    default: Console.WriteLine("Немає такого пункту. Спробуйте ще раз."); Pause(); break;
                }
            }
        }

        /// <summary>
        /// Виводить пункти головного меню. Метод нічого не читає й не повертає:
        /// його єдина робота — показати текст меню.
        /// </summary>
        static void ShowMenu()
        {
            Console.WriteLine("=== Фітнес-клуб ===");
            Console.WriteLine("1 — про програму");
            Console.WriteLine("2 — розрахунок вартості тренувань");
            Console.WriteLine("3 — завантаження тренерів за тиждень");
            Console.WriteLine("0 — вихід");
            Console.Write("Ваш вибір: ");
        }

        /// <summary>
        /// Пауза перед поверненням у меню й очищення екрана.
        /// При перенаправленому вводі (запуск із файлу) клавішу не чекаємо.
        /// </summary>
        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу...");
            if (!Console.IsInputRedirected)
            {
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        /// <summary>
        /// Пункт 1: тема проєкту, головна сутність і перелік її полів.
        /// </summary>
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

        /// <summary>
        /// Рахує вартість абонемента й повертає її як результат.
        /// Метод нічого не виводить: його можна викликати з будь-якого місця —
        /// для таблиці, для підсумку чи для перевірки.
        /// </summary>
        /// <param name="months">тривалість занять у місяцях</param>
        /// <param name="rate">тариф за місяць, грн</param>
        /// <param name="serviceFee">сервісний збір, грн</param>
        static double CalculateCost(double months, double rate, double serviceFee)
        {
            return rate * months + serviceFee * Math.Sqrt(months);
        }

        /// <summary>
        /// Пункт 2: табулювання вартості від тривалості занять.
        /// Діапазон і крок приходять параметрами, тому той самий метод
        /// можна викликати з іншими межами, нічого в ньому не змінюючи.
        /// </summary>
        static void ShowCostTable(double from, double to, double step)
        {
            Console.WriteLine("=== Таблиця вартості абонемента / тренувань ===");
            Console.WriteLine("Залежність вартості від тривалості занять (місяці / блоки):");

            Console.WriteLine($"{"Період (міс)",14}{"Вартість (грн)",16}");
            Console.WriteLine(new string('-', 30));

            // 1e-9 компенсує похибку double, інакше останній рядок може зникнути
            for (double t = from; t <= to + 1e-9; t += step)
            {
                // розрахунок і виведення розділені: рахує CalculateCost, друкує цей цикл
                double cost = CalculateCost(t, 800.0, 150.0);
                Console.WriteLine($"{t,14:F1}{cost,16:F2}");
            }

            Console.WriteLine(new string('-', 30));
        }

        /// <summary>
        /// Пункт 3: таблиця завантаження тренерів, підсумки рядків і стовпців.
        /// </summary>
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
