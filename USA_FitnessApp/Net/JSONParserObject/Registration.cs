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

namespace USA_FitnessApp.Net.JSONParserObject
{
    public class Registration
    {
        public String Username { get; set; }

        public String Password_1 { get; set; }

        public String Password_2 { get; set; }

        public String Email_1 { get; set; }

        public String Email_2 { get; set; }

        public String Gender { get; set; }

        public String Unit { get; set; }

        public int Weight { get; set; }

        public int Height { get; set; }

        public String Birthdate { get; set; }
    }
}
