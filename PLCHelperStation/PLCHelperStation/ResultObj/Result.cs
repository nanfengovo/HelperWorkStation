namespace PLCHelperStation.ResultObj
{
    public class Result
    {
        public int Code { get; set; }

        public bool ResultType { get; set; }

        public string? Message { get; set; }
        public object Data { get; internal set; }
    }
}
