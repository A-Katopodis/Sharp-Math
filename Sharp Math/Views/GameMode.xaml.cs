using Sharp_Math.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Sharp_Math.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GameMode : Page
    {



        

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        DispatcherTimer timer = new DispatcherTimer();
        double count = 6;
        public List<Buttons> randombuttons = new List<Buttons>();
        public int  TimesRefreshed,highScore, currentScore, lvl, lives;

        public GameMode()
        { 
            highScore = (int)localSettings.Values["HighScore"];
            lvl = (int)localSettings.Values["Difficulty"];
            highScore = 10;
            lvl = 10;
            currentScore = 0;
            TimesRefreshed = 0;
            this.InitializeComponent();
            lives = 1;
            Lives.Text =lives.ToString();
            CurrentScore.Text=currentScore.ToString();
            RightProgress.Value = count;
            RightProgress.Maximum = count;
            LeftProgress.Value = count;
            LeftProgress.Maximum = count;
            HighScore.Text = highScore.ToString();
            InitData(lvl);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            timer.Tick += tick;
            timer.Start();
        }

        

        

        public void Refresh()
        {
            TimesRefreshed++;

            if (count >= 1)
            {
                currentScore += 13;
            }else if (count>=1.5)
            {
                currentScore += 15;
            }
            else
            {
                currentScore += 10;
            }

            if (count > 2)
            {
                
                count -= 1;
            }else
            {
                count = 2;
            }
            if (TimesRefreshed == 9)
            {
                lvl+=10;
            }else if (TimesRefreshed == 19)
            {
                lvl+=10;
            }
            updateData(lvl);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            RightProgress.Value = count;
            LeftProgress.Value = count;
            RightProgress.Maximum = count;
            LeftProgress.Maximum = count;
            RightProgress.Foreground = new SolidColorBrush(Colors.Black);
            LeftProgress.Foreground = new SolidColorBrush(Colors.Black);
            
            timer.Start();
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

        private void one_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if (randombuttons[1].result == randombuttons[0].result)
            {
                one.Foreground = new SolidColorBrush(Colors.Green);
                Refresh();
            }
            else
            {
                one.Foreground = new SolidColorBrush(Colors.Red);
                LoseGame();
            }
        }

        private void two_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if (randombuttons[2].result == randombuttons[0].result)
            {
                two.Foreground = new SolidColorBrush(Colors.Green);
                Refresh();
            }
            else
            {
                two.Foreground = new SolidColorBrush(Colors.Red);
                LoseGame();
            }
        }

        private void three_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if (randombuttons[3].result == randombuttons[0].result)
            {
                three.Foreground = new SolidColorBrush(Colors.Green);
                Refresh();
            }
            else
            {
                three.Foreground = new SolidColorBrush(Colors.Red);
                LoseGame();
            }
        }

        private void four_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            if (randombuttons[4].result == randombuttons[0].result)
            {
                four.Foreground = new SolidColorBrush(Colors.Green);
                Refresh();
            }
            else
            {
                four.Foreground = new SolidColorBrush(Colors.Red);
                LoseGame();
            }
        }

        void tick(object sender, object e)
        {

            updateTextBlock();

        }

        private void updateTextBlock()
        {
            count -= 0.1;
            LeftProgress.Value -= 0.1;
            RightProgress.Value -= 0.1;
            string temp = count.ToString();
            if (temp.Length > 3) temp = temp.Substring(0, 3);
            bool isRed = false;
            if (!isRed && count <= 1) {
                LeftProgress.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                RightProgress.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
            }
            if (count <= 0)
            {
                timer.Stop();
                LeftProgress.Value = 0;
                RightProgress.Value = 0;
                LoseGame();
            }

        }



        private void LoseGame()
        {
            if (lives != 0)
            {
                lives--;
                Refresh();
                Lives.Text = lives.ToString();
            }else
            {
                if (highScore < currentScore) localSettings.Values["HighScore"] = currentScore;
                one.IsEnabled = false;
                two.IsEnabled = false;
                three.IsEnabled = false;
                four.IsEnabled = false;
                Information.Visibility = Visibility.Collapsed;
                LosingScreen.Visibility = Visibility.Visible;
                ObjectiveGrid.Opacity = 0.5;
            }
            
        }



        public void InitData(int lvl)
        {
            getNewRandoms(lvl);
            randomizePositions();
            assignButtons();
        }

        public void assignButtons()
        {
            Objective.Text = randombuttons[0].equation;
            CurrentScore.Text = currentScore.ToString();
            if (highScore <= currentScore) HighScore.Text = highScore.ToString();
            Mbtl.Text = randombuttons[1].equation;
            Mbtr.Text = randombuttons[2].equation;
            Mbbl.Text = randombuttons[3].equation;
            Mbbr.Text = randombuttons[4].equation;
        }

       /* private string getStrings(Random rand, int lvl)
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
        }*/

        public void updateData(int lvl)
        {
            RightProgress.Value = count;
            LeftProgress.Value = count;
            getNewRandoms(lvl);
            randomizePositions();
            assignButtons();
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainMenu));
        }



        private void TryAgain_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameMode));
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        public void randomizePositions()
        {
            Random rand = new Random();

            int x, y, counter = rand.Next(10, 20);
            do
            {
                x = rand.Next(1, 5); y = rand.Next(1, 5);
                Swap(randombuttons, x, y);
                counter--;
            } while (counter != 0);
        }

        public List<Buttons> Swap(List<Buttons> list, int indexA, int indexB)
        {
            Buttons tmp = list[indexA];
            list[indexA] = randombuttons[indexB];
            list[indexB] = tmp;
            return list;
        }

        public void getNewRandoms(int lvl)
        {
            Random rand = new Random();
            int i = 0;
            int objective = rand.Next(lvl);
            if (randombuttons != null)
            {
                randombuttons.Clear();
            }
            else
            {
                randombuttons = new List<Buttons>();
            }
            randombuttons.Add(new Buttons(objective.ToString(), objective));
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
                        randombuttons.Add(new Buttons(number1 + "+" + number2, number1 + number2));

                    }
                    else if ((number1 * number2) == objective)
                    {
                        ObjectiveFound = true; i++;
                        randombuttons.Add(new Buttons(number1 + "*" + number2, number1 * number2));

                    }
                    else if (number2 != 0 && number1 >= number2 && number1 % number2 == 0 && ((number1 / number2) == objective))
                    {
                        ObjectiveFound = true; i++;
                        randombuttons.Add(new Buttons(number1 + "/" + number2, number1 / number2));
                        continue;
                    }
                    else if ((number1 != 0) && number2 >= number1 && number2 % number1 == 0 && ((number2 / number1) == objective))
                    {
                        ObjectiveFound = true; i++;
                        randombuttons.Add(new Buttons(number2 + "/" + number1, number2 / number1));

                    }
                    else if (number1 - number2 == objective)
                    {
                        ObjectiveFound = true; i++;
                        randombuttons.Add(new Buttons(number1 + "-" + number2, number1 - number2));

                    }
                    else if (number2 - number1 == objective)
                    {
                        ObjectiveFound = true; i++;
                        randombuttons.Add(new Buttons(number2 + "-" + number1, number2 - number1));

                    }
                    continue;
                }
                else
                {
                    string str = "";

                    do
                    {
                        str = "";
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

                        if (str.Equals("")) continue;

                        foreach (var items in randombuttons)
                        {
                            if (items.equation.Equals(str)) continue;
                        }
                        break;
                    } while (true);
                    randombuttons.Add(new Buttons(str, getValue(str)));
                    i++;
                }
            }
        }








    }
}
