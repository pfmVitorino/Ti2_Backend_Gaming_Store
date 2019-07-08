using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    // Classe responsável pelo carrinho de compras
    public class Carrinho
    {
        [Key]
        public int Id { get; set; }

        // associar o carrinho a um cliente
        [Display(Name = "Cliente")]
        public string ClienteUserName { get; set; }
       
        // associar o jogo ao carrinho
        public int JogoFk { get; set; }

        [ForeignKey("JogoFk")]
        public virtual Jogos Jogos { get; set; }

        public int Quantidade { get; set; }
    }
     

}