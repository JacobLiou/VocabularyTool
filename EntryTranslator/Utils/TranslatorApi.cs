using EntryTranslator.Models;
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

