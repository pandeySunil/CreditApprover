using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CreditApprover.Models;

namespace CreditApprover.ViewModels
{
    public class ApprovalFormModel
    {
        public User User { get; set; } 
        public List<User> UserList { get; set; }
        public ApprovalRequest ApprovalRequest { get; set; }
        public CreditRequest CreditRequest { get; set; }
        public int SelectUserId { get; set; }


    }
}