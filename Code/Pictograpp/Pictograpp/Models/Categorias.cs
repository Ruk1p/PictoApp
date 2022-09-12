using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pictograpp.Models
{
    public class Categorias
    {
        [PrimaryKey,NotNull,AutoIncrement]
        public int CodCat { get; set; }

        [NotNull]
        public string NomCat { get; set; }

    }
}
