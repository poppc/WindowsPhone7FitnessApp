using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Windows.Controls.Primitives;
using USA_FitnessApp.Model;
using USA_FitnessApp.Persistence;
using System.IO;
using Microsoft.Phone.Net.NetworkInformation;
using System.Windows.Media.Imaging;
using USA_FitnessApp.Logic;

namespace USA_FitnessApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;

            if (App.MealViewModel.User.Weight == null || App.MealViewModel.User.Weight.WeightValue <= 0)
            {
                weightTxt.Text = "??";
            }
            else
            {
                weightTxt.Text = App.MealViewModel.User.Weight.WeightValue.ToString();
            }
            App.MealViewModel.WeightBox = weightTxt;
        }

        private void BtnAddMeal_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddMealPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void weightTxt_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                User u = App.MealViewModel.User;
                Weight w = u.Weight;
                w.WeightValue = Double.Parse(weightTxt.Text);
                w.Date = DateTime.Now;
                if (!u.WeightSyncedToday)
                {
                    App.MealViewModel.MainLogic.Sync.AddWeightToSync(new Weight { Date = w.Date, WeightValue = w.WeightValue });
                    u.WeightSyncedToday = true;
                }
                u.CalcBmi();
            }
            catch (Exception)
            {
                App.MealViewModel.User.Weight.WeightValue = -1.0;
            }
        }

        private void statusImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(App.MealViewModel.Status.IsReadyToSync())
            {
                App.MealViewModel.MainLogic.Syncronize();
            }
            else if (!App.MealViewModel.Status.IsLoggedIn())
            {
                NavigationService.Navigate(new Uri("/View/PreferencePage.xaml", UriKind.RelativeOrAbsolute));
            }
        }

        private void BtnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/View/AddExercisePage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}