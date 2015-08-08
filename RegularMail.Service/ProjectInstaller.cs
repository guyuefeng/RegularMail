using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using Common.Logging;


namespace RegularMail.Service
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        ILog log = LogManager.GetLogger(typeof(ProjectInstaller));

        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void ProjectInstaller_BeforeInstall(object sender, InstallEventArgs e)
        {
            DoSetup("BeforeInstall");
        }

        private void ProjectInstaller_BeforeUninstall(object sender, InstallEventArgs e)
        {
            DoSetup("BeforeUninstall");
        }

        private void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            DoSetup("AfterInstall");
        }

        private void ProjectInstaller_Committing(object sender, InstallEventArgs e)
        {
            DoSetup("Committing");
        }

        private void ProjectInstaller_Committed(object sender, InstallEventArgs e)
        {
            DoSetup("Committed");
        }

        private void ProjectInstaller_AfterUninstall(object sender, InstallEventArgs e)
        {
            DoSetup("AfterUninstall");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p_strSetupStatus"></param>
        private void DoSetup(string p_strSetupStatus)
        {
            log.Info("服务安装：" + p_strSetupStatus);
        }
    }
}
