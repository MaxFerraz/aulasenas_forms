using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AulaSenac.Banco
{
    public class ConexaoBanco
    {
        public MySqlConnection connection;
        public DataTable dataTable;
        public DataSet dataSet;
        public MySqlDataAdapter dataAdapter;
        public MySqlDataReader dataReader;
        public MySqlCommandBuilder commandBuilder;
        public int CountSQL;

        private String server = "127.0.0.1";
        private String porta = "3306";
        private String user = "root";
        private String password = "Max922366";
        private String database = "projetoaulasenac";

        //metodo conectar() realiza a conexao com o bancode dados, onde sao passados o servidor do banco, usuario, senha e nome do Banco
        public void Conectar()
        {
            if (connection != null)
            {
                connection.Close();
            }
            string connectionStr = String.Format("Server={0}; Port={1}; User Id={2}; Password={3}; Database={4};", server, porta, user, password, database);
            //MessageBox.Show("Banco: " + connectionStr, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            try
            {
                connection = new MySqlConnection(connectionStr);
                connection.Open();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //metodo ExecutaSQL é usado toda que vez que realiza algum insert, update, delete ou consulta no banco
        public void EnviandoBanco(string comandoSql)
        {
            MySqlCommand comando = new MySqlCommand(comandoSql, connection);
            comando.ExecuteNonQuery();
            connection.Close();

        }

        //Retorna um objeto do tipo dataTable e espera um comando sql de consulta
        public DataTable RetDataTable(string sql)
        {
            dataTable = new DataTable();
            dataAdapter = new MySqlDataAdapter(sql, connection);
            commandBuilder = new MySqlCommandBuilder(dataAdapter);
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        //retorna um dataReader com os dados da consulta no banco
        public void RetDataReader(string sql)
        {
            dataSet = new DataSet();
            dataSet.Reset();
            MySqlCommand comando = new MySqlCommand(sql, connection);
            dataAdapter = new MySqlDataAdapter();
            dataAdapter.SelectCommand = comando;
            dataAdapter.Fill(dataSet);
            CountSQL = dataSet.Tables[0].Rows.Count;
            dataReader = comando.ExecuteReader();
            //return dataReader;
        }

        public List<string> ListDataReader(string sql)
        {
            List<string> uf = new List<string>();

            MySqlCommand comando = new MySqlCommand(sql, connection);
            dataReader = comando.ExecuteReader();
            while (dataReader.Read())
            {
                uf.Add(dataReader["CODUF"].ToString());
            }

            return uf;
        }

        public string AcertaDatas(DateTime Data)
        {
            string retData;
            try
            {
                //retData = String.Format(Data.Date.Day.ToString(), "00") + "/" + String.Format(Data.Date.Month.ToString(), "00") + "/" + String.Format(Data.Date.Year.ToString(), "0000");
                //retData = String.Format(Data.Date.Year.ToString(), "0000") + "/" + String.Format(Data.Month.ToString(), "00") + "/" + String.Format(Data.Day.ToString(), "00");
                retData = Data.ToString("yyyy-MM-dd");
            }
            catch (Exception ex)
            {
                retData = "";
            }
            return retData;
        }
    }
}
