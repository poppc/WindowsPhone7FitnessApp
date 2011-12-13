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

namespace USA_FitnessApp.View
{
    public partial class FitnessHistoryPage : PhoneApplicationPage
    {
        public FitnessHistoryPage()
        {
            InitializeComponent();

            DataContext = App.MealViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (startDatePicker.Value != null && endDatePicker.Value != null)
            {
                App.MealViewModel.FitnessHistoryErrorTxt = "";
                App.MealViewModel.MainLogic.RequestFitnessHistory((DateTime)startDatePicker.Value, (DateTime)endDatePicker.Value);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}