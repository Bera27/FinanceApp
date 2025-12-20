using System.Globalization;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Views;

public partial class DespesaView : ContentPage
{
    private readonly FinanceDataContext _context;

	public DespesaView(FinanceDataContext context)
    {
        InitializeComponent();
        _context = context;
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        => Shell.Current.GoToAsync("AddDespesa");

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CarregarDados();   
    }

    private async Task CarregarDados()
    {
        try
        {
            var despesaList = await _context.Despesas
                                            .Include(x => x.Categoria)
                                            .AsNoTracking()
                                            .ToListAsync();            

            CollectionViewDespesas.ItemsSource = despesaList;
            CalculoDeTotal(despesaList);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", $"Erro: {ex.Message}", "OK");
        }
    }

    // Função para excluir a despesa usando o Swipe
    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
        if (sender is SwipeItem swipeItem && swipeItem.CommandParameter is Despesa item)
        {
            bool confirm = await DisplayAlert("Excluir", $"Deseja excluir '{item.Descricao}'?", "Excluir", "Cancelar");

            if (!confirm) 
                return;

            _context.Remove(item);
            await _context.SaveChangesAsync();

            await CarregarDados();
        }
    }

    // Soma o valor de todas as despesas
    public void CalculoDeTotal(IEnumerable<Despesa> despesas)
    {
        decimal total = despesas.Sum(x => x.Valor);

        var ci = CultureInfo.GetCultureInfo("pt-BR");

        txtTotal.Text = total.ToString("C", ci);
    }
}