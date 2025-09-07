// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class WordHandler
{
    private string _filePath;

    public WordHandler(string filePath)
    {
        _filePath = filePath;
        SaveDocument();
    }

    private void SaveDocument()
    {
        using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(_filePath, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
        {
            // Vytvoření hlavního dokumentu
            MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
            mainPart.Document = new Document();
            Body body = new Body();

            // Přidání textu s formátováním
            Paragraph paragraph = new Paragraph();
            Run run = new Run();
            run.Append(new Text("Ahoj, světe!")); // Text, který chcete přidat
            paragraph.Append(run);
            body.Append(paragraph);

            mainPart.Document.Append(body);
            mainPart.Document.Save();
        }
    }
}
