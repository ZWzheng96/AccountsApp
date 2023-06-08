using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountsApp
{
    class CheckingAccount : Account
    {
        public double WithdrawLimit { get; set; }

        public CheckingAccount(int number,
            string clientName,
            double balance,
            double withdrawLimit) : base(number, clientName, balance)
        {
            WithdrawLimit = withdrawLimit;
        }

        protected const double TRANSACTIONFEE = 0.6;
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
