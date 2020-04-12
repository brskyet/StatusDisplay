using StatusDisplayApi.Interfaces;
using StatusDisplayApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StatusDisplayApi.Services
{
    public class EngWord : IEngWord
    {
        public EngWordModel GetOriginalWord()
        {
            EngWordModel model = new EngWordModel();

            return model;
        }
    }
}
