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

        ServiceReference1.IibServices client ; 

        public HomeController()
        {
        client = new ServiceReference1.IibServicesClient();  
        }

        private void verifyUser()
        {

            if (Session["login"] == null 
                || Session["password"] == null 
                || Session["login"].ToString().Trim() == "" 
                || Session["password"].ToString().Trim() == ""
                || Session["type"].ToString().Trim() == ""
                || Session["type"].ToString().Trim() == "")
            {
                FormsAuthentication.SignOut();
                Response.Redirect("/Login");
            }
        }

        private void verifyLibreUser()
        {
            if (Session["type"] == "gere") Response.Redirect("/");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {


            if (User.Identity.IsAuthenticated) Response.Redirect("/");

            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(User user,string returnUrl = "/")
        {
           int idCLient = client.verifyClient(user.Login, user.Password) ;
            if ( idCLient > 0)
            {
                FormsAuthentication.SetAuthCookie(user.Login, false);

                Session["login"] =  user.Login.ToString();
                Session["password"] =  user.Password.ToString();
                Session["type"] = client.getType(idCLient);             

                Response.Redirect(returnUrl,false);
            }

            ViewBag.erreur = "Invalide Login/Password";

            return View();
        }


        [HttpGet]
        public ActionResult Index()
        {
            verifyUser();
            System.Diagnostics.Debug.WriteLine(Session["type"]);
            return View();
        }



        [HttpGet]
        public ActionResult Cotations()
        {
            verifyUser();
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
            verifyUser();
            verifyLibreUser();
            System.Diagnostics.Debug.WriteLine("User " + Session["login"] + "Password " + (string)Session["password"]);
            return View();
        }
        
        [HttpGet]
        public ActionResult AchatValeur()
        {
            verifyUser();
            verifyLibreUser();
            ViewBag.valeurs = client.GetCotationsJsonList();
            //System.Diagnostics.Debug.WriteLine("User " + Session["login"] + "Password " + Session["password"].ToString());
            ViewBag.solde = client.GetSolde(Session["login"].ToString(), Session["password"].ToString());
            return View();
        }



        [HttpGet]
        public ActionResult VenteValeur()
        {
            verifyUser();
            verifyLibreUser();
            String mesValeurs = null;
            if (Session["mesValeurs"] == null)
            {
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
            verifyUser();
            verifyLibreUser();
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
            verifyUser();
            return  client.historiqueValeurs(code);
        }


        public ActionResult Analyse()
        {
            verifyUser();
            ViewBag.valeurs = client.GetCotationsJsonList();
            return View();
        }

    }
}
