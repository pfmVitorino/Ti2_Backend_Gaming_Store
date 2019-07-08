using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    public class Jogos
    {
        public Jogos()
    {
        // inicialização da lista de Vendas de um jogo
        Compras = new HashSet<Detalhes_Compra>();

    }

    [Key]
    public int Id { get; set; }



    [Required]
    [StringLength(30)]
    [Display(Name = "Jogo")]
    public string Nome { get; set; }

    [Required]
    [Display(Name = "Preço")]
    public decimal Preco { get; set; }


    public string Fotografia { get; set; }


    [Required]
    [StringLength(30)]
    public string Plataforma { get; set; }



    // um Jogo pode ser várias vezes comprado
    public virtual ICollection<Detalhes_Compra> Compras { get; set; }


}
}