using AsteroidGame.GameLogic;
using AsteroidGame.Interfaces;
using AsteroidGame.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame.Scenes
{
    class StartedGameScene : GameScene
    {
        
        private bool ArrowUpIsActive = false;
        private bool ArrowDownIsActive = false;
        private bool SpaceBarIsActive = false;
        private UI gameInterface;

        public StartedGameScene(GameScene Owner) : base(Owner)
        {
            SceneForm.KeyUp += OnKeyUp;
            SceneForm.KeyDown += OnKeyDown;
            EnergyBar enBar = new EnergyBar(20, 20);
            enBar.StartDrawing += OnStartDrawing;
            IEnumerable<IUIElement> uiElements = new List<IUIElement> { enBar, };
            gameInterface = new UI(new Point(10, 0), uiElements);
            StartTheGame();
        }

        override protected Space CreateSceneObject()
        {
            return new Space(DisplaySize, 30, 10);
        }

        private void StartTheGame()
        {
            Game.Initialize(base.SceneObject);
        }

        private void OnKeyDown(object sender, EventArgs e)
        {
            if (!Game.GameOver)
            {
                if (((KeyEventArgs)e).KeyData == Keys.Up)
                    ArrowUpIsActive = true;
                if (((KeyEventArgs)e).KeyData == Keys.Down)
                    ArrowDownIsActive = true;
                if (((KeyEventArgs)e).KeyData == Keys.Space)
                    SpaceBarIsActive = true;
            }
        }

        private void OnKeyUp(object sender, EventArgs e)
        {
            if (!Game.GameOver)
            {
                if (((KeyEventArgs)e).KeyData == Keys.Up)
                    ArrowUpIsActive = false;
                if (((KeyEventArgs)e).KeyData == Keys.Down)
                    ArrowDownIsActive = false;
            }
        }

        private void OnStartDrawing(object sender, EventArgs e)
        {
            EnergyBar enBar = sender as EnergyBar;
            enBar.CurrentEnergy = Game.PlayerCurrentEnergy;
        }

        override public void ShowScene()
        {
            StartFormRendering();
            SceneForm.Show();
        }

        protected override void RenderScene(object sender, EventArgs e)
        {
            if (ArrowUpIsActive)
            {
                Game.MovePlayerUp();
            }
            if(ArrowDownIsActive)
            {
                Game.MovePlayerDown();
            }
            if (SpaceBarIsActive)
            {
                Game.MakeShot();
                SpaceBarIsActive = false;
            }
            Game.PlayTurn();
            if (!SceneFormClosed && SceneForm.Visible)
            {
                Graphics g = buffer.Graphics;
                g.Clear(Color.Black);
                ObjectInSpace[] objArr;
                if (Game.GameOver)
                {
                    g.DrawString("Game Over!", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.Red, 200, 100);
                    g.DrawString("Score: " + Game.Score, new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.Red, 200, 200);
                    objArr = SceneObject.GetBackgroundObjects();
                }
                else
                {
                    objArr = SceneObject.GetSpaceObjects().Where(so=>so.Enabled).ToArray();
                }
                foreach (var game_object in objArr)
                {
                    game_object.Draw(g);
                }
                if (!Game.GameOver) gameInterface.Draw(g);

                buffer.Render();
            }
        }

    }
}
