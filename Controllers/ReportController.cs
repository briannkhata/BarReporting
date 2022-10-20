using BarReporting.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BiselaWeb.Controllers
{
    public class ReportController : Controller
    {
        BEntities db;
        // GET: Report
        public ActionResult Vat()
        {
            return View();
        }

        public ActionResult Stock()
        {
            using (db = new BEntities())
            {
                ViewBag.stock = db.vwStockReports.ToList();
            }
            return View();
        }

        public ActionResult FilterSales()
        {
            using (db = new BEntities())
            {
                ViewBag.paytypes = db.PaymentTypes.ToList();
                ViewBag.users = db.Users.Where(x=>x.Deleted == 0).ToList();
                ViewBag.saletypes = db.Sales.GroupBy(x=>x.SaleType).ToList();
            }
            return View();
        }
        public ActionResult RefreshSales()
        {
            DateTime FromDate = DateTime.Parse(Request.Form["FromDate"]);
            DateTime ToDate = DateTime.Parse(Request.Form["ToDate"]);

            var SaleType = Request.Form["SaleType"];
            int PaymentType = short.Parse(Request.Form["PaymentTypeId"]);
            int User = short.Parse(Request.Form["UserId"]);

            ViewBag.Title = "Sales For | " + FromDate.ToString("dd/M/yyyy") + " To " + ToDate.ToString("dd/M/yyyy");
            using (db = new BEntities())
            {
                if (SaleType == "" && PaymentType.ToString() == null && User.ToString() == null)
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate).ToList();
                }

                if(SaleType != "")
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.SaleType == SaleType).ToList();
                }

                if (String.IsNullOrEmpty(PaymentType.ToString()))
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.PaymentTypeId == PaymentType).ToList();
                }

                if (User.ToString() != null)
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.UserId == User).ToList();
                }

                if (SaleType != "" && PaymentType.ToString() !="")
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.SaleType == SaleType && x.PaymentTypeId == PaymentType).ToList();
                }

                if (SaleType != "" && User.ToString() != "")
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.SaleType == SaleType && x.UserId == User).ToList();
                }

                if (PaymentType.ToString() != "" && User.ToString() != "")
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.PaymentTypeId == PaymentType && x.UserId == User).ToList();
                }

                if (PaymentType.ToString() != "" && User.ToString() != "" && SaleType !="")
                {
                    ViewBag.sales = db.vwSalesReports.Where(x => x.DateSold >= FromDate && x.DateSold <= ToDate && x.PaymentTypeId == PaymentType && x.UserId == User && x.SaleType == SaleType).ToList();
                }
            }
            return View();
        }

        public ActionResult FilterReceivings()
        {
            return View();
        }

        public ActionResult RefreshReceivings()
        {
            DateTime FromDate = DateTime.Parse(Request.Form["FromDate"]);
            DateTime ToDate = DateTime.Parse(Request.Form["ToDate"]);
            ViewBag.Title = "Receivings  | " + FromDate.ToString("dd MMM yyyy") + " To " + ToDate.ToString("dd MMM yyyy");
            using (db = new BEntities())
            {
                ViewBag.receivings = db.vwReceivingReports.Where(x => x.ReceivingDate >= FromDate && x.ReceivingDate <= ToDate).ToList();
            }
            return View();
        }

        public ActionResult FilterIncomes()
        {
            using(db = new BEntities())
            {
                ViewBag.incomeTypes = db.vwIncomTypes.ToList();
            }
            return View();
        }

        public ActionResult RefreshIncomes()
        {
            int IncomeType = short.Parse(Request.Form["IncomeTypeId"]);
            using (db = new BEntities())
            {
                ViewBag.Title = "Incomes  For | " + db.IncomeTypes.Where(x => x.IncomeTypeId == IncomeType).SingleOrDefault().IncomeTypeName;
                ViewBag.incomes = db.vwIncomes.Where(x => x.IncomeTypeId == IncomeType).ToList();
            }
            return View();
        }

        public ActionResult FilterExpenses()
        {
            using (db = new BEntities())
            {
                ViewBag.expenseTypes = db.vwExpenseTypes.ToList();
            }
            return View();
        }

        public ActionResult RefreshExpenses()
        {
            int ExpenseType = short.Parse(Request.Form["ExpenseTypeId"]);
            using (db = new BEntities())
            {
                ViewBag.Title = "Expenses  | " + db.ExpenseTypes.Where(x => x.ExpenseTypeId == ExpenseType).SingleOrDefault().ExpenseTypeName;
                ViewBag.expenses = db.vwExpenses.Where(x => x.ExpenseTypeId == ExpenseType).ToList();
            }
            return View();
        }

        public ActionResult ExpiredProducts()
        {
            var today = DateTime.Now;
            using (db = new BEntities())
            {
                ViewBag.Title = "Expired Products";
                ViewBag.expired = db.vwStockReports.Where(x => x.ExpiryDate < today).ToList();
            }
            return View();
        }

        public ActionResult ExpiringProducts()
        {
            using (db = new BEntities())
            {
                double ExpiryAlert = (double)db.Shops.SingleOrDefault().ExpiryAlert;
                var expiryDate = DateTime.Now.AddDays(-ExpiryAlert);

                ViewBag.Title = "Expiring Products";
                ViewBag.expiring = db.vwStockReports.Where(x => x.ExpiryDate <= expiryDate).ToList();
            }
            return View();
        }
    }
}