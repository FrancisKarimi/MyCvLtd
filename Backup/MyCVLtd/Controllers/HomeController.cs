using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyCVLtd.Models;
using System.Text;
using System.IO;

namespace MyCVLtd.Controllers
{
    public class HomeController : Controller
    {
        private MyCVLimitedEntities db = new MyCVLimitedEntities();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }



        

        public ActionResult UserDashBoard()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        //
        // GET: /Home/Details/5

        public ActionResult Details(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // GET: /Home/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Home/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Member member, string btnRegister)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(btnRegister))
                {
                }
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        //
        // GET: /Home/Edit/5
        public ActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(Member member, string btnRegister, string btnLogin, string fName, string lName, string email, string email2, string PhoneNumber, string DoB, string password, string password2, string passwordConfirm)
        {
            Member mbr = new Member();
            DateTime DateOfBirth = Convert.ToDateTime(DoB);
           if (!string.IsNullOrEmpty(btnRegister))
                {

                    if (DateTime.Today.AddYears(-16) < DateOfBirth)
                    {
                        Response.Write("<script>alert('You must be at least 16 years old to register')</script>");
                        return View();
                    }

                    mbr.Member_No = 3;
                    mbr.First_Name = fName;
                    mbr.Last_Name = lName;
                    mbr.E_Mail = email;
                    mbr.ID_Number = "22222222";
                    mbr.Member_Type = "freeUser";
                    mbr.Physical_Location = "Nairobi";
                    mbr.profession = "Cook";
                    mbr.Phone_Number = "0724237824";
                    mbr.Date_of_Birth = DateTime.Today;
                    mbr.password = password;
                    mbr.verified = false;

                    string activationCode = sendActivationCode(fName + " " + lName, email);
                    mbr.Activation_Code =activationCode;

                    db.Members.Add(mbr);
                    db.SaveChanges();
                    //Session["useremail"] = email;
                    //Response.Write("<script> alert('You have successfully registered. please check your email for activation code')</script>");
                    return RedirectToAction("Registration");
                }
           else if (!string.IsNullOrEmpty(btnLogin))
           {

               var validateUserPass = db.Members.Where(a => a.E_Mail == email2).Select(u => u.password).FirstOrDefault();
               var validateUserEmail = db.Members.Where(a => a.E_Mail == email2).Select(u => u.E_Mail).FirstOrDefault();
               var validateActivationStatus = db.Members.Where(a => a.E_Mail == email2).Select(u => u.verified).FirstOrDefault();
               Session["useremail"] = email2;
               //String userLoggedIn = Session["useremail"].ToString();

               if (validateUserEmail == email2 && validateUserPass == password2)
               {
                   if (validateActivationStatus == true)
                   {
                       //Session["useremail"] = email2;
                       //String userLoggedIn = Session["useremail"].ToString();
                       
                       return RedirectToAction("Profile");

                   }
                   else
                   {
                       Response.Write("<script> alert('Your account is not activated yet. please click ok to activate your account')</script>");
                       return RedirectToAction("EnterActivationCode");
                       
                   }
               }
               else
               {
                   Response.Write("<script> alert('The Email or password you have entered does not exist')</script>");
                   return RedirectToAction("Registration");
                   
               }

           }
            return View(member);

        }

        public ActionResult EnterActivationCode()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterActivationCode(Member member, string activationCode)
        {
            
            String userMail = Session["useremail"].ToString();
            var validateActivationCode = db.Members.Where(a => a.E_Mail == userMail).Select(u => u.Activation_Code).FirstOrDefault();
            var MemberNo = db.Members.Where(a => a.E_Mail == userMail).Select(u => u.Member_No).FirstOrDefault();


            if (validateActivationCode == activationCode)
            {
                Member mbr = db.Members.Find(MemberNo);
                mbr.verified = true;
                db.SaveChanges();
                Response.Write("<script> alert('Activation successful. Please login.')</script>");
                return RedirectToAction("Registration");
                
            }
            else
            {

                Response.Write("<script> alert('The code you have entered does not exist')</script>");

            }

            return View();
        }
        //Send Activation Code
        protected string sendActivationCode(string Name, string email)
        {
            string activationCode;
            activationCode = generateActivationCode();
            if (MyCVConfig.MailFunction(string.Format("Dear " + Name + ", You recently signed up at MyCVLimited.com." +
                " Please use the activation code below to activate your acount <br> <br> " + activationCode), email, "MyCV Limited Activation Code"))
            {
                Response.Write("<script>alert('An Activation code has been sent to your email " + email + ". Please login to your email and use the activation code to activate your account')</script>");
            }
            else
            {
                Response.Write("<script>alert('An error occured while sending you the activation code')</script>");
            }

            return (activationCode);
        }


        //Generate Activation Code
        protected string generateActivationCode()
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var builder = new StringBuilder();

            for (var i = 0; i < 10; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }

            return builder.ToString();
        }

        //Send Security Code
        protected string sendSecurityCode(string email)
        {
            string securityCode;
            securityCode = generateSecurityCode();
            if (MyCVConfig.MailFunction(string.Format("Please use the security code below to activate your acount <br> <br> " + securityCode), email, "MyCV Limited Security Code"))
            {
                Response.Write("<script>alert('A Security code has been sent to your email " + email + ". Please login to your email and use the security code to reset your password')</script>");
            }
            else
            {
                Response.Write("<script>alert('An error occured while sending you the security code')</script>");
            }

            return (securityCode);
        }

        //Generate Security Code
        protected string generateSecurityCode()
        {
            Random rand = new Random();
            const string pool = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var builder = new StringBuilder();
            for (var i = 0; i < 10; i++)
            {
                var c = pool[rand.Next(0, pool.Length)];
                builder.Append(c);
            }
            return builder.ToString();
        }
        public ActionResult Edit(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // POST: /Home/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        //
        // GET: /Home/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        //
        // POST: /Home/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Profile()
        {
            //string nums = "";
            //int k = Convert.ToInt32(db.PhotoAlbums.Where(r => r.member_No == 3) //Change once Session["memberNo"] is complete
            //                                      .ToList().Select(r => r.pictureID).FirstOrDefault().ToString());
            //int count = k + db.PhotoAlbums.Count();
            //do
            //{
            //    nums = nums + "#" + db.PhotoAlbums.Where(r => r.member_No == 3 && r.pictureID == k) //Change once Session["memberNo"] is complete
            //                          .ToList().Select(r => r.pictureID).FirstOrDefault().ToString();
            //    k++;
            //}
            //while (k < count);

            //try
            //{
            //    string[] numArray;
            //    numArray = nums.Split('#');
            //    foreach (var num in numArray)
            //    {

            //        if (!string.IsNullOrEmpty(num))
            //        {
            //            int picID = Convert.ToInt32(num.ToString());
            //            ViewBag.Album = ViewBag.Album + "#" + db.PhotoAlbums.Where(r => r.pictureID == picID).ToList().Select(r => r.Name).FirstOrDefault().ToString();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ex.Data.Clear();
            //}
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Profile(HttpPostedFileBase file, string description)
        {

            Member mbr = new Member();
            string name = db.Members.ToList()
                            .Where(r => r.Member_No == 3) //Change once Session["memberNo"] is complete
                            .Select(r => r.First_Name).SingleOrDefault()
                            .ToString();

            string subPath = "~/Content/imgs/members/" + "3" + "-" + name + "/album"; //Change once Session["memberNo"] is complete

            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

            if (file != null && file.ContentLength > 0)
                try
                {
                    string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
                    string path = Path.Combine(Server.MapPath(subPath), filename);
                    // Path.GetFileName(file.FileName));
                    file.SaveAs(path);

                    PhotoAlbum pic = new PhotoAlbum();
                    pic.PictureDescription = description;
                    //Change this when Session["memberNo"] is configured
                    pic.Name = filename;
                    pic.member_No = 3;

                    pic.Upload_Date = DateTime.Today;
                    //pic.Upload_Time = DateTime.Today;

                    db.PhotoAlbums.Add(pic);
                    db.SaveChanges();

                    ViewBag.Message = "File uploaded successfully";
                    return RedirectToAction("Profile");
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        }
        public ActionResult viewAlbums()
        {
            return View(db.PhotoAlbums.ToList());
        }

        public ActionResult PhotoAlbum()
        {
            return View(db.PhotoAlbums.ToList());
        }

        public ActionResult Album()
        {
            return View(db.PhotoAlbums.ToList());
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        public ActionResult Npass()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Npass(Member member, string btnNewpass, string password)
        {
            if (!string.IsNullOrEmpty(btnNewpass))
            {


                String userMail = Session["emailPass"].ToString();
                var MemberNo = db.Members.Where(a => a.E_Mail == userMail).Select(u => u.Member_No).FirstOrDefault();


                Member mbr = db.Members.Find(MemberNo);
                mbr.password = password;
                db.SaveChanges();
                Response.Write("<script> alert('Password reset successful. Please login.')</script>");
                return RedirectToAction("Registration");

            }
            else
            {

                return View("Registration");
            }
        }
        //Enter security code for password reset
        public ActionResult EnterCode()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnterCode(string code)
        {
            
            String resetpasscode = Session["secCode"].ToString();

            if (resetpasscode == code)
            {
                return RedirectToAction("Npass");
            }
            else
            {

                Response.Write("<script> alert('The code you have entered is invalid')</script>");

            }

            return View();
        }
        public ActionResult Preset()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Preset(string email)
        {


            var checkIfEmailExists = db.Members.Where(a => a.E_Mail == email).ToList();

            if (checkIfEmailExists.Count > 0)
                    {
                        string securityCode = sendSecurityCode(email);
                        Session["secCode"] = securityCode;
                        Session["emailPass"] = email;
                        Response.Write("<script> alert('Security code sent to your email.')</script>");
                            return RedirectToAction("EnterCode");
                    }
                    else
                    {

                        Response.Write("<script> alert('The Email you have entered does not exist')</script>");

                    }

            return View();
        }



        public ActionResult Terms()
        {
            return View();
        }

    }
}