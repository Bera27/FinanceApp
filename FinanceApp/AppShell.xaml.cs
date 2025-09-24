using FinanceApp.Views;

namespace FinanceApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeRouting();
            InitializeComponent();
        }

        public static void InitializeRouting()
        {
            Routing.RegisterRoute("Despesa", typeof(DespesaView));
            Routing.RegisterRoute("AddDespesa", typeof(AddDespesaView));
        }
    }
}
