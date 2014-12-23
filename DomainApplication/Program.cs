using DomainApplication.Domain;
using DomainApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            ValeursDomain d = new ValeursDomain();

            //List<Valeur> valeurs = d.list();

            List<ValeursClient> valeurs = d.getClientValeurs("splinter", "splinter");

            if (valeurs == null)
            {
                Console.WriteLine("Valeurs => Null " );
            }
            else
            {
                foreach (ValeursClient v in valeurs)
                {
                    Console.WriteLine(v.valeur + " " + v.qte);
                }
            }
            Console.ReadKey();
            
        }
    }
}
