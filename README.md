# RegularMail
定时发邮件
/// <summary>
    /// 发送邮件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnFS_Click(object sender, EventArgs e)
    {
        string mailfrom = this.lblfajianren.Text.ToString().Trim();
	//设置发件人
        MailAddress from = new MailAddress("发件人邮件", mailfrom);
        //邮件发送对象
	MailMessage mail = new MailMessage();
        //主题
        mail.Subject = this.lblzhuti.Text.ToString().Trim();
	//发件
        mail.From = from;
	//设置收件人
        mail.To.Add("收件人邮箱");
        //抄送
        string scc = "抄送人邮箱";
	mail.CC.Add(scc);
        //暗送
	string sbcc = "暗送人邮箱"
	mail.Bcc.Add(sbcc);
	//邮件内容
        mail.Body = this.txta.Value +
           "\r\n您好！" +
           "\r\n" + "1" +
           "\r\n" + "2" +
           "\r\n" + " 3" ;
        mail.BodyEncoding = System.Text.Encoding.UTF8;
	//发送附件
        mail.Priority = MailPriority.Normal;
        string pno = Request.QueryString["po"];
	//单个附件
        string fileName2 = "E:/Program Files/DltechNot/LiveFlowNet V5.3/WebSite/LiveFlowWeb/binx/order/email/Upload/" + pno + ".html";
        mail.Attachments.Add(new Attachment(fileName2));
	//多个附件
        string fname = this.txtFujian.Value.ToString().Trim();
        string ffile = this.txtLujing.Value.ToString().Trim();

        if (fname != "")
        {
            string a = fname;
            string[] b = Regex.Split(a, ",", RegexOptions.IgnoreCase);
            string c = "";
            for (int i = 0; i < b.Length; i++)
            {
                c = "E:/Program Files/DltechNot/LiveFlowNet V5.3/WebSite/LiveFlowWeb/upload/" + ffile + "/" + b[i];
                mail.Attachments.Add(new Attachment(c));
            }
        }

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
        SmtpClient client = new SmtpClient();
        client.Host = "smtp.sina.com";
        client.Port = 25;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("发件人邮箱", "密码");
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.Send(mail);
        ScriptManager.RegisterStartupScript(this.Page, typeof(Page), "Sucess", "alert('邮件发送成功！');", true);

        
    }
