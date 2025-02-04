using AulaSenac.Banco;
using AulaSenac.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaSenac.Controllers
{
    public class CadGeralControllers
    {
        public string comandoSql = "";
        ConexaoBanco banco = new ConexaoBanco();
        CadGeral cadgeral = new CadGeral();
        GravaLog gravaLogAqui = new GravaLog();

        public void Inserir(CadGeral geral)
        {
            try
            {
                banco.Conectar();
                string comandoSql = "Insert into CAD_GERAL(RAZAOSOCIAL,FANTASIA,CNPJ,EMAIL,LOGRADOURO,NUMERO,COMPLEMENTO,BAIRRO,NOME_MUNICIPIO,ESTADO,CEP) values('" + geral.RazaoSocial + "','" + geral.Fantasia + "','" + geral.Cnpj +
                  "','" + geral.Email + "','" + geral.Rua + "','" + geral.Numero + "','" + geral.Complemento + "','" + geral.Bairro + "','" + geral.NomeMunicipio + "','" + geral.Uf +
                  "','" + geral.Cep + "')";
                banco.EnviandoBanco(comandoSql);
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Método Inserir() - tabela CAD_GERAL_SENAC - : " + ex);
                throw new Exception("Erro ao tentar Cadastrar o Cliente: " + ex.Message);
            }
        }


        public void Excluir(CadGeral geral)
        {
            try
            {
                banco.Conectar();
                string comandoSql = "Delete * From CAD_GERAL Where CODCAD= " + geral.CodCadGeral;
                banco.EnviandoBanco(comandoSql);
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Método Excluir() - tabela CAD_GERAL - : " + ex);
                throw new Exception("Erro ao tentar excluir o cliente: " + ex.Message);
            }
        }

        public DataTable ConsultaCadGeral(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                banco.Conectar();
                comandoSql = "SELECT CAD_GERAL.CODCAD CODIGO,CAD_GERAL.RAZAOSOCIAL,CAD_GERAL.FANTASIA,CAD_GERAL.CNPJ,CAD_GERAL.EMAIL,CAD_GERAL.LOGRADOURO,CAD_GERAL.NUMERO,CAD_GERAL.COMPLEMENTO,CAD_GERAL.BAIRRO,CAD_GERAL.CEP,CAD_GERAL.NOME_MUNICIPIO,CAD_GERAL.ESTADO FROM CAD_GERAL WHERE CAD_GERAL.RAZAOSOCIAL LIKE '%" + parametro + "%' ORDER BY RAZAOSOCIAL";
                dt = banco.RetDataTable(comandoSql);
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Método ConsultaCadGeral() - tabela CAD_GERAL - : " + ex);
                throw new Exception("Erro ao tentar Consultar os Clientes: " + ex.Message);
            }
            return dt;
        }


        public DataTable CarregaCadGeral(string parametro)
        {
            DataTable dt = new DataTable();
            try
            {
                banco.Conectar();
                comandoSql = "SELECT CAD_GERAL.CODCAD CODIGO,CAD_GERAL.RAZAOSOCIAL,CAD_GERAL.FANTASIA,CAD_GERAL.CNPJ,CAD_GERAL.EMAIL,CAD_GERAL.LOGRADOURO,CAD_GERAL.NUMERO,CAD_GERAL.COMPLEMENTO,CAD_GERAL.BAIRRO,CAD_GERAL.CEP,CAD_GERAL.NOME_MUNICIPIO,CAD_GERAL.ESTADO FROM CAD_GERAL WHERE CAD_GERAL.CODCAD='" + parametro + "'";
                banco.RetDataReader(comandoSql);
                if (banco.CountSQL > 0)
                {
                    while (banco.dataReader.Read())
                    {
                        cadgeral.CodCadGeral = Convert.ToInt32(banco.dataReader["CODIGO"].ToString());
                        cadgeral.RazaoSocial = banco.dataReader["RAZAOSOCIAL"].ToString();
                        cadgeral.Fantasia = banco.dataReader["FANTASIA"].ToString();
                        cadgeral.Cnpj = banco.dataReader["CNPJ"].ToString();
                        cadgeral.Email = banco.dataReader["EMAIL"].ToString();
                        cadgeral.Rua = banco.dataReader["LOGRADOURO"].ToString();
                        cadgeral.Numero = banco.dataReader["NUMERO"].ToString();
                        cadgeral.Complemento = banco.dataReader["COMPLEMENTO"].ToString();
                        cadgeral.Bairro = banco.dataReader["BAIRRO"].ToString();
                        cadgeral.Cep = banco.dataReader["CEP"].ToString();
                        cadgeral.NomeMunicipio = banco.dataReader["NOME_MUNICIPIO"].ToString();
                        cadgeral.Uf = banco.dataReader["ESTADO"].ToString();
                    }
                }
                banco.dataReader.Close();
                banco.dataReader.Dispose();
            }
            catch (Exception ex)
            {
                gravaLogAqui.grava("Erro no Metodo CarregaCadGeral() - CAD_GERAL : " + ex);
                throw new Exception("Erro ao tentar Consultar o Usuário: " + ex.Message);
            }
            return dt;
        }

    }
}
