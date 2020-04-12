using AutoMapper;
using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class Translate : ITranslate
    {
        private readonly ConfigJson config_json;
        private const string path = @"config.json";
        private readonly IMapper _mapper;

        public Translate(IMapper mapper)
        {
            string config = File.ReadAllText(path);
            config_json = JsonConvert.DeserializeObject<ConfigJson>(config);

            _mapper = mapper;
        }

        public EngTranslatedWordModel GetTranslation(EngWordModel engWordModel)
        {
            var translationModel = _mapper.Map<EngTranslatedWordModel>(engWordModel);

            translationModel.TranslatedWord = CallToTranslateApi(translationModel.word);
            translationModel.TranslatedNote = CallToTranslateApi(translationModel.note);
            translationModel.TranslatedDefinitions = new List<TranslatedDefinition>();
            translationModel.TranslatedExamples = new List<TranslatedExample>();

            int index = 0;
            foreach(var d in translationModel.definitions)
            {
                d.Index = index;
                translationModel.TranslatedDefinitions.Add(new TranslatedDefinition
                {
                    Text = CallToTranslateApi(d.text),
                    Note = CallToTranslateApi(d.note),
                    PartOfSpeech = CallToTranslateApi(d.partOfSpeech),
                    Index = index
                });
                index++;
            }

            index = 0;

            foreach (var e in translationModel.examples)
            {
                e.Index = index;
                translationModel.TranslatedExamples.Add(new TranslatedExample
                {
                    Text = CallToTranslateApi(e.text),
                    Title = CallToTranslateApi(e.title),
                    Index = index
                });
                index++;
            }

            return translationModel;
        }

        private string CallToTranslateApi(string text)
        {
            if (text == "")
                return "";
            WebRequest request = WebRequest.Create(
                $"https://translate.yandex.net/api/v1.5/tr.json/translate?key={config_json.yandex_translate_key}" +
                $"&text={text.Replace(';', '.')}&lang={config_json.yandex_translate_lang}");
            WebResponse response = request.GetResponse();
            TranslationModel result;
            using (Stream dataStream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(dataStream);
                string json = reader.ReadToEnd();
                result = JsonConvert.DeserializeObject<TranslationModel>(json);
            }
            response.Close();

            return result.text.First();
        }
    }
}
