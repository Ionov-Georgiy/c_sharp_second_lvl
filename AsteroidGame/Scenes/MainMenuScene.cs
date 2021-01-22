using AsteroidGame.GameLogic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame.Scenes
{
    class MainMenuScene : GameScene
    {

        private StartedGameScene newGameScene;
        private PreferencesScene preferencesScene;

        public MainMenuScene() : base()
        {
            Game.Initialize(base.SceneObject);
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
            Button preferencesButton = new Button();
            preferencesButton.AutoSize = true;
            preferencesButton.Text = "Настройки";
            preferencesButton.Click += OpenPreferencesScene;
            preferencesButton.Location = new Point(DisplaySize.Width / 2 - preferencesButton.Size.Width / 2, startGameButton.Location.Y + startGameButton.Size.Height + preferencesButton.Size.Height);
            SceneForm.Controls.Add(preferencesButton);

            return SceneForm;
        }

        private void StartTheGame(object sender, EventArgs e)
        {
            newGameScene = new StartedGameScene(this);
            SceneForm.Visible = false;
            SceneForm.Enabled = false;
            newGameScene.ShowScene();
        }

        private void OpenPreferencesScene(object sender, EventArgs e)
        {
            preferencesScene = new PreferencesScene(this);
            SceneForm.Visible = false;
            SceneForm.Enabled = false;
            preferencesScene.ShowScene();
        }

        override public void ShowScene()
        {
            StartFormRendering();
            Application.Run(SceneForm);
        }

        protected override void RenderScene(object sender, EventArgs e)
        {
            Game.PlayTurn();
            base.RenderScene(sender, e);
        }

    }
}
