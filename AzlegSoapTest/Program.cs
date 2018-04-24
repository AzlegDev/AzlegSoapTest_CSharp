using AzlegSoapTest.LegService;
using System;
using System.IO;
using System.Xml;

namespace AzlegSoapTest
{
    public class Program
    {
        private const string SoapUserName = "UserNameGoesHere";          // Assigned UserName
        private const string SoapPassword = "PasswordGoesHere"; // Assigned Password

        static void Main()
        {
            // This holds the results retrieved from the server
            XmlNode result = null;

            // Instantiate the LegService and generate header with credentials
            var ls = new LegService.LegService
            {
                SOAPLegServiceHeaderValue = new SOAPLegServiceHeader()
                {
                    UserName = SoapUserName,
                    Password = SoapPassword
                }
            };
            
            try
            {
                // Sends the request and writes the response to 
                // For a complete list of possible commands, navigate to https://www.azleg.gov/xml/legservice.asmx
                result = ls.ARS();
            }
            catch (Exception e)
            {
                // Write error to console window
                Console.Write(e.Message);
                // Display error until keypress
                Console.ReadKey();
                Environment.Exit(0);
            }

            // Write the results to the console window
            Console.Write(result.ToString(2));
            
            // Display output until keypress
            Console.ReadKey();
        }
    }

    // This class only exists to format the XML in the console output.  
    // Code from https://stackoverflow.com/a/6442267/2292906
    public static class MyExtensions
    {
        public static string ToString(this XmlNode node, int indentation)
        {
            using (var sw = new StringWriter())
            {
                using (var xw = new XmlTextWriter(sw))
                {
                    xw.Formatting = Formatting.Indented;
                    xw.Indentation = indentation;
                    node.WriteContentTo(xw);
                }
                return sw.ToString();
            }
        }
    }
}