public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1

        if (value < Data)
        {
            // Insert to the left
            if (Left is null)
                Left = new Node(value);
            else
                Left.Insert(value);
        }
        else if (value > Data)
        {
            // Insert to the right
            if (Right is null)
                Right = new Node(value);
            else
                Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        // TODO Start Problem 2

        // Base Case: If the value matches the current node's data
        if (value == Data)
        {
            return true;
        }

        // Search in the left subtree if the value is smaller
        if (value < Data)
        {
            if (Left is null)
                return false;
            else
                return Left.Contains(value);
        }

        // Search in the right subtree if the value is larger
        else if (value > Data)
        {
            if (Right is null)
                return false;
            else
                return Right.Contains(value);
        }

        return false;
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        
        // Calculate the height of the left subtree (0 if it does not exist)
        int leftHeight = (Left is null) ? 0 : Left.GetHeight();

        // Calculate the height of the right subtree (0 if it does not exist)
        int rightHeight = (Right is null) ? 0 : Right.GetHeight();

        // Return 1 plus the maximum height between both subtrees
        return 1 + Math.Max(leftHeight, rightHeight);
    }
}