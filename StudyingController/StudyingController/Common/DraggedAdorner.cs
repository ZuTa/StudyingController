using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Documents;
using System.Windows.Controls;
using System.Windows;

namespace StudyingController.Common
{
    class DraggedAdorner:Adorner
    {
        private ContentPresenter contentPresenter;
        private double left;
        private double top;
        private AdornerLayer adornerLayer;


        public DraggedAdorner(object dragDropData, DataTemplate dragDropTemplate, UIElement adornerElement, AdornerLayer adornerLayer)
            : base(adornerElement)
        {
            this.adornerLayer = adornerLayer;

            contentPresenter = new ContentPresenter();
            contentPresenter.Content = dragDropData;
            /*contentPresenter.ContentTemplate = dragDropTemplate;
            contentPresenter.Opacity = 0.7;*/
            adornerLayer.Add(this);
        }

        public void SetPosition(double left, double top)
        {
            this.left = left;
            this.top = top;
            if (adornerLayer != null)
            {
                adornerLayer.Update(AdornedElement);
            }
        }

        public void Detach()
        {
            adornerLayer.Remove(this);
        }

    }
}
