using FN.Store.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace FN.StoreApi.Models
{
    public class CategoriasGet
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

    }

    public class CategoriaAddEdit{

        [Required(ErrorMessage = "campo obrigatório!")]
        [StringLength(100, ErrorMessage = "limite de caractereres excedido")]
        public string Nome { get; set; }

        [StringLength(300, ErrorMessage = "limite de caractereres excedido")]
        public string Descricao { get; set; }
    
    }

    public static class CategoriasModelExtensions
    {
        public static CategoriasGet TotCategoriaGet(this Categoria data)
        {
            return new CategoriasGet
            {
                Id = data.Id,
                Nome = data.Nome,
                Descricao = data.Descricao
            };
        }

        public static Categoria ToCategoria(this CategoriaAddEdit model) =>
            new Categoria
            {
                Nome=model.Nome,
                Descricao=model.Descricao
            }; 
       
    }
}
