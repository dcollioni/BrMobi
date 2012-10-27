using System.Collections.Generic;

namespace BrMobi.Core.Service
{
    public class OperationStatus
    {
        public bool Success { get; set; }
        public IList<string> Messages { get; set; }

        public OperationStatus()
        {
            Messages = new List<string>();
        }

        public OperationStatus(bool success)
        {
            Messages = new List<string>();
            Success = success;
        }
    }
}
