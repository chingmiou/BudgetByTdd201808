using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace BudgetByTdd
{
    [TestClass]
    public class AccountingTests
    {
        private Accounting _accounting;
        private IBudgetRepository _budgetRepository = Substitute.For<IBudgetRepository>();

        [TestMethod]
        public void no_budgets()
        {
            GivenBudgets();
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void no_period_overlap_before_budget_firstday()
        {
            GivenBudgets(new Budget { YearMonth = "201807", Amount = 31 });
            AmountShouldBe(0m, "20180601", "20180601");
        }

        [TestMethod]
        public void period_inside_budget_month()
        {
            GivenBudgets(new Budget { YearMonth = "201806", Amount = 30 });
            AmountShouldBe(1m, "20180601", "20180601");
        }

        [TestInitialize]
        public void TestInit()
        {
            _accounting = new Accounting(_budgetRepository);
        }

        private void AmountShouldBe(decimal expected, string startTime, string endTime)
        {
            var start = DateTime.ParseExact(startTime, "yyyyMMdd", null);
            var end = DateTime.ParseExact(endTime, "yyyyMMdd", null);
            var totalAmount = _accounting.TotalAmount(start, end);
            Assert.AreEqual(expected, totalAmount);
        }

        private void GivenBudgets(params Budget[] budgets)
        {
            _budgetRepository.GetAll().Returns(budgets.ToList());
        }
    }
}