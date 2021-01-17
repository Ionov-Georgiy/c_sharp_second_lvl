using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
{
    class StartedGameScene : GameScene
    {
        public StartedGameScene(GameScene Owner) : base(Owner)
        {
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

        override public void ShowScene()
        {
            StartFormRendering();
            SceneForm.Show();
        }

        protected override void RenderScene(object sender, EventArgs e)
        {
            Game.PlayTurn();
            base.RenderScene(sender, e);
        }

    }
}
