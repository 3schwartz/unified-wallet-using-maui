using System.Diagnostics;
using System.Globalization;
using concordium_unified_wallet.Resources;
using Plugin.Fingerprint.Abstractions;

namespace concordium_unified_wallet;

public partial class MainPage : ContentPage
{
    private int _count = 0;
    private readonly IFingerprint _fingerPrint;

    public MainPage(IFingerprint fingerprint)
    {
        InitializeComponent();
        _fingerPrint = fingerprint;
    }

    private async void OnBiometricClicked(object sender, EventArgs e)
    {
        var configuration = new AuthenticationRequestConfiguration("Validate", "Access is granted only to those who have permission to enter.");
        var result = await _fingerPrint.AuthenticateAsync(configuration);
        if (result.Authenticated)
        {
            await DisplayAlert("Authenticate!", "Access Granted", "OK");
        }
        else
        {
            await DisplayAlert("Unauthenticated", "Access Denied", "OK");
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        _count++;

        if (_count == 1)
            CounterBtn.Text = $"Clicked {_count} time";
        else
            CounterBtn.Text = $"Clicked {_count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void GetIdProof(object sender, EventArgs e)
    {
        var sw = Stopwatch.StartNew();
        var idProof = InteropsApp.GetIdProof();
        ProofDuration.Text = sw.Elapsed.TotalSeconds.ToString(CultureInfo.InvariantCulture);
        ProofText.Text = idProof;
    }
}
