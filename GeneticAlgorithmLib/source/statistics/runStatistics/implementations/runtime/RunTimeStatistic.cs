﻿using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithmLib.source.statistics.calculatedResults;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations
{
    public class RunTimeStatistic : RunStatistic
    {
        private IStatisticMeasure _measure;
        private IRuntimeMeasurement _runtimeMeasurement;

        public RunTimeStatistic(IStatisticMeasure measure) : base($"{measure.GetHeading()} Execution time")
        {
            _measure = measure;
            _runtimeMeasurement = new SecondsMeasurement();
        }
        public override string GetStatistic<T>(List<GenerationRecord<T>> generationResults)
        {
            AdjustTimeScale(generationResults);
            var generationRunTimes = GetCalculationResultSet(generationResults);
            
            var averageRunTime = _measure.GetRunStatistic(generationRunTimes);
            
            return GetOutput(generationRunTimes, averageRunTime);
        }

        private void AdjustTimeScale<T>(List<GenerationRecord<T>> generationResults)
        {
            var sample = generationResults.ElementAt(0);
            var timeTaken = sample.RunTime.Milliseconds;

            _runtimeMeasurement = timeTaken switch
            {
                > 600000 => new HoursMeasurement(),
                > 60000 => new MinutesMeasurement(),
                > 2000 => new SecondsMeasurement(),
                _ => new MilliSecondMeasurement()
            };

            Scale = _runtimeMeasurement.GetScale();
        }
        

        private CalculationResultSet GetCalculationResultSet<T>(List<GenerationRecord<T>> generationResults)
        {
            var generationRunTimes = new List<CalculationResult>();

            for (int i = 0; i < generationResults.Count(); i++)
            {
                var generationResult = generationResults.ElementAt(i);

                var runtime = _runtimeMeasurement.GetRunTime(generationResult.RunTime);
                var calculationResult = new CalculationResult(runtime, $"Generation {i}");

                generationRunTimes.Add(calculationResult);
            }

            return new CalculationResultSet(generationRunTimes);
        }
        
    }
}