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
            Routing.RegisterRoute("Home", typeof(HomeView));
            Routing.RegisterRoute("Despesa", typeof(DespesaView));
        }
    }
}
