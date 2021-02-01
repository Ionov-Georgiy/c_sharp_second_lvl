using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    class MainMenuScene : GameScene
    {

        private StartedGameScene newGameScene;

        public MainMenuScene() : base()
        {
            
        }

        override protected Form CreateSceneForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form SceneForm = base.CreateSceneForm();
            Button startGameButton = new Button();
            startGameButton.AutoSize = true;
            startGameButton.Text = "Начать игру";
            startGameButton.Click += StartTheGame;
            startGameButton.Location = new Point(DisplaySize.Width / 2 - startGameButton.Size.Width / 2, DisplaySize.Height / 2 - startGameButton.Size.Height / 2);
            SceneForm.Controls.Add(startGameButton);
            Label devLabel = new Label();
            devLabel.AutoSize = true;
            devLabel.Text = "Ионов Георгий";
            devLabel.BackColor = Color.Black;
            devLabel.ForeColor = Color.Green;
            devLabel.Location = new Point(DisplaySize.Width / 2 - devLabel.PreferredSize.Width/2, startGameButton.Location.X - startGameButton.Location.Y / 2);
            SceneForm.Controls.Add(devLabel);

            return SceneForm;
        }

        private void StartTheGame(object sender, EventArgs e)
        {
            newGameScene = new StartedGameScene(this);
            SceneForm.Visible = false;
            SceneForm.Enabled = false;
            newGameScene.ShowScene();
        }

        override public void ShowScene()
        {
            StartFormRendering();
            Application.Run(SceneForm);
        }
    }
}
