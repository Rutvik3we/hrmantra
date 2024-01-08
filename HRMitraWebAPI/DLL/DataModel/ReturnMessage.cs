using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NDataModel
{
    public class ReturnMesg
    {
        public string status;
        public string Message;

    }
    public class ReturnData
    {
        public string status;
        public object Message;
    }
    public class ReturnMesgs
    {
        public string status;
        public List<string> Message;

    }

}
