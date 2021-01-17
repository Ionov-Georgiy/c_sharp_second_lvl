using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame
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
        private bool sceneFormClosed;

        public GameScene(GameScene Owner)
        {
            this.Owner = Owner;
            SceneForm = CreateSceneForm();
            SceneObject = CreateSceneObject();
            //SceneForm.Show();
            context = BufferedGraphicsManager.Current;
            Graphics g = SceneForm.CreateGraphics();
            buffer = context.Allocate(g, new Rectangle(0, 0, SceneForm.DisplayRectangle.Width, SceneForm.DisplayRectangle.Height));
            timer = new Timer { Interval = 100 };
            timer.Tick += RenderScene;
            SceneForm.FormClosing += OnFormClosing;
        }

        public GameScene()
        {
            sceneFormClosed = false;
            SceneForm = CreateSceneForm();
            SceneObject = CreateSceneObject();
            //SceneForm.Show();
            context = BufferedGraphicsManager.Current;
            Graphics g = SceneForm.CreateGraphics();
            buffer = context.Allocate(g, new Rectangle(0, 0, SceneForm.DisplayRectangle.Width, SceneForm.DisplayRectangle.Height));
            timer = new Timer { Interval = 100 };
            timer.Tick += RenderScene;
            SceneForm.FormClosing += OnFormClosing;
        }

        virtual protected void RenderScene(object sender, EventArgs e)
        {
            if (!sceneFormClosed)
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

        private void OnFormClosing(object sender, EventArgs e)
        {
            sceneFormClosed = true;
            StopRenderingScene(sender, e);
            if (Owner != null)
            {
                Owner.SceneForm.Visible = true;
                Owner.SceneForm.Enabled = true;
            }
        }

        virtual protected Space CreateSceneObject()
        {
            return new Space(DisplaySize, 30, 0);
        }

        virtual protected Form CreateSceneForm()
        {
            Form formResult = new Form();
            this.DisplaySize = new Size(800, 600);
            this.PreferedSize = formResult.GetPreferredSize(DisplaySize);
            this.WindowSize = new Size(DisplaySize.Width + PreferedSize.Width, DisplaySize.Height + PreferedSize.Height);
            formResult.Size = WindowSize;
            
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
