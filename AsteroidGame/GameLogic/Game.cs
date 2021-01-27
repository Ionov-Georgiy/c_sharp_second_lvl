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

        public static void Initialize(Space SceneObject)
        {
            space = SceneObject;
            space.ApplyScoreRecorderOnAsteroids(RecordScore);
            GameOver = false;
        }

        public static void PlayTurn()
        {
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

        private static void RecordScore(int scorePoints)
        {
            if(!GameOver)
               score += scorePoints;
        }
    }
}
