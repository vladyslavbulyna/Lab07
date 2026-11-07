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
                        Console.WriteLine("Поки не зроблено.");
                        Pause();
                        break;
                    case "3":
                        Console.WriteLine("Поки не зроблено.");
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
    }
}
