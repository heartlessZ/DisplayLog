/****************************************************************
* MACHINE: DESKTOP-KFVHQEM
* NAME: boomclub
* CREATEDATE: 2019/2/27 10:14:56
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DisplayLog.Web.Services
{
    /// <summary>
    /// Summary description for SV
    /// </summary>
    public static class FileHelper
    {
        public static byte[] GetBuffer(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }
            return File.ReadAllBytes(filePath);
        }
    }
}
