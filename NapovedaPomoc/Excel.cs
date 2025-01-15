using System.Diagnostics;
using ClosedXML.Excel;

namespace NapovedaPomoc
{
    public class Excel
    {
        public static void GenerateExcel(List<Obed> seznamObedu, string filePath, string[]? jmena = null)
        {

            using (var workbook = new XLWorkbook())
            {
                IXLFont Font = workbook.Style.Font;
                Font.FontName = "Arial";
                Font.FontSize = 12;

                var ws = workbook.Worksheets.Add("Seznam obědů");
                int row = 1; int col = 1;
                ws.Cell(row, col).Value = "G3K spol. s r.o. Rozvoz-jidel.eu";
                ws.Cell(row, col).Style.Font.Bold = true;
                ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Cell(row, col).Style.Font.FontSize = 30;
                //ws.Cell(row, 1).Style.Border.BottomBorder = XLBorderStyleValues.Thick; //dole, tenka
                //row++; 
                // Hlavička
                //ws.Cell(row, col++).Value = "Menu";
                //ws.Cell(row, col++).Value = "Název";
                col += 2;
                //if (jmena == null)
                //jmena = ["Titze", "Csato", "Ivanco", "Litošová", "Nídrová"];

                foreach (var item in jmena)
                {
                    ws.Cell(row, col).Value = item;
                    ws.Cell(row, col).Style.Font = Font;
                    ws.Cell(row, col).Style.Font.Bold = true;
                    ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Cell(row, col++).Style.Alignment.TextRotation = 90;
                }
                int cara = col - 1;
                ws.Range(row, 1, row, cara).Style.Border.BottomBorder = XLBorderStyleValues.Thin; //dole, tenka
                ws.Range(1, 1, row, cara).Style.Border.TopBorder = XLBorderStyleValues.Thin; //dole, tenka

                col = 1; row++;
                // Data
                //for (int i = row; i < seznamObedu.Count; i++)
                foreach (var item in seznamObedu)
                {
                    ws.Cell(row, col).Style.Font = Font;
                    ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Cell(row, col++).Value = item.Menu;

                    ws.Cell(row, col).Style.Font = Font;
                    ws.Cell(row, col).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                    ws.Cell(row, col).Style.Alignment.SetWrapText(true);
                    ws.Cell(row, col++).Value = item.Jidlo;
                    //ws.Cell(i, col).Style.Font.FontSize = 12;
                    //ws.Cell(i, col).Style.Font.FontName = "Arial";
                    //ws.Row(1).AdjustToContents(); // Automaticky přizpůsobí výšku řádku obsahu

                    var style = ws.Range(row, 1, row, cara).Style;
                    if (item.Menu == "Nápoj")
                        style.Border.BottomBorder = XLBorderStyleValues.Thin; //dole, tenka

                    //střídaní barev
                    if (row % 2 == 0)
                        style.Fill.BackgroundColor = XLColor.LightGray; //barva
                    else
                        style.Fill.BackgroundColor = XLColor.White; //barva
                    col = 1;
                    row++;
                }

                //ws.Column(1).AdjustToContents(); // Automaticky přizpůsobí šířku sloupce obsahu
                ws.Column(2).AdjustToContents(); // Automaticky přizpůsobí šířku sloupce obsahu


                ws.Range(1, 1, row, 1).Style.Border.LeftBorder = XLBorderStyleValues.Thin; //dole, tenka
                for (int i = 2; i < jmena.Length + 3; i++)
                {
                    ws.Range(1, i, row, i).Style.Border.RightBorder = XLBorderStyleValues.Thin; //dole, tenka
                    ws.Column(i).AdjustToContents(); // Automaticky přizpůsobí šířku sloupce obsahu
                }

                //zobrazit konce stránek
                ws.SheetView.SetView(XLSheetViewOptions.PageBreakPreview);

                // Nastavení orientace stránkyk
                ws.PageSetup.PageOrientation = XLPageOrientation.Portrait;

                // Nastavení okrajů stránky
                ws.PageSetup.Margins.Top = 1;        // Okraj nahoře (v palcích)
                ws.PageSetup.Margins.Bottom = 1;     // Okraj dole
                ws.PageSetup.Margins.Left = 0.5;    // Levý okraj
                ws.PageSetup.Margins.Right = 0.5;   // Pravý okraj

                ws.PageSetup.FitToPages(1, 1);

                // Nastavení velikosti papíru (A4)
                ws.PageSetup.PaperSize = XLPaperSize.A4Paper;
                // Nastavení tiskového rozlišení (DPI)
                ws.PageSetup.VerticalDpi = 600;
                ws.PageSetup.HorizontalDpi = 600;

                //zalomení radku druhého sloupce

                // Uložit Excel soubor
                workbook.SaveAs(filePath);
                if (File.Exists(filePath))
                    //UseShellExecute = true } je doporučený způsob, jak spustit soubor v aplikaci definované systémem
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
            }
        }
    }

    public class Obed
    {
        public string Menu { get; set; } = string.Empty;
        public string Jidlo { get; set; } = string.Empty;
        //public List<string> Pole { get; set; }
    }
}
