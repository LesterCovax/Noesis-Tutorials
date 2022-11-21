﻿#if NOESIS
using Noesis;
using NoesisApp;
#else
using System;
using System.Windows;
using System.Windows.Controls;
#endif

namespace Buttons
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.WeakReference wr = new System.WeakReference(this);
            this.Initialized += (s, e) => { ((MainWindow)wr.Target).OnInitialized(s, e); };
            this.InitializeComponent();
        }

#if NOESIS
        private void InitializeComponent()
        {
            Noesis.GUI.LoadComponent(this, "/Buttons;component/MainWindow.xaml");
        }
#endif

        private void OnInitialized(object sender, EventArgs args)
        {
            this.DataContext = new ViewModel();
        }
    }
}
