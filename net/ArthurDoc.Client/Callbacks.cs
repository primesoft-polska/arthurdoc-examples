using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArthurDoc.Client
{
    public class Callbacks
    {
        public delegate void ArthurDocJobCommunicationStatus(string status, string color);
    }
}
