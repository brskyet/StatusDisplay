using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class EngTranslatedWordModel
    {
        public string word { get; set; }
        public string TranslatedWord { get; set; }
        public List<Definition> definitions { get; set; }
        public List<TranslatedDefinition> TranslatedDefinitions { get; set; }
        public List<Example> examples { get; set; }
        public List<TranslatedExample> TranslatedExamples { get; set; }
        public string note { get; set; }
        public string TranslatedNote { get; set; }
    }

    public class TranslatedDefinition
    {
        public int Index { get; set; }
        public string Text { get; set; }
        public string Note { get; set; }
        public string PartOfSpeech { get; set; }
    }

    public class TranslatedExample
    {
        public int Index { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
    }
}
