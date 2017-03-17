using System.Linq;

namespace RESTPlayground01.Core.Models
{
    public class DiffRequest
    {
        public int Id;
        public byte[] Left;
        public byte[] Right;

        public DiffRequest Clone()
        {
            var result = new DiffRequest();
            result.Id = Id;
            result.Left = Left != null ? Left.Select(i => i).ToArray() : null;
            result.Right = Right != null ? Right.Select(i => i).ToArray() : null;
            return result;
        }
    }
}
