/* 
 * Name - Sukhmanjeet Singh
 * Student ID - 000838215
 * Date - September 23, 2023
 * Program's purpose - out of order sort assignment 1 
 * I, Sukhmanjeet Singh, 000838215 certify that this material is my original work. No other person's work has been used without due acknowledgement.
 * 
 * Citations:
 * Help for Merge sort was provided by Abdul Bari in their YouTube tutorial:
 * https://youtu.be/mB5HXBb_HY8?si=uMkxUV9vR2O75QPi
 * Description: This code segment is a sorting algorithm called merge sort.
 */
using System;
using System.IO;

/// <summary>
/// Represents an employee with name, number, rate, hours, and gross pay information.
/// </summary>
class Employee
{
    private string name;
    private int number;
    private decimal rate, gross;
    private double hours;

    /// <summary>
    /// Initializes a new instance of the <see cref="Employee"/> class.
    /// </summary>
    /// <param name="name">The name of the employee.</param>
    /// <param name="number">The employee number.</param>
    /// <param name="rate">The pay rate per hour.</param>
    /// <param name="hours">The number of hours worked.</param>
    public Employee(string name, int number, decimal rate, double hours)
    {
        this.name = name;
        this.number = number;
        this.rate = rate;
        this.hours = hours;
        this.gross = GetGross();
    }

    /// <summary>
    /// Calculates the gross pay for the employee.
    /// </summary>
    /// <returns>The gross pay.</returns>
    public decimal GetGross()
    {
        double hoursThreshold = 40;
        decimal regularPay = rate * Convert.ToDecimal(Math.Min(hours, hoursThreshold));
        decimal overtimePay = rate * 1.5m * Convert.ToDecimal(Math.Max(hours - hoursThreshold, 0));
        gross = regularPay + overtimePay;
        return gross;
    }

    /// <summary>
    /// Gets the name of the employee.
    /// </summary>
    /// <returns>The name of the employee.</returns>
    public string GetName() { return name; }

    /// <summary>
    /// Gets the employee number.
    /// </summary>
    /// <returns>The employee number.</returns>
    public int GetNumber() { return number; }

    /// <summary>
    /// Gets the pay rate per hour.
    /// </summary>
    /// <returns>The pay rate per hour.</returns>
    public Decimal GetRate() { return rate; }

    /// <summary>
    /// Gets the number of hours worked.
    /// </summary>
    /// <returns>The number of hours worked.</returns>
    public Double GetHours() { return hours; }

    /// <summary>
    /// Set the name for the employee.
    /// </summary>
    /// <param name="name">The name of the employee.</param>
    public void SetName(string name) { this.name = name; }

    /// <summary>
    /// Set the number for the employee.
    /// </summary>
    /// <param number="number">The number of the employee.</param>
    public void SetNumber(int number) { this.number = number; }

    /// <summary>
    /// Set the rate for the employee.
    /// </summary>
    /// <param rate="rate">The rate of the employee.</param>
    public void SetRate(Decimal rate) { this.rate = rate; }

    /// <summary>
    /// Set the hours for the employee.
    /// </summary>
    /// <param hours="hours">The hours of the employee.</param>
    public void SetHours(Double hours) { this.hours = hours; }

    /// <summary>
    /// To string method of this class
    /// </summary>
    /// <returns>The string representation of all the variables</returns>
    public override string ToString() => base.ToString();
}

/// <summary>
/// Main program class.
/// </summary>
class Lab1
{
    static Employee[] employees = new Employee[100];
    static int employeeCount = 0;

    /// <summary>
    /// Reads employee data from a file and populates the employees array.
    /// </summary>
    /// <param name="filePath">The path to the data file.</param>
    static void ReadDataFromFile(string filePath)
    {
        // Validate that file exists 
        try
        {
            // Attempt to read all lines from the specified file
            string[] lines = File.ReadAllLines(filePath);

            // Iterate through each line read from the file
            foreach (string line in lines)
            {
                // Split the line by commas to extract data fields
                string[] data = line.Split(',');

                // Check if there are exactly 4 data fields in the line
                if (data.Length == 4)
                {
                    // Extract and trim individual data fields
                    string name = data[0].Trim();
                    int number = int.Parse(data[1].Trim());
                    decimal rate = decimal.Parse(data[2].Trim());
                    double hours = double.Parse(data[3].Trim());

                    // Create a new Employee object with the extracted data and add it to the array
                    employees[employeeCount] = new Employee(name, number, rate, hours);

                    // Increment the employee count
                    employeeCount++;
                }
            }
        }
        catch (FileNotFoundException)
        {
            // Handle the exception if the file 'employees.txt' is not found
            Console.WriteLine("File not found. Please make sure 'employees.txt' is in the correct directory.");
        }
        catch (Exception e)
        {
            // Handle other exceptions and display an error message
            Console.WriteLine($"An error occurred: {e.Message}");
        }
    }

    /// <summary>
    /// Performs a merge sort on an array of employees based on a specified comparison function.
    /// </summary>
    /// <param name="arr">The array of employees to be sorted.</param>
    /// <param name="comparison">The comparison function used for sorting.</param>
    /// <param name="left">The left index of the subarray to be sorted.</param>
    /// <param name="right">The right index of the subarray to be sorted.</param>
    static void MergeSort(Employee[] arr, Comparison<Employee> comparison, int left, int right)
    {
        if (left < right)
        {
            int mid = (left + right) / 2;
            MergeSort(arr, comparison, left, mid);
            MergeSort(arr, comparison, mid + 1, right);
            Merge(arr, comparison, left, mid, right);
        }
    }

    /// <summary>
    /// Merges two sorted subarrays into a single sorted subarray.
    /// </summary>
    /// <param name="arr">The array containing the subarrays to be merged.</param>
    /// <param name="comparison">The comparison function used for merging.</param>
    /// <param name="left">The left index of the first subarray.</param>
    /// <param name="mid">The middle index marking the end of the first subarray.</param>
    /// <param name="right">The right index of the second subarray.</param>
    static void Merge(Employee[] arr, Comparison<Employee> comparison, int left, int mid, int right)
    {
        // Calculate the sizes of the left and right subarrays
        int leftSize = mid - left + 1;
        int rightSize = right - mid;

        // Create temporary arrays to hold the left and right subarrays
        Employee[] leftArray = new Employee[leftSize];
        Employee[] rightArray = new Employee[rightSize];

        // Copy data from the main array to the left and right subarrays
        for (int i = 0; i < leftSize; i++)
        {
            leftArray[i] = arr[left + i];
        }

        for (int i = 0; i < rightSize; i++)
        {
            rightArray[i] = arr[mid + 1 + i];
        }

        // Initialize indices for traversing the left and right subarrays
        int leftIndex = 0;
        int rightIndex = 0;

        // Initialize the index for merging back into the main array
        int mergeIndex = left;

        // Merge the left and right subarrays back into the main array
        while (leftIndex < leftSize && rightIndex < rightSize)
        {
            // Compare elements from the left and right subarrays using the provided comparison function
            if (comparison(leftArray[leftIndex], rightArray[rightIndex]) <= 0)
            {
                // If the left element is smaller or equal, copy it to the main array
                arr[mergeIndex] = leftArray[leftIndex];
                leftIndex++;
            }
            else
            {
                // If the right element is smaller, copy it to the main array
                arr[mergeIndex] = rightArray[rightIndex];
                rightIndex++;
            }
            mergeIndex++;
        }

        // Copy any remaining elements from the left subarray to the main array
        while (leftIndex < leftSize)
        {
            arr[mergeIndex] = leftArray[leftIndex];
            leftIndex++;
            mergeIndex++;
        }

        // Copy any remaining elements from the right subarray to the main array
        while (rightIndex < rightSize)
        {
            arr[mergeIndex] = rightArray[rightIndex];
            rightIndex++;
            mergeIndex++;
        }
    }

    /// <summary>
    /// Compare by name comparison function for sorting. 
    /// </summary>
    /// <param name="emp1">Employee one</param>
    /// <param name="emp2">Employee two</param>
    /// <returns>Integer representing the comparison</returns>
    static int CompareByName(Employee emp1, Employee emp2)
    {
        return emp1.GetName().CompareTo(emp2.GetName());
    }

    /// <summary>
    /// Compare by Number comparison function for sorting. 
    /// </summary>
    /// <param name="emp1">Employee one</param>
    /// <param name="emp2">Employee two</param>
    /// <returns>Integer representing the comparison</returns>
    static int CompareByNumber(Employee emp1, Employee emp2)
    {
        return emp1.GetNumber().CompareTo(emp2.GetNumber());
    }

    /// <summary>
    /// Compare by Rate comparison function for sorting. 
    /// </summary>
    /// <param name="emp1">Employee one</param>
    /// <param name="emp2">Employee two</param>
    /// <returns>Integer representing the comparison</returns>
    static int CompareByRateDescending(Employee emp1, Employee emp2)
    {
        return emp2.GetRate().CompareTo(emp1.GetRate());
    }

    /// <summary>
    /// Compare by hours comparison function for sorting. 
    /// </summary>
    /// <param name="emp1">Employee one</param>
    /// <param name="emp2">Employee two</param>
    /// <returns>Integer representing the comparison</returns>
    static int CompareByHoursDescending(Employee emp1, Employee emp2)
    {
        return emp2.GetHours().CompareTo(emp1.GetHours());
    }

    /// <summary>
    /// Compare by Gross pay comparison function for sorting. 
    /// </summary>
    /// <param name="emp1">Employee one</param>
    /// <param name="emp2">Employee two</param>
    /// <returns>Integer representing the comparison</returns>
    static int CompareByGrossPayDescending(Employee emp1, Employee emp2)
    {
        return emp2.GetGross().CompareTo(emp1.GetGross());
    }

    /// <summary>
    /// Displays employee information in a formatted table.
    /// </summary>
    static void DisplayEmployees()
    {
        Console.WriteLine("Employee Information:");
        Console.WriteLine("---------------------------------------------------------------");
        Console.WriteLine("{0,-20} {1,-10} {2,-15} {3,-10} {4,-10}",
                          "Name", "Number", "Rate", "Hours", "Gross Pay");
        Console.WriteLine("---------------------------------------------------------------");

        for (int i = 0; i < employeeCount; i++)
        {
            Employee emp = employees[i];
            Console.WriteLine("{0,-20} {1,-10} {2,-15:C2} {3,-10:F2} {4,-10:C2}",
                              emp.GetName(), emp.GetNumber(), emp.GetRate(), emp.GetHours(), emp.GetGross());
        }
        Console.WriteLine("---------------------------------------------------------------");
    }

    /// <summary>
    /// Main method for the program.
    /// </summary>
    static void Main()
    {
        // Read employee data from specified file path
        ReadDataFromFile("C:\\Users\\Norts\\Downloads\\employees.txt");

        // Check if the employee data is valid
        if (employeeCount == 0)
        {
            Console.WriteLine("No valid employee data found. Exiting.");
            return;
        }

        bool exit = false;

        // Main menu loop
        while (!exit)
        {
            Console.WriteLine("\nMenu Options:");
            Console.WriteLine("1. Sort by Employee Name (ascending)");
            Console.WriteLine("2. Sort by Employee Number (ascending)");
            Console.WriteLine("3. Sort by Employee Pay Rate (ascending)");
            Console.WriteLine("4. Sort by Employee Hours (ascending)");
            Console.WriteLine("5. Sort by Employee Gross Pay (ascending)");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice (1-6): ");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        // Sort employees by name in ascending order using MergeSort
                        MergeSort(employees, CompareByName, 0, employeeCount - 1);
                        break;
                    case 2:
                        // Sort employees by number in ascending order using MergeSort
                        MergeSort(employees, CompareByNumber, 0, employeeCount - 1);
                        break;
                    case 3:
                        // Sort employees by rate in descending order using MergeSort
                        MergeSort(employees, CompareByRateDescending, 0, employeeCount - 1);
                        break;
                    case 4:
                        // Sort employees by hours in descending order using MergeSort
                        MergeSort(employees, CompareByHoursDescending, 0, employeeCount - 1);
                        break;
                    case 5:
                        // Sort employees by gross pay in descending order using MergeSort
                        MergeSort(employees, CompareByGrossPayDescending, 0, employeeCount - 1);
                        break;
                    case 6:
                        // Exit the program
                        exit = true;
                        break;
                    default:
                        // If the input is not valid
                        Console.WriteLine("Invalid choice. Please enter a number from 1 to 6.");
                        break;
                }

                if (!exit)
                {

                    // Display the sorted employee information
                    DisplayEmployees();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number from 1 to 6.");
            }
        }

        Console.WriteLine("Exiting the program.");
    }
}
