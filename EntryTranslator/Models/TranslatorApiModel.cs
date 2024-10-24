using Newtonsoft.Json;
using System;

namespace EntryTranslator.Models
{
    public class RequestModel
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("source_lang")]
        public string SourceLang { get; set; }

        [JsonProperty("target_lang")]
        public string TargetLang { get; set; }

        public string SourceLangText { get; set; }

        public string TargetLangText { get; set; }
    }

    public class ResponseApi
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; } = "";

        public string ErrMsg { get; set; } = "";
    }

    public partial class StranslationResult
    {
        public bool IsSuccess { get; set; } = true;

        public string SourceLang { get; set; }

        public string TargetLang { get; set; }

        public object? Result { get; set; }

        // 可选，如果你想保留异常的详细信息
        public Exception? Exception { get; }

        /// <summary>
        /// 静态方法用于创建成功的结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static StranslationResult Success(object result)
        {
            return new StranslationResult(result);
        }

        /// <summary>
        /// 静态方法用于创建失败的结果
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static StranslationResult Fail(string errorMessage, Exception? exception = null)
        {
            return new StranslationResult(errorMessage, exception);
        }

        /// <summary>
        /// 静态方法用于清空
        /// </summary>
        /// <returns></returns>
        public static StranslationResult Reset => new();

        /// <summary>
        /// 成功时使用的构造函数
        /// </summary>
        /// <param name="result"></param>
        private StranslationResult(object result)
        {
            IsSuccess = true;
            Result = result;
            Exception = null;
        }

        /// <summary>
        /// 失败时使用的构造函数
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="exception"></param>
        private StranslationResult(string errorMessage, Exception? exception = null)
        {
            IsSuccess = false;
            Result = errorMessage;
            //Exception = null;//暂时不记录异常
            Exception = exception;
        }

        /// <summary>
        /// 清空时使用的构造函数
        /// </summary>
        private StranslationResult()
        {
            IsSuccess = true;
            Result = string.Empty;
            Exception = null;
        }
    }
}
