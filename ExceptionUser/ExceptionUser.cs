using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.ExceptionUser
{
    class ExceptionUser : Exception
    {
        public override string Message => base.Message;
        public ExceptionUser(string Message) : base(Message) { }
    }
}
