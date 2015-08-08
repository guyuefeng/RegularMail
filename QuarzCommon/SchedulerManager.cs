using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Quartz;
using Quartz.Impl;

namespace QuarzCommon
{
    /// <summary>
    /// 调度管理器
    /// </summary>
    public static class SchedulerManager
    {
        private static IScheduler scheduler;
        private static readonly object lockObj = new object();
        private static readonly object lockObj1 = new object();
        private static readonly object lockObj2 = new object();
        private static ISchedulerFactory sf;
        private static IJobListener jobListener;

        /// <summary>
        /// 调度工厂
        /// </summary>
        /// <returns></returns>
        public static ISchedulerFactory GetSchedulerFactory()
        {
            if (sf == null)
            {
                lock (lockObj1)
                {
                    if (sf == null)
                    {
                        sf = new StdSchedulerFactory();
                    }
                }
            }
            return sf;
        }

        /// <summary>
        /// 调度器
        /// </summary>
        /// <returns></returns>
        public static IScheduler GetScheduler()
        {
            if (scheduler == null)
            {
                lock (lockObj)
                {
                    if (scheduler == null)
                    {
                        //获取默认调度器
                        scheduler = GetSchedulerFactory().GetScheduler();
                    }
                }
            }
            return scheduler;
        }

        /// <summary>
        /// 任务监视器
        /// </summary>
        /// <returns></returns>
        public static IJobListener GetJobListener()
        {
            if (jobListener == null)
            {
                lock (lockObj2)
                {
                    if (jobListener == null)
                    {
                        //获取监听
                        jobListener = new CustomJobListener();
                    }
                }
            }
            return jobListener;
        }
    }
}
