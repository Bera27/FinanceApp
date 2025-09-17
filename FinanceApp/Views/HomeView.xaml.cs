using System.Threading.Tasks;

namespace FinanceApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView()
	{
		InitializeComponent();
	}

    private async void Despesa_Tapped(object sender, TappedEventArgs e)
    {
		await Shell.Current.GoToAsync("Despesa");
    }
}