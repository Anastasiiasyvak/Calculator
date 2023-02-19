string input = Console.ReadLine();  // зчитує input
string b = ""; // створюємо порожній буфер куди будуть вноситись тєкущі токени 
string[] result = new String[50]; // створюємо list з максимум 50 символами куди вносяться усі токени
int index = 0; // починаємо з індекса нуль
// char 'd' одиничний символ, нам буде повертатися після foreach
// "d" string 
foreach (var x in input)   // для кожного x елемента у input який ввів користувач 
{
    if (Char.IsDigit(x)) // якщо char елемент це число 
    {
        b += x; // додаємо x до буферу 
    }
    else if (Char.IsWhiteSpace(x)) // якщо x це пробіл
    {
        if (b != "") // якщо b не пустий, то він додається до result
        {
            result[index] = b; // додаємо в список result з індексом який спочатку є нулем b 
            b = ""; // опустошаємо наш буфер 
            index += 1; // в наступний раз у нас індекс збільшиться на 1
        }
    }
    else if (x == '(' || x == ')' || x == '+' || x == '-' || x == '*' || x == '/')
    {
        if (b != "")
        {
            result[index] = b; // додаємо в список result з індексом який спочатку є нулем b 
            b = "";
            index += 1;
        }
        result[index] = x.ToString(); // переводимо x з char на string 
        index += 1;
    }
}

if (b != "")  // додає останній токен якщо він не є порожнім
{
    result[index] = b;
}

foreach (string x in result)
{
    Console.WriteLine(x);
}