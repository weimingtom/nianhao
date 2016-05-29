using System;
using System.Collections.Generic;
using System.Text;

namespace nianhao
{
    public class Config
    {
        public static readonly string DB_DATABASE = "nianhao";

        public static string GetDstConstr()
        {
            return String.Format("Data Source={0}.sqlite;Version=3;", Config.DB_DATABASE);
        }
    }
}
