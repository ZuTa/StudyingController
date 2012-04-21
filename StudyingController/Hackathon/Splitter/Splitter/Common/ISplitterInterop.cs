using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelDTO;

namespace StudyingController.Common
{
    public interface ISplitterInterop
    {
        Splitter.SS.SplitterServiceClient Service { get; }

        Splitter.SS.Session Session { get; set; }

        SystemUserDTO User { get; }

        event EventHandler SessionChanged;
    }
}
