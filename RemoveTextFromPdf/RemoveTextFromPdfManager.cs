using PdfSharp.Pdf.Content;
using PdfSharp.Pdf.Content.Objects;
using PdfSharp.Pdf.IO;

namespace RemoveTextFromPdf
{
    public class RemoveTextFromPdfManager
    {
        public void ReplaceTextInPdf(Options options)
        {
            string originalPdf = options.Path;
            using (var doc = PdfReader.Open(originalPdf, PdfDocumentOpenMode.Modify))
            {
                var replacedText = false;
                var pages = doc.Pages;
                doc.Info.Author = options.Author;
                doc.Info.Title = options.Title;
                doc.Info.CreationDate = System.DateTime.Now;
                
                foreach (var page in pages)
                {
                    var contents = ContentReader.ReadContent(page);
                    
                    var result = ReplaceText(contents, options.SearchText, options.SearchValue);
                    if (result)
                    {
                        replacedText = true;
                        page.Contents.ReplaceContent(contents);
                    }
                }

                if (options.AlwaysSave || replacedText)
                {
                    doc.Save(originalPdf);
                }
            }
        }

        // Please refer to the pdf tech specs on what all entails in the content stream
        // https://www.adobe.com/content/dam/acom/en/devnet/pdf/pdfs/PDF32000_2008.pdf
        public static bool ReplaceText(CSequence contents, string searchText, string replaceText)
        {
            var replacedText = false;
            // Iterate thru each content items. Each item may or may not contain the entire
            // word if there are different stylings (ex: bold parts of the word) applied to a word.
            // So you may have to replace a character at a time.
            for (int i = 0; i < contents.Count; i++)
            {
                if (contents[i] is COperator)
                {
                    var cOp = contents[i] as COperator;
                    for (int j = 0; j < cOp.Operands.Count; j++)
                    {
                        if (cOp.OpCode.Name == OpCodeName.Tj.ToString() ||
                            cOp.OpCode.Name == OpCodeName.TJ.ToString())
                        {
                            if (cOp.Operands[j] is CString)
                            {
                                var cString = cOp.Operands[j] as CString;
                                if (cString.Value.Contains(searchText))
                                {
                                    cString.Value = cString.Value.Replace(searchText, replaceText);
                                    replacedText = true;
                                }
                            }
                        }
                    }
                }

            }

            return replacedText;
        }
    }
}
