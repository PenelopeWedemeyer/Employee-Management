using System.Linq;

List<Employee> employeeList = new List<Employee>();
bool continueProgram = true;

string[] readingFile = File.ReadAllLines("employees.txt");
foreach(string f in readingFile)
{
    Employee person = CovertStringToEmployee(f);
    employeeList.Add(person);
}


while(continueProgram)
{
    DisplayOptions();
    Console.Write("Enter your choice: ");
    string choice = Console.ReadLine() ?? "";

    switch(choice)
    {
        case "1":
            CreateEmployee();
            break;
        case "2":
            ViewAllEmployees();
            break;
        case "3":
            DisplaySearchForEmployee();
            break;
        case "4":
            DeleteEmployee();
            break;
        case "5":
            //Updates employee.txt
            using(StreamWriter file = new StreamWriter("employees.txt"))
            {
                foreach(Employee e in employeeList)
                {
                    file.WriteLine(ConvertEmployeeToString(e));
                }
            }

            Console.WriteLine("\nGoodBye!");
            continueProgram = false;
            break;
        default: 
            Console.WriteLine("Invalid Input");
            break;
    }
}

void DisplayOptions()
{
    Console.WriteLine("\n***************");
    Console.WriteLine("1. Create New Employee");
    Console.WriteLine("2. View All Employees");
    Console.WriteLine("3. Search Employees");
    Console.WriteLine("4. Delete Employee");
    Console.WriteLine("5. Exit & Update");
    Console.WriteLine("***************\n");
}

void DisplayTitles(string title)
{
    Console.WriteLine("\n***************");
    Console.WriteLine(title);
    Console.WriteLine("***************\n");
}

void CreateEmployee()
{
    DisplayTitles("Create New Employee");

    Console.Write("\nWhat is the employee's name? ");
    string staffName = Console.ReadLine() ?? "";

    Console.Write("What is the employee's title? ");
    string staffTitle = Console.ReadLine() ?? "";

    bool dateCheck = true; 
    DateOnly date = new DateOnly();
    while(dateCheck)
    {
        try
        {
            Console.Write("What is the employee's start date? ");
            string staffStartDate = Console.ReadLine() ?? "";
            date = DateOnly.Parse(staffStartDate);  
            dateCheck = false;
        }
        catch(FormatException)
        {
            Console.WriteLine("\nInvalid Input. MM/DD/YY or Month Day, Year\n");
        }
    }

    int generatedID = Employee.GeneratedRandomNum();

    Employee employeePerson = new Employee(generatedID, staffName, staffTitle, date);
    employeeList.Add(employeePerson);
    
    Console.WriteLine($"\nNew Employee Added - Employee ID: {generatedID}");
}

void ViewAllEmployees()
{
    DisplayTitles("View All Contacts");

    foreach(var e in employeeList)
    {
        e.PrintDetails();
    }
}


void DisplaySearchForEmployee()
{
    Console.WriteLine("\n***************");
    Console.WriteLine("Search Employees By:");
    Console.WriteLine("1. Employee ID");
    Console.WriteLine("2. Name");
    Console.WriteLine("***************\n");

    bool continueDisplay = true;

    while(continueDisplay)
    {
        Console.Write("Enter your choice: ");
        string choice = Console.ReadLine() ?? "";

        switch(choice)
        {
            case "1":
                SearchEmployeeByID();
                continueDisplay = false;
                break;
            case "2":
                SearchEmployeeByName();
                continueDisplay = false;
                break;
            default:
                break;

        }
    }
}

void SearchEmployeeByID()
{
    int ID = 0;
    try
    {
        Console.Write("Enter the Employee ID: ");
        ID = int.Parse(Console.ReadLine() ?? "");

    }
    catch(FormatException)
    {
        Console.WriteLine("Input must be a number\n");
        SearchEmployeeByID();
        return;
    }

    var foundID = employeeList.Where(e => e.EmployeeID == ID);

    DisplayTitles("Result");

    DisplayFoundEmployee(foundID);
}

void SearchEmployeeByName()
{
    Console.Write("Enter a Name: ");
    string name = Console.ReadLine() ?? "";
    name = name.ToLower();
    var foundName = employeeList.Where(n => n.Name.ToLower().Split(" ")[0].Contains(name) ||  n.Name.ToLower().Split(" ")[1].Contains(name));

    DisplayTitles("Result:");

    DisplayFoundEmployee(foundName);
    
}

void DisplayFoundEmployee(IEnumerable<Employee> foundID)
{
    if(foundID.Any() == true)
    {
        foreach(Employee e in foundID)
        {
            e.PrintDetails();
        }
    } 
    else
    {
        Console.WriteLine("No employees found.");
    }
}

void DeleteEmployee()
{  
    DisplayTitles("Delete an Employee:");

    int ID = 0;

    try
    {
        Console.Write("Enter Employee ID to delete: ");
        ID = int.Parse(Console.ReadLine() ?? "");
    }
    catch(FormatException)
    {
        Console.WriteLine("Input must be a number\n");
        return;
    }

    string index = "";

    for(int i = 0; i < employeeList.Count; i++)
    {
        if(employeeList[i].EmployeeID == ID)
        {
            index = $"{i}";

        }
    }

    if(index != "")
    {
        int parseIndex = int.Parse(index);
        Console.WriteLine($"\nEmployee ID {employeeList[parseIndex].EmployeeID} has been deleted.");
        employeeList.Remove(employeeList[parseIndex]);
    }
    else
    {
        Console.WriteLine($"Employee ID {ID} not found.");
    }

}


string ConvertEmployeeToString(Employee employee)
{
    return $"{employee.EmployeeID},{employee.Name},{employee.Title},{employee.StartDate}";
}

Employee CovertStringToEmployee(string worker)
{
    var employee = worker.Split(",");

    //Conversions
    int IDConversion = int.Parse(employee[0]);
    DateOnly startDateConversion = DateOnly.Parse(employee[3]);

    Employee employeeInfo = new Employee(IDConversion, employee[1], employee[2], startDateConversion);
    return employeeInfo;
}