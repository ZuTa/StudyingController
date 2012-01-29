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
    /// Interaction logic for FacultyView.xaml
    /// </summary>
    public partial class FacultyView : UserControl
    {
        public FacultyView()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = ViewHelper.FindAnchestor<ListBoxItem>(sender as DependencyObject);
            item.IsSelected = true;
        }

        private void lbSpecializations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VerifyActions();
        }

        private void VerifyActions()
        {
            btnAdd.IsEnabled = btnRemove.IsEnabled = lbSpecializations.SelectedItem != null;
        }
    }
}
