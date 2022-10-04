using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using wpfBDtesto.ViewModel;

namespace wpfBDtesto.View
{
    /// <summary>
    /// Логика взаимодействия для InsertEventWindow.xaml
    /// </summary>
    public partial class InsertEventWindow : Window
    {
        public InsertEventWindow()
        {
            InitializeComponent();
            DataContext = new AppVM();

        }
    }
}
