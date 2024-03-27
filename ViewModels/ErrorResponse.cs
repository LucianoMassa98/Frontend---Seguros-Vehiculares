using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MycGroupApp
{
    internal class ErrorResponse
    {

        public int statusCode { get; set; }
        public string error { get; set; }
        public string message { get; set; }

    }
}
