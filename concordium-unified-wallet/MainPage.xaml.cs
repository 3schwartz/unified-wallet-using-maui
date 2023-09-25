using System.Diagnostics;
using System.Globalization;
using concordium_unified_wallet.Resources;

namespace concordium_unified_wallet;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

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
