﻿using System;
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

namespace USA_FitnessApp.Net.JSONParserObject
{
    public class Track_Weight : ListResponse
    {
        public class JSONWeight
        {
            public Double Weight { get; set; }

            public String Date { get; set; }

            public String WeightUnit { get; set; }
        }

        public List<JSONWeight> Items { get; set; }
    }
}
