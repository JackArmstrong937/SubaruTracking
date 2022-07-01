using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SubaruEfficiencyTracking.Models;

namespace SubaruEfficiencyTracking.Logic
{
    public class PayPeriodService
    {
        private IDBConnector _DB;

        public PayPeriodService(IDBConnector db)
        {
            _DB = db;
        }

        public void VerifyPayPeriods()
        {
            List<PayPeriodModel> Current = _DB.SelectRows<PayPeriodModel>().OrderByDescending(m => m.EndDate).ToList<PayPeriodModel>();
            if (Current.Count <= 0 || Current.FindAll(m => m.StartDate < DateTime.Now && m.EndDate > DateTime.Now).Count == 0)
            {
                PayPeriodModel NewPayPeriod = new PayPeriodModel();
                NewPayPeriod.PayPeriodGuid = Guid.NewGuid();

                DateTime PeriodStart = DateTime.Now;
                DateTime PeriodEnd = DateTime.Now;
                FindPeriodStartEnd(DateTime.Now, ref PeriodStart, ref PeriodEnd);
                NewPayPeriod.StartDate = PeriodStart;
                NewPayPeriod.EndDate = PeriodEnd;

                _DB.CreateRow<PayPeriodModel>(NewPayPeriod);
            }
        }

        private void FindPeriodStartEnd(DateTime TargetDate, ref DateTime StartDate, ref DateTime EndDate)
        {
            DateTime FirstGoodPeriodStart = DateTime.Parse("6/16/2022");
            
            DateTime TestStart = FirstGoodPeriodStart;
            DateTime TestEnd = FirstGoodPeriodStart.AddDays(14).AddSeconds(-1);

            while (TestStart > TargetDate || TestEnd < TargetDate)
            {
                TestStart = TestStart.AddDays(14);
                TestEnd = TestEnd.AddDays(14);
            }
            StartDate = TestStart;
            EndDate = TestEnd;
        }

       
    }

}
