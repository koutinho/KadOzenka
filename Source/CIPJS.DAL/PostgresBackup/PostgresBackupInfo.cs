using System;
using System.Collections.Generic;
using System.Text;

namespace CIPJS.DAL.PostgresBackup
{
    public class PostgresBackupInfo
    {
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string WAL { get; set; }
    }
}
