using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Common.Logging;
using Quartz;


namespace RegularMailJob
{
    public class RegularMail_T1:IJob
    {
        ILog log = LogManager.GetLogger(typeof(RegularMail_T1));

        #region IJob 成员

        public void Execute(JobExecutionContext context)
        {
            string m_strJobName = context.JobDetail.FullName;

            //如果失败则再试 1 次
            if (context.RefireCount == 1)
            {
                log.Info(m_strJobName + "任务 重试" + "1" + "次后还是失败！");
                return;
            }
            EmailHelper email = new EmailHelper();
            email.Password = "Guyuefengxue2";
            email.EmailFrom = "liguofeng@swirebev.com";
            email.EmailTo = "liguofeng@swirebev.com";

            string emailBody=@" <table width='800' border='1' cellPadding='0' cellSpacing='1' borderColor='#c0c0c0' style='BORDER-COLLAPSE:collapse;font-size:12px;color:#535353;'>
                             <tr height='56'><td height='56' colspan='6' align='center' valign='middle' style=' font-size: 25px;font-weight: bold;'>******登记表</td></tr>
                             <tr><td colspan='4' height='26'>工号:</td>
                             <td width='81'>编号：</td><td>INS-REPORT-MAIL-</td>
                             </tr>
                             </table>";

            email.SendEmail("测试邮件", emailBody);
        }

        #endregion
    }
}
