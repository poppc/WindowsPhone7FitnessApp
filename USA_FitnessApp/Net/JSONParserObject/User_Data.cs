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
    public class User_Data : GeneralResponse
    {
        public String JoinDate { get; set; }

        public String Gender { get; set; }

        public String Birthday { get; set; }

        public int Height { get; set; }

        public Double Weight { get; set; }

        public String Unit { get; set; }
    }
}
