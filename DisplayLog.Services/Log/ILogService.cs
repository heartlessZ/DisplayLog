/****************************************************************
* MACHINE: DESKTOP-KFVHQEM
* NAME: boomclub
* CREATEDATE: 2019/2/27 10:28:57
* DESC: <DESCRIPTION>
* **************************************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DisplayLog.Services.Log
{
    /// <summary>
    /// Summary description for ILogService
    /// </summary>
    public interface ILogService : IDependency
    {
        byte[] GetBuffer(string clientName, string fileName);

        List<string> GetFilesName(string clientName);
    }
}
