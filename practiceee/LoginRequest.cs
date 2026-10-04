using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practiceee
{
    public class LoginRequest
    {
        public string username { get; set; } = "";
        public string pass_word { get; set; } = "";
    }

    public class LoginResponse
    {
        public string Message { get; set; } = "";
        public int UserId { get; set; }
        public string username { get; set; } = "";
    }
}
