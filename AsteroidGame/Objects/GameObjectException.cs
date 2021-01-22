using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class GameObjectException : Exception
    {
        public override string Message { get { return "Неверно задан параметр \"" + ParamName + "\" в объекте \"" + this.ToString() + "\"\nОписание: " + description; } }
        public string ParamName { get { return paramName; }  }
        private string paramName;
        public string Description { get { return description; } }
        private string description;

        public GameObjectException(string paramName, string description) : base()
        {
            this.paramName = paramName;
            this.description = description;
        }
    }
}
