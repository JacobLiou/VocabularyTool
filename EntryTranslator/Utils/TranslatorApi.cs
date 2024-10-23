using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EntryTranslator.Utils;

public class TranslatorApi
{
    public string _url = "https://deeplx.deno.dev/translate";

    public string _appID = string.Empty;

    public string _appKey = string.Empty;

    public StranslationResult _data = StranslationResult.Reset;

    public string Tips { get; set; } =
        @"请求:
                {
                    ""text"": ""test"",
                    ""source_lang"": ""auto"",
                    ""target_lang"": ""zh""
                }
                回复:
                {
                    ""code"": 200,
                    ""data"": ""测试""
                }";

    private string Token = string.Empty;

    public async Task<StranslationResult> TranslateAsync(RequestModel request, CancellationToken canceltoken)
    {
        if (request is RequestModel)
        {
            var req = JsonConvert.SerializeObject(request);

            var authToken = string.IsNullOrEmpty(Token) ? [] : new Dictionary<string, string> { { "Authorization", $"Bearer {Token}" } };

            string resp = await HttpUtil.PostAsync(_url, req, null, authToken, canceltoken);
            if (string.IsNullOrEmpty(resp))
                throw new Exception("请求结果为空");

            var ret = JsonConvert.DeserializeObject<ResponseApi>(resp ?? "");

            if (ret is null || string.IsNullOrEmpty(ret.Data.ToString()))
            {
                throw new Exception(resp);
            }

            return StranslationResult.Success(ret.Data.ToString() ?? "");
        }

        throw new Exception($"请求数据出错: {request}");
    }
}

public class RequestModel(string text, string source, string target)
{
[JsonProperty("text")]
public string Text { get; set; } = text;

[JsonProperty("source_lang")]
public string SourceLang { get; set; } = source;

[JsonProperty("target_lang")]
public string TargetLang { get; set; } = target;
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