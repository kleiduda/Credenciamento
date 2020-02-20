using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Credenciamento
{
    public class DataCadastro
    {
        public int ID { get; set; }
        public string CNPJ { get; set; }
        public string NomeFantasia { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Tipo { get; set; }
        public string NomeCracha { get; set; }
        public string CPF { get; set; }


        public DataCadastro() { 
        
        }

        public DataCadastro(int iD, string cNPJ, string nomeFantasia, string estado, string cidade, string tipo, string nomeCracha, string cPF)
        {
            ID = iD;
            CNPJ = cNPJ;
            NomeFantasia = nomeFantasia;
            Estado = estado;
            Cidade = cidade;
            Tipo = tipo;
            NomeCracha = nomeCracha;
            CPF = cPF;
        }

        //
        protected Connection Con = new Connection();
        protected SqlCommand Comando = new SqlCommand();
        protected SqlDataReader LerLinhas;


        //metodo para inserir cadastro
        public string Insert(DataCadastro Cadastro)
        {
            string rpta = "";

            try
            {
                //Código
                Comando.Connection = Con.OpenConection();
                Comando.CommandText = "Register";
                Comando.CommandType = CommandType.StoredProcedure;

                SqlParameter parId = new SqlParameter();
                parId.ParameterName = "@id";
                parId.SqlDbType = SqlDbType.Int;
                parId.Direction = ParameterDirection.Output;
                Comando.Parameters.Add(parId);

                SqlParameter parCnpj = new SqlParameter();
                parCnpj.ParameterName = "@cnpj";
                parCnpj.SqlDbType = SqlDbType.VarChar;
                parCnpj.Size = 18;
                parCnpj.Value = Cadastro.CNPJ;
                Comando.Parameters.Add(parCnpj);

                SqlParameter parNomeFantasia = new SqlParameter();
                parNomeFantasia.ParameterName = "@nome_fantasia";
                parNomeFantasia.SqlDbType = SqlDbType.VarChar;
                parNomeFantasia.Size = 150;
                parNomeFantasia.Value = Cadastro.NomeFantasia;
                Comando.Parameters.Add(parNomeFantasia);

                SqlParameter parEstado = new SqlParameter();
                parEstado.ParameterName = "@estado";
                parEstado.SqlDbType = SqlDbType.VarChar;
                parEstado.Size = 150;
                parEstado.Value = Cadastro.Estado;
                Comando.Parameters.Add(parEstado);

                SqlParameter parCidade = new SqlParameter();
                parCidade.ParameterName = "@cidade";
                parCidade.SqlDbType = SqlDbType.VarChar;
                parCidade.Size = 150;
                parCidade.Value = Cadastro.Cidade;
                Comando.Parameters.Add(parCidade);

                SqlParameter par = new SqlParameter();
                parCidade.ParameterName = "@cidade";
                parCidade.SqlDbType = SqlDbType.VarChar;
                parCidade.Size = 150;
                parCidade.Value = Cadastro.Cidade;
                Comando.Parameters.Add(parCidade);

                SqlParameter parNomeCracha = new SqlParameter();
                parNomeCracha.ParameterName = "@nome_cracha";
                parNomeCracha.SqlDbType = SqlDbType.VarChar;
                parNomeCracha.Size = 150;
                parNomeCracha.Value = Cadastro.NomeCracha;
                Comando.Parameters.Add(parNomeCracha);

                SqlParameter parCpf = new SqlParameter();
                parCpf.ParameterName = "@cpf";
                parCpf.SqlDbType = SqlDbType.VarChar;
                parCpf.Size = 150;
                parCpf.Value = Cadastro.CPF;
                Comando.Parameters.Add(parCpf);

                SqlParameter parTipo = new SqlParameter();
                parTipo.ParameterName = "@tipo";
                parTipo.SqlDbType = SqlDbType.VarChar;
                parTipo.Size = 150;
                parTipo.Value = Cadastro.Tipo;
                Comando.Parameters.Add(parTipo);


                rpta = Comando.ExecuteNonQuery() == 1 ? "OK" : "Erro ao Inserir Registro (cadastro)";


            }
            catch (Exception ex)
            {
                rpta = ex.Message;
            }
            finally
            {
                Comando.Connection = Con.ClosedConection();
            }
            return rpta;

        }

        public DataTable ListCadastro()
        {
            DataTable DtResult = new DataTable("cadastro");

            try
            {
                Comando.Connection = Con.OpenConection();
                Comando.CommandText = "ListOrders";
                Comando.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter SqlDat = new SqlDataAdapter(Comando);
                SqlDat.Fill(DtResult);

            }
            catch (Exception ex)
            {
                DtResult = null;
            }
            return DtResult;

        }
    }
}
