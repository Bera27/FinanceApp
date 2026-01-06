using System.Globalization;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Views;

public partial class AddRendaView : ContentPage
{
    private readonly FinanceDataContext _context;
    private int _selectedCategoriaId;

	public AddRendaView(FinanceDataContext context)
	{
		InitializeComponent();
        _context = context;
	}

    private void CVCategoria_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Categoria;

        if (selected != null)
            _selectedCategoriaId = selected.Id;
        else
            _selectedCategoriaId = 0;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if(valorEntry == null || string.IsNullOrWhiteSpace(valorEntry.Text))
                await DisplayAlert("Alerta", "Informe um valor", "OK");

            var input = valorEntry.Text.Trim();

            if(!decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal valor))
            {
                await DisplayAlert("Alerta", "Valor inválido", "OK");
                return;
            }

            DateTime dataRecebimento = dataRecebimentoPck.Date;
            var descricao = descricaoEntry.Text;

            var novaRenda = new Receita
            {
                Valor = valor,
                DataDeRecebimento = dataRecebimento,
                Decricao = descricao,
                CategoriaId = _selectedCategoriaId
            };

            _context.Receitas.Add(novaRenda);
            await _context.SaveChangesAsync();

            await DisplayAlert("Sucesso", "Renda Salva.", "OK");
            await Shell.Current.GoToAsync("RendaView");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", $"Ocorreu um erro: {ex.Message}", "OK");
        }
    }

    public async Task LoadImagens()
    {
        try
        {
            var categorias = await _context.Categorias
                                   .Where(c => c.Tipo == CategoriaTipo.Receita)
                                   .AsNoTracking()
                                   .ToListAsync();

            CVCategoria.ItemsSource = categorias;
        }
        catch(Exception ex)
        {
            await DisplayAlert("Ops", $"Falha ao carregar categorias: {ex.Message}", "OK");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadImagens();
    }
}