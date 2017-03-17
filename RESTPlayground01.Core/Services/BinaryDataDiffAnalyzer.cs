using System.Collections.Generic;
using RESTPlayground01.Core.Models;

namespace RESTPlayground01.Core.Services
{
    public class BinaryDataDiffAnalyzer : IBinaryDataDiffAnalyzer
    {
        public BinaryDataDiffAnalyzerResult Diff(byte[] leftSideData, byte[] rightSideData)
        {
            if (leftSideData.Length != rightSideData.Length)
            {
                return new BinaryDataDiffAnalyzerResult(null, DiffResultType.SizeDoNotMatch);
            }

            var diffList = new List<BinaryDataDiff>();
            var lastOffset = 0;
            var length = 0;
            for (var i = 0; i < leftSideData.Length; i++)
            {
                // if new difference has been found
                if(leftSideData[i] != rightSideData[i])
                {
                    length++;

                    if (lastOffset == 0)
                        lastOffset = i;

                    // if found the tail of difference
                    if (i == leftSideData.Length - 1)
                    {
                        var diff = new BinaryDataDiff(lastOffset, length);
                        diffList.Add(diff);
                    }
                }
                // found the tail of difference
                else if (leftSideData[i] == rightSideData[i] && length > 0)
                {
                    var diff = new BinaryDataDiff(lastOffset, length);
                    diffList.Add(diff);

                    // reset length and offset
                    length = 0;
                    lastOffset = 0;
                }
            }

            return diffList.Count > 0
                ? new BinaryDataDiffAnalyzerResult(diffList.ToArray(), DiffResultType.ContentDoNotMatch)
                : new BinaryDataDiffAnalyzerResult(null, DiffResultType.Equals);
        }
    }
}