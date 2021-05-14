using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
namespace WebServices.Models
{
    
    public class ServerConexion
    {       
        public ServerConexion()
        {
            init();          
        }

        public string init() 
        {
<<<<<<< HEAD
            return "Server=localhost; Port = 5431;User Id=postgres;Password = 090917; Database = SmartHome";
=======
            return "Server=localhost; Port = 5432;User Id=postgres;Password = 090917; Database = SmartHome";
>>>>>>> parent of 6edb36a (Merge branch 'API_Main' into WebApplication)
        }
    }
}
