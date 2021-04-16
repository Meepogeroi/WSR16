using System.Collections.Generic;

namespace WSR16.Pages
{
    class GetClass
    {
        public string patient { get; set; }

        public List<Code> services { get; set; }
    }

    class Code
    {
        public Code(int code, string result)
        {
            this.code = code;
            this.result = result;
        }
        public int code { get; set; }
        public string result { get; set; }
    }
    //{ “patient”: “{id}”, “services”: [{ “сode”: 000, “result”: “” },
}




/*
  { “patient”: “{id}”, “services”: [{ “сode”: 000, “result”: “” }, { “code”: 000,“result”: “” }, ….] }
 */