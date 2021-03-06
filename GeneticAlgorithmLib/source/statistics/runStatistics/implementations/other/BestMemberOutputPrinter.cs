using System.Collections.Generic;
using GeneticAlgorithmLib.source.statistics.output;
using GeneticAlgorithmLib.source.statistics.runStatistics.implementations.measure;

namespace GeneticAlgorithmLib.source.statistics.runStatistics.implementations.other
{
    public class BestMemberOutputPrinter : IRunStatistic
    {
        private readonly BestPerformerMeasure _bestMemberMeasure;

        public BestMemberOutputPrinter()
        {
            _bestMemberMeasure = new BestPerformerMeasure();
        }
        public StatisticOutput GetStatistic<T>(List<GenerationRecord<T>> evaluationResults)
        {
            var bestMemberInRun = _bestMemberMeasure.GetBestPerformer(evaluationResults);

            var memberOutput = bestMemberInRun.Member.GetResult();

            var output = string.Join(" - ", memberOutput);
            
            return new StatisticOutput()
                .SetHeading("Best member Output")
                .SetRunValue(output);
        }
    }
}