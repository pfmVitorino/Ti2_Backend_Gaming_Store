using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GamingStore_Projectoti2.Models
{
    public class Clientes 
    {
        // vai representar os dados da tabela dos Clientes

        // criar o construtor desta classe
        // e carregar a lista dos Jogos

        public Clientes()
        {
            ListaDeCompras = new HashSet<Compras>();
        }

        [Key]
        public int Id { get; set; }


       [Required]
        public string Nome { get; set; }

        [Required]
        public string NIF { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Morada { get; set; }

        [Required]
        public string CodPostal { get; set; }


        // Um  Cliente  pode comprar 1 ou mais Jogos
        public ICollection<Compras> ListaDeCompras { get; set; }

      
    }
}