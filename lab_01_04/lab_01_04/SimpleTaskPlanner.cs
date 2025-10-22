public class SimpleTaskPlanner
{
    public WorkItem[] CreatePlan(WorkItem[] items)
    {
        var itemsAsList = items.ToList();
        itemsAsList.Sort(CompareWorkItems);
        return itemsAsList.ToArray();
    }

    private static int CompareWorkItems(WorkItem firstItem, WorkItem secondItem)
    {
        // 1. Сортування за пріоритетом (спадання)
        int priorityComparison = secondItem.Priority.CompareTo(firstItem.Priority);
        if (priorityComparison != 0)
        {
            return priorityComparison;
        }

        // 2. Сортування за терміном виконання (зростання)
        int dueDateComparison = firstItem.DueDate.CompareTo(secondItem.DueDate);
        if (dueDateComparison != 0)
        {
            return dueDateComparison;
        }

        // 3. Сортування за назвою (алфавітний порядок)
        return string.Compare(firstItem.Title, secondItem.Title, StringComparison.Ordinal);
    }
}