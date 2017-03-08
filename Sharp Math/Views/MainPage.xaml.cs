using Sharp_Math.Views;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Sharp_Math
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        public bool phone;
        public MainPage()
        {
            if (localSettings.Values.Count < 2)
            {
                localSettings.Values["Difficulty"] = 10;
                localSettings.Values["HighScore"] = 0;
            }
            this.InitializeComponent();
            if (Windows.System.Profile.AnalyticsInfo.VersionInfo.DeviceFamily == "Windows.Mobile")
            {
                Change.Visibility = Visibility.Collapsed;
                Device.Text = "Phone";
                Board.Navigate(typeof(MainMenu));
                phone = true;
            }
            else
            {
                Device.Text = "Phone";
                Board.Navigate(typeof(MainMenuDesktop));
                phone = false;
            }
            Listbox.SelectedIndex = 0;



        }

        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            if (HamburgerMenu.IsPaneOpen)
            {
                HamburgerMenu.IsPaneOpen = false;
            }else
            {
                HamburgerMenu.IsPaneOpen = true;
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                if(phone)
                {
                    Device.Text = "Phone";
                    Board.Navigate(typeof(MainMenu));
                }
                else
                {
                Device.Text = "Desktop";
                Board.Navigate(typeof(MainMenuDesktop));
                }
                
            }
            else if (Setting.IsSelected)
            {
                if (phone)
                {
                    Board.Navigate(typeof(Settings));
                }else
                {
                    Board.Navigate(typeof(SettingsDesktop));
                }
            }
            
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            if (!phone)
            {
                Device.Text = "Phone";
                Board.Navigate(typeof(MainMenuDesktop));
                
            }
            else
            {
                Device.Text = "Desktop";
                Board.Navigate(typeof(MainMenu));
            }
            phone = !phone;
            Listbox.SelectedIndex = 0;
        }
    }
}
