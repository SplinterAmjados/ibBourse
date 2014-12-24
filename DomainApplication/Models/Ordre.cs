using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainApplication.Models
{
    public class Ordre
    {

        public static string NEW_ORDRE = "new ordre";
        public static string PARTIELLEMENT_EXECUTE = "partiellement ex";
        public static string TOTALEMENT_EXECUTE = "totalement ex";
        public static string ORDRE_REJETE = "rejete";
        
        public Ordre()
        {
            this.etat = Ordre.NEW_ORDRE;
            this.qte_restante = 0;
            this.dateTime = DateTime.Now.ToString();
        }


        public long id { get; set; }
        public String dateTime { get; set; }
        public string code_val { get; set; }
        public int id_client { get; set; }
        public int qte { get; set; }
        public int qte_restante { get; set; }
        public int type { get; set; }
        public Nullable<decimal> prix { get; set; }
        public string etat { get; set; }
        public string valeur { get; set; }

        [JsonIgnore]
        public virtual Client client { get; set; }

        public void executer(int qteExecute)
        {
            if (qteExecute > 0)
            {
                this.qte_restante -= qteExecute;

                if (qte_restante == 0)
                {
                    etat = Ordre.TOTALEMENT_EXECUTE;
                }
                else
                {
                    etat = Ordre.PARTIELLEMENT_EXECUTE;
                }
            }
        }

    }
}
