using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    public class Detalhes_Compra
    {
        
        [Key]
        public int Id { get; set; }

        [Required]

        public int Quantidade { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }

        [Display(Name = "Plataforma")]
        public int PlataformasFK { get; set; }
        [ForeignKey("PlataformasFK")]
        public virtual Plataformas Plataformas { get; set; }


        public int JogosFK { get; set; }
        [ForeignKey("JogosFK")]
        public virtual Jogos Jogos { get; set; }


        public int ComprasFK { get; set; }
        [ForeignKey("ComprasFK")]
        public virtual Compras Compras { get; set; }


    } 
    
}