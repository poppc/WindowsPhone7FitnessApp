﻿#pragma checksum "C:\Users\Ezio\documents\visual studio 2010\Projects\USA_FitnessApp\USA_FitnessApp\View\WeightHistoryPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "C967E2DD6DF04FF4EE82DF4055B15664"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace USA_FitnessApp.View {
    
    
    public partial class WeightHistoryPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal Microsoft.Phone.Controls.DatePicker startDatePicker;
        
        internal Microsoft.Phone.Controls.DatePicker endDatePicker;
        
        internal System.Windows.Controls.TextBlock ErrorTxt;
        
        internal System.Windows.Controls.ListBox weightListBox;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/USA_FitnessApp;component/View/WeightHistoryPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.startDatePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("startDatePicker")));
            this.endDatePicker = ((Microsoft.Phone.Controls.DatePicker)(this.FindName("endDatePicker")));
            this.ErrorTxt = ((System.Windows.Controls.TextBlock)(this.FindName("ErrorTxt")));
            this.weightListBox = ((System.Windows.Controls.ListBox)(this.FindName("weightListBox")));
        }
    }
}

