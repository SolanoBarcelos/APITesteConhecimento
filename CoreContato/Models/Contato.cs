using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreContato.Models
{
    [Table("contatos")]
    public class Contato
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("pk_contato")]
        public int id_contato { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        [Column("nome_contato")]
        public string nome_contato { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "O telefone deve ter no máximo 11 caracteres.")]
        [Column("telefone_contato")]
        public string telefone_contato { get; set; }

        public Contato(string nome_contato, string telefone_contato)
        {
            this.nome_contato = nome_contato;
            this.telefone_contato = telefone_contato;
        }

        public Contato() { }
    }
}

