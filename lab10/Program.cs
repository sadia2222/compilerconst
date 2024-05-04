using System;
using System.Collections.Generic;
using System.Linq;

public class SLRParser
{
    Stack<string> Stack = new Stack<string>();
    List<string> finalArray = new List<string>();
    Dictionary<string, Dictionary<string, string>> dict = new Dictionary<string, Dictionary<string, string>>();
    List<string> States = new List<string>();

    public SLRParser()
    {
        Stack.Push("0");
        ParseTableInit();
    }

    public void ParseTableInit()
    {
        States.Add("Program_begin ( ) { DecS Decs Decs AssS IffS } end");
        States.Add("DecS_int Var = Const ;");
        States.Add("AssS_Var = Var + Var ;");
        States.Add("IffS_if ( Var > Var ) { PriS } else { PriS }");
        States.Add("PriS_print Var ;");
        States.Add("Var_a");
        States.Add("Var_b");
        States.Add("Var_c");
        States.Add("Const_5");
        States.Add("Const_10");
        States.Add("Const_0");

        dict.Add("0", new Dictionary<string, string>()
        {
            {"begin", "S1"}, // Shift to state 1
            {"(", ""},
            {")", ""},
            {"{", ""},
            {"int", ""},
            {"a", ""},
            {"b", ""},
            {"c", ""},
            {"=", ""},
            {"5", ""},
            {"10", ""},
            {"0", ""},
            {";", ""},
            {"if", ""},
            {">", ""},
            {"print", ""},
            {"else", ""},
            {"$", ""},
            {"}", ""},
            {"+", ""},
            {"end", ""},
            {"Program", "1"},
            {"DecS", ""},
            {"AssS", ""},
            {"IffS", ""},
            {"PriS", ""},
            {"Var", ""},
            {"Const", ""}
        });

        dict.Add("1", new Dictionary<string, string>()
        {
            {"begin", ""},
            {"(", ""},
            {")", ""},
            {"{", ""},
            {"int", ""},
            {"a", ""},
            {"b", ""},
            {"c", ""},
            {"=", ""},
            {"5", ""},
            {"10", ""},
            {"0", ""},
            {";", ""},
            {"if", ""},
            {">", ""},
            {"print", ""},
            {"else", ""},
            {"$", "Accept"},
            {"}", ""},
            {"+", ""},
            {"end", ""},
            {"Program", ""},
            {"DecS", ""},
            {"AssS", ""},
            {"IffS", ""},
            {"PriS", ""},
            {"Var", ""},
            {"Const", ""}
        });
    }

    public void ParseInput(string input)
    {
        finalArray = input.Split(' ').ToList();
        int pointer = 0;

        while (Stack.Count != 0)
        {
            string top = Stack.Peek();
            if (dict.ContainsKey(top) && dict[top].ContainsKey(finalArray[pointer]))
            {
                string value = dict[top][finalArray[pointer]];
                if (value.StartsWith("S"))
                {
                    Stack.Push(finalArray[pointer]);
                    Stack.Push(value.Substring(1));
                    pointer++;
                    PrintStack();
                }
                else if (value.StartsWith("R"))
                {
                    int num = int.Parse(value.Substring(1));
                    string[] splitValue = States[num - 1].Split('_');
                    for (int i = 0; i < 2 * splitValue[1].Length; i++)
                    {
                        Stack.Pop();
                    }
                    string stackTop = Stack.Peek();
                    Stack.Push(splitValue[0]);
                    if (dict.ContainsKey(stackTop) && dict[stackTop].ContainsKey(splitValue[0]))
                    {
                        Stack.Push(dict[stackTop][splitValue[0]]);
                        PrintStack();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                        return;
                    }
                }
                else if (value == "Accept")
                {
                    Console.WriteLine("Accepted");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Error");
                return;
            }
        }

        Console.WriteLine("Parsing completed.");
    }

    public void PrintStack()
    {
        Console.WriteLine("Stack: " + string.Join(" ", Stack.Reverse()));
    }

    public static void Main(string[] args)
    {
        SLRParser parser = new SLRParser();
        string input = "begin int a=5; int b=10; int c=0; c=a+b; if(c>a) print a; else print c; end";
        parser.ParseInput(input);
    }
}
