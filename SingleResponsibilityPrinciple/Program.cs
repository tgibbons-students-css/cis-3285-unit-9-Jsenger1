using System;
using System.Reflection;

using SingleResponsibilityPrinciple.AdoNet;
using SingleResponsibilityPrinciple.Contracts;

namespace SingleResponsibilityPrinciple
{
    class Program
    {
        public static ITradeDataProvider UrltradeDataProvider { get; private set; }

        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("SingleResponsibilityPrinciple.trades.txt");

            var logger = new ConsoleLogger();
            var tradeValidator = new SimpleTradeValidator(logger);
            var tradeDataProvider = new StreamTradeDataProvider(tradeStream);
            var URlTradeDataProvider = new URLTradeDataProvider("http://faculty.css.edu/tgibbons/trades4.txt");
            var tradeMapper = new SimpleTradeMapper();
            var tradeParser = new SimpleTradeParser(tradeValidator, tradeMapper);
            var tradeStorage = new AdoNetTradeStorage(logger);

            var tradeProcessor = new TradeProcessor(tradeDataProvider, tradeParser, tradeStorage);
            tradeProcessor.ProcessTrades();
            var urltradeProcessor = new TradeProcessor(UrltradeDataProvider, tradeParser, tradeStorage);
            tradeProcessor.ProcessTrades();

            Console.ReadKey();
        }
    }
}
