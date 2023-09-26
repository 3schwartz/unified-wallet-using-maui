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

    private async void GoToWeb(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new WebPage());
    }
    
    private async void GoToStorage(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new StoragePage(_fingerPrint));
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
