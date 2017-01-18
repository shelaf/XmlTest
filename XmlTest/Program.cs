using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlTest
{
    class Program
    {
        /// <summary>
        /// メイン
        /// </summary>
        /// <param name="args">実行時引数</param>
        static void Main(string[] args)
        {
            using (FileStream fs = new FileStream(@"Library.xml", FileMode.Open))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Library));
                try
                {
                    Library library = (Library)serializer.Deserialize(fs);

                    ValidationContext context = new ValidationContext(library, null, null);
                    List<ValidationResult> results = new List<ValidationResult>();

                    Validator.TryValidateObject(library, context, results, true);
                    foreach (ValidationResult result in results)
                    {
                        Console.WriteLine(result.ErrorMessage);
                    }

                    foreach (Book book in library.Books)
                    {
                        Console.WriteLine(String.Format("ISBN={0}, タイトル={1}, 価格=\\{2}, 分類コード={3}", book.ISBN, book.Title, book.Price, book.CCode.ToString()));
                    }

                    serializer.Serialize(new StreamWriter(Console.OpenStandardOutput(), Encoding.GetEncoding(932)), library);
                }
                catch (InvalidOperationException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                }

            }
        }
    }
}
