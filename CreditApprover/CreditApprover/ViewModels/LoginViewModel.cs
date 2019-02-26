using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditApprover.Models;
namespace CreditApprover.ViewModels
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public ApprovalRequest ApprovalRequest { get; set; }
        public List<CreditRequest> CreditRequestList { get; set; }
    }
}