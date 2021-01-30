using AsteroidGame.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsteroidGame.GameLogic
{
    static class Game
    {
        private static Space space;
        public static bool GameOver;
        public static int Score => score;
        private static int score = 0;
        public static int PlayerMaxEnergy => 20;
        public static int PlayerCurrentEnergy => space.PlayerSpaceShip.Energy;

        public static void Initialize(Space SceneObject)
        {
            space = SceneObject;
            space.ApplyScoreRecorderOnAsteroids(RecordScore);
            GameOver = false;
        }

        public static void PlayTurn()
        {
            if(space.GetForegroundObjects().Count() > 2)
            {
                if (CheckSuccess())
                {
                    ChangeDifficultie();
                }
            }
            space.MoveSpaceObjects();
        }

        internal static void MovePlayerUp()
        {
            space.PlayerSpaceShip.MoveUp();
        }

        internal static void MovePlayerDown()
        {
            space.PlayerSpaceShip.MoveDown();
        }

        internal static void MakeShot()
        {
            space.CreateLaserBeem();
        }

        private static void RecordScore(int scorePoints)
        {
            if(!GameOver)
               score += scorePoints;
        }

        private static bool CheckSuccess() => space.GetForegroundObjects().Where(obj => obj is Asteroid && obj.Enabled).Count() < 1;

        private static void ChangeDifficultie()
        {
            space.IncreaseAsteroidsAmount(1);
            space.ResetAsteroids();
            space.ApplyScoreRecorderOnAsteroids(RecordScore);
        }
    }
}
