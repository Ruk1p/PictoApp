using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pictograpp.Models
{
    [Table("Categorias")]
    public class MCategorias
    {
        [PrimaryKey,NotNull,AutoIncrement]
        public int CodCat { get; set; }

        [NotNull]
        public string NomCat { get; set; }

    }
}
