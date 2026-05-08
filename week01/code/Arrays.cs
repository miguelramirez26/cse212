public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // 1. Create a double array of size 'length' to store the multiples.
        double[] multiples = new double[length];

        // 2. Use a for loop to iterate from 0 to 'length - 1'.
        for (int i = 0; i < length; i++)
        {
            // 3. Inside the loop, calculate the multiple: number * (i + 1).
            // 4. Store each result in the corresponding index of the array.
            multiples[i] = number * (i + 1);
        }
        
        // 5. Return the completed array.
        return multiples;

    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // 1. Calculate the split point using 'data.Count - amount'.
        int splitPoint = data.Count - amount;

        // 2. Get the part that will move to the front (the end of the list).
        // 3. Get the part that will move to the back (the start of the list).
        List<int> backPart = data.GetRange(splitPoint, amount);
        List<int> frontPart = data.GetRange(0, splitPoint);

        // 4. Clear the original list and add the parts back in the new order.
        data.Clear();
        data.AddRange(backPart);
        data.AddRange(frontPart);
        
    }
}
