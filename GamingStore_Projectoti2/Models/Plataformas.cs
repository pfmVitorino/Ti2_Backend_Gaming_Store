using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    public class Plataformas
    {

        // representa os dados da tabela da plataforma

        // criar o construtor desta classe
        // e carregar a lista de Jogos na respetiva plataforma
        public Plataformas()
        {
            ListaDeJogos = new HashSet<Detalhes_Compra>();
        }
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }


        // especificar que um Jogo pode pertecer a uma ou mais plataformas
        public ICollection<Detalhes_Compra> ListaDeJogos { get; set; }


    }
}