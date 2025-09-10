using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Models
{
    public class MetaOrcamento
    {
        public int Id { get; set; }
        public decimal ValorMaximo { get; set; }
        public string MesAno { get; set; }

        public Categoria CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}