using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AplicatieSalariati.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using AplicatieSalariati.ViewModels;
using System.Data.Entity.Infrastructure;

namespace AplicatieSalariati.Controllers
{
    [Authorize]
    public class SalarizareController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private TaxePrestabiliteModel taxePrestabilite;

        public SalarizareController() {
            taxePrestabilite = db.TaxePrestabilite.FirstOrDefault();
        }

        // GET: Salariat
        public ActionResult Index(string message, string type = "Edit", string query = "")
        {
            ViewBag.Message = (message == null) ? "" : message;
            ViewBag.Type = type;
            SalariatiViewModel salariatList = new SalariatiViewModel();
            salariatList.Manageri = db.DateManagerModels.ToList();
            salariatList.Salariati = db.DateAngajatModels.ToList();
            //var salariatList = db.Salariati.Where(a => a.Nume.Contains(query) || a.Prenume.Contains(query)).ToList();
            return View(salariatList);
        }

        // GET: Salariat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalariatModel salariatModel = db.Salariati.Find(id);
            if (salariatModel == null)
            {
                return HttpNotFound();
            }
            return View(salariatModel);
        }

        public ActionResult Fluturasi(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalarizareModel salarizareModel = db.SalarizareModels.Find(id);
            if (salarizareModel == null)
            {
                return HttpNotFound();
            }
            return View(salarizareModel);
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
        public ActionResult Create([Bind(Include = "Nr_Crt,Nume,Prenume,Functie,Salar_Negociat,Salar_Realizat,Vechime,Spor,Premii_Brute,Compensatie,Total_Brut,Brut_Impozabil,Impozit,CAS,Somaj,Sanatate,Avans,Retineri,RestPlata")] SalariatModel salariatModel)
        {
            if (ModelState.IsValid)
            {
                //CalculeazaTaxe(ref salariatModel);
                db.Salariati.Add(salariatModel);
                db.SaveChanges();
                createAngajatUser(salariatModel.Nume, salariatModel.Prenume, 
                                    salariatModel.Nume, salariatModel.Nume);
                return RedirectToAction("Index", new { message = "Creat cu succes!" });
            }

            return View(salariatModel);
        }

        // GET: Salariat/Edit/5
        public ActionResult EditManager(string id)
        {
            SalariatiViewModel salariatiView = new SalariatiViewModel();
            salariatiView.Manager = db.DateManagerModels.Find(id);
            salariatiView.Salariat = db.DateAngajatModels.Find(id);

            if (salariatiView == null)
            {
                return HttpNotFound();
            }

            salariatiView.Salarizat = db.SalarizareModels.Find(id);
            if(salariatiView.Salarizat == null)
            {
                salariatiView.Salarizat = new SalarizareModel();
                salariatiView.Salarizat.CNP = salariatiView.Manager.CNP;

            }
            
            return View(salariatiView);
        }

        public ActionResult EditAngajat(string id)
        {
            SalariatiViewModel salariatiView = new SalariatiViewModel();
            salariatiView.Salariat = db.DateAngajatModels.Find(id);

            if (salariatiView == null)
            {
                return HttpNotFound();
            }

            salariatiView.Salarizat = db.SalarizareModels.Find(id);
            if (salariatiView.Salarizat == null)
            {
                salariatiView.Salarizat = new SalarizareModel();
                salariatiView.Salarizat.CNP = salariatiView.Salariat.CNP;

            }

            return View(salariatiView);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAngajat(SalariatiViewModel salariatiViewModel, string cnp)
        {
            if (ModelState.IsValid)
            {

                SalarizareModel salarizareModel = new SalarizareModel();
                salarizareModel = salariatiViewModel.Salarizat;

                if (db.SalarizareModels.Find(salarizareModel.CNP) != null)
                {
                    SalarizareModel salarizareModelMod = db.SalarizareModels.Find(salarizareModel.CNP);
                    db.SalarizareModels.Remove(salarizareModelMod);
                    salarizareModelMod = salariatiViewModel.Salarizat;
                    db.Entry(salarizareModelMod).State = EntityState.Added;
                }
                else
                {
                    db.Entry(salarizareModel).State = EntityState.Added;
                }
                salarizareModel = CalculeazaTaxe(ref salarizareModel);
                db.SaveChanges();

                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View();
        }

        // POST: Salariat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditManager(SalariatiViewModel salariatiViewModel, string cnp)
        {
            if (ModelState.IsValid)
            {
                
                SalarizareModel salarizareModel = new SalarizareModel();
                salarizareModel = salariatiViewModel.Salarizat;

                if (db.SalarizareModels.Find(salarizareModel.CNP) != null)
                {
                    SalarizareModel salarizareModelMod = db.SalarizareModels.Find(salarizareModel.CNP);
                    db.SalarizareModels.Remove(salarizareModelMod);
                    salarizareModelMod = salariatiViewModel.Salarizat;
                    db.Entry(salarizareModelMod).State = EntityState.Added;
                }
                else
                {
                    db.Entry(salarizareModel).State = EntityState.Added;
                }
                salarizareModel = CalculeazaTaxe(ref salarizareModel);
                db.SaveChanges();

                return RedirectToAction("Index", new { message = "Editat cu succes!" });
            }
            return View();
        }

        // GET: Salariat/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalariatModel salariatModel = db.Salariati.Find(id);
            if (salariatModel == null)
            {
                return HttpNotFound();
            }
            return View(salariatModel);
        }

        // POST: Salariat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SalariatModel salariatModel = db.Salariati.Find(id);
            db.Salariati.Remove(salariatModel);
            db.SaveChanges();
            return RedirectToAction("Index", new { message = "Înregistrare ștearsă cu succes!"});
        }

        public ActionResult CalculeazaSalariu(string id) {
            SalariatModel salariatModel = db.Salariati.FirstOrDefault(a => a.CNP == id);
            //CalculeazaTaxe(ref salariatModel);
            db.Entry(salariatModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", new { type = "Calcul", message = "Recalculat cu succes!" });
        }

        public ActionResult CalculeazaToateSalariile() {
            var salariatiList = db.Salariati;
            foreach (var salariat in salariatiList) {
                var salariatModel = salariat;
                //CalculeazaTaxe(ref salariatModel);
                db.Entry(salariatModel).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", new { type = "Calcul", message = String.Format("Recalculat cu succes {0} salariați !", salariatiList.Count()) });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Private Helpers
        private SalarizareModel CalculeazaTaxe(ref SalarizareModel model) {
            var precision = 2;
            if (taxePrestabilite != null) {
                model.CAS = Math.Round((model.Salar_Brut * (taxePrestabilite.CAS * 0.01)), precision);
                model.CASS = Math.Round((model.Salar_Brut * (taxePrestabilite.CASS * 0.01)), precision);       
                model.CAM = Math.Round((model.Salar_Brut * (taxePrestabilite.CAM * 0.01)), precision);
                model.IV = Math.Round(((model.Salar_Brut-model.CAS-model.CASS+
                                                    model.NrBonuri*model.ValBonuri) * (taxePrestabilite.Impozit * 0.01)), precision);
                model.Salar_Net = Math.Round(model.Salar_Brut - model.CAS - model.CASS - model.IV);           
            }
            return model;
        }
        private void createAngajatUser(string nume, string prenume, string email, string cnp)
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // first we create Admin rool   
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            role.Name = "Angajat";
            roleManager.Create(role);

            //Here we create a Admin super user who will maintain the website                  

            var user = new ApplicationUser();
            user.UserName = prenume.ToLower()[0] + nume.ToLower();
            user.Email = email.ToLower();

            string userPWD = "123456";

            var chkUser = UserManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var result1 = UserManager.AddToRole(user.Id, "Angajat");

            }

            //SmtpClient client = new SmtpClient();
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.EnableSsl = true;
            //client.Host = "smtp.gmail.com";
            //client.Port = 587;

            //// setup Smtp authentication
            //System.Net.NetworkCredential credentials =
            //    new System.Net.NetworkCredential("fudulu.flavius@gmail.com", "cib3rweb");
            //client.UseDefaultCredentials = false;
            //client.Credentials = credentials;

            //MailMessage msg = new MailMessage();
            //msg.From = new MailAddress("fudulu.flavius@gmail.com");
            //msg.To.Add(new MailAddress(email));

            //msg.Subject = "Credentials";
            //msg.IsBodyHtml = true;
            //msg.Body = string.Format("<html><head></head><body><b>The password is your cnp</b></body>");

            //try
            //{
            //    client.Send(msg);
            //    //lblMsg.Text = "Your message has been successfully sent.";
            //}
            //catch (Exception ex)
            //{
            //    //lblMsg.ForeColor = Color.Red;
            //    //lblMsg.Text = "Error occured while sending your message." + ex.Message;
            //}
        }
        #endregion
    }
}
