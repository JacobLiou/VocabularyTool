namespace EntryTranslator.Models
{
    public class TranslateAPIConfig
    {
        public string SourceLanguage { get; set; }

        public string TargetLanguage { get; set; }

        public string SourceLanguageZh { get; set; }

        public string TargetLanguageZh { get; set; }

        public bool Overwrite { get; set; }

        public string DefaultLanguage { get; set; }
    }
}