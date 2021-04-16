using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR16
{
    class PostClass
    {
        public string patient { get; set; }

        public List<ServiceCode> services { get; set; }
    }


    class ServiceCode
    {
        public ServiceCode(int code)
        {
            this.serviceCode = code;
        }

        public int serviceCode { get; set; }
    }
}
