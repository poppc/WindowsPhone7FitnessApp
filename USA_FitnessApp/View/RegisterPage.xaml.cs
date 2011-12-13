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
using USA_FitnessApp.Net.JSONParserObject;
using USA_FitnessApp.Model;

namespace USA_FitnessApp.View
{
    public partial class RegisterPage : PhoneApplicationPage
    {
        public RegisterPage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            Registration reg = new Registration();
            reg.Username = usernameTxt.Text;
            reg.Password_1 = password1Txt.Password;
            reg.Password_2 = password2Txt.Password;
            reg.Email_1 = email1Txt.Text;
            reg.Email_2 = email2Txt.Text;
            reg.Gender = ((String)genderPicker.SelectedItem).Substring(0, 1);
            reg.Unit = ((MeasurementSystem)msPicker.SelectedItem).ToString().Substring(0, 2).ToLower();
            reg.Weight = (int)Double.Parse(weightTxt.Text);
            reg.Height = (int)Double.Parse(heightTxt.Text);
            reg.Birthdate = ((DateTime)birthdayPicker.Value).ToString("yyyy-MM-dd");
            App.MealViewModel.MainLogic.Register(reg);

            Preference p = App.MealViewModel.Preference;
            p.Username = reg.Username;
            p.Password = reg.Password_1;
            p.MeasurementSystem = (MeasurementSystem)msPicker.SelectedItem;
            App.MealViewModel.MsPicker.SelectedItem = p.MeasurementSystem;

            User u = App.MealViewModel.User;
            if (u.Weight == null)
                u.Weight = new Weight();
            u.Weight.WeightValue = reg.Weight;
            u.Weight.Date = DateTime.Now;
            u.Height = reg.Height;
            u.CalcBmi();
            App.MealViewModel.WeightBox.Text = u.Weight.WeightValue.ToString();
            u.WeightSyncedToday = true;

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