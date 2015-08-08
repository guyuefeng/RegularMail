using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Common.Logging;
using Quartz;
using QuarzCommon;

namespace RegularMail.WinUI
{
    public partial class Form1 : Form
    {
        //日志 log4net
        ILog log = LogManager.GetLogger(typeof(Form1));

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            //初始化调度器工厂
            ISchedulerFactory sf = SchedulerManager.GetSchedulerFactory();
            //获取默认调度器
            IScheduler scheduler = SchedulerManager.GetScheduler();
            //安装全局任务监视器
            SchedulerManager.GetScheduler().AddGlobalJobListener(SchedulerManager.GetJobListener());
            //启动调度器
            scheduler.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnMailTest_Click(object sender, EventArgs e)
        {
            MutilThreadRun(MailExecute);
        }

        /// <summary>
        /// 多线程异步运行
        /// </summary>
        /// <param name="p_ThreadStart"></param>
        private void MutilThreadRun(System.Threading.ThreadStart p_ThreadStart)
        {

            this.btnMailTest.Enabled = false;

            System.Threading.Thread schedulerThread = new System.Threading.Thread(p_ThreadStart);
            schedulerThread.IsBackground = true;
            schedulerThread.Start();

            this.btnMailTest.Enabled = true;
        }

        private void MailExecute()
        {
            //删除日志的工作任务  
            DateTime startTime = DateTime.UtcNow.AddSeconds(1);

            //JobDetail m_MailJobDetail = new JobDetail("立即发邮件", Guid.NewGuid().ToString("N"), typeof(TimingMailJob.TimingMailNowJob));
            //JobDetail m_MailJobDetail = new JobDetail("立即发邮件", Guid.NewGuid().ToString("N"), typeof(TimingMailJob.MyTest));
            // jobs can be scheduled before sched.start() has been called

            // get a "nice round" time a few seconds in the future...
            //DateTime ts = TriggerUtils.GetNextGivenSecondDate(null, 15);

            //// job1 will only fire once at date/time "ts"
            JobDetail job = new JobDetail("job1", "group1", typeof(RegularMailJob.RegularMail_T2));
            //SimpleTrigger trigger = new SimpleTrigger("trigger1", "group1");
            //// set its start up time
            //trigger.StartTimeUtc = ts;
            //// set the interval, how often the job should run (10 seconds here) 

            //// set the number of execution of this job, set to 10 times. 
            //// It will run 10 time and exhaust.
            //trigger.RepeatCount = 100;


            //// schedule it to run!
            //log.Info(string.Format("{0} will run at: {1} and repeat: {2} times, every {3} seconds",
            //    job.FullName, "", trigger.RepeatCount, (10)));
            //log.Info("------- Waiting five minutes... ------------");
            Trigger m_MailTrigger = new SimpleTrigger(Guid.NewGuid().ToString("N"), Guid.NewGuid().ToString("N"), job.Name, job.Group, startTime, null, 0, TimeSpan.FromDays(7));



            log.Info("Starting service");

            log.Info("------- Scheduling Jobs ----------------");

            //CronTrigger trigger = new CronTrigger("打印", "group1", "0/30 * * * * ?");



            SchedulerManager.GetScheduler().ScheduleJob(job, m_MailTrigger);




        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SchedulerManager.GetScheduler().Shutdown(false);

            e.Cancel = false;
        }


    }
}
