﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MenuDemoLibrary
{
    public class DataAccess
    {
        public static string currentDBName = "MenuDemo";
        public static string CnnVal(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
    }
}
