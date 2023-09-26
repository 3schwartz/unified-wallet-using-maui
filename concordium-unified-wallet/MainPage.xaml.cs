using System.Diagnostics;
using System.Globalization;
using concordium_unified_wallet.Resources;
using Plugin.Fingerprint.Abstractions;

namespace concordium_unified_wallet;

public partial class MainPage : ContentPage
{
    private const string storageKey = "foo";
    private int _count = 0;
    private readonly IFingerprint _fingerPrint;
    public bool StorageEnabled { get; set; } = false;

    public MainPage(IFingerprint fingerprint)
    {
        InitializeComponent();
        _fingerPrint = fingerprint;
        FromStorageBtn.IsEnabled = false;
        ToStorageBtn.IsEnabled = false;
    }

    private async void WriteToStorage(object sender, EventArgs e)
    {
        await SecureStorage.Default.SetAsync(storageKey, StorageInsert.Text);
        StorageInsert.Text = "";
        StorageOutput.Text = "";
    }

    private async void GetFromStorage(object sender, EventArgs e)
    {
        StorageOutput.Text = "";
        var fromStorage = await SecureStorage.Default.GetAsync(storageKey);
        StorageOutput.Text = fromStorage is null or "" ? "There was nothing" : fromStorage;
    }

    private async void OnBiometricClicked(object sender, EventArgs e)
    {
        var configuration = new AuthenticationRequestConfiguration("Validate", "Access is granted only to those who have permission to enter.");
        var result = await _fingerPrint.AuthenticateAsync(configuration);
        if (result.Authenticated)
        {
            await DisplayAlert("Authenticate!", "Access Granted", "OK");
            FromStorageBtn.IsEnabled = true;
            ToStorageBtn.IsEnabled = true;
        }
        else
        {
            await DisplayAlert("Unauthenticated", "Access Denied", "OK");
            FromStorageBtn.IsEnabled = false;
            ToStorageBtn.IsEnabled = false;
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
