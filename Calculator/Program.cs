using System.Collections;

string input = Console.ReadLine();
string b = "";
string[] result = new String[50];
int index = 0;

foreach (var x in input)
{
    if (Char.IsDigit(x))
    {
        b += x;
    }
    else if (Char.IsWhiteSpace(x))
    {
        if (b != "")
        {
            result[index] = b;
            b = "";
            index++;
        }
    }
    else if (x == '+'|| x == '-'||x == '*'||x == '/'||x == '^'||x == '('||x == ')')
    {
        if (b != "")
        {
            result[index] = b;
            b = "";
            index++;
        }
        result[index] = x.ToString();
        index++;
    }
}

if (b != "")
{
    result[index] = b;
}

Queue queue = new Queue();
Stack stack = new Stack();
int priority = 0;

foreach (string x in result)
{
    if (!(x == "+"||x == "-"||x == "*"||x == "/"||x == "^"||x == "("||x == ")"))
    {
        queue.Enqueue(x);
    }
    else if (x == "+"||x == "-"||x == "*"||x == "/"||x == "^")
    {

        Dictionary<string, int> operatorPriorities = new Dictionary<string, int>
        {
            { "+", 0 },
            { "-", 0 },
            { "*", 1 },
            { "/", 1 },
            { "^", 2 }
        };

        int currentPriority = operatorPriorities[x];
        while (stack.Count > 0 && (stack.Peek().ToString() == "*"||stack.Peek().ToString() == "/"||stack.Peek().ToString() == "^"))
        {
            queue.Enqueue(stack.Pop().ToString());
        }

        if (stack.Count > 0 && (stack.Peek().ToString() == "+"||stack.Peek().ToString() == "-"))
        {
            int lastPriority = 0;
            string lastOperator = stack.Peek().ToString();

            if (lastOperator == "+"||lastOperator == "-")
            {
                lastPriority = 0;
            }
            else if (lastOperator == "*" || lastOperator == "/")
            {
                lastPriority = 1;
            }
            else if (lastOperator == "^")
            {
                lastPriority = 2;
            }

            if (currentPriority <= lastPriority)
            {
                lastOperator = stack.Pop().ToString();
                queue.Enqueue(lastOperator);
            }
        }

        stack.Push(x);
    }
    else if (x == "(")
    {
        stack.Push(x);
    }
    else if (x == ")")
    {
        while (stack.Count > 0 && stack.Peek().ToString() != "(")
        {
            queue.Enqueue(stack.Pop().ToString());
        }

        if (stack.Count > 0 && stack.Peek().ToString() == "(")
        {
            stack.Pop();
        }
    }
}

while (stack.Count > 0)
{
    string lastOperator = stack.Pop().ToString();
    queue.Enqueue(lastOperator);
}

foreach (var item in queue)
{
    Console.Write(item + " ");
}