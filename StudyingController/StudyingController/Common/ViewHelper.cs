using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace StudyingController.Common
{
    public static class ViewHelper
    {
        public static T FindAnchestor<T>(DependencyObject current) where T : DependencyObject
        {
            while (!(current is T) && current!=null)
                current = VisualTreeHelper.GetParent(current);

            return current as T;
        }
    }
}
