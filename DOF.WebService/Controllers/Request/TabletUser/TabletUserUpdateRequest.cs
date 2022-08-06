﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DOF.WebService.Controllers.Request.TabletUser
{
    public class TabletUserUpdateRequest
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MiddleName { get; set; }
    }
}
