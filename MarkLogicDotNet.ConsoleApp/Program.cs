using System;
using MarkLogicDotNet.Api.Contracts;
using MarkLogicDotNet.Core;

namespace MarkLogicDotNet.ConsoleApp
{
    class Program
    {
        static MarkLogicClient _client;
        static void Main(string[] args)
        {
            var config = new ConnectionConfig
            {
                User = "admin",
                Password = "Melbourne2020",
                Host = "http://localhost:8000",
            };

            _client = new MarkLogicClient(config);

            GetDocument();
        }

        static void GetDocument()
        {
            var req = new DocumentRequest
            {
                Uri = "sample.json",
                Database = "test"

            };
            var doc = _client.GetDocument(req).GetAwaiter().GetResult();

            if (doc.Success)
            {
                Console.WriteLine(doc.Data);
            }
            else
            {
                Console.WriteLine(doc.ErrorMessage);
            }
        }
    }
}
