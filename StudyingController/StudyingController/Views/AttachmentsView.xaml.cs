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
    /// Interaction logic for AttachmentView.xaml
    /// </summary>
    public partial class AttachmentsView : UserControl
    {
        public AttachmentsView()
        {
            InitializeComponent();
        }

        private void GotFocus(object sender, RoutedEventArgs e)
        {
            ListViewItem item = ViewHelper.FindAnchestor<ListViewItem>(sender as DependencyObject);
            ListView lv = ViewHelper.FindAnchestor<ListView>(sender as DependencyObject);
            item.IsSelected = true;
        }
    }
}
