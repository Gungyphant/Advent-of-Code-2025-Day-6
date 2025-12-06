namespace AOC2025Day6
{
    public class Program
    {
        public static long StringToLong(string s)
        {
            return Convert.ToInt64(s);
        }
        public static char StringToChar(string s)
        {
            return Convert.ToChar(s);
        }
        public static string PartOne(string data)
        {
            long[] operand1s = [];
            long[] operand2s = [];
            long[] operand3s = [];
            long[] operand4s = [];
            char[] operators = [];

            int lineID = 0;
            foreach (string line in data.Split(Environment.NewLine))
            {
                string[] parts = line.Split(" ").Where(item => !string.IsNullOrEmpty(item)).ToArray();
                switch (lineID)
                {
                    case 0:
                        operand1s = Array.ConvertAll(parts, new Converter<string, long>(StringToLong));
                        break;
                    case 1:
                        operand2s = Array.ConvertAll(parts, new Converter<string, long>(StringToLong));
                        break;
                    case 2:
                        operand3s = Array.ConvertAll(parts, new Converter<string, long>(StringToLong));
                        break;
                    case 3:
                        operand4s = Array.ConvertAll(parts, new Converter<string, long>(StringToLong));
                        break;
                    case 4:
                        operators = Array.ConvertAll(parts, new Converter<string, char>(StringToChar));
                        break;
                }
                lineID++;
            }

            long grandTotal = 0;
            for (int i = 0; i < operators.Length; i++)
            {
                long result = 0;
                long operand1 = operand1s[i];
                long operand2 = operand2s[i];
                long operand3 = operand3s[i];
                long operand4 = operand4s[i];
                char thisOperator = operators[i];
                switch (thisOperator)
                {
                    case '+':
                        result = operand1 + operand2 + operand3 + operand4;
                        break;
                    case '*':
                        result = operand1 * operand2 * operand3 * operand4;
                        break;
                }
                //Console.WriteLine(result);
                grandTotal += result;
            }
            return Convert.ToString(grandTotal);
        }
        public static T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(1)).Select(y => matrix[columnNumber, y]).ToArray();
        }
        public static string PartTwo(string data)
        {
            char[,] letters = new char[data.Split(Environment.NewLine)[0].Length + 1, data.Split(Environment.NewLine).Length];
            int y = 0;
            foreach (string ogLine in data.Split(Environment.NewLine))
            {
                string line = " " + ogLine; // Otherwise the rightmost equation never finishes
                int x = 0;
                foreach (char c in line)
                {
                    letters[x, y] = c;
                    x++;
                }
                y++;
            }
            long grandTotal = 0;
            char currentOperator = ' ';
            List<long> operands = new List<long>();
            for (int x = letters.GetLength(0) - 1; x >= 0; x--)
            {
                char[] column = GetColumn(letters, x);
                string text = new string(column[..4]).Replace(" ", "");
                if (column[column.Length - 1] == '*' || column[column.Length - 1] == '+')
                {
                    currentOperator = column[column.Length - 1];
                }
                if (text == "")
                {
                    long currentResult = 0;
                    switch (currentOperator)
                    {
                        case '+':
                            currentResult = 0;
                            foreach (long operand in operands)
                            {
                                currentResult += operand;
                            }
                            break;
                        case '*':
                            currentResult = 1;
                            foreach (long operand in operands)
                            {
                                currentResult *= operand;
                            }
                            break;
                    }
                    operands = new List<long>();
                    currentOperator = ' ';
                    grandTotal += currentResult;
                }
                else
                {
                    long number = Convert.ToInt64(text);
                    operands.Add(number);
                }
            }
            return Convert.ToString(grandTotal);
        }
        static void Main()
        {
            string file = File.ReadAllText(@"../../../input.txt");
            Console.WriteLine(PartOne(file));
            Console.WriteLine(PartTwo(file));
        }
    }
}
