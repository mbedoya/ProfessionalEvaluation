using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PdfSharp;
using ProfessionalEvaluation.TO.AssesmentResults;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Configuration;
using System.Web;

namespace ProfessionalEvaluation.Utilities
{
    public class Pdf
    {
        private static string fontName = "Baskerville";
        private static XFont regularFont = new XFont(fontName, 14, XFontStyle.Regular);
        private static XFont subtitleFont = new XFont(fontName, 14, XFontStyle.BoldItalic);
        private static XFont smallFont = new XFont(fontName, 12, XFontStyle.Regular);

        public static string GenerateSimplePdf(AssesmentReportTO report)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Evaluación Laboru Tech";

            const string PdfGenerationPathSetting = "PdfGenerationPath";

            AddMainPage(document, report);
            AddComparisonsPage(document, report);

            string filePath = String.Format("{0}AssesmentReport_{1}.pdf", ConfigurationManager.AppSettings[PdfGenerationPathSetting], report.AssesmentInfo.ID);
            document.Save(filePath);
            document.Close();
            document.Dispose();

            return filePath;
        }

        private static void AddComparisonsPage(PdfDocument document, AssesmentReportTO report)
        {
            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;

            AddHeader(gfx, report, page);
            AddAbout(gfx, report);

            // -------------------Info Section
            int heightSpace = 70;
            int leftMargin = 20;
            int subtitleWidth = 150;
            int sectionTextWidth = 180;
            int numberWidth = 30;

            if (report.Analysis.Candidates != null && report.Analysis.Candidates.Count > 0)
            {
                // Sections Results
                heightSpace = heightSpace + 50;
                gfx.DrawString("Ranking de candidatos", subtitleFont, XBrushes.Black,
                  new XRect(leftMargin, heightSpace, subtitleWidth, 20),
                  XStringFormats.TopLeft);

                int index = 1;
                int candidatePadding = 20;
                foreach (var item in report.Analysis.Candidates)
                {
                    heightSpace = heightSpace + 30;
                    XBrush brush = XBrushes.Black;

                    if (report.AssesmentInfo.ID == item.AssesmentID)
                    {
                        brush = XBrushes.White;
                        gfx.DrawRectangle(XBrushes.Blue, new XRect(leftMargin, heightSpace-2, page.Width - leftMargin*2, 20 + 2));
                    }

                    //Position
                    tf.DrawString(index.ToString(), regularFont, brush,
                  new XRect(leftMargin + candidatePadding, heightSpace, numberWidth, 20),
                  XStringFormats.TopLeft);

                    //Name
                    tf.DrawString(item.Name, regularFont, brush,
                  new XRect(leftMargin + candidatePadding + numberWidth + 20, heightSpace, sectionTextWidth, 20),
                  XStringFormats.TopLeft);

                    //Points
                    tf.DrawString(item.Points.ToString() + " puntos", regularFont, brush,
                  new XRect(leftMargin + candidatePadding + numberWidth + sectionTextWidth + 20, heightSpace, numberWidth, 20),
                  XStringFormats.TopLeft);

                    index++;
                }

                // Sections Results
                heightSpace = heightSpace + 30;
                gfx.DrawString("* El máximo de puntos posibles es " + report.Analysis.RoleResult.PossiblePoints, smallFont, XBrushes.Black,
                  new XRect(leftMargin, heightSpace, subtitleWidth, 20),
                  XStringFormats.TopLeft);
            }            
        }

        private static void AddMainPage(PdfDocument document, AssesmentReportTO report)
        {
            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;

            AddHeader(gfx, report, page);
            AddAbout(gfx, report);

            // -------------------Info Section
            int heightSpace = 70;
            int leftMargin = 20;
            int subtitleWidth = 150;
            int sectionTextWidth = 180;

            // Person Name
            heightSpace = heightSpace + 50;
            gfx.DrawString("Persona evaluada:", subtitleFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, subtitleWidth, 20),
              XStringFormats.TopLeft);
            gfx.DrawString(report.AssesmentInfo.PersonName, regularFont, XBrushes.Black,
              new XRect(leftMargin + subtitleWidth, heightSpace, 300, 20),
              XStringFormats.TopLeft);

            // Person Name
            heightSpace = heightSpace + 30;
            gfx.DrawString("Fecha finalización:", subtitleFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, subtitleWidth, 20),
              XStringFormats.TopLeft);
            gfx.DrawString(report.AssesmentInfo.DateFinished.Value.ToString("MMMM dd yyyy"), regularFont, XBrushes.Black,
              new XRect(leftMargin + subtitleWidth, heightSpace, 300, 20),
              XStringFormats.TopLeft);

            // Sections Results
            heightSpace = heightSpace + 50;
            gfx.DrawString("Detalle de la evaluación", subtitleFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, subtitleWidth, 20),
              XStringFormats.TopLeft);

            int roleTitleWidth = 90;
            int colorWidth = 20;
            if (report.Analysis != null)
            {
                if (report.Analysis.RoleResult != null)
                {
                    // Title Obtained
                    heightSpace = heightSpace + 30;
                    gfx.DrawString("Resultado:", subtitleFont, XBrushes.Black,
                      new XRect(leftMargin, heightSpace, roleTitleWidth, 20),
                      XStringFormats.TopLeft);
                    gfx.DrawString(report.Analysis.RoleResult.Title, regularFont, XBrushes.Black,
                      new XRect(leftMargin + roleTitleWidth + colorWidth + 10, heightSpace, 300, 20),
                      XStringFormats.TopLeft);

                    // Points Obtained
                    heightSpace = heightSpace + 30;
                    gfx.DrawString("Puntos:", subtitleFont, XBrushes.Black,
                      new XRect(leftMargin, heightSpace, roleTitleWidth, 20),
                      XStringFormats.TopLeft);
                    gfx.DrawString(report.Analysis.RoleResult.Points.ToString() + " / " + report.Analysis.RoleResult.PossiblePoints.ToString(), regularFont, XBrushes.Black,
                      new XRect(leftMargin + roleTitleWidth + colorWidth + 10, heightSpace, 300, 20),
                      XStringFormats.TopLeft);
                }

                List<XSolidBrush> brushes = new List<XSolidBrush>();
                brushes.Add(XBrushes.Yellow);
                brushes.Add(XBrushes.YellowGreen);
                brushes.Add(XBrushes.Orange);
                brushes.Add(XBrushes.LightGreen);
                brushes.Add(XBrushes.Green);

                if (report.Analysis.RoleLevels != null)
                {
                    int brushIndex = brushes.Count - report.Analysis.RoleLevels.Count;
                    heightSpace = heightSpace + 30;
                    foreach (var item in report.Analysis.RoleLevels)
                    {
                        int paragraphHeight = 20;
                        if (item.Description.Length > 50)
                        {
                            paragraphHeight = item.Description.Length / 50 * 30;
                            if (paragraphHeight < 40)
                            {
                                paragraphHeight = 40;
                            }
                        }

                        //Name
                        tf.DrawString(item.Name, regularFont, XBrushes.Black,
                      new XRect(leftMargin + colorWidth, heightSpace, roleTitleWidth + 10, paragraphHeight),
                      XStringFormats.TopLeft);

                        //Description
                        tf.DrawString(item.Description, regularFont, XBrushes.Black,
                      new XRect(leftMargin + roleTitleWidth + colorWidth + 10, heightSpace, 400, paragraphHeight),
                      XStringFormats.TopLeft);

                        heightSpace = heightSpace + paragraphHeight + 10;
                        brushIndex++;
                    }
                }
            }

            // Sections Results
            heightSpace = heightSpace + 20;
            gfx.DrawString("Detalle de las capacidades", subtitleFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, subtitleWidth, 20),
              XStringFormats.TopLeft);

            heightSpace = heightSpace + 30;
            int barSize = 250;
            foreach (var item in report.Sections)
            {
                int itemBarSize = Convert.ToInt32(barSize * item.Percentage / 100);

                //Name
                tf.DrawString(item.Name, regularFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, sectionTextWidth, 35),
              XStringFormats.TopLeft);

                XBrush resultBrush = GetBrushByPercentage(item.Percentage);

                //Bar
                gfx.DrawRectangle(resultBrush, new XRect(leftMargin + sectionTextWidth + 20, heightSpace + 10, itemBarSize, 2));

                //Percentage
                tf.DrawString(item.Percentage.ToString() + "%", regularFont, resultBrush,
              new XRect(leftMargin + sectionTextWidth + barSize + 30, heightSpace, 50, 35),
              XStringFormats.TopLeft);

                heightSpace = heightSpace + 30;
            }
        }

        private static XBrush GetBrushByPercentage(double p)
        {
            XBrush brush = XBrushes.Red;
            if (p >= 25 && p < 50)
            {
                brush = XBrushes.OrangeRed;
            }
            else
            {
                if (p >= 50 && p < 74)
                {
                    brush = XBrushes.Yellow;
                }
                else
                {
                    if (p >= 75 & p < 95)
                    {
                        brush = XBrushes.YellowGreen;
                    }
                    else
                    {
                        if (p >= 95)
                        {
                            brush = XBrushes.Green;
                        }                        
                    }
                }
            }

            return brush;
        }

        private static void AddHeader(XGraphics gfx, AssesmentReportTO report, PdfPage page)
        {
            //Client Header
            XBrush brush = new XSolidBrush(XColor.FromArgb(0, 188, 188, 188));
            XImage clientImage = XImage.FromFile(HttpContext.Current.Server.MapPath("~/img/" + report.AssesmentInfo.Company.Logo));
            gfx.DrawRectangle(brush, 0, 50, page.Width, 1);
            gfx.DrawImage(clientImage, 2, 2, 180, 45);

            // Evaluation Name
            XFont titleFont = new XFont(fontName, 15, XFontStyle.BoldItalic);
            gfx.DrawString(report.AssesmentInfo.Evaluation.Name.ToUpper(), titleFont, XBrushes.Black,
              new XRect(0, 70, page.Width, 20),
              XStringFormats.Center);
        }

        private static void AddAbout(XGraphics gfx, AssesmentReportTO report)
        {
            XPen separatorsPen = new XPen(XColor.FromArgb(0, 238, 238, 238), 1);

            // About font
            XFont aboutFont = new XFont(fontName, 7, XFontStyle.Regular);
            XBrush aboutBrushText = XBrushes.Silver;

            //Logo and Separators
            int imageX = 200;
            int imageWidth = 41;
            int imageHeight = 58;

            int separatorX = imageX + imageWidth + 15;
            int aboutTextX = separatorX + 20;
            int imageY = 770;

            XImage laboruImage = XImage.FromFile(HttpContext.Current.Server.MapPath("~/img/logo-laboru.png"));
            gfx.DrawImage(laboruImage, imageX, imageY, imageWidth, imageHeight);
            gfx.DrawLine(separatorsPen, separatorX, imageY, separatorX, imageY + imageHeight);

            //Text
            gfx.DrawString("Elaborado por Laboru para " + report.AssesmentInfo.Company.Name, aboutFont, aboutBrushText,
              new XRect(aboutTextX, imageY + 10, 200, 20),
              XStringFormats.TopLeft);
            gfx.DrawString("Todos los derechos reservados", aboutFont, aboutBrushText,
              new XRect(aboutTextX, imageY + 25, 200, 20),
              XStringFormats.TopLeft);
            gfx.DrawString("http://laboru.co/tech", aboutFont, aboutBrushText,
              new XRect(aboutTextX, imageY + 40, 200, 20),
              XStringFormats.TopLeft);
        }
    }
}
