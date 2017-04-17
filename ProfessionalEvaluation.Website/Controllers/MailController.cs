using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfessionalEvaluation.Website.Controllers
{
    public class MailController : Controller
    {
        //
        // GET: /Mail/

        public ActionResult Index()
        {
            MesssageTO message = new MesssageTO();
            message.Text = "Successful";
            try
            {
                Mail mail = new Mail(GetMailMessage());
                mail.Send();
            }
            catch (Exception ex)
            {
                message.Text = ex.Message;
            }
            return View(message);
        }

        private MailMessageTO GetMailMessage()
        {
            MailMessageTO message = new MailMessageTO();
            message.Title = "Resultado Evaluación Mauricio Bedoya";
            message.ToEmail = "mauricio.bedoya@gmail.com";
            message.ToName = "Mauricio Bedoya";
            message.Body = "<p>Buenos días,</p><p>Estamos enviando el resultado de la evaluación 'DESARROLLADOR DE SOFTWARE' de Mauricio Bedoya</p><p>Saludos cordiales,</p>";

            return message;
        }

    }

    public class MesssageTO
    {
        public string Text { get; set; }
    }
}
