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
using System.Collections.Generic;

namespace USA_FitnessApp.Model
{
    public class FitnessHistoryEntry
    {
        public String Date { get; set; }

        public List<Exercise> Exercises { get; set; }
    }
}
