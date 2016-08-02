using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace TX.Framework.WindowUI
{
    /// <summary>
    /// ini配置文件操作
    /// </summary>
    /// User:Ryan  CreateTime:2012-8-7 23:23.
    internal class IniConfig
    {
        private string _FilePath;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary> 
        /// 构造方法 
        /// </summary> 
        /// <param name="filePath">文件路径</param> 
        public IniConfig(string filePath)
        {
            _FilePath = filePath;
        }
        /// <summary> 
        /// 写入INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        /// <param name="Value">值</param> 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            if (this.ExistINIFile())
            {
                WritePrivateProfileString(Section, Key, Value, this._FilePath);
            }
            else
            {
                throw new Exception("指定的配置文件读写错误！");
            }
        }
        /// <summary> 
        /// 读出INI文件 
        /// </summary> 
        /// <param name="Section">项目名称(如 [TypeName] )</param> 
        /// <param name="Key">键</param> 
        public string IniReadValue(string Section, string Key, string sdef)
        {
            if (this.ExistINIFile())
            {
                StringBuilder temp = new StringBuilder(500);
                int i = GetPrivateProfileString(Section, Key, sdef, temp, 500, this._FilePath);
                return temp.ToString().Trim().Replace(",",string.Empty);
            }
            else
            {
                throw new Exception("指定的配置文件读写错误！");
            }
        }
        /// <summary> 
        /// 验证文件是否存在 
        /// </summary> 
        /// <returns>布尔值</returns> 
        public bool ExistINIFile()
        {
            //return false;
            return File.Exists(_FilePath);
        }
    }
}
