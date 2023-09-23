using AppListaCompras.Models;
using SQLite;

namespace AppListaCompras.Helpers;

public class SQLiteDatabaseHelper
{
    readonly SQLiteAsyncConnection _conn;

    public SQLiteDatabaseHelper(string path)
    {
        _conn = new SQLiteAsyncConnection(path);
        _conn.CreateTableAsync<Produto>().Wait();
    }

    public async Task<int> Insert(Produto p)
        => await _conn.InsertAsync(p);

    public Task<List<Produto>> Update(Produto produto)
    {
        string sql = "UPDATE Produto SET Descricao=?, Quantidade=?, Preco=? WHERE id=?";
        return _conn.QueryAsync<Produto>(sql, produto.Descricao, produto.Quantidade, produto.Preco, produto.Id);
    }

    public Task<List<Produto>> GetAll()
        => _conn.Table<Produto>().ToListAsync();

    public Task<List<Produto>> GetByQueryString(string query)
        => _conn.Table<Produto>()
            .Where(p => p.Descricao.Contains(query))
            .ToListAsync();

    public Task<List<Produto>> Search(string q)
    {
        string sql = "SELECT * FROM Produto WHERE Descricao LIKE '%" + q + ")";
        return _conn.QueryAsync<Produto>(sql);
    }

    public Task<int> Delete(int id)
        => _conn.Table<Produto>().DeleteAsync(i => i.Id == id);
}