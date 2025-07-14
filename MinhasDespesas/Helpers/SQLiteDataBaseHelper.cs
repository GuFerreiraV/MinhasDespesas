using MinhasDespesas.Models;
using SQLite;

namespace MinhasDespesas.Helpers
{
    public class SQLiteDataBaseHelper
    {

        readonly SQLiteAsyncConnection _connectionAsync;

        public SQLiteDataBaseHelper(string Path) 
        {
            _connectionAsync = new SQLiteAsyncConnection(Path);
            // cria a tabela Produto se não existir no db
            _connectionAsync.CreateTableAsync<Produto>().Wait();

        }

        public Task<int> Insert(Produto prod) 
        {
            return _connectionAsync.InsertAsync(prod);
        }

        public Task<List<Produto>> Update(Produto prod)
        {
            string sql = "UPDATE Produto SET Descricao=?, QtdeProduto=?, ValorProduto=? WHERE Id=?";
            return _connectionAsync.QueryAsync<Produto>(
                sql,
                prod.Descricao,
                prod.QtdeProduto,
                prod.ValorProduto,
                prod.Id
                );
        }

        public Task<int> Delete(int ID)
        {
            return  _connectionAsync.Table<Produto>().DeleteAsync(i => i.Id == ID);
        }

        public Task<List<Produto>> GetAll() 
        {
            return _connectionAsync.Table<Produto>().ToListAsync();
        }

        public Task<List<Produto>> Search(string query)
        {
            string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%"+ query + "%' ";
            return _connectionAsync.QueryAsync<Produto>(sql);
        }

    }
}
