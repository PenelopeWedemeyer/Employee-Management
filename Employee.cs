public class Employee 
{
    public string Name { get; private set; }

    public int EmployeeID { get; private set; }

    public string Title { get; private set; }

    public DateOnly StartDate { get; private set; }

    public Employee(int ID, string employeeName, string position, DateOnly startingDate)
    {
        Name = employeeName;
        EmployeeID = ID;
        Title = position;
        StartDate = startingDate;
    }

    public void PrintDetails() 
    {
        Console.WriteLine($"{Name}, {Title}");
        Console.WriteLine($"Employee ID: {EmployeeID}");
        Console.WriteLine($"Start Date: {StartDate}\n");
    }

    public static int GeneratedRandomNum()
    {
        Random random = new Random();
        int num = random.Next(1000, 9999);
        return num;
    }
}