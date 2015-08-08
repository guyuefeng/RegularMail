using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Common.Logging;
using QuarzCommon;
using Quartz;

namespace RegularMail.Service
{
    public partial class RegularMailService : ServiceBase
    {
        ILog log = LogManager.GetLogger(typeof(RegularMailService));

        public RegularMailService()
        {
            InitializeComponent();

            //初始化调度器工厂
            ISchedulerFactory sf = SchedulerManager.GetSchedulerFactory();
            //获取默认调度器
            IScheduler scheduler = SchedulerManager.GetScheduler();
            SchedulerManager.GetScheduler().AddGlobalJobListener(SchedulerManager.GetJobListener());

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                
                //0 0 12 * * ?	 每天12点触发
                //0 15 10 ? * *	 每天10点15分触发
                //0 15 10 * * ?	 每天10点15分触发
                //0 15 10 * * ? *	 每天10点15分触发
                //0 15 10 * * ? 2005	 2005年每天10点15分触发
                //0 * 14 * * ?	 每天下午的 2点到2点59分每分触发
                //0 0/5 14 * * ?	 每天下午的 2点到2点59分(整点开始，每隔5分触发)
                //0 0/5 14,18 * * ?	 每天下午的 2点到2点59分(整点开始，每隔5分触发)
                //每天下午的 18点到18点59分(整点开始，每隔5分触发)
                //0 0-5 14 * * ?	 每天下午的 2点到2点05分每分触发
                //0 10,44 14 ? 3 WED	 3月分每周三下午的 2点10分和2点44分触发
                //0 15 10 ? * MON-FRI	 从周一到周五每天上午的10点15分触发
                //0 15 10 15 * ?	 每月15号上午10点15分触发
                //0 15 10 L * ?	 每月最后一天的10点15分触发
                //0 15 10 ? * 6L	 每月最后一周的星期五的10点15分触发
                //0 15 10 ? * 6L 2002-2005	 从2002年到2005年每月最后一周的星期五的10点15分触发
                //0 15 10 ? * 6#3	 每月的第三周的星期五开始触发
                //0 0 12 1/5 * ?	 每月的第一个中午开始每隔5天触发一次
                //0 11 11 11 11 ?	 每年的11月11号 11点11分触发(光棍节)

                //发邮件工作任务  
                JobDetail m_TimingMailJobDetail1 = new JobDetail("打印测试文件", Guid.NewGuid().ToString("N"), typeof(RegularMailJob.RegularMail_T2));
                JobDetail m_TimingMailJobDetail2 = new JobDetail("打印测试文件", Guid.NewGuid().ToString("N"), typeof(RegularMailJob.RegularMail_T2));
                JobDetail m_TimingMailJobDetail3 = new JobDetail("打印测试文件", Guid.NewGuid().ToString("N"), typeof(RegularMailJob.RegularMail_T2));
                //Trigger m_TimingMailTrigger = new SimpleTrigger(Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"), m_TimingMailJobDetail.Name, m_TimingMailJobDetail.Group, DateTime.UtcNow, null, 0, TimeSpan.FromDays(7));

                //CronTrigger trigger = new CronTrigger("打印", "group1", "0/30 * * * * ?");


                //从周一到周五每天上午的10点15分触发
                CronTrigger trigger1 = new CronTrigger("打印1", "group1", "0 20 15 ? * MON-FRI");

                CronTrigger trigger2 = new CronTrigger("打印2", "group2", "0 25 15 ? * MON-FRI");

                CronTrigger trigger3 = new CronTrigger("打印3", "group3", "0 15 15 ? * MON-FRI");
                

                SchedulerManager.GetScheduler().ScheduleJob(m_TimingMailJobDetail1, trigger1);
                log.Info("任务:" + m_TimingMailJobDetail1.FullName + " 已经调度成功！");
                SchedulerManager.GetScheduler().ScheduleJob(m_TimingMailJobDetail2, trigger2);
                log.Info("任务:" + m_TimingMailJobDetail2.FullName + " 已经调度成功！");
                SchedulerManager.GetScheduler().ScheduleJob(m_TimingMailJobDetail3, trigger3);
                log.Info("任务:" + m_TimingMailJobDetail3.FullName + " 已经调度成功！");



                SchedulerManager.GetScheduler().Start();

                log.Info("service started ok");
            }
            catch (Exception ex)
            {
                log.Error("service started fail", ex);
                this.Stop();
            }
        }

        protected override void OnStop()
        {
            try
            {
                IScheduler scheduler = SchedulerManager.GetScheduler();
                scheduler.Shutdown(false);

                log.Info("service stopped ok");
            }
            catch (Exception ex)
            {
                log.Error("service stopped fail", ex);
            }
        }
    }
}
