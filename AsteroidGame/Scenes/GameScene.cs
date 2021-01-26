using AsteroidGame.GameLogic;
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
    abstract class GameScene
    {
        protected Form SceneForm;
        protected Space SceneObject;
        protected BufferedGraphicsContext context;
        protected BufferedGraphics buffer;
        protected Size DisplaySize;
        protected Size PreferedSize;
        protected Size WindowSize;
        private static Timer timer;
        protected GameScene Owner;
        protected bool SceneFormClosed;

        public GameScene(GameScene Owner) : this()
        {
            this.Owner = Owner;
            //SceneForm = CreateSceneForm();
            //SceneObject = CreateSceneObject();
            ////SceneForm.Show();
            //context = BufferedGraphicsManager.Current;
            //Graphics g = SceneForm.CreateGraphics();
            //buffer = context.Allocate(g, new Rectangle(0, 0, SceneForm.DisplayRectangle.Width, SceneForm.DisplayRectangle.Height));
            //timer = new Timer { Interval = 50 };
            //timer.Tick += RenderScene;
            //SceneForm.FormClosing += OnFormClosing;
            //SceneForm.FormClosed += OnFormClosed;
        }

        public GameScene()
        {
            SceneFormClosed = false;
            SceneForm = CreateSceneForm();
            SceneObject = CreateSceneObject();
            //SceneForm.Show();
            context = BufferedGraphicsManager.Current;
            Graphics g = SceneForm.CreateGraphics();
            buffer = context.Allocate(g, new Rectangle(0, 0, SceneForm.DisplayRectangle.Width, SceneForm.DisplayRectangle.Height));
            timer = new Timer { Interval = 100 };
            timer.Tick += RenderScene;
            SceneForm.FormClosing += OnFormClosing;
            SceneForm.FormClosed += OnFormClosed;
        }

        virtual protected void RenderScene(object sender, EventArgs e)
        {
            if (!SceneFormClosed && SceneForm.Visible)
            {
                Graphics g = buffer.Graphics;
                g.Clear(Color.Black);
                ObjectInSpace[] objArr = SceneObject.GetSpaceObjects();
                foreach (var game_object in objArr)
                {
                    game_object.Draw(g);
                }

                buffer.Render();
            }
        }

        private void StopRenderingScene(object sender, EventArgs e)
        {
            timer.Stop();
            timer.Dispose();
        }

        virtual protected void OnFormClosing(object sender, EventArgs e)
        {
            SceneFormClosed = true;
            StopRenderingScene(sender, e);
            if (Owner != null)
            {
                Owner.SceneForm.Visible = true;
                Owner.SceneForm.Enabled = true;
            }
        }

        virtual protected void OnFormClosed(object sender, EventArgs e)
        {
            SceneForm.Dispose();
        }

        virtual protected Space CreateSceneObject()
        {
            return new Space(GamePreferences.DisplaySettings, 30, 0);
        }

        virtual protected Form CreateSceneForm()
        {
            Form formResult = new Form();
            this.DisplaySize = GamePreferences.DisplaySettings;
            this.PreferedSize = formResult.GetPreferredSize(DisplaySize);
            this.WindowSize = new Size(DisplaySize.Width + PreferedSize.Width, DisplaySize.Height + PreferedSize.Height);
            formResult.Size = WindowSize;
            formResult.MaximumSize = WindowSize;
            formResult.MinimumSize = WindowSize;

            return formResult;
        }

        protected void StartFormRendering()
        {
            timer.Start();
        }

        virtual public void ShowScene()
        {
            StartFormRendering();
            SceneForm.Show();
        }
    }
}
