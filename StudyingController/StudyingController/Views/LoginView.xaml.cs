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
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl, IPasswordSource
    {
        public LoginView()
        {
            InitializeComponent();

            this.DataContextChanged += new DependencyPropertyChangedEventHandler(LoginView_DataContextChanged);
        }

        void LoginView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IPasswordConsumer old = e.OldValue as IPasswordConsumer;
            if (old != null)
                old.PasswordSource = null;
            IPasswordConsumer _new = e.NewValue as IPasswordConsumer;
            if (_new != null)
                _new.PasswordSource = this;
        }

        public string GetPassword()
        {
            return password.Password;
        }

        public void SetPassword(string password)
        {
            this.password.Password = password;
        }
    }
}
