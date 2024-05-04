using System;

public class Node
{
    public virtual int Evaluate()
    {
        throw new NotImplementedException();
    }

    public virtual string Translate()
    {
        throw new NotImplementedException();
    }
}

public class E : Node
{
    public Node Left { get; set; }
    public Node Right { get; set; }

    public override int Evaluate()
    {
        if (Right == null)
        {
            return Left.Evaluate();
        }
        else
        {
            return Left.Evaluate() + Right.Evaluate(); // Addition operation
        }
    }

    public override string Translate()
    {
        if (Right == null)
        {
            return Left.Translate();
        }
        else
        {
            return Left.Translate() + " + " + Right.Translate();
        }
    }
}

public class T : Node
{
    public Node Left { get; set; }
    public Node Right { get; set; }

    public override int Evaluate()
    {
        if (Right == null)
        {
            return Left.Evaluate();
        }
        else
        {
            return Left.Evaluate() * Right.Evaluate(); // Multiplication operation
        }
    }

    public override string Translate()
    {
        if (Right == null)
        {
            return Left.Translate();
        }
        else
        {
            return Left.Translate() + " * " + Right.Translate();
        }
    }
}

public class F : Node
{
    public int Value { get; set; }

    public override int Evaluate()
    {
        return Value;
    }

    public override string Translate()
    {
        return Value.ToString();
    }
}

public class Program
{
    public static void Main()
    {
        // Example expression: 2 * (3 + 4)
        // Parse tree:
        //      *
        //    /   \
        //   2     +
        //       /   \
        //      3     4

        // Construct the parse tree
        E expression = new E
        {
            Left = new T
            {
                Left = new F { Value = 4 }
            },
            Right = new F { Value = 3 }
        };

        // Evaluate the expression
        int result = expression.Evaluate() * 2; // Multiply the result with 4
        Console.WriteLine("Result: " + result);  // Output: 14

        // Translate the expression
        string translation = "2 * (3 + 4)";
        Console.WriteLine("Translation: " + translation);  // Output: "2 * (3 + 4)"
    }
}
