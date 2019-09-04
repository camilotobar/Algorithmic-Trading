using System;
using System.Linq;
using cAlgo.API;
using cAlgo.API.Indicators;
using cAlgo.API.Internals;
using cAlgo.Indicators;

namespace cAlgo.Robots
{
    [Robot(TimeZone = TimeZones.UTC, AccessRights = AccessRights.None)]
    public class cTraderElements : Robot
    {
        [Parameter(DefaultValue = 0.0)]
        public double Parameter { get; set; }

        // EVENTS ===================================================
        protected override void OnStart()
        {
            // Logica ejecutada al inicio del Bot
            Elements();
        }

        protected override void OnTick()
        {
            // Logica ejecutada cada nuevo tick
        }

        protected override void OnBar()
        {
            // Logica ejecutada cada nueva barra
        }

        protected override void OnStop()
        {
            // Logica ejecutada el finalizar el Bot
        }


        // MOST RELEVANT ELEMENTS/CLASSES ==========================
        public void Elements()
        {
            //Symbol
            var symbol = Symbol;
            Print("Prices of {0}: Ask {1}, Bid {2}", symbol.Name, symbol.Ask, symbol.Bid);
            Print("Volume: Step {0}, Min {1}, Max {2}", symbol.VolumeInUnitsStep, symbol.VolumeInUnitsMin, symbol.VolumeInUnitsMax);

            //Timeframe
            var currentTimeFrame = TimeFrame;
            var someTimeFrame = TimeFrame.Daily;

            //MarketData
            var mktDepth = MarketData.GetMarketDepth(symbol.Name);
            var mktSeries = MarketData.GetSeries(symbol.Name, currentTimeFrame);
            Print("Last Close: {0}", mktSeries.Close.LastValue);
            Print("Last Open: {0}", mktSeries.Open.LastValue);
            Print("Last High: {0}", mktSeries.High.LastValue);
            Print("Last Low: {0}", mktSeries.Low.LastValue);

            //Technical Indicators
            var rsi = Indicators.RelativeStrengthIndex(MarketSeries.Close, 14);
            var bollinger = Indicators.BollingerBands(MarketSeries.Close, 20, 2, MovingAverageType.Simple);

            //Execution
            //var executeMarketO = ExecuteMarketOrder(TradeType.Buy, symbol.Name, symbol.VolumeInUnitsMin);
            //var executeMarketO = PlaceLimitOrder(TradeType.Buy, symbol.Name, symbol.VolumeInUnitsMin, 1.3500);

            //Position
            var positions = Positions;
            foreach (var position in Positions)
                Print("Position {0} of {1}: Pips: {2}, NetProfit: {3}, Label: {4}", position.Id, position.EntryTime, position.Pips, position.NetProfit, position.Label);
        }
    }
}
