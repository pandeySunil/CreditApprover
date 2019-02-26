using CreditApprover.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CreditApprover.ViewModels;

namespace CreditApprover.Controllers
{
    public class HomeController : Controller
    {
        public CreditApproverDbEntities1 db;
        public HomeController() {

            this.db = new CreditApproverDbEntities1();

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddUser(User User) {
            this.db.Users.Add(new User {
                UserId = db.Users.Count() + 1,
                UserName = User.UserName,
                Password = User.Password,
                EmailId = User.EmailId,
            });
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Login() {
            return View();
        }
        public ActionResult ValidateUser(LoginViewModel loginViewModel) {
            if (loginViewModel == null && loginViewModel.User == null) {
                return View("Login");
            }
            int UserId = db.Users.FirstOrDefault(u => u.UserName == loginViewModel.User.UserName).UserId;
            List<ApprovalRequest> ApprovalRequestList = db.ApprovalRequests.Where(r => r.UserId == UserId).ToList();
            List<CreditRequest> CreditRequestList = db.CreditRequests.ToList();
            List<User> UserList = db.Users.ToList();
            

            var creditData = from c in ApprovalRequestList join s in CreditRequestList  
                        on c.CreditRequestId equals s.CreditRequestId
                      select (new {Amount  = s.Amount,
                      Purpose = s.Purpose,
                      CreditRequestId =s.CreditRequestId});
            
            foreach (var t in creditData) {
                CreditRequest c =   new CreditRequest { Amount = t.Amount, Purpose = t.Purpose,
                    CreditRequestId =t.CreditRequestId};
                CreditRequestList.Add(c);
            }
            loginViewModel.CreditRequestList = CreditRequestList;


             var validUser =   db.Users.FirstOrDefault(u => u.UserName == loginViewModel.User.UserName);
            var validPassword = validUser.Password;
            if (validPassword == loginViewModel.User.Password) {
                loginViewModel.User = validUser;
                return View(loginViewModel);
            }
            return RedirectToAction("Login");
        }

        public ActionResult AmountApprovalAction(User user) {

            var currentUser = user;
            ApprovalFormModel approvalFormModel = new ApprovalFormModel() {
                User = currentUser,
                UserList = db.Users.ToList()
            };
            return View(approvalFormModel);
        }
        public ActionResult AddApprovalRequest(ApprovalFormModel appovalFormModel) {

            db.CreditRequests.Add(new CreditRequest
            {
                CreditRequestId = db.CreditRequests.Count()+1,
                ApplicantId = appovalFormModel.User.UserId,
                Amount = appovalFormModel.CreditRequest.Amount,
                Purpose = appovalFormModel.CreditRequest.Purpose,
            });
            db.ApprovalRequests.Add(new ApprovalRequest {
                ApprovalRequestId = db.ApprovalRequests==null? 0:db.ApprovalRequests.Count()+1,
                CreditRequestId = db.CreditRequests.Count() + 1,
                UserId = appovalFormModel.SelectUserId

            });
            var currentUser = db.Users.FirstOrDefault(u=>u.UserId == appovalFormModel.User.UserId);
            currentUser.ApprovalRequestApplied = currentUser.ApprovalRequestApplied==null?0:currentUser.ApprovalRequestApplied + 1;
            var requestApprovalUser = db.Users.FirstOrDefault(u => u.UserId == appovalFormModel.SelectUserId);
            requestApprovalUser.ApprovalRequestPending = requestApprovalUser.ApprovalRequestPending == null?0:requestApprovalUser.ApprovalRequestPending + 1;
            db.SaveChanges();

            return View();
        }
        public ActionResult RequestApproveAction(string CreditRequestid) {
            var userList = db.Users.ToList();
            int urId = Convert.ToInt32(CreditRequestid);
            var amount = db.CreditRequests.FirstOrDefault(c => c.CreditRequestId == urId).Amount;
            var approvalrequestList = db.ApprovalRequests.ToList();
            var applicant = from u in userList
                            join a in approvalrequestList on u.UserId equals a.UserId
                            select (new {userId = u.UserId  }).userId;
            foreach (var  id in  applicant){
                var creditedUser = db.Users.FirstOrDefault(s => s.UserId == id);
                creditedUser.Balance = Convert.ToString(Convert.ToInt32(creditedUser.Balance) + amount);
            }
            db.CreditRequests.FirstOrDefault(c => c.CreditRequestId == urId);
            CreditRequest CreditRequest = db.CreditRequests.Find(urId);
            db.CreditRequests.Remove(CreditRequest);
            ApprovalRequest approvalRequest = db.ApprovalRequests.FirstOrDefault(f => f.CreditRequestId == urId);
            db.ApprovalRequests.Remove(approvalRequest);

            db.SaveChanges();
            return View("Login");
        }
    }
}