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
            db.CreateTableAsync<Categorias>().Wait();
        }
        /// <summary>
        /// Guardar categorias en la base de datos
        /// </summary>
        /// <param name="Cate"></param>
        /// <returns></returns>
        public Task <int> SaveCatAsync(Categorias Cate)
        {
            if (Cate.CodCat==0)
            {
                return db.InsertAsync(Cate);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Mostrar todas los categorias en la base de datos
        /// </summary>
        /// <returns>Todas las categorias</returns>
        public Task<List<Categorias>> GetCategoriasAsync()
        {
            return db.Table<Categorias>().ToListAsync();
        }

        /// <summary>
        /// Muestra las categorias por el cod que le demos
        /// </summary>
        /// <param name="codCat"></param>
        /// <returns></returns>
        public Task<Categorias> GetCategoriasByCodAsync(int codCat)
        {
            return db.Table<Categorias>().Where(a => a.CodCat == codCat).FirstOrDefaultAsync();
        }

    }
}
