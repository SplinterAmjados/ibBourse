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

        public Client getClient(int id)
        {
            return this.context.Clients.Find(id);
        }

        public Client getClient(string login, string password)
        {
            try
            {
                return this.context.Clients.Where(c => c.login == login && c.password == password).FirstOrDefault();
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("here");
                return null;
            }
        }



        public List<Valeur> list()
        {
            
            try
            {
                return context.valeurs.Where(e => e.SEANCE == "2013-01-02").ToList();
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public List<ValeursClient> getClientValeurs(int idClient)
        {

            Client client = context.Clients.Find(idClient);

            if (client != null)
            {
                return client.valeurs_client.ToList();
            }

            return null;
        }

        public Decimal getSolde(int idClient)
        {
            Client client = context.Clients.Find(idClient);

            if (client != null)
            {
                return (decimal) client.solde;
            }

            return 0;
        }


        public void debiterSolde(int idClient,double montant)
        {

            Client c = context.Clients.Find(idClient);

            if (c != null)
            {
                if (c.solde >= montant)
                {
                    c.solde -= montant;
                    context.SaveChanges();
                }
            
            }

        }

        public void crediterSolde(int idClient, double montant)
        {

            Client c = context.Clients.Find(idClient);

            if (c != null)
            {
                    c.solde += montant;
                    context.SaveChanges();
            }

        }

        public Ordre getOrdre(long id)
        {
            Ordre or = context.OrdreAchats.Find(id);
            if (or == null) or = context.OrdreVentes.Find(id);
            return or;
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

        public List<Valeur> getHistoriqueValeur(string code)
        {
            return context.valeurs.Where(e => e.CODE == code).OrderByDescending(e => e.SEANCE ).Take(6).ToList();
        }


        public void modifierOrdre(Ordre o,int qte)
        {
            Ordre or = this.getOrdre(o.id);
            or.executer(qte);
            context.SaveChanges();
        }

        public void saveOrdreAchat(OrdreAchat o)
        {
            context.OrdreAchats.Add(o);
            context.SaveChanges();
        }

        public void saveOrdreVente(OrdreVente o)
        {
            context.OrdreVentes.Add(o);
            context.SaveChanges();
            
        }

        public string getType(int id)
        {
           Client c = context.Clients.Find(id);
           return c.type ;
        }

    }
}
