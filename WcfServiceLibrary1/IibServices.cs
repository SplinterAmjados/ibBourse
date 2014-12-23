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
        string GetData(int value);


        [OperationContract]
        string GetCotationsJsonList();


        [OperationContract]
        string GetClientValeurs(String login, String Password);

        [OperationContract]
        string GetSolde(String login, String Password);

        [OperationContract]
        string ordreVente(int type, string code_val, decimal cours, int qte, string login, string password);

        [OperationContract]
        string ordreAchat(int type, string code_val, double cours, int qte, string login, string password);

        // TODO: Add your service operations here
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
