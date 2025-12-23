using System.Globalization;
using System.Threading.Tasks;
using FinanceApp.Data;
using FinanceApp.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

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

    private void TapGestureRecognizer_Renda(object sender, TappedEventArgs e)
        => Shell.Current.GoToAsync("RendaView");

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
            CarregarGrafico(despesaList);
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
    private void CalculoDeTotal(IEnumerable<Despesa> despesas)
    {
        decimal total = despesas.Sum(x => x.Valor);

        var ci = CultureInfo.GetCultureInfo("pt-BR");

        txtTotal.Text = total.ToString("C", ci);
    }

    private void CarregarGrafico(List<Despesa> despesas)
    {
        var series = despesas
            .GroupBy(d => d.Categoria.Nome)
            .Select(g => new PieSeries<decimal>
            {
                Name = g.Key,
                Values = new[] { g.Sum(x => x.Valor) }
            })
            .Cast<ISeries>()
            .ToArray();

        GraficoDespesas.Series = series;
    }
}