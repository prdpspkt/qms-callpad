namespace QmsCallPad
{
    public partial class MainPage : ContentPage
    {

        String displayText = "0";
        int count = 0;
        private CancellationTokenSource? _cancellationTokenSource;

        public MainPage()
        {
            InitializeComponent();
            dispLay.Text = displayText;
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
            this.SpeakNow(dispLay.Text);
        }

        private void btnBack_Clicked(object sender, EventArgs e)
        {
            if (count > 1)
            {
                count--;
                dispLay.Text = count.ToString();
                this.SpeakNow(dispLay.Text);
            }

        }

        private void btnNext_Clicked(object sender, EventArgs e)
        {
            if (count < 999)
            {
                count++;
                dispLay.Text = count.ToString();
                this.SpeakNow(dispLay.Text);
            }
        }

        private void btnClear_Clicked(object sender, EventArgs e)
        {
            displayText = "0";
            dispLay.Text = displayText;
            count = 0;
        }

        private async void SpeakNow(String text)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var token = _cancellationTokenSource.Token;

            // Start speaking the text
            try
            {
                await TextToSpeech.SpeakAsync(dispLay.Text, cancelToken: token);
            }
            catch (OperationCanceledException)
            {
                // Handle the cancellation if the task was canceled
                Console.WriteLine("Speech was canceled.");

            }
        }
    }

}
