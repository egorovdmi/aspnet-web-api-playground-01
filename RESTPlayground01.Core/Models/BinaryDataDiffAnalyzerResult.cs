using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RESTPlayground01.Core.Models
{
    public class BinaryDataDiffAnalyzerResult
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public readonly BinaryDataDiff[] Diffs;

        [JsonConverter(typeof(StringEnumConverter))]
        public readonly DiffResultType DiffResultType;

        public BinaryDataDiffAnalyzerResult(BinaryDataDiff[] diffs, DiffResultType diffResultType)
        {
            Diffs = diffs;
            DiffResultType = diffResultType;
        }
    }
}
