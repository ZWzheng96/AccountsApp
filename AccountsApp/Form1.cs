using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AccountsApp
{
    public partial class frmAccounts : Form
    {
        private List<Account> accounts = new List<Account>();
        private Account myAccount = null;
        public frmAccounts()
        {
            InitializeComponent();
        }

        private void rbtnChecking_CheckedChanged(object sender, EventArgs e)
        {
            txtWithdrawLimit.Enabled = true;
            txtInterestRate.Enabled = false;
        }

        private void rbtnVisa_CheckedChanged(object sender, EventArgs e)
        {
            txtInterestRate.Enabled = true;
            txtWithdrawLimit.Enabled = false;
        }

        private void btnCreatAccount_Click(object sender, EventArgs e)
        {
            try
            {
                if (rbtnChecking.Checked)
                {
                    CheckingAccount newCheckingAccount = new CheckingAccount(int.Parse(txtNumber.Text),
                        txtName.Text,
                        double.Parse(txtBalance.Text),
                        double.Parse(txtWithdrawLimit.Text));
                    
                    accounts.Add(newCheckingAccount);
                }
                else
                {
                    VisaAccount newVisaAccount = new VisaAccount(int.Parse(txtNumber.Text),
                        txtName.Text,
                        double.Parse(txtBalance.Text),
                        double.Parse(txtInterestRate.Text));

                    accounts.Add(newVisaAccount);
                }

                txtNumber.Text = "";
                txtName.Text = "";
                txtBalance.Text = "";
                txtWithdrawLimit.Text = "";
                txtInterestRate.Text = "";


                rbtnChecking.Checked = true;

                txtWithdrawLimit.Enabled = true;
                txtInterestRate.Enabled = false;

                MessageBox.Show($"Total number of accounts: {accounts.Count}");
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Format!");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            myAccount = null;

            foreach (Account a in accounts)
                if (a.Number == int.Parse(txtSearch.Text))
                {
                    myAccount = a;
                    break;
                }

            if (myAccount != null)
            {
                btnWithdraw.Enabled = true;
                btnDeposit.Enabled = true;

                txtNumber.Text = myAccount.Number.ToString();
                txtName.Text = myAccount.ClientName;
                txtBalance.Text = myAccount.Balance.ToString();

                if (myAccount is CheckingAccount)
                {
                    txtWithdrawLimit.Text = ((CheckingAccount)myAccount).WithdrawLimit.ToString();

                    txtWithdrawLimit.Enabled = true;
                    txtInterestRate.Enabled = false;

                    rbtnChecking.Checked = true;
                    rbtnVisa.Checked = false;
                }
                else
                if (myAccount is VisaAccount)
                {
                    txtInterestRate.Text = ((VisaAccount)myAccount).InterestRate.ToString();

                    txtWithdrawLimit.Enabled = false;
                    txtInterestRate.Enabled = true;

                    rbtnChecking.Checked = false;
                    rbtnVisa.Checked = true;
                }
            }
            else
            {
                btnWithdraw.Enabled = false;
                btnDeposit.Enabled = false;

                MessageBox.Show("Account not found!");
            }
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            double amount = double.Parse(txtWithdraw.Text);
            if (myAccount is CheckingAccount)
            {
                if(amount > ((CheckingAccount)myAccount).WithdrawLimit)
                {
                    MessageBox.Show("Please enter a value less than the withdraw limit!");
                }
                else
                {
                    myAccount.Withdraw(amount);
                    MessageBox.Show("Your current balance is: " + myAccount.Balance.ToString());
                }
            }
            else
            {
                myAccount.Withdraw(amount);
                MessageBox.Show("Your current balance is: " + myAccount.Balance.ToString());
            }
            txtWithdraw.Text = "";
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            double amount = double.Parse(txtDeposit.Text);
            myAccount.Deposit(amount);
            MessageBox.Show("Your current balance is: "+ myAccount.Balance.ToString());
            txtDeposit.Text = "";
        }
    }
}
