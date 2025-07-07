using System;
using ConsoleApp1.ProjetoPOO.Menus;

namespace ProjetoPOO;

internal class Program
{
    public static void Main(string[] args)
    {
        var menuAdmin = new MenuAdministrador();
        menuAdmin.RealizarLogin();
    }
}