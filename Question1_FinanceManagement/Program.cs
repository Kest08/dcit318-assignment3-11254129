using System;
using System.Collections.Generic;

namespace Question1_FinanceManagement
{
    public record Transaction(int Id, DateTime Date, decimal Amount, string Category);

    public interface ITransactionProcessor
    {
        void Process(Transaction transaction);
    }

    public class BankTransferProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[BankTransfer] Processed {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class MobileMoneyProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[MobileMoney] Sent {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class CryptoWalletProcessor : ITransactionProcessor
    {
        public void Process(Transaction transaction)
        {
            Console.WriteLine($"[CryptoWallet] Transferred {transaction.Amount:C} for {transaction.Category}");
        }
    }

    public class Account
    {
        public string AccountNumber { get; }
        public decimal Balance { get; protected set; }

        public Account(string accountNumber, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            Balance = initialBalance;
        }

        public virtual void ApplyTransaction(Transaction transaction)
        {
            Balance -= transaction.Amount;
            Console.WriteLine($"Balance after deduction: {Balance:C}");
        }
    }

    public sealed class SavingsAccount : Account
    {
        public SavingsAccount(string accountNumber, decimal initialBalance) : base(accountNumber, initialBalance) { }

        public override void ApplyTransaction(Transaction transaction)
        {
            if (transaction.Amount > Balance)
                Console.WriteLine("Insufficient funds");
            else
                base.ApplyTransaction(transaction);
        }
    }

    public class FinanceApp
    {
        private List<Transaction> _transactions = new();

        public void Run()
        {
            var account = new SavingsAccount("SA001", 1000m);

            var t1 = new Transaction(1, DateTime.Today, 120m, "Groceries");
            var t2 = new Transaction(2, DateTime.Today, 250m, "Utilities");
            var t3 = new Transaction(3, DateTime.Today, 150m, "Entertainment");

            new MobileMoneyProcessor().Process(t1);
            account.ApplyTransaction(t1);
            _transactions.Add(t1);

            new BankTransferProcessor().Process(t2);
            account.ApplyTransaction(t2);
            _transactions.Add(t2);

            new CryptoWalletProcessor().Process(t3);
            account.ApplyTransaction(t3);
            _transactions.Add(t3);

            Console.WriteLine("Transactions:");
            foreach (var t in _transactions)
                Console.WriteLine($"{t.Id}: {t.Category} {t.Amount:C}");
        }
    }

    public class Program
    {
        public static void Main() => new FinanceApp().Run();
    }
}
