using CommandLine;

namespace RemoveTextFromPdf
{
    public class Options
    {
        [Option('p', "path", Required = true, HelpText = "Sets the path for the PDF file to edit")]
        public string Path { get; set; }
        [Option('s', "searchtext", Required = true, HelpText = "Sets text to replace")]
        public string SearchText { get; set; }
        [Option('v', "searchvalue", Required = false, HelpText = "Sets what the text should be replaced with", Default = "")]
        public string SearchValue { get; set; }
        [Option('a', "author", Required = false, HelpText = "Sets who the author of the document is", Default = "Author")]
        public string Author { get; set; }
        
        [Option('t', "title", Required = false, HelpText = "Sets the title of the document", Default = "Title")]
        public string Title { get; set; }
        
        [Option('w', "always-save", Required = false, HelpText = "Always save the document - even when no values has been replaced", Default = true)]
        public bool AlwaysSave { get; set; }
    }
}
