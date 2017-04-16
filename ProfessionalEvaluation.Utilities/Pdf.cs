using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PdfSharp;
using ProfessionalEvaluation.TO.AssesmentResults;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace ProfessionalEvaluation.Utilities
{
    public class Pdf
    {
        public static void GenerateSimplePdf(AssesmentReportTO report)
        {
            // Create a new PDF document
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PDFsharp";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter tf = new XTextFormatter(gfx);
            tf.Alignment = XParagraphAlignment.Left;

            AddHeader(gfx, report, page);
            AddAbout(gfx, report);

            XFont regularFont = new XFont("Verdana", 14, XFontStyle.Regular);

            // -------------------Info Section
            int heightSpace = 80;
            int leftMargin = 20;
            int subtitleWidth = 150;
            int sectionTextWidth = 180;

            XFont subtitleFont = new XFont("Verdana", 14, XFontStyle.BoldItalic);

            // Evaluation Name
            XFont titleFont = new XFont("Verdana", 15, XFontStyle.BoldItalic);
            gfx.DrawString(report.AssesmentInfo.Evaluation.Name, titleFont, XBrushes.Black,
              new XRect(0, heightSpace, page.Width, 20),
              XStringFormats.Center);

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

                        //Rectangle
                        //gfx.DrawRectangle(brushes[brushIndex],
                      //new XRect(leftMargin, heightSpace, colorWidth, 20));

                        //Name
                        tf.DrawString(item.Name, regularFont, XBrushes.Black,
                      new XRect(leftMargin + colorWidth + 10, heightSpace, roleTitleWidth, paragraphHeight),
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

                //Bar
                gfx.DrawRectangle(XBrushes.Black, new XRect(leftMargin + sectionTextWidth + 20, heightSpace + 10, itemBarSize, 2));

                //Percentage
                tf.DrawString(item.Percentage.ToString() + "%", regularFont, XBrushes.Black,
              new XRect(leftMargin + sectionTextWidth + barSize + 30, heightSpace, 50, 35),
              XStringFormats.TopLeft);

                heightSpace = heightSpace + 30;
            }

            // Save the document...
            const string filename = @"c:\temp\AssesmentReport.pdf";
            document.Save(filename);
        }

        private static void AddHeader(XGraphics gfx, AssesmentReportTO report, PdfPage page)
        {
            //Client Header
            XBrush brush = new XSolidBrush(XColor.FromArgb(0, 188, 188, 188));
            XImage clientImage = XImage.FromFile(@"C:\Temp\" + report.AssesmentInfo.Company.Logo);
            gfx.DrawRectangle(brush, 0, 0, page.Width, 55);
            gfx.DrawImage(clientImage, 2, 2, 210, 50);
        }

        private static void AddAbout(XGraphics gfx, AssesmentReportTO report)
        {
            XPen separatorsPen = new XPen(XColor.FromArgb(0, 238, 238, 238), 1);

            // About font
            XFont aboutFont = new XFont("Verdana", 9, XFontStyle.Regular);

            //Logo and Separators
            int imageX = 150;
            int imageWidth = 46;
            int imageHeight = 64;

            int separatorX = imageX + imageWidth + 15;
            int aboutTextX = separatorX + 20;
            int imageY = 750;
            XImage laboruImage = XImage.FromFile(@"C:\Temp\logo-laboru.png");
            gfx.DrawImage(laboruImage, imageX, imageY, imageWidth, imageHeight);
            gfx.DrawLine(separatorsPen, separatorX, imageY, separatorX, imageY + imageHeight);

            //Text
            gfx.DrawString("Elaborado por Laboru para " + report.AssesmentInfo.Company.Name, aboutFont, XBrushes.Silver,
              new XRect(aboutTextX, imageY + 20, 200, 20),
              XStringFormats.TopLeft);
            gfx.DrawString("Todos los derechos reservados", aboutFont, XBrushes.Silver,
              new XRect(aboutTextX, imageY + 40, 200, 20),
              XStringFormats.TopLeft);
        }
    }
}
