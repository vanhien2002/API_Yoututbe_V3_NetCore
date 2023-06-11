﻿namespace BlueTube.NETCore.Models
{
    public class lstLanguages
    {
        public List<LanguageModel> Languages { get; set; }
    }
    public class LanguageModel
    {
        public string id { get; set; }
        public string keyword { get; set; }
        public string title { get; set; }
        public string search { get; set; }
        public string Home { get; set; }
        public string Trending { get; set; }
        public string Music { get; set; }
        public string Film { get; set; }
        public string Game { get; set; }
        public string Football { get; set; }
        public string Film_And_Animation { get; set; }
        public string Contact { get; set; }
        public string Introduce { get; set; }
        public string language { get; set; }
    }
}
