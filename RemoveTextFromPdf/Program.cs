using CommandLine;
using CommandLine.Text;
using System;

namespace RemoveTextFromPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var parser = Parser.Default.ParseArguments<Options>(args);
                parser.WithParsed<Options>(o =>
                {
                    var manager = new RemoveTextFromPdfManager();
                    manager.ReplaceTextInPdf(o);
                });

                parser.WithNotParsed<Options>(o =>
                {
                    var helpText = HelpText.AutoBuild(parser, helptext =>
                    {
                        helptext.AddOptions(parser);
                        return helptext;
                    }, example =>
                    {
                        return example;
                    });

                    Console.WriteLine(helpText);
                });
            } catch (Exception e)
            {
                Console.WriteLine($"ERROR: {e.Message}");
            }
        }
    }
}
