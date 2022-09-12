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

        public 
    }
}
