using System;
using System.Collections.Generic;

class Program
{
    static bool IsOperator(char c)
    {
        return (c == '+' || c == '-' || c == '*' || c == '/');
    }

    static bool IsDigit(char c)
    {
        return (c >= '0' && c <= '9');
    }

    static int Precedence(char op)
    {
        if (op == '+' || op == '-')
        {
            return 1;
        }
        if (op == '*' || op == '/')
        {
            return 2;
        }
        return 0;
    }

    static string InfixToRPN(string infix)
    {
        string rpn = "";
        Stack<char> opStack = new Stack<char>();
        foreach (char c in infix)
        {
            if (IsDigit(c))
            {
                rpn += c;
            }
            else if (c == '(')
            {
                opStack.Push(c);
            }
            else if (c == ')')
            {
                while (opStack.Count > 0 && opStack.Peek() != '(')
                {
                    rpn += opStack.Pop();
                }
                if (opStack.Count > 0 && opStack.Peek() == '(')
                {
                    opStack.Pop();
                }
            }
            else if (IsOperator(c))
            {
                while (opStack.Count > 0 && Precedence(opStack.Peek()) >= Precedence(c))
                {
                    rpn += opStack.Pop();
                }
                opStack.Push(c);
            }
        }
        while (opStack.Count > 0)
        {
            rpn += opStack.Pop();
        }
        return rpn;
    }

    static int EvaluateRPN(string rpn)
    {
        Stack<int> operandStack = new Stack<int>();
        foreach (char c in rpn)
        {
            if (IsDigit(c))
            {
                operandStack.Push(c - '0');
            }
            else if (IsOperator(c))
            {
                int b = operandStack.Pop();
                int a = operandStack.Pop();
                switch (c)
                {
                    case '+': operandStack.Push(a + b); break;
                    case '-': operandStack.Push(a - b); break;
                    case '*': operandStack.Push(a * b); break;
                    case '/': operandStack.Push(a / b); break;
                }
            }
        }
        return operandStack.Pop();
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        string expression;
        if (args.Length > 0)
        {
            expression = args[0];
        }
        else
        {
            Console.Write("Введіть арифметичний вираз: ");
            expression = Console.ReadLine();
        }
        string rpn = InfixToRPN(expression);
        int result = EvaluateRPN(rpn);
        Console.WriteLine(result);
    }
}