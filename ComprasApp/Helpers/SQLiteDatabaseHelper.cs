using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComprasApp.Models;
using SQLite;

namespace ComprasApp.Helpers {
    // Classe responsável por gerenciar operações de banco de dados SQLite de forma assíncrona.
    public class SQLiteDatabaseHelper
    {
        // Cria um atributo do tipo de conexão assíncrona com o banco de dados SQLite.
        readonly SQLiteAsyncConnection _conn;

        // Construtor que recebe o caminho do banco de dados e inicializa a conexão.
        public SQLiteDatabaseHelper(string path)
        {
            _conn = new SQLiteAsyncConnection(path); // Cria uma nova conexão com o banco de dados no caminho especificado.
            _conn.CreateTableAsync<Produto>().Wait(); // Cria a tabela 'Produto' se ela não existir, espera a conclusão da operação.
        }

        // Método para inserir um novo produto no banco de dados.
        public Task<int> Insert(Produto p)
        {
            return _conn.InsertAsync(p); // Insere o produto de forma assíncrona.
        }

        // Método para atualizar um produto.
        public Task<int> Update(Produto p)
        {
            /*
            // O código SQL comentado é uma alternativa para realizar a atualização manualmente.
            string sql = "UPDATE Produto SET Descricao = ?, Quantidade = ?, Preco = ? WHERE Id = ?";
            return _conn.QueryAsync<Produto>(sql, p.Descricao, p.Quantidade, p.Preco, p.Id);
            */
            return _conn.UpdateAsync(p); // Atualiza o produto de forma assíncrona.
        }

        // Método para recuperar todos os produtos do banco de dados.
        public Task<List<Produto>> GetAll()
        {
            return _conn.Table<Produto>().ToListAsync(); // Retorna uma lista de todos os produtos de forma assíncrona.
        }

        // Método para excluir um produto pelo seu ID.
        public Task<int> Delete(int id)
        {
            return _conn.Table<Produto>().DeleteAsync(i => i.Id == id); // Exclui o produto com o ID especificado de forma assíncrona.
        }

        // Método para buscar produtos com base em uma descrição que contém a string de busca.
        public Task<List<Produto>> Search(string q)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + "%'"; // Consulta SQL para buscar produtos que correspondem à descrição.

            return _conn.QueryAsync<Produto>(sql); // Executa a consulta de forma assíncrona e retorna a lista de produtos encontrados.
        }
    }
}
