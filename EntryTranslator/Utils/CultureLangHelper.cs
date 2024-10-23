using EntryTranslator.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntryTranslator.Utils
{
    internal class CultureLangHelper
    {
        private static IEnumerable<string> _cultures;

        public static async Task<List<CultureModel>> GetLanguageList()
        {
            if (_cultures == null)
                _cultures = await GetProperCulturesAsync();
            var languageList = await Task.Factory.StartNew(() =>
                _cultures.Select(x =>
                    new CultureModel { Code = x, Name = CultureInfo.GetCultureInfo(x).DisplayName }).ToList());
            return languageList;
        }

        private static async Task<IEnumerable<string>> GetProperCulturesAsync()
        {
            var allCodes = await Task.Factory.StartNew(() => CultureInfo.GetCultures(CultureTypes.AllCultures).Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => x.Name));

            try
            {
                var downloadedCodes = GetLanguageCodesOffline();
                var properCodes = await Task.Factory.StartNew(() => allCodes.Where(x => downloadedCodes.Contains(x)));

                return properCodes ?? allCodes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            GC.Collect();
            return allCodes;
        }

        private static List<string> GetLanguageCodesOffline()
        {
           return Enum.GetNames(typeof(LanguageEnum)).ToList();
        }
    }
}