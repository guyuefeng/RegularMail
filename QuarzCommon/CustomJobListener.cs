using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Logging;
using Quartz;
using Quartz.Job;
using Quartz.Xml;

namespace QuarzCommon
{
    /// <summary>
    /// 预警报表生成任务监视器
    /// </summary>
    public class CustomJobListener : IJobListener
    {
        ILog log = LogManager.GetLogger(typeof(CustomJobListener));

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
        {
            get { return "CustomJobListener"; }
        }

        public virtual void JobToBeExecuted(JobExecutionContext inContext)
        {
            string m_strJobName = inContext.JobDetail.FullName;
            log.Info("报表监视器： " + m_strJobName + " Is about to be executed。\r\n");
        }

        public virtual void JobExecutionVetoed(JobExecutionContext inContext)
        {
            string m_strJobName = inContext.JobDetail.FullName;

            log.Info("报表监视器： " + m_strJobName + " Execution was vetoed。\r\n");
        }

        public virtual void JobWasExecuted(JobExecutionContext inContext, JobExecutionException inException)
        {
            string m_strJobName = inContext.JobDetail.FullName;

            log.Info("报表监视器： " + m_strJobName + " Execution was Executed。\r\n");
            try
            {
                //如果任务发生错误
                if (inException != null)
                {
                    log.Info(inException.Message);

                    //重试
                    inException.RefireImmediately = true;



                    //if (inContext.RefireCount != Model.DelLogCfg.RefireCount) return;
                }
                
            }
            catch (System.Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }
    }
}
