using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Credenciamento
{
    public class Connection
    {
        public string serverConection { get; set; }

        static private string ServerConexao = @"Server=CONTABILIDADE\SQLEXPRESS; DataBase=dbCredencial; Integrated Security=true";
        private SqlConnection Conexao = new SqlConnection(ServerConexao);

        //abrindo a conexão
        public SqlConnection OpenConection()
        {
            if (Conexao.State == ConnectionState.Closed)

                Conexao.Open();
            return Conexao;

        }

        //fechando a conexão
        public SqlConnection ClosedConection()
        {
            if (Conexao.State == ConnectionState.Open)
                Conexao.Close();
            return Conexao;

        }
    }
}
