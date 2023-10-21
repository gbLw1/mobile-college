using AppFit.Models;

namespace AppFit.Helpers;

public class SQLiteDatabaseHelper
{
    private readonly SQLiteDatabaseHelper _db;

    public SQLiteDatabaseHelper(string path)
    {
        _db = new SQLiteAsyncConnection(path);
        _db.CreateTableAsync<Atividade>().Wait();
    }

    public Task<List<Atividade>> GetAllRows()
    {
        return _db.Table<Atividade>()
          .OrderByDescending(a => a.Id)
          .ToListAsync();
    }

    public Task<Atividade> GetById(int id)
    {
        return _db.Table<Atividade>()
          .FirstAsync(a => a.Id == id);
    }

    public Task<int> Insert(Atividade atividade)
    {
        return _db.Insert(atividade);
    }

    public Task<Atividade> Update(Atividade atividade)
    {
        string sql = $"UPDATE Atividade " +
                $"SET Descricao=?, Data=?, Peso=?, " +
                $"Observacoes=? WHERE Id=?";
        return _db.QueryAsync<Atividade>(sql,
            atividade.Descricao, atividade.Data, atividade.Peso,
            atividade.Observacoes, atividade.Id);
    }

    public Task<int> Delete(int id)
    {
        return _db.Table<Atividade>().DeleteAsync(a => a.Id == id);
    }

    public Task<List<Atividade>> Search(string str)
    {
        string sql = $"SELECT * FROM Atividade WHERE Descricao LIKE '%{str}%'";
        return _db.QueryAsync<Atividade>(sql);
    }
}
