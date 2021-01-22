using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    static class SplashScreen
    {
        public static void Initialize()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //MainMenu
            Form mainMenu = new Form();
            Size DisplaySize = new Size(800, 600);
            Size PreferedSize = mainMenu.GetPreferredSize(DisplaySize);
            Size WindowSize = new Size(DisplaySize.Width + PreferedSize.Width, DisplaySize.Height + PreferedSize.Height);
            mainMenu.Size = WindowSize;
            Button startGameButton = new Button();
            startGameButton.Text = "Начать игру";
            startGameButton.Click += StartTheGame;
            startGameButton.Location = new Point(DisplaySize.Width/2 - startGameButton.Size.Width / 2, DisplaySize.Height / 2 - startGameButton.Size.Height / 2);
            mainMenu.Controls.Add(startGameButton);
            mainMenu.Show();
            mainMenu.Shown += RefreshMainMenu;
            mainMenu.SizeChanged += RefreshMainMenu;

            //GameForm
            Form game_form = new Form();
            game_form.FormClosing += EndTheGame;
            game_form.FormClosed += AfterFormClosed;
            game_form.Size = WindowSize;
            
            Game.Initialize(game_form, mainMenu);
            Game.StartMainMenu();

            Application.Run(mainMenu);
        }

        private static void StartTheGame(object sender, EventArgs e)
        {
            Game.StartGame();
        }

        private static void EndTheGame(object sender, EventArgs e)
        {
            Game.EndGame();
        }

        private static void RefreshMainMenu(object sender, EventArgs e)
        {
            Form mainMenu = sender as Form;
            if (mainMenu.Visible)
            {
                Game.StartMainMenu();
            }
        }

        private static void AfterFormClosed(object sender, EventArgs e)
        {
            Form fSender = sender as Form;
            fSender.Owner.Visible = true;
            fSender.Owner.Enabled = true;
        }

    }
}
