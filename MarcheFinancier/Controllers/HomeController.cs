using MarcheFinancier.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MarcheFinancier.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {


            if (User.Identity.IsAuthenticated) Response.Redirect("/");

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user,string returnUrl)
        {
            //Remplacer par un test sur une base de données
            if (user.Login == "splinter" && user.Password == "splinter")
            {
                FormsAuthentication.SetAuthCookie(user.Login, false);
                Session["login"] =  user.Login.ToString();
                Session["password"] =  user.Password.ToString();
                
                Response.Redirect(returnUrl,false);
            }

            ViewBag.erreur = "Invalide Login/Password";

            return View();
        }


        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }



        [HttpGet]
        public ActionResult Cotations()
        {
            ServiceReference1.IibServices client = new ServiceReference1.IibServicesClient();
            ViewBag.valeursList = client.GetCotationsJsonList();

            String mesValeurs = null;
            if (Session["mesValeurs"] == null)
            {

                System.Diagnostics.Debug.WriteLine("User " + Session["login"] + "Password " + (string)Session["password"]);

                mesValeurs = client.GetClientValeurs(Session["login"].ToString(), Session["password"].ToString());
                Session["mesValeurs"] = mesValeurs;
            }
            else {
                mesValeurs = (String) Session["mesValeurs"];
            }
            ViewBag.mesValeurs = mesValeurs;
            

            return View();
        }

        [HttpGet]
        public ActionResult PassageOrdre()
        {
            System.Diagnostics.Debug.WriteLine("User " + Session["login"] + "Password " + (string)Session["password"]);
            return View();
        }
        
        [HttpGet]
        public ActionResult AchatValeur()
        {
            ServiceReference1.IibServices client = new ServiceReference1.IibServicesClient();
            ViewBag.valeurs = client.GetCotationsJsonList();
            ViewBag.solde = client.GetSolde(Session["login"].ToString(), Session["password"].ToString());
            return View();
        }



        [HttpGet]
        public ActionResult VenteValeur()
        {
            
            String mesValeurs = null;
            if (Session["mesValeurs"] == null)
            {
                ServiceReference1.IibServices client = new ServiceReference1.IibServicesClient();
                mesValeurs = client.GetClientValeurs(Session["login"].ToString(), Session["password"].ToString());
                Session["mesValeurs"] = mesValeurs;
            }
            else
            {
                mesValeurs = (String)Session["mesValeurs"];
            }
            ViewBag.mesValeurs = mesValeurs;


            return View();
        }

        [HttpPost]
        public ActionResult VenteValeurPost()
        {
            List<String> erreurs = new List<String>();

            string code_val = Request["valeur"];
            int type_ordre = 0;
            int nbre_action = 0;
            decimal cours = 0;

            try
            {
                type_ordre = Convert.ToInt16(Request["type_ordre"]);
            }
            catch (FormatException e)
            {            
            erreurs.Add("type ordre doit être un entier") ;
            }

            try
            {
                nbre_action = Convert.ToInt16(Request["nbre_actions"]);
                if (nbre_action <= 0)
                {
                    erreurs.Add("Nombre d'action doit être supérieur à Zéro");                
                }
            }
            catch (FormatException e)
            {
                erreurs.Add("Nombre d'action doit être un entier");
            }


            if (type_ordre == 1)
            {
                try
                {
                    string cours_request = Request["cours"];
                    cours_request.Replace('.', ';');
                    cours = Convert.ToDecimal(cours_request);
                    if (cours <= 0 )
                    {
                        erreurs.Add("Le cours doit être supérieur à Zéro");
                    }

                }
                catch (FormatException e)
                {
                    erreurs.Add("Le cours doit être un nombre");
                }
            }

            if (erreurs.Count() > 0)
            {

                String mesValeurs = null;
                if (Session["mesValeurs"] == null)
                {
                    ServiceReference1.IibServices client = new ServiceReference1.IibServicesClient();
                    mesValeurs = client.GetClientValeurs(Session["login"].ToString(), Session["password"].ToString());
                    Session["mesValeurs"] = mesValeurs;
                }
                else
                {
                    mesValeurs = (String)Session["mesValeurs"];
                }
                ViewBag.mesValeurs = mesValeurs;

                ViewBag.erreurs = erreurs;
                return View("VenteValeur");
            }

            return View();
        }


        [HttpGet]
        public String HistoriqueValeur(String code)
        {
            ServiceReference1.IibServices client = new ServiceReference1.IibServicesClient();
            return  client.historiqueValeurs(code);
        }

    }
}
