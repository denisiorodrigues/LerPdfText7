// See https://aka.ms/new-console-template for more information
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Filter;
using iText.Kernel.Pdf.Canvas.Parser.Listener;

static string ReadPdfTextInArea(PdfDocument pdfDocument, float startX, float startY, float endX, float endY)
{
    TextRegionEventFilter regionFilter = new TextRegionEventFilter(new iText.Kernel.Geom.Rectangle(startX, startY, endX, endY));
    FilteredTextEventListener textListener = new FilteredTextEventListener(new LocationTextExtractionStrategy(), regionFilter);

    for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
    {
        PdfPage page = pdfDocument.GetPage(pageNumber);
        PdfCanvasProcessor parser = new PdfCanvasProcessor(textListener);
        parser.ProcessPageContent(page);
    }

    return textListener.GetResultantText();
}

static string ReadPdfText(PdfDocument pdfDocument)
{
    SimpleTextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
    for (int pageNumber = 1; pageNumber <= pdfDocument.GetNumberOfPages(); pageNumber++)
    {
        Console.WriteLine($"----------------- Page {pageNumber} ---------------------");
        PdfPage page = pdfDocument.GetPage(pageNumber);
        PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
        parser.ProcessPageContent(page);
    }

    return strategy.GetResultantText();
}

Console.WriteLine("Iniciando o programa...");
Console.WriteLine("--------------------------------------");

string fileName = "extrato.pdf";
string pdfFilePath = $"files/{fileName}";
string outputFilePath = $"files/texto-extraido-{fileName}.txt";

using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfFilePath)))
{
    string text = string.Empty;
    
    text = ReadPdfText(pdfDocument);
    //text = LocationTextExtraction(pdfDocument);
    File.WriteAllText(outputFilePath, text);
    Console.WriteLine(text);
}
  // float startX = 100; // coordenada X inicial
  // float startY = 100; // coordenada Y inicial
  // float endX = 300;   // coordenada X final
  // float endY = 200;   // coordenada Y final

  // using (PdfDocument pdfDocument = new PdfDocument(new PdfReader(pdfFilePath)))
  // {
  //     string textInArea = ReadPdfTextInArea(pdfDocument, startX, startY, endX, endY);
  //     Console.WriteLine(textInArea);
  //     File.WriteAllText(outputFilePath, textInArea);
  // }

Console.WriteLine("--------------------------------------");
Console.WriteLine("FIM do programa.");
