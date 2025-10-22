using System;
using System.Collections.Generic;
using System.Text;


internal static class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        Console.WriteLine("Ласкаво просимо до Планувальника завдань!");

        // 1. Додаємо список готових завдань для демонстрації
        var workItems = new List<WorkItem>
        {
            new WorkItem
            {
                Title = "Закінчити практичну роботу",
                Description = "Написати код і звіт",
                DueDate = DateTime.Now.AddDays(2),
                Priority = Priority.High,
                Complexity = Complexity.Days,
                CreationDate = DateTime.Now.AddDays(-1),
                IsCompleted = false
            },
            new WorkItem
            {
                Title = "Сходити в магазин",
                Description = "Купити молоко і хліб",
                DueDate = DateTime.Now.AddDays(1),
                Priority = Priority.Medium,
                Complexity = Complexity.Hours,
                CreationDate = DateTime.Now,
                IsCompleted = false
            },
            new WorkItem
            {
                Title = "Зателефонувати другу",
                Description = "Привітати з днем народження",
                DueDate = DateTime.Now.AddDays(1), // Та сама дата, що й у "Сходити в магазин"
                Priority = Priority.Medium,      // Той самий пріоритет
                Complexity = Complexity.Minutes,
                CreationDate = DateTime.Now.AddHours(-2),
                IsCompleted = false
            }
        };


        while (true)
        {
            Console.WriteLine("\n--- Виберіть пункт меню ---");
            Console.WriteLine("\n 1. Список задач\n 2. Додати нову задачу\n 3. Вихід");
            Console.Write("Ваш вибір (1-3): ");
            int menuChoice = int.Parse(Console.ReadLine() ?? "3");

            switch (menuChoice)
            {
                case 1:
                    if (workItems.Count > 0)
                    {
                        // 2. Запитуємо у користувача про тип сортування
                        Console.WriteLine("\n--- Виберіть критерій сортування ---");
                        Console.WriteLine(" 1. За пріоритетом (стандартно)");
                        Console.WriteLine(" 2. За датою виконання");
                        Console.WriteLine(" 3. За назвою (в алфавітному порядку)");
                        Console.Write("Ваш вибір (1-3): ");
                        int sortChoice = int.Parse(Console.ReadLine() ?? "1");

                        SortCriteria sortBy;
                        switch (sortChoice)
                        {
                            case 2:
                                sortBy = SortCriteria.ByDueDate;
                                break;
                            case 3:
                                sortBy = SortCriteria.ByTitle;
                                break;
                            default:
                                sortBy = SortCriteria.ByPriority;
                                break;
                        }

                        var planner = new SimpleTaskPlanner();
                        // Передаємо обраний критерій у метод CreatePlan
                        var sortedItems = planner.CreatePlan(workItems.ToArray(), sortBy);

                        Console.WriteLine("\n--- Ваш упорядкований план ---");
                        foreach (var item in sortedItems)
                        {
                            Console.WriteLine(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Не було введено жодних робочих елементів.");
                    }
                    break;
                case 2:
                    Console.WriteLine("\nВведіть новий робочий елемент (або введіть «done», щоб закінчити):");

                    Console.Write("Назва: ");
                    string title = Console.ReadLine();
                    if (title.ToLower() == "done")
                    {
                        break;
                    }

                    Console.Write("Опис: ");
                    string description = Console.ReadLine();

                    DateTime dueDate = DateTime.Now;
                    try
                    {
                        Console.Write("Термін виконання (дд.мм.рррр): ");
                        dueDate = DateTime.Parse(Console.ReadLine());
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Неправильний формат дати. Будь ласка, використовуйте формат дд.мм.рррр.");
                        continue;
                    }

                    Console.Write("Пріоритет (None, Low, Medium, High, Urgent): ");
                    Priority priority = (Priority)Enum.Parse(typeof(Priority), Console.ReadLine(), true);

                    Console.Write("Складність (None, Minutes, Hours, Days, Weeks): ");
                    Complexity complexity = (Complexity)Enum.Parse(typeof(Complexity), Console.ReadLine(), true);

                    workItems.Add(new WorkItem
                    {
                        Title = title,
                        Description = description,
                        DueDate = dueDate,
                        Priority = priority,
                        Complexity = complexity,
                        CreationDate = DateTime.Now,
                        IsCompleted = false
                    });
                    break;
                case 3:// Вихід
                    Console.WriteLine("\nДякуємо за використання Планувальника. До побачення!");
                    return; // Вихід з методу Main і завершення програми
                default:
                    Console.WriteLine("⚠️ Невідомий пункт меню. Будь ласка, виберіть 1, 2 або 3.");
                    break;
            }
        }
    }
}