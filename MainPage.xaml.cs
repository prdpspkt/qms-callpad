using CommunityToolkit.Maui.Alerts;
using QmsCallPad.Database;

namespace QmsCallPad
{
    public partial class MainPage : ContentPage
    {
        String displayText = "0";
        int count = 0;
        private CancellationTokenSource? _cancellationTokenSource;
        private DatabaseHelper helper;
        private string terminalName = "New Terminal";
        private string terminalAlphabet = "Z";
        private string ipAddress = "192.168.4.1";
        private string speakText = "Token number {letter}{number}. Please proceed to the {name} counter";


        public MainPage()
        {
            InitializeComponent();
            dispLay.Text = displayText;
            helper = new DatabaseHelper();
            var setting = helper.FirstRecord();
            if (helper.FirstRecord() != null)
            {
                this.terminalAlphabet = setting.TokenStartLetter;
                this.terminalName = setting.TerminalName;
                this.speakText = setting.SpeakText;
                this.ipAddress = setting.IpAddress;

            }

        }

        private void numberBtn_Clicked(object sender, EventArgs e)
        {
            if (dispLay.Text == "0")
            {
                displayText = "";
            }
            if (displayText.Length < 3)
            {
                Button btn = sender as Button;
                displayText = displayText + btn.Text;
                dispLay.Text = displayText;
                count = Int32.Parse(dispLay.Text);
            }


        }

        private async void btnCall_Clicked(object sender, EventArgs e)
        {
            await this.SpeakNow();
        }

        private async void btnBack_Clicked(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;
                dispLay.Text = count.ToString();
                await this.SpeakNow();
            }

        }

        private async void btnNext_Clicked(object sender, EventArgs e)
        {
            if (count < 999)
            {
                count++;
                dispLay.Text = count.ToString();
                await this.SpeakNow();
            }
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            displayText = "0";
            dispLay.Text = displayText;
            count = 0;
        }

        private async Task SpeakNow()
        {
            await this.SendText();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;
            string tokenNumber = dispLay.Text;
            string speakit = this.speakText
                .Replace("{number}", tokenNumber)
                .Replace("{letter}", this.terminalAlphabet)
                .Replace("{name}", this.terminalName);
            // Start speaking the text
            try
            {
                await TextToSpeech.SpeakAsync(speakit, token);
            }
            catch (OperationCanceledException ex)
            {
                await Toast.Make(ex.Message).Show();

            }
        }
        private async Task SendText()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // The URL of your API endpoint
                    var url = "http://" + this.ipAddress + "/text";


                    var formData = new Dictionary<string, string>
                    {
                        {"text", this.terminalAlphabet + count.ToString() },
                    };
                    var content = new FormUrlEncodedContent(formData);

                    // Send the POST request
                    var response = await client.PostAsync(url, content);
                }
                catch (Exception ex)
                {
                    // Handle any exceptions that occur
                    await DisplayAlert("Exception", "Error: " + ex.Message, "OK");
                }
            }
        }

    }
}
