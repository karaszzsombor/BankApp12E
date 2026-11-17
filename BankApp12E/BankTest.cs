using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp12E
{
    [TestFixture]
    public class BankTest
    {
        Bank bank;

        [SetUp]
        public void SetUp()
        {
            bank = new Bank();
            bank.CreateAccount("1234", "Gipsz Jakab");
        }

        [Test]
        public void StartingBalanceIsZero()
        {
            Assert.That(bank.GetBalance("1234"), Is.Zero);
        }

        [Test]
        public void DepositOnce()
        {
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
        }

        [Test]
        public void DepositMultiple()
        {
            bank.Deposit("1234", 10000);
            bank.Deposit("1234", 15000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(25000));
        }

        [Test]
        public void DepositNegativeAmount()
        {
            Assert.That(() => bank.Deposit("1234", -10000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositMultipleAccounts()
        {
            bank.CreateAccount("5678", "Jipsz Gakab");
            bank.Deposit("1234", 10000);
            Assert.That(bank.GetBalance("1234"), Is.EqualTo(10000));
            Assert.That(bank.GetBalance("5678"), Is.Zero);
        }

        [Test]
        public void GetBalanceOfInvalidAccount()
        {
            Assert.That(() => bank.GetBalance("5678"), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void DepositToInvalidAccount()
        {
            Assert.That(() => bank.Deposit("5678", 5000), Throws.TypeOf<ArgumentException>());
        }

        [Test]
        public void WithdrawTestNegativeAmount()
        {
            Assert.That(() => bank.Withdraw("1234", -1000), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void WithdrawTestInvalidAccount()
        {
            Assert.That(() => bank.Withdraw("5678", 1000), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void WithdrawTestInvalidAmount()
        {
            bank.Deposit("1234", 5000);
            Assert.That(() => bank.Withdraw("1234",5001), Is.False);
        }

        [Test]
        public void TransferTestInvalidfromAccount()
        {
            Assert.That(() => bank.Transfer("5678", "1234", 100), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void TransferTestInvalidtoAccount()
        {
            Assert.That(() => bank.Transfer("1234", "5678", 100), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void TransferTestInvalidTransferAmount()
        {
            bank.CreateAccount("5678", "Jipsz Hakab");
            bank.Deposit("5678", 500);
            Assert.That(() => bank.Transfer("5678", "1234", 501), Is.False);
        }

        [Test]
        public void CloseAccountTest()
        {
            Assert.That(() => bank.CloseAccount("5678"), Throws.TypeOf<ArgumentException>());
        }
        [Test]
        public void CloseAccountTestNonZeroBalance()
        {
            bank.Deposit("1234", 1);
            Assert.That(() => bank.CloseAccount("1234"), Throws.TypeOf<InvalidOperationException>());
        }

        [Test]
        public void GetOwnerTest()
        {
            Assert.That(() => bank.GetOwner("5678"), Throws.TypeOf<ArgumentException>());
        }
    }
}
