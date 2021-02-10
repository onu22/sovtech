using System;
using System.Collections.Generic;

namespace Open.API.Models
{
    public class SearchData
    {
        public JokeData ChuckNorris { get; set; }
        public SwapData SwAPI { get; set; }

        public List<string> SourceApis { get; set; }
        public SearchData()
        {
            SourceApis = new List<string>();
        }
    }

    
}
