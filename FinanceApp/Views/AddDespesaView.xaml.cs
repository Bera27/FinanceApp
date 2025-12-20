using System.Globalization;
using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Views;

public partial class AddDespesaView : ContentPage
{
    private readonly FinanceDataContext _context;
    private int _selectedCategoriaId;

    public AddDespesaView(FinanceDataContext context)
    {
        InitializeComponent();
        _context = context;
    }
    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (valorEntry == null || string.IsNullOrWhiteSpace(valorEntry.Text))
                await DisplayAlert("Alerta", "Informe um valor.", "OK");

            var input = valorEntry.Text.Trim();

            if (!decimal.TryParse(input, NumberStyles.Number, CultureInfo.CurrentCulture, out decimal valor))
            {
                await DisplayAlert("Alerta", "Valor inválido!", "OK");
                return;
            }

            DateTime dataCompra = dataCompraPck.Date;
            DateTime dataVencimento = dataVencimentoPck.Date;
            var descricao = descricaoEntry.Text;

            var novaDespesa = new Despesa
            {
                Valor = valor,
                DataPagamento = dataCompra,
                DataVencimento = dataVencimento,
                Descricao = descricao,
                CategoriaId = _selectedCategoriaId
            };

            _context.Despesas.Add(novaDespesa);
            await _context.SaveChangesAsync();

            await DisplayAlert("Sucesso", "Despesa Salva.", "OK");
            await Shell.Current.GoToAsync("Despesa");
        }
        catch (Exception ex)
        { 
           await DisplayAlert($"Ops", ex.Message, "OK"); 
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadImages();
    }

    private void CVCategoria_SelectionChanged(object sender ,SelectionChangedEventArgs e)
    {
        var selected = e.CurrentSelection.FirstOrDefault() as Categoria;

        if (selected != null)
            _selectedCategoriaId = selected.Id;

        else
            _selectedCategoriaId = 0;
    }

    private async Task LoadImages()
    {
        try
        {
            var categorias = await _context.Categorias
                                           .Where(c => c.Tipo == CategoriaTipo.Despesa)
                                           .AsNoTracking()
                                           .ToListAsync();
            CVCategoria.ItemsSource = categorias;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", $"Falha ao carregar categorias: {ex.Message}", "Ok");
        }
    }
}