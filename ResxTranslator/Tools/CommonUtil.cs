using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Principal;

namespace ResxTranslator.Tools
{
    public class CommonUtil
    {
        #region Other


        /// <summary>
        /// 执行程序
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static bool ExecuteProgram(string filename, string[] args)
        {
            try
            {
                string arguments = "";
                foreach (string arg in args)
                {
                    arguments += $"\"{arg}\" ";
                }
                arguments = arguments.Trim();
                Process process = new();
                ProcessStartInfo startInfo = new(filename, arguments);
                process.StartInfo = startInfo;
                process.Start();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 枚举信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Dictionary<string, T> GetEnumList<T>()
            where T : Enum
        {
            var dict = new Dictionary<string, T>();
            List<T> list = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            list.ForEach(x =>
            {
                Type type = x.GetType();
                string name = Enum.GetName(type, x);
                if (name != null)
                {
                    FieldInfo field = type.GetField(name);
                    if (field != null)
                    {
                        DescriptionAttribute attr =
                            Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                        if (attr != null)
                        {
                            dict.Add(attr.Description, x);
                        }
                    }
                }              
            });
            return dict;
        }

        /// <summary>
        /// 是否为管理员权限
        /// </summary>
        /// <returns></returns>
        public static bool IsUserAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new(identity);

            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// 计算文件大小函数(保留两位小数),Size为字节大小
        /// </summary>
        /// <param name="Size">初始文件大小</param>
        /// <returns></returns>
        public static string CountSize(long Size)
        {
            string result = "";
            long FactSize = 0;
            FactSize = Size;
            if (FactSize < 1024.00)
                result = FactSize.ToString("F2") + " Byte";
            else if (FactSize >= 1024.00 && FactSize < 1048576)
                result = (FactSize / 1024.00).ToString("F2") + " KB";
            else if (FactSize >= 1048576 && FactSize < 1073741824)
                result = (FactSize / 1024.00 / 1024.00).ToString("F2") + " MB";
            else if (FactSize >= 1073741824)
                result = (FactSize / 1024.00 / 1024.00 / 1024.00).ToString("F2") + " GB";
            return result;
        }

        #endregion Other
    }
}
