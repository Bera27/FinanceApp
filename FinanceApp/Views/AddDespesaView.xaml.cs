using System.Globalization;
using FinanceApp.Data;
using FinanceApp.Models;

namespace FinanceApp.Views;

public partial class AddDespesaView : ContentPage
{
    private readonly FinanceDataContext _context;

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
                CategoriaId = 6
            };

            _context.Despesas.Add(novaDespesa);
            await _context.SaveChangesAsync();
            await DisplayAlert("Sucesso", "Despesa Salva.", "OK");
        }
        catch (Exception ex)
        { 
           await DisplayAlert($"Ops", ex.Message, "OK"); 
        }
    }
}