using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Configurations
{
    public class SqlServerConfiguration
    {
        public static string GetConnectionString()
        {
            return @"Data Source=SQL8004.site4now.net;Initial Catalog=db_a8c15d_agendacontatos;User ID=db_a8c15d_agendacontatos_admin;Password=suasenha";
                }
    }
}
