using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Models
{
    public class Receita
    {
        public int Id { get; set; }
        public string Decricao { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataDeRecebimento { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
    }
}