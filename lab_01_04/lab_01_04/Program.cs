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
        var workItems = new List<WorkItem>();

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
                        var planner = new SimpleTaskPlanner();
                        var sortedItems = planner.CreatePlan(workItems.ToArray());

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
