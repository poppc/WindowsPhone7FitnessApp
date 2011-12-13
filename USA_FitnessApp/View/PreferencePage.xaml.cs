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
using USA_FitnessApp.ViewModel;
using USA_FitnessApp.Model;

namespace USA_FitnessApp
{
    public partial class PreferencePage : PhoneApplicationPage
    {
        public PreferencePage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;

            App.MealViewModel.MsPicker = msPicker;
            msPicker.ItemsSource = App.MealViewModel.ALL_MEASUREMENT_SYSTEMS;
            if (App.MealViewModel.Preference != null && App.MealViewModel.Preference.MeasurementSystem != null)
                msPicker.SelectedItem = App.MealViewModel.Preference.MeasurementSystem;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Preference p = App.MealViewModel.Preference;
            User u = App.MealViewModel.User;
            p.Username = usernameTxt.Text;
            p.Password = passwordTxt.Password;
            if (!App.MealViewModel.Status.IsLoggedIn() && p.Username.Length > 0 && p.Password.Length > 0)
            {
                App.MealViewModel.MainLogic.Login(p.Username, p.Password);
            }
            p.MeasurementSystem = (MeasurementSystem)msPicker.SelectedItem;

            App.MealViewModel.RegistrationFailedTxt = "";
            NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            App.MealViewModel.RegistrationFailedTxt = "";
            NavigationService.GoBack();
        }
    }
}