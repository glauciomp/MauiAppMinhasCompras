using MauiAppMinhasCompras.Models;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _db;

        public SQLiteDatabaseHelper(string path)
        {
            _db = new SQLiteAsyncConnection(path);
            _db.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return _db.InsertAsync(p);
        }

        public Task<List<Produto>> GetAll()
        {
            return _db.Table<Produto>().OrderBy(i => i.Id).ToListAsync();
        }

        public Task<int> Update(Produto p)
        {
            return _db.UpdateAsync(p);
        }

        public Task<int> Delete(int id)
        {
            return _db.DeleteAsync(new Produto { Id = id });
        }

        public Task<int> DeleteAll()
        {
            return _db.DeleteAllAsync<Produto>();
        }

        // Busca por descrição
        public Task<List<Produto>> Search(string q)
        {
            string lowercaseQuery = q.ToLowerInvariant();
            return _db.Table<Produto>()
                      .Where(i => i.Descricao.ToLower().Contains(lowercaseQuery))
                      .OrderBy(i => i.Descricao)
                      .ToListAsync();
        }

        // Busca por categoria
        public Task<List<Produto>> SearchByCategory(string q)
        {
            string lowercaseQuery = q.ToLowerInvariant();
            return _db.Table<Produto>()
                      .Where(i => i.Categoria.ToLower().Contains(lowercaseQuery))
                      .OrderBy(i => i.Categoria)
                      .ToListAsync();
        }
    }
}