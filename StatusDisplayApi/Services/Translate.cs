using AutoMapper;
using Newtonsoft.Json;
using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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

        public async Task<EngTranslatedWordModel> GetTranslation(EngWordModel engWordModel)
        {
            var translationModel = _mapper.Map<EngTranslatedWordModel>(engWordModel);

            translationModel.TranslatedWord = await CallToTranslateApi(translationModel.word);
            translationModel.TranslatedDefinitions = new List<TranslatedDefinition>();
            translationModel.TranslatedExamples = new List<TranslatedExample>();

            int index = 0;
            foreach(var d in translationModel.definitions)
            {
                d.Index = index;
                translationModel.TranslatedDefinitions.Add(new TranslatedDefinition
                {
                    Text = await CallToTranslateApi(d.text),
                    PartOfSpeech = await CallToTranslateApi(d.partOfSpeech),
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
                    Text = await CallToTranslateApi(e.text),
                    Title = await CallToTranslateApi(e.title),
                    Index = index
                });
                index++;
            }

            return translationModel;
        }

        private async Task<string> CallToTranslateApi(string text)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return "";

                var client = new HttpClient
                {
                    BaseAddress = new Uri(
                        $"https://translate.yandex.net/api/v1.5/tr.json/translate?key={config_json.yandex_translate_key}" +
                        $"&text={text.Replace(';', '.')}&lang={config_json.yandex_translate_lang}")
                };

                var response = await client.GetAsync("");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(await response.Content.ReadAsStringAsync());
                }

                var result = JsonConvert.DeserializeObject<TranslationModel>(await response.Content.ReadAsStringAsync());

                return result.text.First();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            
        }
    }
}
