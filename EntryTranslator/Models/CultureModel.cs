namespace EntryTranslator.Models
{
    internal class CultureModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string CodeName => Code.PadRight(3) + " - " + Name;
    }
}
