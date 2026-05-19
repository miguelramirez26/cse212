using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue three items with different priorities (2, 5, 3) and dequeue the highest.
    // Expected Result: "High" should be returned first since it has the highest priority (5).
    // Defect(s) Found: None
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 2);
        priorityQueue.Enqueue("High", 5);
        priorityQueue.Enqueue("Medium", 3);

        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items where the highest priority item is at the very end of the queue.
    // Expected Result: "High" should be returned since it has the highest priority (5).
    // Defect(s) Found: The loop condition 'index' < _queue.Count -1' was cutting off early, ignoring the very last item in the queue.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Low", 2);
        priorityQueue.Enqueue("Medium", 3);
        priorityQueue.Enqueue("High", 5);

        Assert.AreEqual("High", priorityQueue.Dequeue());
    }

    // Add more test cases as needed below.
    [TestMethod]
    // Scenario: Enqueue two items with the exact same highest priority.
    // Expected Result: The item that was enqueued first ("FirstHigh") should be returned first.
    // Defect(s) Found: The operator '>=' incorrectly favored the newest item in a priority tie instead of keeping the oldest one.
    public void TestPriorityQueue_3()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("FirstHigh", 5);
        priorityQueue.Enqueue("SecondHigh", 5);

        Assert.AreEqual("FirstHigh", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: An InvalidOperationException should be thrown with the message "The queue is empty."
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();

        var exception = Assert.ThrowsException<InvalidOperationException>(() => priorityQueue.Dequeue());
        Assert.AreEqual("The queue is empty.", exception.Message);
    }
}