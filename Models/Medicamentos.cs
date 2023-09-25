using System.ComponentModel.DataAnnotations;

namespace APIExemplo.Models
{
    public class Medicamentos
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set;}
        [Required]
        public string Lote { get; set; }
        [Required]
        public int MesVencimento { get; set; }
        [Required]
        public int AnoVencimento { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Fabricante { get; set; }
    }
}
