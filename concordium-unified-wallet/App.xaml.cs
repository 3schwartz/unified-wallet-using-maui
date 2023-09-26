using Plugin.Fingerprint.Abstractions;

namespace concordium_unified_wallet;

public partial class App : Application
{
    public App(IFingerprint fingerprint)
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage(fingerprint));
    }
}
