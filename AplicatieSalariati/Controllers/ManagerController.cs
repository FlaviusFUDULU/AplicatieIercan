using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AplicatieSalariati.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AplicatieSalariati.ViewModels;

namespace AplicatieSalariati.Controllers
{
    [Authorize]
    public class ManagerController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;

        public ActionResult AprobConcedii()
        {
            ConcediiViewmodel concediiViewmodel = new ConcediiViewmodel();
            concediiViewmodel.Manageri = db.DateManagerModels.ToList();
            concediiViewmodel.Salariati = db.DateAngajatModels.ToList();
            concediiViewmodel.Concedii = db.ConcediiModels.ToList();
            List<ConcediileModel> concediileModelsList = new List<ConcediileModel>();
            foreach (var salariat in concediiViewmodel.Salariati)
            {
                foreach (var concediu in  concediiViewmodel.Concedii)
                {
                    if ((salariat.CNP == concediu.CNP) && concediu.Confirmat == false)
                    {
                        ConcediileModel con = new ConcediileModel();
                        con.CNP = salariat.CNP;
                        con.Nume = salariat.Nume;
                        con.Prenume = salariat.Prenume;
                        con.dataStart = concediu.dataStart;
                        con.DataFinal = concediu.DataFinal;
                        con.id = concediu.Id;
                        concediileModelsList.Add(con);
                    }
                }
            }
            
            return View(concediileModelsList);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public void AprobaConcedii(int? id)
        {
            ConcediiModel concediiModel = db.ConcediiModels.Find(id);
            if (concediiModel != null)
            {
                concediiModel.Confirmat = true;
                db.Entry(concediiModel).State = EntityState.Modified;
                db.SaveChanges();
            }
            //return RedirectToAction("VizConcedii");
        }

        public ActionResult ConcediuNou()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ConcediuNou(ConcediiModel concediiModel)
        {
            DateManagerModel dateManagerModel = new DateManagerModel();
            string nume = User.Identity.GetUserName().Substring(1);
            var dateManagerModelList = db.DateManagerModels.Where
                (a => a.Nume.Contains(nume)).ToList();
            dateManagerModel = dateManagerModelList.First();
            if (dateManagerModel == null)
            {
                return HttpNotFound();
            }
            concediiModel.CNP = dateManagerModel.CNP;
            concediiModel.Confirmat = false;
            db.ConcediiModels.Add(concediiModel);
            db.SaveChanges();
            return RedirectToAction("VizConcedii");

        }

        public ActionResult VizConcedii()
        {
            DateManagerModel dateManagerModel = new DateManagerModel();
            string nume = User.Identity.GetUserName().Substring(1);
            var dateManagerModellist = db.DateManagerModels.Where
                (a => a.Nume.Contains(nume)).ToList();
            dateManagerModel = dateManagerModellist.First();
            if (dateManagerModel == null)
            {
                return HttpNotFound();
            }

            ConcediiModel concediiModel = new ConcediiModel();
            var concediiModellist = db.ConcediiModels.Where(a => a.CNP.Contains(dateManagerModel.CNP));

            return View(concediiModellist);
        }

        public ManagerController() {
            //taxePrestabilite = db.TaxePrestabilite.FirstOrDefault();
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            var dateManagerList = db.DateManagerModels.Where(a => a.Nume.Contains(query) || a.Prenume.Contains(query)).ToList();
            return View(dateManagerList);
        }

        public ActionResult Fluturasi()
        {
            DateManagerModel dateManagerModel = new DateManagerModel();

            string nume = User.Identity.GetUserName().Substring(1);
            var dateManagerModelList = db.DateManagerModels.Where
                (a => a.Nume.Contains(nume)).ToList();
            dateManagerModel = dateManagerModelList.First();
            if (dateManagerModel == null)
            {
                return HttpNotFound();
            }

            SalarizareModel salarizareModel = db.SalarizareModels.Find(dateManagerModel.CNP);
            if (salarizareModel == null)
            {
                return HttpNotFound();
            }
            return View(salarizareModel);
        }

        // GET: Salariat/Details/5
        public ActionResult Details(string id)
        {
            DateManagerModel dateManagerModel = new DateManagerModel();
            if (id == null)
            {
                string nume = User.Identity.GetUserName().Substring(1);
                var dateManagerModels = db.DateManagerModels.Where
                    (a => a.Nume.Contains(nume)).ToList();
                dateManagerModel = dateManagerModels.First();
                if (dateManagerModel == null)
                {
                    return HttpNotFound();
                }
                return View(dateManagerModel);
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                dateManagerModel = db.DateManagerModels.Find(id);
                if (dateManagerModel == null)
                {
                    return HttpNotFound();
                }
                return View(dateManagerModel);
            }
        }



        // GET: Salariat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Salariat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Adresa,TelefonPersonal,TelefonServici")] DateManagerModel dateManagerModel)
        {
            if (ModelState.IsValid)
            {
                db.DateManagerModels.Add(dateManagerModel);
                db.SaveChanges();

                createManagerUser(dateManagerModel.Nume, dateManagerModel.Prenume,
                                dateManagerModel.Email, dateManagerModel.CNP);

                return RedirectToAction("Create", new { message = "Creat cu succes! Parola este CNP-ul" });
            }

            return View(dateManagerModel);
        }

        // GET: Salariat/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateManagerModel dateManagerModel = db.DateManagerModels.Find(id);
            if (dateManagerModel == null)
            {
                return HttpNotFound();
            }
            return View(dateManagerModel);
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CNP,Nume,Prenume,Email,Functie,Adresa,TelefonPersonal,TelefonServici")] DateManagerModel dateManagerModel)
        {
            if (ModelState.IsValid)
            {
                //dateAdministratorModel = CalculeazaTaxe(ref dateAdministratorModel);
                db.Entry(dateManagerModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View(dateManagerModel);
        }

        // GET: Salariat/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DateManagerModel dateManagerModel = db.DateManagerModels.Find(id);
            if (dateManagerModel == null)
            {
                return HttpNotFound();
            }
            return View(dateManagerModel);
        }

        // POST: Salariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DateManagerModel dateManagerModel = db.DateManagerModels.Find(id);
            db.DateManagerModels.Remove(dateManagerModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "Administrator ștearsă cu succes din sistem!"});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void createManagerUser(string nume, string prenume, string email, string cnp)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // first we create Admin rool   
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = "Manager";
            roleManager.Create(role);

            //Here we create a Admin super user who will maintain the website                  

            var user = new ApplicationUser();
            user.UserName = prenume[0]+nume;
            user.Email = email+"@gmail.com";

            string userPWD = cnp;

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Manager");

            }

            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            // setup Smtp authentication
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("fudulu.flavius@gmail.com", "cib3rweb");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("fudulu.flavius@gmail.com");
            msg.To.Add(new MailAddress(email));

            msg.Subject = "Credentials";
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>The password is your cnp</b></body>");

            try
            {
                client.Send(msg);
                //lblMsg.Text = "Your message has been successfully sent.";
            }
            catch (Exception ex)
            {
                //lblMsg.ForeColor = Color.Red;
                //lblMsg.Text = "Error occured while sending your message." + ex.Message;
            }
        }

        #region Private Helpers
        private SalariatModel CalculeazaTaxe(ref SalariatModel model) {
            var precision = 2;
            if (taxePrestabilite != null) {
                model.Total_Brut = Math.Round(((model.Salar_Brut * model.Salar_Realizat / 100) * (1 + model.Vechime / 100 + model.Spor / 100) + model.Premii_Brute + model.Compensatie), precision);
                //model.CAS = Math.Round((model.Total_Brut * taxePrestabilite.CAS), precision);
                //.Somaj = Math.Round((model.Total_Brut * taxePrestabilite.Somaj), precision);
               // model.Sanatate = Math.Round((model.Total_Brut * taxePrestabilite.Sanatate), precision);
                model.Brut_Impozabil = Math.Round((model.Total_Brut - model.CAS - model.Somaj - model.Sanatate), precision);
                model.Impozit = Math.Round((model.Brut_Impozabil * taxePrestabilite.Impozit), precision);
                model.RestPlata = Math.Round((model.Total_Brut - model.Impozit - model.CAS - model.Somaj - model.Sanatate - model.Retineri - model.Avans), precision);
            }
            return model;
        }
        #endregion
    }

    
}
