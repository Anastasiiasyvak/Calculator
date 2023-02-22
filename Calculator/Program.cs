using System.Collections; // дає можливість працювати з масивами, списками та чергами 

string input = Console.ReadLine();  
string b = ""; // створюємо порожній буфер куди будуть вноситись тєкущі токени 
string[] result = new String[50]; // створюємо list з максимум 50 символами куди вносяться усі токени
int index = 0; 
// char 'd' одиничний символ, нам буде повертатися після foreach
// "d" string 
foreach (var x in input)    
{
    if (Char.IsDigit(x))  
    {
        b += x; 
    }
    else if (Char.IsWhiteSpace(x)) // якщо x це пробіл
    {
        if (b != "") 
        {
            result[index] = b; 
            b = ""; 
            index ++ ; 
        }
    }
    else if (x == '+' || x == '-' ||  x == '*' || x == '/' || x == '^')
    {
        if (b != "")
        {
            result[index] = b; 
            b = "";
            index ++ ;
        }
        result[index] = x.ToString(); 
        index ++ ;
    }
}

if (b != "")  
{
    result[index] = b;
}

Queue queue = new Queue();
Stack stack = new Stack();
int priority = 0; // початковий пріорітет для першого оператора

foreach (string x in result)
{
    if (!(x == "+" || x == "-" || x == "*" || x == "/" || x == "^"))
    {
        queue.Enqueue(x);
    }
    else if (x == "+" || x == "-" || x == "*" || x == "/" || x == "^")
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
        while (stack.Count > 0 && (stack.Peek().ToString() == "*" || stack.Peek().ToString() == "/" || stack.Peek().ToString() == "^")) // peek перевіряє останній оператор 
        {
            queue.Enqueue(stack.Pop().ToString());
        }

        if (stack.Count > 0 && (stack.Peek().ToString() == "+" || stack.Peek().ToString() == "-"))
        {
            int lastPriority = 0;
            string lastOperator = stack.Peek().ToString();

            if (lastOperator == "+" || lastOperator == "-")
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
}

while (stack.Count > 0)
{
    string lastOperator = stack.Pop().ToString();
    queue.Enqueue(lastOperator);
}

foreach (var item in queue)
{
    Console.Write(item + "");
}

// =============================================================
//
// Stack stack2 = new Stack();
// foreach (var item in queue)
// {
//     if (!(item == "+" || item == "-" || item == "*" || item == "/"))
//     {
//         stack2.Push(item);
//         int firstresult =  Convert.ToInt32(item);
//     }
//     else if (item == "+" || item == "-" || item == "*" || item == "/")
//     {
//         stack2.Pop();
//         
//     }
// }
