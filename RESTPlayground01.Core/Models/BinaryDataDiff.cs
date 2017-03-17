namespace RESTPlayground01.Core.Models
{
    public class BinaryDataDiff
    {
        public readonly int Offset;
        public readonly int Length;

        public BinaryDataDiff(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }
    }
}
