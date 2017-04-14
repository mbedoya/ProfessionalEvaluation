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

            // Create a font
            XFont font = new XFont("Verdana", 15, XFontStyle.BoldItalic);

            XBrush brush = new XSolidBrush(XColor.FromArgb(0, 238, 238, 238));
            XImage image = XImage.FromFile(@"C:\Temp\logo-perceptio.png");

            gfx.DrawRectangle(brush, 0, 0, page.Width, 55);
            gfx.DrawImage(image, 2, 2, 210, 50);

            int heightSpace = 80;
            foreach (var item in report.Sections)
            {
                gfx.DrawString(item.Name + " " + item.Percentage.ToString() + "%", font, XBrushes.Black,
              new XRect(20, heightSpace, page.Width - 20, 20),
              XStringFormats.TopLeft);

                heightSpace = heightSpace + 30;
            }

            // Save the document...
            const string filename = @"c:\temp\AssesmentReport.pdf";
            document.Save(filename);

        }
    }
}
