using QmsCallPad.Database;

namespace QmsCallPad
{
    public partial class SettingPage : ContentPage
    {
        private DatabaseHelper helper;
        private Setting setting;

        public SettingPage()
        {
            InitializeComponent();
            helper = new DatabaseHelper();
            setting = helper.FirstRecord();
            if (setting != null)
            {
                Name.Text = setting.TerminalName;
                Alphabet.Text = setting.TokenStartLetter;
                IpAddress.Text = setting.IpAddress;
                speakText.Text = setting.SpeakText;
            }
            else
            {
                setting = new Setting();
            }

        }

        private void saveBtn_Clicked(object sender, EventArgs e)
        {
            DatabaseHelper db = new DatabaseHelper();
            setting.TerminalName = Name.Text;
            setting.TokenStartLetter = Alphabet.Text;
            setting.IpAddress = IpAddress.Text;
            setting.SpeakText = speakText.Text;
            int saved = db.SaveSetting(setting);
            if (saved > 0)
            {
                CommunityToolkit.Maui.Alerts.Toast.Make("Setting updated.").Show();
            }

        }
    }

}
