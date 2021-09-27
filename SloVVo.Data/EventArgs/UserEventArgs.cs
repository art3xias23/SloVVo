using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;

namespace SloVVo.Data.EventArgs
{
    public class UserEventArgs : System.EventArgs
    {
        public User User { get; set; }
    }
}
