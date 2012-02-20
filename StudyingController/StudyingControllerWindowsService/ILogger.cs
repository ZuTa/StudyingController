using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StudyingControllerWindowsService
{

    public interface ILogger
    {

        void Write(string message, EventLogEntryType messageType);

    }
}
