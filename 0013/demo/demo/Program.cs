using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Transforms.TimeSeries;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace demo
{
    class Program
    {
        static readonly string _data1Path = Path.Combine(Environment.CurrentDirectory, "data1.csv");
        static readonly string _data2Path = Path.Combine(Environment.CurrentDirectory, "data2.csv");

        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();
            IDataView data1View = mlContext.Data.LoadFromTextFile<ModelInput>(_data1Path, separatorChar: ',', hasHeader: false);
            IDataView data2View = mlContext.Data.LoadFromTextFile<ModelInput>(_data2Path, separatorChar: ',', hasHeader: false);

            var forecastingPipeline = mlContext.Forecasting.ForecastBySsa(
                outputColumnName: "forecasted_count",
                inputColumnName: "count",
                windowSize: 7,
                seriesLength: 334,
                trainSize: 334,
                horizon: 7,
                confidenceLevel: 0.95f,
                confidenceLowerBoundColumn: "lower_count",
                confidenceUpperBoundColumn: "upper_count");

            SsaForecastingTransformer forecaster = forecastingPipeline.Fit(data1View);
            Evaluate(data2View, forecaster, mlContext);

            var forecastEngine = forecaster.CreateTimeSeriesEngine<ModelInput, ModelOutput>(mlContext);
            //forecastEngine.CheckPoint(mlContext, modelPath);

            ModelOutput forecast = forecastEngine.Predict();

            IEnumerable<string> forecastOutput =
                mlContext.Data.CreateEnumerable<ModelInput>(data2View, reuseRowObject: false)
                    .Take(7)
                    .Select((ModelInput data, int index) =>
                    {
                        string action_date = data.action_time.ToString("yyyy-MM-dd");
                        float actual_count = data.count;
                        float lowerEstimate = Math.Max(0, forecast.lower_count[index]);
                        float estimate = forecast.forecasted_count[index];
                        float upperEstimate = forecast.upper_count[index];
                        return $"Date: {action_date}\n" +
                        $"Actual Count: {actual_count}\n" +
                        $"Lower Estimate: {lowerEstimate}\n" +
                        $"Forecast: {estimate}\n" +
                        $"Upper Estimate: {upperEstimate}\n";
                    });

            Console.WriteLine("Forecast");
            Console.WriteLine("---------------------");
            foreach (var prediction in forecastOutput)
            {
                Console.WriteLine(prediction);
            }


            Console.ReadKey();
        }

        static void Evaluate(IDataView testData, ITransformer model, MLContext mlContext)
        {
            IDataView predictions = model.Transform(testData);

            IEnumerable<float> actual =
                mlContext.Data.CreateEnumerable<ModelInput>(testData, true)
                    .Select(p => p.count);

            IEnumerable<float> forecast =
                mlContext.Data.CreateEnumerable<ModelOutput>(predictions, true)
                    .Select(p => p.forecasted_count[0]);

            var metrics = actual.Zip(forecast, (actualValue, forecastValue) => actualValue - forecastValue);

            var MAE = metrics.Average(error => Math.Abs(error));
            var RMSE = Math.Sqrt(metrics.Average(error => Math.Pow(error, 2)));

            Console.WriteLine("Evaluation Metrics");
            Console.WriteLine("---------------------");
            Console.WriteLine($"Mean Absolute Error: {MAE:F3}");
            Console.WriteLine($"Root Mean Squared Error: {RMSE:F3}\n");
        }
    }
}
