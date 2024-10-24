using EntryTranslator.Models;
using EntryTranslator.Util;
using EntryTranslator.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace STranslate.ViewModels.Preference.Services
{
    public partial class TranslatorBaidu
    {
        public string _url = "https://fanyi-api.baidu.com/api/trans/vip/translate";

        public string _appID = "20241018002179257";

        public string _appKey = "vhPEk3Gv6hvBc160VRWw";

        public StranslationResult _data = StranslationResult.Reset;

        public async Task<StranslationResult> TranslateAsync(RequestModel request, CancellationToken token)
        {
            string salt = new Random().Next(100000).ToString();
            string sign = StringUtil.EncryptString(_appID + request.Text + salt + _appKey);

            //百度API和CultureInfo不一样 需要转换
            var queryparams = new Dictionary<string, string>
                {
                    { "q", request.Text },
                    { "from",BaiduLang.LangDic[request.SourceLangText] },
                    { "to", BaiduLang.LangDic[request.TargetLangText] },
                    { "appid", _appID },
                    { "salt", salt },
                    { "sign", sign }
                };


            string resp = await HttpUtil.GetAsync(_url, queryparams, token);
            if (string.IsNullOrEmpty(resp))
                throw new Exception("请求结果为空");

            var ret = JsonConvert.DeserializeObject<ResponseBaidu>(resp ?? "");

            //如果出错就将整个返回信息写入取值处
            if (ret is null || string.IsNullOrEmpty(ret.TransResult?.FirstOrDefault()?.Dst))
            {
                throw new Exception(resp);
            }

            var transResults = ret.TransResult ?? [];
            var data = transResults.Length == 0 ? throw new Exception("请求结果为空")
                    : string.Join(Environment.NewLine, transResults.Where(trans => !string.IsNullOrEmpty(trans.Dst)).Select(trans => trans.Dst));

            return StranslationResult.Success(data);
        }
    }
}