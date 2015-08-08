using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Common.Logging;

namespace RegularMailJob
{
    public class RegularMail_T2:IJob 
    {
        ILog log = LogManager.GetLogger(typeof(RegularMail_T2));
        #region IJob 成员

        public void Execute(JobExecutionContext context)
        {
            string sql = "select top 10 OrderID,CustomerID,ShipName,OrderDate from Orders ";
            System.Data.DataTable dt = SqlHelper.ExecuteDataset(sql).Tables[0];

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                log.Info("任务测试 打印 orderid:" + dt.Rows[i]["OrderID"].ToString() + "customerid:" + dt.Rows[i]["OrderID"].ToString() + "name:" + dt.Rows[i]["ShipName"].ToString() + DateTime.Now.ToLongTimeString() + " 执行！");  
            }
            
        }

        #endregion
    }
}