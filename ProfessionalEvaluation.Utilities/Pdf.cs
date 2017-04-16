using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using PdfSharp;
using ProfessionalEvaluation.TO.AssesmentResults;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

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

            AddHeader(gfx, report, page);
            AddAbout(gfx, report);

            XFont regularFont = new XFont("Verdana", 14, XFontStyle.Regular);

            // -------------------Info Section
            int heightSpace = 80;
            int leftMargin = 20;
            int subtitleWidth = 150;
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
            gfx.DrawString("RESULTADOS", subtitleFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, subtitleWidth, 20),
              XStringFormats.TopLeft);

            heightSpace = heightSpace + 30;
            int sectionTextWidth = 250;
            foreach (var item in report.Sections)
            {
                //Name
                gfx.DrawString(item.Name, regularFont, XBrushes.Black,
              new XRect(leftMargin, heightSpace, sectionTextWidth, 20),
              XStringFormats.TopLeft);

                //Percentage
                gfx.DrawString(item.Percentage.ToString() + "%", regularFont, XBrushes.Black,
              new XRect(leftMargin + sectionTextWidth + 50, heightSpace, 50, 20),
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
