using DomainApplication;
using DomainApplication.Domain;
using DomainApplication.Models;
using FixInitiator;
using Newtonsoft.Json;
using QuickFix;
using QuickFix.Fields;
using QuickFix.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class IBServices : IibServices
    {
        private ValeursDomain domain;
        public IBServices()
        {
            domain = new ValeursDomain();
        }

        public int verifyClient(string login, String password)
        {
            Client c = domain.getClient(login, password);
            if (c != null) return c.id;
            return 0;
        }

        //Type : 1 pour limite , 2 pour atp
        public void passerOrdreAchatById(int idClient, string codeValeur, string valeur, int type, int qte, decimal prix = 0)
        {
            OrdreAchat o = new OrdreAchat();
            Client c = domain.getClient(idClient);

            o.type = type;
            o.qte = qte;
            o.prix = prix;
            o.code_val = codeValeur;
            o.client = c ;
            o.valeur = valeur;

            domain.saveOrdreAchat(o);

        }

        public void passerOrdreAchat(String login, String password, string codeValeur, string valeur, int type, int qte, decimal prix = 0)
        {
            Client c = domain.getClient(login,password);        
            //appel aux ws pr passer l'ordre
            if (c != null ) this.passerOrdreAchatById(c.id, codeValeur,valeur, type, qte, prix);
        }

        public void passerOrdreVenteById(int idClient, string codeValeur, string valeur, int type, int qte, decimal prix = 0)
        {
            OrdreVente o = new OrdreVente();
            Client c = domain.getClient(idClient);

            o.type = type;
            o.qte = qte;
            o.prix = prix;
            o.code_val = codeValeur;
            o.client = c;
            o.valeur = valeur;

            domain.saveOrdreVente(o);
        }

        public void passerOrdreVente(String login, String password, string codeValeur, string valeur, int type, int qte, decimal prix = 0)
        {
            Client c = domain.getClient(login, password);
            //appel aux ws pr passer l'ordre
            if (c != null) this.passerOrdreVenteById(c.id, codeValeur, valeur, type, qte, prix);
        }

        public double getSoldeById(int idClient)
        {
            Client c = domain.getClient(idClient);
            return (double) c.solde;
        }

        public double GetSolde(String login, string password)
        {
            Client c = domain.getClient(login, password);
            return (double)c.solde;
        }

        public string getClientValeursById(int idClient)
        {
            Client c = domain.getClient(idClient);
            if (c == null) return null;

            List<ValeursClient> valeurs = domain.getClientValeurs(c.id);

            if (valeurs == null)
            {
                return "[{}]";
            }
            else
            {
                string json = JsonConvert.SerializeObject(valeurs);

                return json;
            }
        }

        public string GetClientValeurs(String login, String password)
        {
            Client c = domain.getClient(login, password); // Appel ws pour récupérer le client à partir de son login et son mot de passe

            if (c == null) return null;

            return getClientValeursById(c.id); //Appel ws pour recupérer la liste des valeurs d'un client

        }

        public string GetCotationsJsonList()
        {
            List<Valeur> valeurs = domain.list();

            string json = JsonConvert.SerializeObject(valeurs);

            return json;
        }


        //Type ordre : 1 => Ordre Achat , 2 => Ordre Vente
        public void executerOrdre(long idOrdre, int typeOrdre, int qte, double prix = 0) // WS Orchestrateur bel behi hedha
        {
            Ordre o = domain.getOrdre(idOrdre);
            if (o != null)
            {
                domain.modifierOrdre(o,qte);
                if (typeOrdre == 1)
                {
                    this.debiterSolde(o.id_client, prix * qte); // Appel aux ws pr debiter le solde du client
                }
                else {
                    this.crediterSolde(o.id_client, prix * qte); //Appel aux ws pr crediter le solde du client
                }
            }


        }

        public void debiterSolde(int idClient, double montant)
        {
            Client c = domain.getClient(idClient);

            if (c != null)
            {
                domain.debiterSolde(c.id, montant);            
            }

        }

        public void crediterSolde(int idClient, double montant)
        {
            Client c = domain.getClient(idClient);

            if (c != null)
            {
                domain.crediterSolde(c.id, montant);
            }

        }



        // WS 5ass lel courbe mte3 les valeurs
         public string historiqueValeurs(string code)
        {

            List<Valeur> valeurs = domain.getHistoriqueValeur(code);

            string r = "[";
            foreach (Valeur v in valeurs)
            {
                r += "{ \"date\" : \" " + v.SEANCE + "\" ,  \"cours\" : "+v.COURS_REF+" }  ";
            }
            r = r + "]";

            return r;
        }

    }
}
