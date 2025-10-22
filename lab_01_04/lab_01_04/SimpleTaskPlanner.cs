public class SimpleTaskPlanner
{
    // Оновлений метод, який приймає критерій сортування
    public WorkItem[] CreatePlan(WorkItem[] items, SortCriteria sortBy)
    {
        // Конвертуємо в List для зручного сортування за допомогою LINQ
        var itemsAsList = items.ToList();

        // Використовуємо LINQ для гнучкого сортування
        switch (sortBy)
        {
            // Сортування за датою
            case SortCriteria.ByDueDate:
                itemsAsList = itemsAsList
                    .OrderBy(item => item.DueDate)           // 1. За датою (зростання)
                    .ThenByDescending(item => item.Priority) // 2. За пріоритетом (спадання)
                    .ThenBy(item => item.Title)              // 3. За назвою (алфавіт)
                    .ToList();
                break;

            // Сортування за назвою
            case SortCriteria.ByTitle:
                itemsAsList = itemsAsList
                    .OrderBy(item => item.Title)             // 1. За назвою (алфавіт)
                    .ThenByDescending(item => item.Priority) // 2. За пріоритетом (спадання)
                    .ThenBy(item => item.DueDate)            // 3. За датою (зростання)
                    .ToList();
                break;

            // Сортування за пріоритетом (як було раніше, за замовчуванням)
            case SortCriteria.ByPriority:
            default:
                itemsAsList = itemsAsList
                    .OrderByDescending(item => item.Priority) // 1. За пріоритетом (спадання)
                    .ThenBy(item => item.DueDate)           // 2. За датою (зростання)
                    .ThenBy(item => item.Title)             // 3. За назвою (алфавіт)
                    .ToList();
                break;
        }

        return itemsAsList.ToArray();
    }

    // Старий метод CompareWorkItems більше не потрібен, 
    // оскільки ми використовуємо гнучкіше сортування через LINQ.
}