using GameOfThronesAPI.Services;

namespace GameOfThronesAPI.Models
{

    public class Character
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Culture { get; set; }
        public string Born { get; set; }
        public string Died { get; set; }
        public string[] Titles { get; set; }
        public string[] Aliases { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public string Spouse { get; set; }
        public object[] Allegiances { get; set; }
        public object[] Books { get; set; }
        public object[] PovBooks { get; set; }
        public string[] TvSeries { get; set; }
        public string[] PlayedBy { get; set; }

        public string TitleText { get { return FrozenUtils.GetArrayInString(Titles); } }
        public string AliasText { get { return FrozenUtils.GetArrayInString(Aliases); } }
        public string TvSeriesText { get { return FrozenUtils.GetArrayInString(TvSeries); } }
        public string PlayedByText { get { return FrozenUtils.GetArrayInString(PlayedBy); } }
    }
}
