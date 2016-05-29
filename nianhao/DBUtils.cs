using System;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Data.SQLite;

namespace nianhao
{
    public class DBUtils
    {
        public static string GetString(SQLiteDataReader reader, string name)
        {
            int ordinal = reader.GetOrdinal(name);
            if (reader.IsDBNull(ordinal))
            {
                return null;
            }
            else
            {
                return reader.GetString(ordinal);
            }
        }

        public static void AddWithValue(SQLiteCommand cmd, string name, string value)
        {
            cmd.Parameters.AddWithValue(name, (value != null) ? (object)value : DBNull.Value);
        }

        public static void AddToJObject(JObject obj, string prefix, object bean)
        {
            Regex AutoPropertyNamePattern = new Regex("<(.*)>k__BackingField", RegexOptions.Compiled);
            Type type = bean.GetType();
            FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.FlattenHierarchy);
            foreach (FieldInfo field in fields)
            {
                string propertyName = field.Name;
                Match match = AutoPropertyNamePattern.Match(field.Name);
                if (match.Success)
                {
                    propertyName = match.Result("$1");
                }
                PropertyInfo property = type.GetProperty(propertyName);
                object objValue = null;
                if (property != null)
                {
                    objValue = property.GetValue(bean, null);
                }
                else
                {
                    objValue = field.GetValue(bean);
                }
                obj.Add(prefix + propertyName, new JValue(objValue));
            }
        }


        public static string ToMD5(string str)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            return BitConverter.ToString(mD5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", "").ToLower();
        }
    }
}
