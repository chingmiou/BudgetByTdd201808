using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace BudgetByTdd
{
    [TestClass]
    public class AccountingTests
    {
        private IBudgetRepository _budgetRepository = Substitute.For<IBudgetRepository>();
        private Accounting _accounting;

        [TestMethod]
        public void no_budgets()
        {
            AmountShouldBe(0m, "20180601", "20180601");
        }

        private void AmountShouldBe(decimal expected, string startTime, string endTime)
        {
            var start = DateTime.ParseExact(startTime, "yyyyMMdd", null);
            var end = DateTime.ParseExact(endTime, "yyyyMMdd", null);
            var totalAmount = _accounting.TotalAmount(start, end);
            Assert.AreEqual(expected, totalAmount);
        }

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }
    }
}