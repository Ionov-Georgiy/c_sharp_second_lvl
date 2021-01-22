using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.Scenes;
using AsteroidGame.Objects;

namespace AsteroidGame
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                MainMenuScene mainMenuScene = new MainMenuScene();
                mainMenuScene.ShowScene();
            }
            catch(GameObjectException ex)
            {
                string desc = ex.Description;
                MessageBox.Show(desc);
            } 
        }
    }
}
