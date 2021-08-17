﻿using System;
using GeneticAlgorithmLib.source.mockImplementations;
using GeneticAlgorithmLib.source.statistics;
using GeneticAlgorithmLib.source.statistics.history;
using Xunit;

namespace GeneticAlgorithmLib.test.statisticsTests.historyTests
{
    public class RunHistoryTests
    {

        [Fact]
        public static void GetGenerationReturnsCorrectRun()
        {
            var history = GetHistory();

            var gen1 = history.GetGeneration(0);
            var gen10 = history.GetGeneration(9);
            
            Assert.Equal(45,gen1.GetTotalFitness());
            Assert.Equal(945,gen10.GetTotalFitness());
       
        }
        
        [Fact]
        public static void GetGenerationThrowsOnInvalidAccess()
        {
            var history = GetHistory();

            Assert.Throws<IndexOutOfRangeException>(() => history.GetGeneration(-1));
            Assert.Throws<IndexOutOfRangeException>(() => history.GetGeneration(10));
        }

        private static RunRecord<double> GetHistory()
        {
            var history = new RunRecord<double>(0);

            for (int i = 0; i < 10; i++)
            {
                history.AddGeneration( GetEval(i));
            }

            return history;
        }

        private static GenerationRecord<double> GetEval(int generation)
        {
            var eval = new GenerationRecord<double>();

            for (int i = 0; i < 10; i++)
            {
                var number = (generation * 10) + i;
                var member = new RandomNumberMember(number);
                eval.Add(member,number);
            }

            return eval;
        }
    }
}