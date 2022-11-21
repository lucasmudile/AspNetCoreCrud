using System;

namespace FN.Store.Domain.Entities
{
    public abstract class _Entity
    {
        public DateTime DataCriacao { get; set; } =  DateTime.Now;
        public DateTime DataAlteracao { get; set; }= DateTime.Now;
    }
}
