using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace USA_FitnessApp.Net.SyncObjects
{
    public class ExerciseSync
    {
        public String Date { get; set; }

        public int Id { get; set; }

        public int TimeFrame { get; set; }

        public int Amount { get; set; }

        public String Intensity { get; set; }
    }
}
