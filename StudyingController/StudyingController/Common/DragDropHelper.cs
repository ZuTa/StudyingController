using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using System.Collections;

namespace StudyingController.Common
{
    class DragDropHelper
    {
        #region Fields & Properties

        private  DataFormat dataFormat = DataFormats.GetDataFormat("DragDropItemsControl");
        private object draggedData;
        private DraggedAdorner draggedAdorner;
        private Window topWindow;

        private ItemsControl sourceItemsControl;
        private FrameworkElement sourceItemContainer;

        private ItemsControl targetItemsControl;
        //private FrameworkElement targetItemContainer;

        private static DragDropHelper instance;
        public static DragDropHelper Instance
        {
            get
            {
                if (instance == null)
                    instance = new DragDropHelper();
                return instance;
            }
        }

        public static readonly DependencyProperty IsDragSourceProperty = DependencyProperty.RegisterAttached("IsDragSource",typeof(bool),typeof(DragDropHelper),new UIPropertyMetadata(false, IsDragSourceChanged));

        public static readonly DependencyProperty DragDropTemplateProperty = DependencyProperty.RegisterAttached("DragDropTemplate", typeof(DataTemplate), typeof(DragDropHelper), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsDropTargetProperty = DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDropTargetChanged));

        #endregion

        #region Methods

        public static bool GetIsDragSource(DependencyObject obj, bool value)
        {
            return (bool)obj.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }

        public static DataTemplate GetDragDropTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DragDropTemplateProperty);
        }

        public static void SetDragDropTemplate(DependencyObject obj, DataTemplate value)         
        {
           obj.SetValue(DragDropTemplateProperty, value);
        }

        public static bool GetIsDropTarget(DependencyObject obj)
        {
           return (bool)obj.GetValue(IsDropTargetProperty);
        }

        public static void SetIsDropTarget(DependencyObject obj, bool value)
        {
           obj.SetValue(IsDropTargetProperty, value);
        }

        private static void IsDropTargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dropTarget = obj as ItemsControl;
            if (dropTarget != null)
            {
                if (Object.Equals(e.NewValue, true))
                {
                    dropTarget.AllowDrop = true;
                    dropTarget.PreviewDrop += Instance.dropTarget_PreviewDrop;
                    dropTarget.PreviewDragEnter += Instance.dropTarget_PreviewDragEnter;
                    dropTarget.PreviewDragOver += Instance.dropTarget_PreviewDragOver;
                    dropTarget.PreviewDragLeave += Instance.dropTarget_PreviewDragLeave;
                }
                else
                {
                    dropTarget.AllowDrop = false;
                    dropTarget.PreviewDrop -= Instance.dropTarget_PreviewDrop;
                    dropTarget.PreviewDragEnter -= Instance.dropTarget_PreviewDragEnter;
                    dropTarget.PreviewDragOver -= Instance.dropTarget_PreviewDragOver;
                    dropTarget.PreviewDragLeave -= Instance.dropTarget_PreviewDragLeave;
                }
            }
        }

        private void dropTarget_PreviewDragLeave(object sender, DragEventArgs e)
        {
            object draggedItem = e.Data.GetData(dataFormat.Name);
            //if (draggedItem != null)
            //{
            //}
            e.Handled = true;
        }

        private void dropTarget_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (targetItemsControl != sourceItemsControl)
            {
            object draggedItem = e.Data.GetData(dataFormat.Name);

            if (draggedItem != null)
            {
                ShowDraggedAdorner(e.GetPosition(topWindow)); 
            }
            }
            else
            {
                ShowDraggedAdorner(e.GetPosition(topWindow));
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void dropTarget_PreviewDragEnter(object sender, DragEventArgs e)
        {
            
            targetItemsControl = (ItemsControl)sender;
            if (targetItemsControl != sourceItemsControl)
            {
                object draggedItem = e.Data.GetData(dataFormat.Name);

                if (draggedItem != null)
                {
                    ShowDraggedAdorner(e.GetPosition(topWindow));
                }
            }
            else
            {
                ShowDraggedAdorner(e.GetPosition(topWindow));
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void dropTarget_PreviewDrop(object sender, DragEventArgs e)
        {
            object draggedItem = e.Data.GetData(dataFormat.Name);
            if (draggedItem != null)
            {
                if ((e.Effects & DragDropEffects.Move) != 0)
                {
                    RemoveItemFromItemsControl(sourceItemsControl, draggedItem);
                }

                InsertItemInItemsControl(targetItemsControl, draggedItem);
                RemoveDraggedAdorner();
            }
            e.Handled = true;
        }

        private static void IsDragSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dragSource = obj as ItemsControl;
            if(dragSource != null)
            {
                if(Object.Equals(e.NewValue,true))
                {
                    dragSource.PreviewMouseLeftButtonDown+=Instance.dragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp+=Instance.dragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove+=Instance.dragSource_PreviewMouseMove;
                }
                else
                {
                    dragSource.PreviewMouseLeftButtonDown-=Instance.dragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp-=Instance.dragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove-=Instance.dragSource_PreviewMouseMove;
                }
            }
        }

        private void  dragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (draggedData != null)
            {
                DataObject data = new DataObject(dataFormat.Name, draggedData);
                bool previousAllowDrop = topWindow.AllowDrop;
                topWindow.AllowDrop = true;
                topWindow.DragEnter += topWindow_DragEnter;
                topWindow.DragOver += topWindow_DragOver;
                topWindow.DragLeave += topWindow_DragLeave;

                DragDropEffects effects = DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Move);

                RemoveDraggedAdorner();

                topWindow.AllowDrop = previousAllowDrop;
                topWindow.DragEnter -= topWindow_DragEnter;
                topWindow.DragOver -= topWindow_DragOver;
                topWindow.DragLeave -= topWindow_DragLeave;

                draggedData = null;
            }
        }

        private void topWindow_DragLeave(object sender, DragEventArgs e)
        {
            RemoveDraggedAdorner(); 
            e.Handled = true;
        }

        private void topWindow_DragOver(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(topWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void topWindow_DragEnter(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(topWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void  dragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            draggedData = null;
        }

        private void  dragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
 	        sourceItemsControl = (ItemsControl)sender;
            Visual visual = e.OriginalSource as Visual;

            topWindow = Window.GetWindow(sourceItemsControl);

            sourceItemContainer = sourceItemsControl.ContainerFromElement(visual) as FrameworkElement;
            if(sourceItemContainer!=null)
            {
                draggedData = sourceItemContainer.DataContext;
            }
        }

        private void RemoveDraggedAdorner()
        {
            if (draggedAdorner != null)
            {
                draggedAdorner.Detach();
                draggedAdorner = null;
            }
        }

        private void ShowDraggedAdorner(Point currentPoint)
        {
            if (draggedAdorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(sourceItemsControl);
                draggedAdorner = new DraggedAdorner(draggedData, GetDragDropTemplate(sourceItemsControl), sourceItemContainer, adornerLayer);
            }
            draggedAdorner.SetPosition(currentPoint.X, currentPoint.Y);
        }

        private void RemoveItemFromItemsControl(ItemsControl itemsControl,object itemToRemove)
        {
            if(itemToRemove!=null)
            {
                int indexToRemove = itemsControl.Items.IndexOf(itemToRemove);
                if (indexToRemove != -1)
                {
                    IEnumerable itemsSource = itemsControl.ItemsSource;
                    if (itemsSource is IList)
                    {
                        ((IList)itemsSource).RemoveAt(indexToRemove);
                    }

                }
            }
        }

        private void InsertItemInItemsControl(ItemsControl itemsControl, object itemToInsert)
        {
            if (itemToInsert != null)
            {
                IEnumerable itemsSource = itemsControl.ItemsSource;
                if (itemsSource is IList)
                {
                    ((IList)itemsSource).Add(itemToInsert);
                }
            }
        }

        #endregion

        #region Event


        #endregion

    }
}
