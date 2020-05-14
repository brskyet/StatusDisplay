using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StatusDisplayApi.Models
{
    public class EngWordModel
    {
        public string word { get; set; }
        public List<Definition> definitions { get; set; }
        public List<Example> examples { get; set; }
    }
    
    public class Definition
    {
        public int Index { get; set; }
        public string text { get; set; }
        public string partOfSpeech { get; set; }
    }

    public class Example
    {
        public int Index { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
}
