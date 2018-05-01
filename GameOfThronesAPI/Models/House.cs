using GameOfThronesAPI.Services;

namespace GameOfThronesAPI.Models
{

    public class House
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Region { get; set; }
        public string CoatOfArms { get; set; }
        public string Words { get; set; }
        public string[] Titles { get; set; }
        public string[] Seats { get; set; }
        public string CurrentLord { get; set; }
        public string Heir { get; set; }
        public string Overlord { get; set; }
        public string Founded { get; set; }
        public string Founder { get; set; }
        public string DiedOut { get; set; }
        public string[] AncestralWeapons { get; set; }
        public object[] CadetBranches { get; set; }
        public object[] SwornMembers { get; set; }

        /// <summary>
        /// These properties return with a string which created from a string array
        /// </summary>
        public string TitleText { get { return FrozenUtils.GetArrayInString(Titles); } }
        public string SeatText { get { return FrozenUtils.GetArrayInString(Seats); } }
        public string AncestralWeaponsText { get { return FrozenUtils.GetArrayInString(AncestralWeapons); } }
    }
}
