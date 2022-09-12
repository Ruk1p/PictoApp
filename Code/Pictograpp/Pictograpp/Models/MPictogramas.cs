using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Pictograpp.Models
{
    public class MPictogramas
    {
        [PrimaryKey,AutoIncrement]
        public int CodPicto { get; set; }

        [NotNull,MaxLength(100)]
        public string NomPicto { get; set; }

        [NotNull,MaxLength(100)]
        public string TextoPicto { get; set; }

        [NotNull]
        public int CodCat { get; set; }

    }
}
