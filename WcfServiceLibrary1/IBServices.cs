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

        private SocketInitiator initiator;
        private QuickFix.SessionSettings settings;
        private Initiator application;
        private QuickFix.IMessageStoreFactory storeFactory;
        private QuickFix.ILogFactory logFactory;
        private int orderID = 123;

        public IBServices()
        {
            try
            {
                settings = new QuickFix.SessionSettings(@"C:\Users\Splinter\Documents\visual studio 2012\Projects\AcceptorFix\AcceptorFix\initiator.cfg");
                application = new Initiator();
                storeFactory = new QuickFix.FileStoreFactory(settings);
                logFactory = new QuickFix.ScreenLogFactory(settings);
                initiator = new QuickFix.Transport.SocketInitiator(application, storeFactory, settings, logFactory);
                application.MyInitiator = initiator;

                initiator.Start();
                
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }


        ~IBServices()
        {
            try { }
            catch (Exception e) {
                initiator.Stop();
            }
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public string GetCotationsJsonList()
        {
            ValeursDomain domain = new ValeursDomain();
            List<Valeur> valeurs = domain.list();

            string json = JsonConvert.SerializeObject(valeurs);

            return json;
        }

        public string GetClientValeurs(String login, String Password)
        {
            ValeursDomain domain = new ValeursDomain();
            List<ValeursClient> valeurs = domain.getClientValeurs(login, Password);

            if (valeurs == null)
            {
                return "[{}]" ;
            }
            else
            {
                string json = JsonConvert.SerializeObject(valeurs);

                return json;
            }

        }

        public string GetSolde(String login, string password)
        {
           // ValeursDomain domain = new ValeursDomain();
           // String solde = domain.getSolde(login, password);


            String solde = "1000,10";
            if (solde != null) return solde;

            return null;
        }

        /*
         * 
         * 
         * Type => 1 : Limite
         *      => 2 : Au Cours d'ouverture
         *      => 3 : A la meilleur Limite
         *      => 4 : ATP
         * 
         * */
        public string ordreVente(int type, string code_val, decimal cours, int qte, string login, string password)
        {
            //Appel aux methodes de l'initiator , le retour sera un msg FIX ,   
            try
            {
                System.Diagnostics.Debug.WriteLine("Methode ordreVente Appellée");
                if (application.sendSingleOrdre(""+orderID+"",2, "100", 1, 50, 10.3m))
                {
                    orderID++;
                    System.Diagnostics.Debug.WriteLine("Execution Ok");
                }
                else 
                {
                    System.Diagnostics.Debug.WriteLine("Execution Not Ok");
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Exception call sendSingleOrdre " + e.ToString());
            }
            return null;
        }

        public string ordreAchat(int type, string code_val, double cours, int qte, string login, string password)
        {

            return null;
        }

    }
}
