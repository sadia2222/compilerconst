using System;
using System.Collections.Generic;

// Token class to represent individual tokens
public class Token
{
    public TokenType Type { get; set; }
    public string Value { get; set; }

    public Token(TokenType type, string value)
    {
        Type = type;
        Value = value;
    }
}

// Enum to define token types
public enum TokenType
{
    Identifier,
    Keyword,
    Number,
    Operator,
    // Add more token types as needed
}

// Class to represent symbol table entry
public class SymbolTableEntry
{
    public string Name { get; }
    public TokenType Type { get; }

    public SymbolTableEntry(string name, TokenType type)
    {
        Name = name;
        Type = type;
    }
}

// Class representing the symbol table
public class SymbolTable
{
    private Dictionary<string, SymbolTableEntry> entries = new Dictionary<string, SymbolTableEntry>();

    public void AddEntry(string name, TokenType type)
    {
        if (!entries.ContainsKey(name))
        {
            entries.Add(name, new SymbolTableEntry(name, type));
        }
        else
        {
            // Handle redefinition error
            Console.WriteLine($"Error: Identifier '{name}' already defined.");
        }
    }

    public void DisplayEntries()
    {
        Console.WriteLine("Symbol Table:");
        Console.WriteLine("Name\t\tType");
        foreach (var entry in entries)
        {
            Console.WriteLine($"{entry.Key}\t\t{entry.Value.Type}");
        }
    }
}

// Lexical analyzer class
public class LexicalAnalyzer
{
    private SymbolTable symbolTable = new SymbolTable();

    public List<Token> Analyze(string input)
    {
        List<Token> tokens = new List<Token>();

        // Tokenize input code and populate symbol table
        // For simplicity, let's assume input code is space-separated
        string[] words = input.Split(new char[] { ' ', '=', ';', '(', ')', '{', '}', '[', ']', ',' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {
            TokenType type = DetermineTokenType(word);
            tokens.Add(new Token(type, word));

            if (type == TokenType.Identifier)
            {
                // Add identifier to symbol table
                symbolTable.AddEntry(word, TokenType.Identifier);
            }
            else if (type == TokenType.Number)
            {
                // Add number to symbol table
                symbolTable.AddEntry(word, TokenType.Number);
            }
        }

        return tokens;
    }

    // Method to determine token type
    private TokenType DetermineTokenType(string word)
    {
        // Simple token type determination logic, you might need more sophisticated logic
        if (IsKeyword(word))
        {
            return TokenType.Keyword;
        }
        else if (IsOperator(word))
        {
            return TokenType.Operator;
        }
        else if (IsNumber(word))
        {
            return TokenType.Number;
        }
        else
        {
            return TokenType.Identifier;
        }
    }

    // Methods to check if a word is a keyword, operator, or number
    private bool IsKeyword(string word)
    {
        // Example: check if the word is a keyword
        string[] keywords = { "int", "float", "if", "else", /* Add more keywords as needed */ };
        return Array.Exists(keywords, k => k.Equals(word));
    }

    private bool IsOperator(string word)
    {
        // Example: check if the word is an operator
        string[] operators = { "+", "-", "*", "/", "=", "<", ">" /* Add more operators as needed */ };
        return Array.Exists(operators, op => op.Equals(word));
    }

    private bool IsNumber(string word)
    {
        // Example: check if the word is a number
        double result;
        return double.TryParse(word, out result);
    }

    public void DisplaySymbolTable()
    {
        symbolTable.DisplayEntries();
    }
}

class Program
{
    static void Main(string[] args)
    {
        LexicalAnalyzer analyzer = new LexicalAnalyzer();
        string inputCode = "int x = 10; float y = 20.5; if (x > y) { y = x + y; }";

        List<Token> tokens = analyzer.Analyze(inputCode);

        // Output tokens
        Console.WriteLine("Tokens:");
        foreach (Token token in tokens)
        {
            Console.WriteLine($"{token.Type}: {token.Value}");
        }

        // Output symbol table entries
        analyzer.DisplaySymbolTable();
    }
}
