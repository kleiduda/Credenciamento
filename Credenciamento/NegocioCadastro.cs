using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Credenciamento
{
    public class NegocioCadastro
    {
        public static string Register(string cnpj, string nomeFantasia, string estado, string cidade, string nomeCracha, string cpf, string tipo)
        {
            DataCadastro obj = new DataCadastro();
            obj.CNPJ = cnpj;
            obj.NomeFantasia = nomeFantasia;
            obj.Estado = estado;
            obj.Cidade = cidade;
            obj.NomeCracha = nomeCracha;
            obj.CPF = cpf;
            obj.Tipo = tipo;

            return obj.Insert(obj);
        }
    }
}
