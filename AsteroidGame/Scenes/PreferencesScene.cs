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
    class PreferencesScene : GameScene
    {
        public int DisplaySettingsIndex { get; set; }

        public PreferencesScene(GameScene Owner) : base(Owner)
        {
            Game.Initialize(base.SceneObject);
        }

        override protected Form CreateSceneForm()
        {
            Form SceneForm = base.CreateSceneForm();

            Size ComboBoxSize = new Size(180, 21);
            Size LabelSize = new Size(120, 21);
            Size SettingsSize = new Size(ComboBoxSize.Width + LabelSize.Width + 20, ComboBoxSize.Height + LabelSize.Height);

            Label lbl = new Label();
            lbl.Text = "Настройки окна";
            lbl.Size = LabelSize;
            lbl.Location = new Point(DisplaySize.Width / 2 - SettingsSize.Width/2 , DisplaySize.Height / 2 - SettingsSize.Height / 2);
            lbl.Font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            lbl.ForeColor = Color.Green;
            lbl.BackColor = Color.Black;
            SceneForm.Controls.Add(lbl);

            ComboBox DisplaySettings = new ComboBox();
            DisplaySettings.FormattingEnabled = true;
            DisplaySettings.Size = ComboBoxSize;
            DisplaySettings.Location = new Point(DisplaySize.Width / 2 - LabelSize.Width/2 + 20, DisplaySize.Height / 2 - SettingsSize.Height / 2);
            DisplaySettings.Name = "DisplaySettings";
            DisplaySettings.Items.Add("800 - 600");
            DisplaySettings.Items.Add("1024 - 768");
            DisplaySettings.Items.Add("1920 - 1080");
            DisplaySettings.Items.Add("2000 - exeption");
            DisplaySettings.SelectedIndex = 0;
            DisplaySettings.DataBindings.Add("SelectedIndex", this, "DisplaySettingsIndex");
            SceneForm.Controls.Add(DisplaySettings);

            Button AcceptButton = new Button();
            AcceptButton.AutoSize = true;
            AcceptButton.Text = "Закрыть";
            AcceptButton.Click += ClosePreferencesScene;
            AcceptButton.Location = new Point(DisplaySize.Width/2 - AcceptButton.Size.Width/2, DisplaySettings.Location.Y + AcceptButton.Size.Height + 10);
            SceneForm.Controls.Add(AcceptButton);

            SceneForm.FormClosed += OnFormClosed;

            return SceneForm;
        }

        private void ClosePreferencesScene(object sender, EventArgs e)
        {
            SceneForm.Close();
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            GamePreferences.SetDisplaySettings(DisplaySettingsIndex);
        }

        protected override void RenderScene(object sender, EventArgs e)
        {
            Game.PlayTurn();
            base.RenderScene(sender, e);
        }

    }
}
