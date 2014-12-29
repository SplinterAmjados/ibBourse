using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IibServices
    {
        [OperationContract]
        int verifyClient(string login, String password);

        [OperationContract]
        void passerOrdreAchatById(int idClient, string codeValeur,string valeur, int type, int qte, decimal prix = 0);

        [OperationContract]
        void passerOrdreAchat(String login, String password, string codeValeur, string valeur, int type, int qte, decimal prix = 0);

        [OperationContract]
        void passerOrdreVenteById(int idClient, string codeValeur, string valeur, int type, int qte, decimal prix = 0);

        [OperationContract]
        void passerOrdreVente(String login, String password, string codeValeur, string valeur, int type, int qte, decimal prix = 0);

        [OperationContract]
        double getSoldeById(int idClient);

        [OperationContract]
        double GetSolde(String login, string password);

        [OperationContract]
        string GetCotationsJsonList();


        [OperationContract]
        string GetClientValeurs(String login, String Password);

        [OperationContract]
        string getClientValeursById(int idClient);

        [OperationContract]
        string historiqueValeurs(string code);

        [OperationContract]
        String getType(int id);
        
    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "WcfServiceLibrary1.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }

}
