using Plugin.Fingerprint.Abstractions;

namespace concordium_unified_wallet;

public partial class StoragePage : ContentPage
{
    private const string StorageKey = "foo";
    private readonly IFingerprint _fingerprint;

    public StoragePage(IFingerprint fingerprint)
    {
        _fingerprint = fingerprint;
        InitializeComponent();
        FromStorageBtn.IsEnabled = false;
        ToStorageBtn.IsEnabled = false;
    }
    
    private async void WriteToStorage(object sender, EventArgs e)
    {
        if (StorageInsert.Text == "")
        {
            return;
        }
        await SecureStorage.Default.SetAsync(StorageKey, StorageInsert.Text);
        StorageInsert.Text = "";
        StorageOutput.Text = "";
    }

    private async void GetFromStorage(object sender, EventArgs e)
    {
        StorageOutput.Text = "";
        var fromStorage = await SecureStorage.Default.GetAsync(StorageKey);
        StorageOutput.Text = fromStorage is null or "" ? "There was nothing" : fromStorage;
    }

    private async void OnBiometricClicked(object sender, EventArgs e)
    {
        var configuration = new AuthenticationRequestConfiguration("Validate", "Access is granted only to those who have permission to enter.");
        var result = await _fingerprint.AuthenticateAsync(configuration);
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

    private async void GoBack(object sender, EventArgs args)
    {
        await Navigation.PopAsync();
    }
}
