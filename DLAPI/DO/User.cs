using DLAPI.DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class User
    {
        public string UsserName { get; set; }
        public string Password { get; set; }
        public bool Admit { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
