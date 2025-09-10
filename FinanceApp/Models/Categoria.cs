using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Models
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Tipo { get; set; }

        public ICollection<Despesa> Despesas { get; set; } = new List<Despesa>();
        public ICollection<MetaOrcamento> MetaOrcamentos { get; set; } = new List<MetaOrcamento>();
        public ICollection<Receita> Receitas { get; set; } = new List<Receita>();
    }
}