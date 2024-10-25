using System.Globalization;

namespace EntryTranslator.Models
{
    public class LanguageHolder
    {
        private string _languageId;

        public LanguageHolder(string languageId)
        {
            LanguageId = languageId;
        }

        public CultureInfo CultureInfo { get; private set; }

        public string LanguageId
        {
            get { return _languageId; }
            set
            {
                CultureInfo = CultureInfo.GetCultureInfo(value);
                _languageId = value;
            }
        }

        public override string ToString()
        {
            return LanguageId;
        }
    }
}