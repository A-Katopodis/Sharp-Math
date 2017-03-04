using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Settings : Page
    {
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public int HighScore, NewDifficutly;
        public Settings()
        {
            HighScore = (int)localSettings.Values["HighScore"];
            this.InitializeComponent();
            setOpacity((int)localSettings.Values["Difficulty"]);


        }
        private void Easy_Click(object sender, RoutedEventArgs e)
        {
            setOpacity(10);
            NewDifficutly = 10;
        }

        private void Normal_Click(object sender, RoutedEventArgs e)
        {
            setOpacity(20);
            NewDifficutly = 20;
        }

        private void Hard_Click(object sender, RoutedEventArgs e)
        {
            setOpacity(30);
            NewDifficutly = 30;

        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainMenuDesktop));
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GameModeDesktop));
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            localSettings.Values["Difficulty"] = NewDifficutly;
        }


        public void setOpacity(int diff)
        {
            if (diff == 10)
            {
                Normal.Opacity = 1;
                Hard.Opacity = 1;
                Easy.Opacity = 0.5;
            }
            else if (diff == 20)
            {
                Normal.Opacity = 0.5;
                Hard.Opacity = 1;
                Easy.Opacity = 1;
            }
            else
            {
                Normal.Opacity = 1;
                Hard.Opacity = 0.5;
                Easy.Opacity = 1;
            }

        }
    }
}
