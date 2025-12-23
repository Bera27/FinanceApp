using System.Globalization;
using FinanceApp.Data;
using FinanceApp.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Views;

public partial class RendaView : ContentPage
{
	private readonly FinanceDataContext _context;

	public RendaView(FinanceDataContext context)
	{
		InitializeComponent();
		_context = context;
	}

	private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
		=> Shell.Current.GoToAsync("AddRendaView");

    private void TapGestureRecognizer_Despesa(object sender, TappedEventArgs e)
        => Shell.Current.GoToAsync("Despesa");

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await CarregarDados();
    }

	private async Task CarregarDados()
	{
		try
		{
			var rendalist = await _context.Receitas
										.Include(x => x.Categoria)
										.AsNoTracking()
										.ToListAsync();

            CollectionViewRenda.ItemsSource = rendalist;
			CalculoTotal(rendalist);
            CarregarGrafico(rendalist);
        }
		catch (Exception ex)
		{
			await DisplayAlert("Erro", $"{ex.Message}", "OK");
		}
	}

    // Função para excluir a Renda usando o Swipe
    private async void SwipeItem_Invoked(object sender, EventArgs e)
    {
		if(sender is SwipeItem swipeItem && swipeItem.CommandParameter is Receita item)
		{
            bool confirm = await DisplayAlert("Excluir", $"Deseja excluir '{item.Decricao}'?", "Excluir", "Cancelar");

            if (!confirm)
                return;

            _context.Remove(item);
            await _context.SaveChangesAsync();

            await CarregarDados();
        }
    }

	private void CalculoTotal(IEnumerable<Receita> receitas)
	{
		decimal total = receitas.Sum(x => x.Valor);
        var ci = CultureInfo.GetCultureInfo("pt-BR");

        txtTotal.Text = total.ToString("C", ci);
    }

	private void CarregarGrafico(List<Receita> receitas)
	{
        var series = receitas
            .GroupBy(d => d.Categoria.Nome)
            .Select(g => new PieSeries<decimal>
            {
                Name = g.Key,
                Values = new[] { g.Sum(x => x.Valor) }
            })
            .Cast<ISeries>()
            .ToArray();

        GraficoRenda.Series = series;
    }
}