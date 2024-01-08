using System;
using System.Collections.Generic;

namespace NDataModel
{
    public class APIResponseModel
    {
        public int ResponseStatus { get; set; } = 0;

        public string Message { get; set; } = null;

        public object Data { get; set; } = null;
    }
}
