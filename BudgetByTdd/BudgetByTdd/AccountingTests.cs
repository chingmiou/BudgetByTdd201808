using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;

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
            GivenBudgets();
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            GivenBudgets(new Budget { YearMonth = "201806", Amount = 30 });
            AmountShouldBe(1m, "20180601", "20180601");
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }

        private void AmountShouldBe(decimal expected, string startTime, string endTime)
        {
            var start = DateTime.ParseExact(startTime, "yyyyMMdd", null);
            var end = DateTime.ParseExact(endTime, "yyyyMMdd", null);
            Assert.AreEqual(expected, _accounting.TotalAmount(start, end));
        }

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }
    }

    public class Budget
    {
        public string YearMonth { get; set; }
        public int Amount { get; set; }
    }
}