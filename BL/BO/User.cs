﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL.BO;
using BO;

namespace BO
{
    class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Admit { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
