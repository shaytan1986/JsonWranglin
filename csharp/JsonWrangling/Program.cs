using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace JsonWrangling
{
    public static class JsonReaderExtensions
    {
        public static void WriteIndented(this JsonReader reader, int lineNumber)
        {
            int depth = reader.Depth;
            string indent = string.Concat(Enumerable.Repeat(" ", (depth + 2) - 1));
            string info = $"[{depth}] {reader.TokenType} (line: {lineNumber})";
            Console.WriteLine($"{indent}{info}");
        }

        public static void LogToken(this JsonReader reader, int lineNumber)
        {
            Console.WriteLine($"[{lineNumber}]: {reader.TokenType}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            StartEndDepth();
            Console.ReadLine();
        }

        public enum ObservationState
        {
            Sleeping,
            Searching,
            Found
        }
        private static void StartEndDepth()
        {
            Dictionary<int, StringBuilder> objects = new Dictionary<int, StringBuilder>();
            ObservationState observationState = ObservationState.Sleeping;

            int i = 0;
            int objectNumber = 0;
            int baselineDepth = int.MinValue;
            int objectStart = int.MinValue;

            using JsonReader reader = GetJsonResponseReader();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Path == "Result")
                {
                    Console.WriteLine("[{i}]: Found Result Node...");
                    observationState = ObservationState.Searching;
                }

                if (observationState == ObservationState.Searching && reader.TokenType == JsonToken.StartObject)
                {
                    baselineDepth = reader.Depth;
                    Console.WriteLine($"[{i}]: Baseline Depth = {baselineDepth}{Environment.NewLine}");
                    observationState = ObservationState.Found;
                }

                if (observationState == ObservationState.Found && reader.Depth == baselineDepth)
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        objectStart = i;
                    }

                    if (reader.TokenType == JsonToken.EndObject)
                    {
                        Console.WriteLine($"obj[{objectNumber}], Lines: [{objectStart}:{i}]");
                        objectStart = int.MinValue;
                        objectNumber++;
                    }

                }

                i++;
            }
        }

        private static JsonReader GetJsonFileReader(string path)
        {
            return new JsonT
        }
        private static JsonReader GetJsonResponseReader(int batchSize = 1)
        {
            WebRequest req = WebRequest.Create($"http://localhost:3000/?{batchSize}");
            WebResponse res = req.GetResponse();

            using (Stream dataStream = res.GetResponseStream())
            {
                using (StreamReader streamReader = new StreamReader(dataStream))
                {
                    return new JsonTextReader(streamReader);
                }
            }
        }
    }
}
