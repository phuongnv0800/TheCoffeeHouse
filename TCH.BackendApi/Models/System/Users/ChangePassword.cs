using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCH.ViewModel.System.Users
{
    public class ChangePassword
    {
        public string UserName { get; set; }
        public string PasswordOld { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
