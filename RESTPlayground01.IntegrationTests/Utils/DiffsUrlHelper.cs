namespace RESTPlayground01.IntegrationTests.Utils
{
    public class DiffsUrlHelper : UrlHelperBase
    {
        public DiffsUrlHelper()
            : base(string.Empty)
        {
        }

        public DiffsUrlHelper(string baseAddress) 
            : base(baseAddress)
        {
        }

        internal string GetDiff(int id)
        {
            return string.Format("{0}/diff/{1}", BaseAddress, id);
        }

        internal string PutLeftSide(int id)
        {
            return string.Format("{0}/diff/{1}/left", BaseAddress, id);
        }

        internal string PutRightSide(int id)
        {
            return string.Format("{0}/diff/{1}/right", BaseAddress, id);
        }
    }
}
