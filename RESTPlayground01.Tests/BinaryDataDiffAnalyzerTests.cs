using System;
using System.Text;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RESTPlayground01.Core.Models;
using RESTPlayground01.Core.Services;

namespace RESTPlayground01.Tests
{
    [TestClass]
    public class BinaryDataDiffAnalyzerTests
    {
        private ILog _logger;
        private Random _random;

        [TestInitialize]
        public void Initialize()
        {
            _logger = LogManager.GetLogger(typeof(BinaryDataDiffAnalyzerTests));
            _random = new Random();
        }

        [TestMethod]
        public void Diff_RandomDataAndKnownDifferencesCount_DifferencesCountEqualsToKnown()
        {
            // arrange
            var testCasesCount = 3;
            var testCasesInputs = new GenerateBinaryDataResult[testCasesCount];
            var testCasesResults = new BinaryDataDiffAnalyzerResult[testCasesCount];
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            // action
            for (var i = 0; i < testCasesCount; i++)
            {
                var dataSize = _random.Next(10, 10000);
                var leftSideData = new byte[dataSize];
                testCasesInputs[i] = GenerateBinaryData(dataSize);
                _logger.Debug(String.Format("Tast case #{0}", i));
                _logger.Debug(String.Format("Generated {0}", ByteArrayAsString(testCasesInputs[i].Data)));
                _logger.Debug(String.Format("NonZeroGroupsCount: {0}", testCasesInputs[i].NonZeroGroupsCount));
                testCasesResults[i] = binaryDataDiffAnalyzer.Diff(leftSideData, testCasesInputs[i].Data);
            }

            // assert
            for (var i = 0; i < testCasesResults.Length; i++)
                Assert.AreEqual(testCasesInputs[i].NonZeroGroupsCount, testCasesResults[i].Diffs.Length);
        }

        [TestMethod]
        public void Diff_SameDataAndTwoDifferences_ExpectedDiffOffsetAndLength()
        {
            // arrange
            var leftSideData = new byte[] { 0, 0, 0, 0 };
            var rightSideData = new byte[] { 1, 0, 1, 1 };
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            // action
            var result = binaryDataDiffAnalyzer.Diff(leftSideData, rightSideData);

            // assert
            Assert.AreEqual(0, result.Diffs[0].Offset);
            Assert.AreEqual(1, result.Diffs[0].Length);
            Assert.AreEqual(2, result.Diffs[1].Offset);
            Assert.AreEqual(2, result.Diffs[1].Length);
        }

        [TestMethod]
        public void Diff_SameSizeAndTotallyDifferentContent_FoundOneDifference()
        {
            // arrange
            var leftSideData = new byte[] { 0, 0, 0, 0 };
            var rightSideData = new byte[] { 1, 1, 1, 1 };
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            // action
            var result = binaryDataDiffAnalyzer.Diff(leftSideData, rightSideData);

            // assert
            Assert.AreEqual(1, result.Diffs.Length);
        }

        private class GenerateBinaryDataResult
        {
            public readonly byte[] Data;
            public readonly int NonZeroGroupsCount;

            public GenerateBinaryDataResult(byte[] data, int nonZeroGroupsCount)
            {
                Data = data;
                NonZeroGroupsCount = nonZeroGroupsCount;
            }
        }

        [TestMethod]
        public void Diff_SameSizeAndDifferentContent_ContentDoNotMatch()
        {
            //arrange
            var leftSideData = new byte[] { 0, 0, 0, 0 };
            var rightSideData = new byte[] { 1, 0, 1, 1 };
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            //action
            var result = binaryDataDiffAnalyzer.Diff(leftSideData, rightSideData);

            //assert 
            Assert.AreEqual(DiffResultType.ContentDoNotMatch, result.DiffResultType);
        }

        [TestMethod]
        public void Diff_SameSizeAndContent_Equals()
        {
            //arrange
            var leftSideData = new byte[] { 0, 0, 0, 0 };
            var rightSideData = new byte[] { 0, 0, 0, 0 };
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            //action
            var result = binaryDataDiffAnalyzer.Diff(leftSideData, rightSideData);

            //assert 
            Assert.AreEqual(DiffResultType.Equals, result.DiffResultType);
        }

        [TestMethod]
        public void Diff_DifferentSize_SizeDoNotMatch()
        {
            //arrange
            var leftSideData = new byte[] { 0, 0, 0, 0 };
            var rightSideData = new byte[] { 0, 0, 0, 0, 0 };
            var binaryDataDiffAnalyzer = new BinaryDataDiffAnalyzer();

            //action
            var result = binaryDataDiffAnalyzer.Diff(leftSideData, rightSideData);

            //assert 
            Assert.AreEqual(DiffResultType.SizeDoNotMatch, result.DiffResultType);
        }

        private GenerateBinaryDataResult GenerateBinaryData(int size)
        {
            var data = new byte[size];
            var nonZeroGroupsCount = 0;

            for (var i = 0; i < data.Length; i++)
            {
                data[i] = (byte)_random.Next(0, 5);
                if (i == 0 && data[i] > 0 || i > 0 && data[i - 1] == 0 && data[i] > 0)
                    nonZeroGroupsCount++;
            }

            return new GenerateBinaryDataResult(data, nonZeroGroupsCount);
        }

        private string ByteArrayAsString(byte[] bytes)
        {
            var sb = new StringBuilder("byte[] { ");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("}");
            return sb.ToString();
        }
    }
}
