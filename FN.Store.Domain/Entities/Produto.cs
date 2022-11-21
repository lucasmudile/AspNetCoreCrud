using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FN.Store.Domain.Entities
{
    public class Produto:_Entity
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

        public void Update(string nome, decimal preco, int categoriaId)
        {
            Nome = nome;
            Preco = preco;
            CategoriaId = categoriaId; 
        }
    }
}
