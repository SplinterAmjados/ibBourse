using DomainApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication.Domain
{
    public class ValeursDomain
    {

        IBEntitiesConnection context;

        public ValeursDomain()
        {
            this.context = new IBEntitiesConnection();
        }


        public List<Valeur> list()
        {
            return context.valeurs.Where(e => e.SEANCE == "2013-01-02").ToList();
        }


        public List<ValeursClient> getClientValeurs(String login, String pw)
        {

            Client client = context.Clients.Where(c => c.login == login && c.password == pw).FirstOrDefault();

            if (client != null)
            {
                return client.valeurs_client.ToList();
            }

            return null;
        }

        public String getSolde(string login, string pw)
        {
            Client client = context.Clients.Where(c => c.login == login && c.password == pw).FirstOrDefault();

            if (client != null)
            {
                return client.solde.ToString();
            }

            return null;
        }

        public Valeur getValeur(int cod_val)
        {
            return null;
          //  return context.valeurs.Find(cod_val);
        }

        public Valeur getValeurByName(string name)
        {
            return null;
           // return context.valeurs.Where(e => e.nom == name).First();
        }

    }
}
