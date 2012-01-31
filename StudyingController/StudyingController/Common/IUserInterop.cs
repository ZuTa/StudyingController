using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudyingController.Common
{
    public enum MessageButtons
    {
        OK,
        OKCancel,
        YesNo,
        YesNoCancel
    }

    public enum MessageResults
    {
        OK,
        Yes,
        No,
        Cancel,
    }

    public enum MessageTypes
    {
        Error,
        Information,
        Question
    }

    public interface IUserInterop
    {
        void ShowMessage(string text);

        MessageResults ShowMessage(string message, string caption, MessageButtons buttons, MessageTypes type);
    }
}
