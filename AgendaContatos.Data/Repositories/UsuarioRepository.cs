using AgendaContatos.Data.Configurations;
using AgendaContatos.Data.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Repositories
{
    public class UsuarioRepository
    {
        
        public void Create(Usuario usuario)
        {
            var sql = @"
                INSERT INTO USUARIO (
                    IDUSUARIO,
                    NOME,
                    EMAIL,
                    SENHA,
                    DATACADASTRO)

                VALUES(
                    @IdUsuario,
                    @Nome,
                    @Email,
                    CONVERT(VARCHAR(32), HASHBYTES('MD5', @Senha), 2),
                    @DataCadastro)            
            ";
            using(var connection = new SqlConnection(SqlServerConfiguration.GetConnectionString()))
            {
                connection.Execute(sql, usuario);
            }
        }
    }
}
