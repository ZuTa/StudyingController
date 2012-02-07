using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StudyingController.Common;

namespace StudyingController.Views
{
    /// <summary>
    /// Interaction logic for CathedraView.xaml
    /// </summary>
    public partial class CathedraView : UserControl
    {
        public CathedraView()
        {
            InitializeComponent();
        }

        private void lbSubjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VerifyActions();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = ViewHelper.FindAnchestor<ListBoxItem>(sender as DependencyObject);
            item.IsSelected = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            VerifyActions();
        }

        private void VerifyActions()
        {
            btnRemove.IsEnabled = lbSubjects.SelectedItem != null;
        }

    }
}
