using System;
using BankDB;

namespace BankDB
{
    class Bank
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Option: 1. Add Bank, 2. Update Bank, 3. Delete Bank");
            string selectedOption = Console.ReadLine();

            if (selectedOption == "1")
            {
                // New Bank
                Console.WriteLine("Add new Bank");
                Console.WriteLine("Write Bank Name");
                string NewBankName = Console.ReadLine();
                Console.WriteLine("Write Bank BIC");
                string NewBankBIC = Console.ReadLine();

                BankUtilities.AddBank(NewBankName, NewBankBIC);
            }
            else if (selectedOption == "2")
            {
                // Update Bank
                Console.WriteLine("Update Bank");
                BankUtilities.GetBanks();
                Console.WriteLine("Select Bank by giving Bank Id from the list above.");
                string selectedBank = Console.ReadLine();

                Console.WriteLine("Give new Name:");
                string NewName = Console.ReadLine();

                Console.WriteLine("Give new Bic:");
                string NewBic = Console.ReadLine();

                BankUtilities.UpdateBank(selectedBank, NewName, NewBic);
            }
            else if (selectedOption == "3")
            {
                // Delete Bank
                Console.WriteLine("Delete Bank");
                BankUtilities.GetBanks();
                Console.WriteLine("Select Bank by giving Bank Id from the list above.");
                string selectedBank = Console.ReadLine();

                BankUtilities.DeleteBank(selectedBank);
            }

            Console.ReadLine();
        }

    }
}
