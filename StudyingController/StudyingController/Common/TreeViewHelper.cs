using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using StudyingController.SCS;
using System.Diagnostics.Eventing;
using System.ComponentModel;
using System.Windows.Controls;
using StudyingController.ViewModels;
using System.Windows.Input;
using EntitiesDTO;

namespace StudyingController.Common
{
    public class TreeViewHelper
    {

        private class SingleHandler
        {
            private SingleHandler()
            {
            }

            public static readonly SingleHandler Instance = new SingleHandler();

            public void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> args)
            {
                DependencyObject dp = sender as DependencyObject;
                
                if (dp != null)
                {
                    if ((dp as TreeView).SelectedItem != null && (dp as TreeView).SelectedItem is TreeNode)
                    {
                        BaseEntityDTO entity = ((dp as TreeView).SelectedItem as TreeNode).Tag as BaseEntityDTO;
                        if (entity != null && !entity.IsSameDatabaseObject(GetSelectedEntity(dp)))
                        {
                            SetSelectedEntity(dp, ((dp as TreeView).SelectedItem as TreeNode).Tag as BaseEntityDTO);

                            ICommand command = GetSelectedEntityChangedCommand(dp);
                            if (command != null && command.CanExecute(GetSelectedEntity(dp)))
                                command.Execute(GetSelectedEntity(dp));
                        }
                    }
                    else
                    {
                        SetSelectedEntity(dp, null);
                        ICommand command = GetSelectedEntityChangedCommand(dp);
                        if (command != null && command.CanExecute(GetSelectedEntity(dp)))
                            command.Execute(GetSelectedEntity(dp));
                    }
                }
            }
        }

        public static BaseEntityDTO GetSelectedEntity(DependencyObject obj)
        {
            return (BaseEntityDTO)obj.GetValue(SelectedEntityProperty);
        }

        public static void SetSelectedEntity(DependencyObject obj, BaseEntityDTO value)
        {
            obj.SetValue(SelectedEntityProperty, value);
        }

        public static readonly DependencyProperty SelectedEntityProperty =
            DependencyProperty.RegisterAttached("SelectedEntity", typeof(BaseEntityDTO), typeof(TreeViewHelper), new UIPropertyMetadata(null, OnSelectedEntityChanged));

        private static void OnSelectedEntityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            System.ComponentModel.EventDescriptor ev = TypeDescriptor.GetEvents(d)["SelectedItemChanged"];
            
            if (ev != null && e.OldValue != null)
                ev.RemoveEventHandler(d, new RoutedPropertyChangedEventHandler<object>(SingleHandler.Instance.OnSelectedItemChanged));
            if (ev != null && e.NewValue != null)
            {

                ev.AddEventHandler(d, new RoutedPropertyChangedEventHandler<object>(SingleHandler.Instance.OnSelectedItemChanged));

            }
        }

        public static ICommand GetSelectedEntityChangedCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(SelectedEntityChangedCommandProperty);
        }

        public static void SetSelectedEntityChangedCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(SelectedEntityChangedCommandProperty, value);
        }

        public static readonly DependencyProperty SelectedEntityChangedCommandProperty =
            DependencyProperty.RegisterAttached("SelectedEntityChangedCommand", typeof(ICommand), typeof(TreeViewHelper), new UIPropertyMetadata(null, OnSelectedEntityChanged));
    }
}
