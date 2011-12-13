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
    public class Login : GeneralResponse
    {
        public int UserId { get; set; }

        public string Auth { get; set; }
    }
}
