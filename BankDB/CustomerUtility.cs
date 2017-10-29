using System;
using BankDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace BankDB
{
    public class CustomerUtilities
    {
        //public static List<int> AddCustomer(string NewFirstName, string NewLastName, string Id)
        public static Customer AddCustomer(string NewFirstName, string NewLastName, string Id)
        {
            int SelectedBankId;
            int.TryParse(Id, out SelectedBankId);
            List<int> UserData = new List<int>();

            var context = new BankdbContext();
            var newCustomer = new Model.Customer
            {
                FirstName = NewFirstName,
                LastName = NewLastName,
                BankId = SelectedBankId
            };

            if (SelectedBankId > 0 && NewFirstName.Length >= 2 && NewLastName.Length >= 2 && NewFirstName.Length <= 50 && NewLastName.Length <= 50)
            {
                context.Customer.Add(newCustomer);
                context.SaveChanges();

                UserData.Add(newCustomer.BankId);
                UserData.Add(newCustomer.Id);

                Console.WriteLine("User Added to database.");

                return newCustomer;
                //return UserData;
            }
            else
            {
                Console.WriteLine("Invalid Inputs");
                return newCustomer;
                //return UserData;
            }
        }

        public static void AddCustomerBankAccount(string NewIBAN, string NewName, int NewBankId, int NewCustomerId, string NewBalance)
        {
            decimal Balance;
            decimal.TryParse(NewBalance, out Balance);
            if (NewIBAN.Length >= 12 && NewName.Length >= 2 && Balance >= 0 && NewIBAN.Length <= 50 && NewName.Length <= 50)
            {

                try
                {
                    var context = new BankdbContext();
                    var newBankAccount = new Model.BankAccount
                    {
                        Iban = NewIBAN,
                        Name = NewName,
                        BankId = NewBankId,
                        CustomerId = NewCustomerId,
                        Balance = Balance
                    };

                    context.BankAccount.Add(newBankAccount);
                    context.SaveChanges();

                    Console.WriteLine("User Bank Account added to database.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                Console.WriteLine("Invalid Inputs");
            }
        }

        public static void UpdateCustomerData(string selectedUserId, string FirstName, string LastName)
        {
            int userId;
            int.TryParse(selectedUserId, out userId);

            if (userId > 0)
            {
                try
                {
                    var context = new BankdbContext();
                    var UpdatedCustomer = context.Customer.Where(c => c.Id == userId).FirstOrDefault();
                    UpdatedCustomer.FirstName = FirstName;
                    UpdatedCustomer.LastName = LastName;
                    context.Customer.Update(UpdatedCustomer);
                    context.SaveChanges();

                    Console.WriteLine("Customer Data Updated.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Inputs");
            }
        }

        public static void DeleteCustomer(string selectedUserId)
        {
            int customerId;
            int.TryParse(selectedUserId, out customerId);

            if (customerId > 0)
            {
                try
                {
                    var context = new BankdbContext();

                    var DeletedUser = context.Customer.Where(c => c.Id == customerId).FirstOrDefault();
                    context.Customer.Remove(DeletedUser);
                    context.SaveChanges();

                    Console.WriteLine("Customer Deleted.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Inputs");
            }

        }

        public static List<BankAccount> GetCustomerAccounts(string SelectedCustomer)
        {
            int customerId;
            int.TryParse(SelectedCustomer, out customerId);
            //List<string> IbanList = new List<string>();
            List<BankAccount> BankAccounts = null;

            if (customerId > 0)
            {
                try
                {
                    var context = new BankdbContext();
                    BankAccounts = context.BankAccount.Where(b => b.CustomerId == customerId)
                        .ToListAsync().Result;

                    /*
                    int i = 0;
                    foreach (var item in BankAccounts)
                    {
                        i++;
                        Console.WriteLine("{2}. Name: {0}, Balance: {1}", item.Name, item.Balance, i);
                        IbanList.Add(item.Iban);
                    }
                    */

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Inputs");
            }

            return BankAccounts;
            //return IbanList;
        }

        public static void DeleteCustomerAccount(string selectedIBAN)
        {
            if (selectedIBAN.Length >= 12 && selectedIBAN.Length <= 50)
            {
                try
                {
                    var context = new BankdbContext();

                    var DeletedAccount = context.BankAccount.Where(b => b.Iban == selectedIBAN).FirstOrDefault();
                    context.BankAccount.Remove(DeletedAccount);
                    context.SaveChanges();

                    Console.WriteLine("Account Deleted.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Invalid Inputs");
            }

        }


        //public static void AddCustomerTransaction(string selectedAccount, List<string> IbanList, string amount)
        public static void AddCustomerTransaction(string iban, string amount)
        {
            //int IbanSelector;
            //int.TryParse(selectedAccount, out IbanSelector);
            decimal newAmount;
            decimal.TryParse(amount, out newAmount);

            //string selectedIban = IbanList[IbanSelector - 1];
            string selectedIban = iban;

            try
            {
                var context = new BankdbContext();
                var newTransaction = new Model.BankAccountTransaction
                {
                    Iban = selectedIban,
                    Amount = newAmount,
                    TimeStamp = DateTime.Now
                };

                context.BankAccountTransaction.Add(newTransaction);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        public static List<BankAccountTransaction> GetAccountTransactions(string iban)
        {
            try
            {
                var context = new BankdbContext();
                var bankAccounts = context.BankAccountTransaction.Where(t => t.Iban == iban).ToListAsync().Result;

                return bankAccounts;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public static void GetCustomerTransactions(string selectedUser)
        {
            int customerId;
            int.TryParse(selectedUser, out customerId);
            try
            {
                var context = new BankdbContext();
                var bankAccounts = context.BankAccount.Where(b => b.CustomerId == customerId).ToListAsync().Result;

               foreach (var item in bankAccounts)
               {
                    var transactions = context.BankAccountTransaction
                        .Where(t => t.Iban == item.Iban)
                     .ToListAsync().Result;

                    Console.WriteLine("Bank Account: {0} - IBAN: {1}", item.Name, item.Iban);

                    if(transactions.Count == 0)
                    {
                        Console.WriteLine("No Transactions found.");
                    } else
                    {
                        foreach (var transaction in transactions)
                        {
                            Console.WriteLine("Id: {0}, Amount: {1}, Timestamp: {2}", transaction.Id, transaction.Amount, transaction.TimeStamp);
                        }
                    }
                }

                   

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
