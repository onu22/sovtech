using System;
using System.Collections.Generic;

namespace Open.API.Models
{
    public class SwapData
    {
        public int Count { get; set; }
        public string Next { get; set; }
        public string Previous { get; set; }
        public IEnumerable<SwapDataResult> Results { get; set; }

      
    }

    public class SwapDataResult
    {

        public string Name { get; set; }
        public string Height { get; set; }
        public string Mass { get; set; }
        public string Hair_color { get; set; }
        public string Skin_color { get; set; }
        public string Eye_color { get; set; }
        public string Birth_year { get; set; }
        public string Gender { get; set; }
        public IEnumerable<String> Films { get; set; }
        public IEnumerable<String> Species { get; set; }
        public IEnumerable<String> Vehicles { get; set; }
        public IEnumerable<String> Starships { get; set; }
        public string Created { get; set; }
        public string Edited { get; set; }
        public string Url { get; set; }
    }

}
