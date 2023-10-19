using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerStudent.DTO
{
    public class Response
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object Data {  get; set; }
        public Response() { }
        public Response(bool status, string message, object data) {  Status = status; Message = message; Data = data; }
    }
}
