using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sharp_Math.Classes
{
    public class GameLogic
    {
        public string equation { get; set; }
        public int result { get; set; }






        public GameLogic()
        {

        }
        public GameLogic(string equation, int result)
        {
            this.equation = equation;
            this.result = result;
        }







        public List<GameLogic> getNewRandoms(int lvl)
        {
            Random rand = new Random();
            int i = 0;
            int objective = rand.Next(lvl);
            List<GameLogic> numbers = new List<GameLogic>();
            numbers.Add(new GameLogic(objective.ToString(), objective));
            bool ObjectiveFound = false;
            while (i < 4)
            {

                if (!ObjectiveFound)
                {
                    int number1 = rand.Next(lvl);
                    int number2 = rand.Next(lvl);
                    if ((number1 + number2) == objective)
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number1 + "+" + number2, number1 + number2));

                    }
                    else if ((number1 * number2) == objective)
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number1 + "*" + number2, number1 * number2));

                    }
                    else if (number2 != 0 && number1 >= number2 && number1 % number2 == 0 && ((number1 / number2) == objective))
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number1 + "/" + number2, number1 / number2));
                        continue;
                    }
                    else if ((number1 != 0) && number2 >= number1 && number2 % number1 == 0 && ((number2 / number1) == objective))
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number2 + "/" + number1, number2 / number1));

                    }
                    else if (number1 - number2 == objective)
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number1 + "-" + number2, number1 - number2));

                    }
                    else if (number2 - number1 == objective)
                    {
                        ObjectiveFound = true; i++;
                        numbers.Add(new GameLogic(number2 + "-" + number1, number2 - number1));

                    }
                    continue;
                }
                else
                {
                    string str = "";

                    do
                    {
                        str = getStrings(rand, lvl);
                        if (str.Equals("")) continue;

                        foreach (var items in numbers)
                        {
                            if (items.equation.Equals(str)) continue;
                        }
                        break;
                    } while (true);







                    numbers.Add(new GameLogic(str, getValue(str)));
                    i++;
                }


            }
            return numbers;

        }

        private string getStrings(Random rand, int lvl)
        {
            string str = "";
            int number1 = rand.Next(lvl), number2 = rand.Next(lvl);
            int x = rand.Next(4);
            if (x == 0)
            {
                x = rand.Next(2);
                if (x == 0)
                {
                    str = number1 + "+" + number2;
                }
                else
                {
                    str = number2 + "+" + number1;
                }
            }
            else if (x == 1)
            {
                if (number1 >= number2)
                {
                    str = number1 + "-" + number2;
                }
                else
                {
                    str = number2 + "-" + number1;
                }
            }
            else if (x == 2)
            {
                x = rand.Next(2);
                if (x == 0)
                {
                    str = number1 + "*" + number2;
                }
                else
                {
                    str = number2 + "*" + number1;
                }
            }
            else
            {
                x = rand.Next(2);
                if (x == 0 && number2 != 0)
                {
                    str = number1 + "+" + number2;
                }
                else if (number1 != 0)
                {
                    str = number2 + "+" + number1;
                }
            }
            return str;
        }


        private int getValue(string operation)
        {

            int x = operation[0];
            char oper = operation[1];
            int y = operation[2];
            if (oper == '+')
            {
                return x + y;
            }
            else if (oper == '*')
            {
                return x * y;
            }
            else if (oper == '/')
            {
                return x / y;
            }
            else
            {
                return x - y;
            }
        }
    }
}
