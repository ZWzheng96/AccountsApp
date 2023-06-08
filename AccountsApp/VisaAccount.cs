using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsApp
{
    class VisaAccount : Account
    {
        public double InterestRate { get; set; }

        public VisaAccount(int number,
            string clientName,
            double balance,
            double interestRate) : base(number, clientName, balance)
        {
            InterestRate = interestRate;
        }

        protected const double TRANSACTIONFEE = 3.0;
        public override void Deposit(double amount)
        {

            Balance = Balance + amount - TRANSACTIONFEE;
        }
        public override void Withdraw(double amount)
        {
            Balance = Balance - amount - TRANSACTIONFEE;
        }
    }
}
