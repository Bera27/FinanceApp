using System.Threading.Tasks;

namespace FinanceApp.Views;

public partial class DespesaView : ContentPage
{
	public DespesaView()
	{
		InitializeComponent();
	}

    private async void Despesa_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("AddDespesa");
    }
}