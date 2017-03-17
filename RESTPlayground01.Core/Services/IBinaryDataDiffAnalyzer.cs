using RESTPlayground01.Core.Models;

namespace RESTPlayground01.Core.Services
{
    public interface IBinaryDataDiffAnalyzer
    {
        BinaryDataDiffAnalyzerResult Diff(byte[] leftSideData, byte[] rightSideData);
    }
}
