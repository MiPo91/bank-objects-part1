using System;
using BankDB.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;

namespace BankDB
{
    public class BankUtilities
    {
        public static void AddBank(string NewName, string NewBic)
        {
            if (NewName.Length >= 2 && NewBic.Length >= 5 && NewName.Length <= 50 && NewBic.Length <= 10)
            {

                var context = new BankdbContext();
                var newBank = new Bank
                {
                    Name = NewName,
                    Bic = NewBic
                };

                context.Bank.Add(newBank);
                context.SaveChanges();

                Console.WriteLine("Bank Added to database.");
            } else
            {
                Console.WriteLine("Invalid Inputs");
            }
        }

        public static void UpdateBank(string Id, string NewName, string NewBic)
        {
            int bankId;
            int.TryParse(Id, out bankId);

            if (bankId > 0 && NewName.Length >= 2 && NewBic.Length >= 5 && NewName.Length <= 50 && NewBic.Length <= 10)
            {
                try
                {
                    var context = new BankdbContext();
                    var UpdatedBank = context.Bank.Where(b => b.Id == bankId).FirstOrDefault();
                    UpdatedBank.Name = NewName;
                    UpdatedBank.Bic = NewBic;
                    context.Bank.Update(UpdatedBank);
                    context.SaveChanges();

                    Console.WriteLine("Bank Updated.");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } else
            {
                Console.WriteLine("Invalid Inputs");
            }
        }


        public static void DeleteBank(string Id)
        {
            int bankId;
            int.TryParse(Id, out bankId);

            if (bankId > 0)
            {
                try
                {
                    var context = new BankdbContext();
                    var DeletedBank = context.Bank.Where(b => b.Id == bankId).FirstOrDefault();
                    context.Bank.Remove(DeletedBank);
                    context.SaveChanges();

                    Console.WriteLine("Bank Deleted.");
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

        public static void GetBanks()
        {
            var context = new BankdbContext();
            var banks = context.Bank.ToListAsync().Result;

            foreach (var item in banks)
            {
                Console.WriteLine("Id: {0}, Name: {1}, BIC: {2}", item.Id, item.Name, item.Bic);
            }
        }


        public static void GetBankAccounts()
        {
            var context = new BankdbContext();
            var banks = context.BankAccount
                .Include(b => b.Customer)
                .Include(b => b.Bank)
                .ToListAsync().Result;

           
            foreach (var item in banks)
            {
                Console.WriteLine("IBAN: {0}, Account Name: {1}, Bank Name: {2}, Customer Name: {3} {4}, Balance: {5}", item.Iban, item.Name, item.Bank.Name, item.Customer.FirstName, item.Customer.LastName, item.Balance);
            }
        }

        public static void GetUsers()
        {
            var context = new BankdbContext();
            var customers = context.Customer
                .Include(c => c.Bank)
                .ToListAsync().Result;

            foreach (var item in customers)
            {
                Console.WriteLine("Id: {0}, First Name: {1}, Last Name: {2}, Bank Name: {3}", item.Id, item.FirstName, item.LastName, item.Bank.Name);
            }
        }

    }
}
