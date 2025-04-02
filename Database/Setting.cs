using SQLite;
namespace QmsCallPad.Database
{
    internal class Setting
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string TerminalName { get; set; }
        public string TokenStartLetter { get; set; }
        public string IpAddress { get; set; }

        public string SpeakText { get; set; }


    }
}
