using GameOfThronesAPI.Services;
using System;

namespace GameOfThronesAPI.Models
{

    public class Book
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Isbn { get; set; }
        public string[] Authors { get; set; }
        public int NumberOfPages { get; set; }
        public string Publisher { get; set; }
        public string Country { get; set; }
        public string MediaType { get; set; }
        public DateTime Released { get; set; }
        public object[] Characters { get; set; }
        public object[] PovCharacters { get; set; }

        /// <summary>
        /// Returns with authors list in a string
        /// </summary>
        public string AuthorsText { get { return FrozenUtils.GetArrayInString(Authors); } }

        /// <summary>
        /// Gets a formatted release date
        /// </summary>
        public string ReleasedFormatted { get { return Released.ToString("yyyy.MM.dd dddd"); } }
    }

}
