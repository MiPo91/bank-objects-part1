using System;
using BankDB;
using System.Collections.Generic;

namespace BankDB
{
    class Customer
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Choose Option: 1.Add new Customer & Account, 2. Update Customer Data, 3. Delete Customer");
            Console.WriteLine("4. Get User Bank Accounts & Balance, 5. Add New Transaction to user, 6. List all user Transactions");
            string selectedOption = Console.ReadLine();

            if (selectedOption == "1")
            {
                Console.WriteLine("Add New Customer");
                Console.WriteLine("Give First Name:");
                string FirstName = Console.ReadLine();
                Console.WriteLine("Give Last Name:");
                string LastName = Console.ReadLine();

                Console.WriteLine("Choose Bank from List:");
                BankUtilities.GetBanks();
                string selectedBank = Console.ReadLine();

                List<int> NewUser = CustomerUtilities.AddCustomer(FirstName, LastName, selectedBank); // add new user

                Console.WriteLine("Add New Bank Account for created Customer");
                Console.WriteLine("Give IBAN:");
                string Iban = Console.ReadLine();
                Console.WriteLine("Give Account Name:");
                string AccountName = Console.ReadLine();
                Console.WriteLine("Give Balance:");
                string Balance = Console.ReadLine();
                int BankId = NewUser[0];
                int CustomerId = NewUser[1];


                CustomerUtilities.AddCustomerBankAccount(Iban, AccountName, BankId, CustomerId, Balance); // add bank account to user
            }
            else if (selectedOption == "2")
            {
                Console.WriteLine("Update Customer");
                Console.WriteLine("Select Customer by giving Id from the list above.");
                BankUtilities.GetUsers();
                string selectedCustomer = Console.ReadLine();

                Console.WriteLine("Give First Name:");
                string FirstName = Console.ReadLine();

                Console.WriteLine("Give Last Name:");
                string LastName = Console.ReadLine();

                CustomerUtilities.UpdateCustomerData(selectedCustomer, FirstName, LastName);
            }
            else if (selectedOption == "3")
            {
                Console.WriteLine("Delete Customer");
                Console.WriteLine("Select Customer by giving Id from the list above.");
                BankUtilities.GetUsers();
                string selectedCustomer = Console.ReadLine();

                CustomerUtilities.DeleteCustomer(selectedCustomer);
            }
            else if (selectedOption == "4")
            {
                Console.WriteLine("Get Customer Accounts & Balance");
                Console.WriteLine("Select Customer by giving Id from the list above.");
                BankUtilities.GetUsers();
                string selectedCustomer = Console.ReadLine();

                CustomerUtilities.GetCustomerAccounts(selectedCustomer);
            }
            else if (selectedOption == "5")
            {
                Console.WriteLine("Add customer transaction");
                Console.WriteLine("Select Customer by giving Id from the list above.");
                BankUtilities.GetUsers();
                string selectedCustomer = Console.ReadLine();
                Console.WriteLine("Select Bank Account to add transaction");
                List<string> IbanList = CustomerUtilities.GetCustomerAccounts(selectedCustomer);
                string selectedAccount = Console.ReadLine();

                Console.WriteLine("Give transaction amount:");
                string amount = Console.ReadLine();


                CustomerUtilities.AddCustomerTransaction(selectedAccount, IbanList, amount);

                
            }
            else if(selectedOption == "6")
            {
                Console.WriteLine("Get customer transactions");
                Console.WriteLine("Select Customer by giving Id from the list above.");
                BankUtilities.GetUsers();
                string selectedCustomer = Console.ReadLine();

                CustomerUtilities.GetCustomerTransactions(selectedCustomer);
            }


                //CustomerUtilities.GetUsers();


                Console.ReadLine();
        }

    }
}
