using Azure;
using Azure.AI.OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoGPT
{
    public class APIMan
    {
        public OpenAIClient OpenAI { get; set; }
        public string LM = "gpt-3.5-turbo";
        public APIMan()
            => OpenAI = new(Preferences.Default.Get("openaikey", "NO OPENAI KEY SET, GO TO PREFERENCES PAGE AND SET ONE."));
    }
}
