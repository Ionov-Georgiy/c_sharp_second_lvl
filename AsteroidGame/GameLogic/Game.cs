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

        public static void Initialize(Space SceneObject)
        {
            space = SceneObject;
        }

        public static void PlayTurn()
        {
            space.MoveSpaceObjects();
        }
    }
}
