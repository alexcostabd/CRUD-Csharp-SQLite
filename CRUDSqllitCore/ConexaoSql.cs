using System.Data;
using System.Data.SQLite;
using System;

namespace CRUDSqllitCore
{
    public class ConexaoSql
    {
        private static SQLiteConnection SqlConexao;

               
        //CONSTRUTOR DA CLASSE
        public ConexaoSql()
        {
            //conn = new SQLiteConnection(@"Data Source = C:\Users\Alex Costa\CadastroFunc.bd; Version=3;");
        }

        private static SQLiteConnection conectaBanco()
        {
            SqlConexao = new SQLiteConnection(@"Data Source = C:\TempTangram\Temp\CRUD\SqliteBD\CadastroFunc.db; Version=3;");
            SqlConexao.Open();
            return SqlConexao;
        }

        public static DataTable pegaFuncionario(string cpf)
        {
            SQLiteDataAdapter dados = null;
            DataTable dadostabela = new DataTable();
            try
            {
                using(var cmd = conectaBanco().CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM CadastroFunc WHERE CDO_cpf =" + cpf;
                    dados = new SQLiteDataAdapter(cmd.CommandText, conectaBanco());
                    dados.Fill(dadostabela);
                    return dadostabela;
                }

            }
            catch (SQLiteException erro)
            {

                throw erro;
            }
            
        }

        public static void inserir(Funcionario funcionario)
        {
            try
            {
                using (var cmd = conectaBanco().CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO CadastroFunc(CDO_Nome, CDO_Cpf, CDO_Funcao, CDO_DatContratacao, CDO_Telefone) VALUES(@nome, @cpf, @funcao, @datcontratacao, @telefone)";

                    cmd.Parameters.AddWithValue("@nome", funcionario.nome);
                    cmd.Parameters.AddWithValue("@cpf", funcionario.cpf);
                    cmd.Parameters.AddWithValue("@funcao", funcionario.funcao);
                    cmd.Parameters.AddWithValue("@datcontratacao", funcionario.dataContratacao);
                    cmd.Parameters.AddWithValue("@telefone", funcionario.telefone);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }            

        }


        public static void alterar(Funcionario funcionario)
        {
            
            try
            {
                using (var cmd = conectaBanco().CreateCommand())
                {
                    if (funcionario.cpf != null)
                    {
                        cmd.CommandText = "UPDATE CadastroFunc SET CDO_Nome = @nome, CDO_Cpf = @cpf, CDO_Funcao = @funcao, CDO_DatContratacao = @datcontratacao, CDO_Telefone=@telefone WHERE CDO_Cpf = @cpf";

                        cmd.Parameters.AddWithValue("@nome", funcionario.nome);
                        cmd.Parameters.AddWithValue("@cpf", funcionario.cpf);
                        cmd.Parameters.AddWithValue("@funcao", funcionario.funcao);
                        cmd.Parameters.AddWithValue("@datcontratacao", funcionario.dataContratacao);
                        cmd.Parameters.AddWithValue("@telefone", funcionario.telefone);
                        cmd.ExecuteNonQuery();
                    }
                    
                };

            }
            catch (SQLiteException erro)
            {

                throw erro;
            }            
        }


        public static void deletar(string cpf)
        {
            var funcionario = new Funcionario();
            try
            {
                using (var cmd = conectaBanco().CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CadastroFunc WHERE CDO_Cpf =" + cpf;

                    cmd.Parameters.AddWithValue("@cpf", funcionario.cpf);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException erro)
            {

                throw erro;
            }            
        }       
    }
}
