using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

enum Post
{
    Laborer,
    OfficeWorker,
    Director
}

class Employee
{
    private static int nextId = 0;

    public int Id { get; private set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public Post post { get; set; }
    public int Salary { get; set; }

    public Employee(string name, int age, Post post, int salary)
    {
        if (String.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("name cannot be emty or null");
        }

        if (age < 18)
        {
            throw new ArgumentOutOfRangeException("Age must be > 18");
        }

        Id = nextId++;
        Name = name;
        Age = age;
        this.post = post;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Age: {Age}, Post: {post}, Salary: ${Salary}";
    }
}

class Factory
{
    private List<Employee> employees = new List<Employee>();
    
    public void createEmployee(string Name, int Age, Post post, int Salary)
    {
        employees.Add(new Employee(Name, Age, post, Salary));
    }

    public void addEmployee(Employee employee)
    {
        employees.Add(employee);
    }

    public void removeEmployee(Employee employee) 
    {

        if (!employees.Remove(employee))
        {
            throw new ElementNotFoundException("Такого элемента нет в списке!");
        }
    }

    public void changePost(Employee employee)
    {
        Console.WriteLine("Выберите профессию для работника.");
        string input = Console.ReadLine();
        switch (input.ToLower())
        {
            case "laborer":
                employee.post = Post.Laborer;
                Console.WriteLine("Должность изменена на Laborer.");
                break;
            case "officeworker":
                employee.post = Post.OfficeWorker;
                Console.WriteLine("Должность изменена на OfficeWorker.");
                break;
            case "director":
                employee.post = Post.Director;
                Console.WriteLine("Должность изменена на Director.");
                break;
            default:
                Console.WriteLine("Некорректный ввод. Попробуйте снова.");
                break;
        }

        
    }

    public void transferEmployee(Factory factory, Employee employee)
    {
        if (!this.employees.Contains(employee))
        {
            throw new ElementNotFoundException("Такого работника нет на производстве!");
        }
        this.employees.Remove(employee);
        factory.employees.Add(employee);
    }
    
    public void changeSalary(Employee employee)
    {
        Console.WriteLine("Назначьте новую зарплату работнику.");
        string input = Console.ReadLine();
        int newSalary = int.Parse(input);
        employee.Salary = newSalary;
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Employee employee in this.employees)
        { 
            stringBuilder.AppendLine(employee.ToString());
        }
        return stringBuilder.ToString();

    }
}

class ElementNotFoundException: Exception
{
    public ElementNotFoundException(string message) : base(message) { }
}

internal class Program
{
    private static void Main(string[] args)
    {
        try
        {
            Factory factory1 = new Factory();
            Factory factory2 = new Factory();
            Employee employee1 = new Employee("Nikita", 21, Post.Director, 1500);
            Employee employee2 = new Employee("Egor", 18, Post.Laborer, 500);
            factory1.createEmployee("Nazgul", 19, Post.OfficeWorker, 1000);
            factory2.addEmployee(employee1);
            factory2.addEmployee(employee2);
            Console.WriteLine("Factory1");
            Console.WriteLine(factory1);
            Console.WriteLine("Factory2:");
            Console.WriteLine(factory2);
            factory2.transferEmployee(factory1, employee1);
            factory1.changePost(employee1);
            factory1.changeSalary(employee1);
            Console.WriteLine("Factory1");
            Console.WriteLine(factory1);
            Console.WriteLine("Factory2:");
            Console.WriteLine(factory2);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
        
    }
}
