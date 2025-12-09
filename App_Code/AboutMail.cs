using System;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;

/// <summary>
/// SendMail 的摘要描述
/// </summary>
public class AboutMail
{
  readonly string AdminMailAddress;
  readonly string AdminMailCCAddress;
  public AboutMail()
  {
    try
    {
      AdminMailAddress = ConfigurationManager.AppSettings["AdminMail"];
      AdminMailCCAddress = ConfigurationManager.AppSettings["AdminMail"];
    }
    catch
    {
      AdminMailAddress = "jamestung@kcis.ntpc.edu.tw";
      AdminMailCCAddress = "jamestung@kcis.ntpc.edu.tw";
    }
  }

  /// <summary>
  /// 寄送多位使用者信件，用 ; 分開
  /// </summary>
  /// <param name="Host">網站位置，用來判斷是否為localhost(本機)</param>
  /// <param name="Mail_body">內容</param>
  /// <param name="Mail_Subject">標題</param>
  /// <param name="Mail_Mutiaddress">電子信箱，用 ; 分開</param>
  public void Send_Mail(string Host, string Mail_body, string Mail_Subject, string Mail_Mutiaddress)
  {
    if (HttpContext.Current.Request.IsLocal || HttpContext.Current.Server.MachineName.Contains("TEST"))
    {
      Mail_body = Mail_body + "<br>";
      Mail_body = Mail_body + "此為測試寄信，原收件者為" + Mail_Mutiaddress;

      string Mail = HttpContext.Current.Session["EMail"].ToString();
      Mail_Mutiaddress = string.Format("{0};{1}", Mail, AdminMailAddress);
    }

    MailMessage myMail = new MailMessage();
    myMail.Subject = Mail_Subject;
    myMail.From = new MailAddress("kcismail@kcis.ntpc.edu.tw");
    foreach (string Mail_address in Mail_Mutiaddress.Split(';'))
    {
      if (Mail_address != String.Empty)
      {
        myMail.To.Add(new MailAddress(Mail_address));
      }
    }
    myMail.BodyEncoding = Encoding.GetEncoding(65001);
    myMail.SubjectEncoding = Encoding.GetEncoding(65001);
    myMail.IsBodyHtml = true;
    myMail.Body = Mail_body;
    SmtpClient smtpServer = new SmtpClient("192.168.10.2", 25);
    smtpServer.Send(myMail);
  }

  /// <summary>
  /// 寄送多位使用者信件，用 ; 分開；副本也是用 ; 分開
  /// </summary>
  /// <param name="Host">網站位置，用來判斷是否為localhost(本機)</param>
  /// <param name="Mail_body">內容</param>
  /// <param name="Mail_Subject">標題</param>
  /// <param name="Mail_Mutiaddress">電子信箱，用 ; 分開</param>
  /// <param name="MailCC_Mutiaddress">電子信箱，用 ; 分開</param>
  public void Send_Mail(string Host, string Mail_body, string Mail_Subject, string Mail_Mutiaddress, string MailCC_Mutiaddress)
  {
    if (HttpContext.Current.Request.IsLocal || HttpContext.Current.Server.MachineName.Contains("TEST"))
    {
      Mail_body = Mail_body + "<br>";
      Mail_body = Mail_body + "此為測試寄信，原收件者為" + Mail_Mutiaddress;
      Mail_body = Mail_body + "<BR>原副本為" + MailCC_Mutiaddress;

      string Mail = HttpContext.Current.Session["EMail"].ToString();
      Mail_Mutiaddress = string.Format("{0};{1}", Mail, AdminMailAddress);
      MailCC_Mutiaddress = AdminMailCCAddress;
    }

    MailMessage myMail = new MailMessage();
    myMail.Subject = Mail_Subject;
    myMail.From = new MailAddress("kcismail@kcis.ntpc.edu.tw");
    foreach (string Mail_address in Mail_Mutiaddress.Split(';'))
    {
      if (Mail_address != String.Empty)
      {
        myMail.To.Add(new MailAddress(Mail_address));
      }
    }
    foreach (string Mail_address in MailCC_Mutiaddress.Split(';'))
    {
      if (Mail_address != String.Empty)
      {
        myMail.CC.Add(new MailAddress(Mail_address));
      }
    }
    myMail.BodyEncoding = Encoding.GetEncoding(65001);
    myMail.SubjectEncoding = Encoding.GetEncoding(65001);
    myMail.IsBodyHtml = true;
    myMail.Body = Mail_body;
    SmtpClient smtpServer = new SmtpClient("192.168.10.2", 25);
    smtpServer.Send(myMail);
  }
}