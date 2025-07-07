using System;
using ProjetoPOO.Menus;

namespace ProjetoPOO;

internal class Program
{
    public static void Main(string[] args)
    {
        var menuPrincipal = new MenuPrincipal();
        menuPrincipal.ExibirMenu();
    }
}