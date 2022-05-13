using UnityEngine;
using System.Net;
using UnityEngine.UI;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class EmailFactory// : MonoBehaviour
{
    string toSendTo = "daedalus.scripts@gmail.com";
    // [SerializeField]
    //Default email will be sent to the daedalus email to avoid pinging anyone else
    public EmailFactory()
    {
    }
    public string setEmail()
    {
        return toSendTo;
    }

    public void SendEmail(string map = "LabyrinthMap.png", string recipient = "daedalus.scripts@gmail.com", string subject = "Open this Message to See the Daedalus Maze Map!", string body = "This is a good email :) -The Daedalus Team")
    {
        Debug.Log($"Attempted to send email to: {recipient}\nwith subject: {subject}");
        //string toSendTo = recipient;
        string toSendTo = PersistentStorage.emailReturn().ToString();
        Debug.Log("The Email Factory sets the recepient to: " + toSendTo);
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
        SmtpServer.Timeout = 10000;
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Port = 587;

        mail.From = new MailAddress("daedalus.scripts@gmail.com");
        //mail.To.Add(new MailAddress(recipientEmail.text));
        //mail.To.Add(new MailAddress(PersistentStorage.emailReturn()));
        //Debug.Log("Returned email from stored value");
        mail.To.Add(toSendTo);

        mail.Subject = subject;
        //mail.Body = bodyMessage.text;
        mail.Body = body;

        LinkedResource mapImage = new LinkedResource(map);

        mapImage.ContentId = "map";

        AlternateView html = AlternateView.CreateAlternateViewFromString($"<p>{body}</p><img src=cid:map>", null, "text/html");
        html.LinkedResources.Add(mapImage);

        AlternateView plain = AlternateView.CreateAlternateViewFromString("This message is viewable if your email does not let you view html. If so, this program does not work. We are working on fixing this problem. Thank you for being patient.", null, "text/plain");

        mail.AlternateViews.Add(html);
        mail.AlternateViews.Add(plain);

        SmtpServer.Credentials = new System.Net.NetworkCredential("daedalus.scripts@gmail.com", "pivfos-fescip-9sebdU") as ICredentialsByHost; SmtpServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        };

        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        SmtpServer.Send(mail);

        Debug.Log($"Sent an email to: {recipient}\nwith subject: {subject}");
    }


    /** public void SendText(string phoneNumber)
     {
         MailMessage mail = new MailMessage();
         SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
         SmtpServer.Timeout = 10000;
         SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
         SmtpServer.UseDefaultCredentials = false;

         mail.From = new MailAddress("daedalus.scripts@gmail.com");

         mail.To.Add(new MailAddress(phoneNumber + "@txt.att.net"));//See carrier destinations below
                                                                    //message.To.Add(new MailAddress("5551234568@txt.att.net"));
         mail.To.Add(new MailAddress(phoneNumber + "@vtext.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@messaging.sprintpcs.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@tmomail.net"));
         mail.To.Add(new MailAddress(phoneNumber + "@vmobl.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@messaging.nextel.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@myboostmobile.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@message.alltel.com"));
         mail.To.Add(new MailAddress(phoneNumber + "@mms.ee.co.uk"));



         mail.Subject = "Subject";
         mail.Body = "";

         SmtpServer.Port = 587;

         SmtpServer.Credentials = new System.Net.NetworkCredential("daedalus.scripts@gmail.com", "pivfos-fescip-9sebdU") as ICredentialsByHost; SmtpServer.EnableSsl = true;
         ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
         {
             return true;
         };

         mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
         SmtpServer.Send(mail);
     }**/
}