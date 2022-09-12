using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using Pictograpp.Models;
using System.Threading.Tasks;

namespace Pictograpp.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<MCategorias>().Wait();
        }
        /// <summary>
        /// Guardar categorias en la base de datos
        /// </summary>
        /// <param name="Cate"></param>
        /// <returns></returns>
        public Task <int> SaveCatAsync(MCategorias Cate)
        {
            if (Cate.CodCat !=0)
            {
                return db.UpdateAsync(Cate);
            }
            else
            {
                return db.InsertAsync(Cate);
            }
        }

        public Task<int> DeleteCatAsync(MCategorias Cate)
        {
            return db.DeleteAsync(Cate);
        }

        /// <summary>
        /// Mostrar todas los categorias en la base de datos
        /// </summary>
        /// <returns>Todas las categorias</returns>
        public Task<List<MCategorias>> GetCategoriasAsync()
        {
            return db.Table<MCategorias>().ToListAsync();
        }

        /// <summary>
        /// Muestra las categorias por el cod que le demos
        /// </summary>
        /// <param name="codCat"></param>
        /// <returns></returns>
        public Task<MCategorias> GetCategoriasByCodAsync(int codCat)
        {
            return db.Table<MCategorias>().Where(a => a.CodCat == codCat).FirstOrDefaultAsync();
        }

    }
}
