/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Initialize the CustomerService class with an invalid max size of 0.
        // Expected Result: The constructor should automatically correct the invalid size and set the max size to 10.
        Console.WriteLine("Test 1");
        var cs1 = new CustomerService(0);
        Console.WriteLine($"Detect(s) Found: {cs1}"); // Check if the max size is 10
        Console.WriteLine("----------------------");

        // Defect(s) Found: Originally the maxSize was reassigned an invalid size, rather using the default of 10.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Create a queue of size 2 and try to add 3 customers.
        // Expected Result: The 3rd customer should trigger a queue full error message.
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(2);
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();

        // Defect(s) Found: The issue was due to a strictly greater than (>) check instead of greater than or equal to (>=) which meant that the queue could overflow past its maximum size.

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below
        // Test 3
        // Scenario: Create a new queue and try to serve a customer immediately.
        // Expected Result: It should display an empty queue error message instead of crashing.
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(5);
        cs3.ServeCustomer();

        // Defect(s) Found: In the original code for the ServeCustomer method, after removing the customer from the front of the queue, it would try to create a new Customer object for the deleted customer (lost data). Additionally, it didn’t check to see if the queue was empty before attempting to delete the front customer, causing an out of range exception.

        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) {
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // 1. Check if the queue is empty to prevent the program from crashing
        if (_queue.Count == 0) {
            Console.WriteLine("The queue is empty.");
            return;
        }

        // 2. Save the data of the first customer in line before removing them
        var customer = _queue[0];

        // 3. Remove the customer from the front of the queue
        _queue.RemoveAt(0);

        // 4. Display the customer's information on the console
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}