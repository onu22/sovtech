using System;
using System.Collections.Generic;

namespace Open.API.Models
{
    public class JokeData
    {
        public int Total { get; set; }
        public IEnumerable<Result> Result { get; set; }
    }

    public class Result
    {
        public IEnumerable<String> Categories { get; set; }
        public string Created_at { get; set; }
        public string Icon_url { get; set; }
        public string Id { get; set; }
        public string Updated_at { get; set; }
        public string Url { get; set; }
        public string Value { get; set; }

    }
}
