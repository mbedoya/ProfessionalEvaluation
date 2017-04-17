using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProfessionalEvaluation.TO;
using ProfessionalEvaluation.Utilities;

namespace ProfessionalEvaluationUnitTest.Tests
{
    [TestClass]
    public class MailTest
    {
        [TestMethod]
        public void Send_MailSent_NoExceptionThrown()
        {            
            Mail mail = new Mail(GetMailMessage());
            mail.Send();
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
}
