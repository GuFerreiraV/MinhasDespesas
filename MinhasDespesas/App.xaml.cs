﻿namespace MinhasDespesas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListarProduto());
        }
    }
}
