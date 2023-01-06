using System.Text.Json.Serialization;

namespace AntdMangement.RequestModels
{
    /// <summary>
    /// 列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonListResult<T> : CommonResult
    {
        public CommonListResult() : base()
        {

        }
        public T Data { get; set; }

        public int TotalCount { get; set; }

    }

    /// <summary>
    /// 详情
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonResult<T> : CommonResult
    {
        public CommonResult() : base()
        {

        }
        [JsonPropertyName("data")]
        public T Data { get; set; }

    }

    /// <summary>
    /// 操作
    /// </summary>
    public class CommonResult
    {
        public CommonResult()
        {
            Code = 2000;
            Message = "";
        }
        public CommonResult(int code, string message)
        {
            Code = code;
            Message = message;
        }
        [JsonPropertyName("code")]

        public int Code { get; set; }
        [JsonPropertyName("message")]

        public string Message { get; set; }
    }
}
