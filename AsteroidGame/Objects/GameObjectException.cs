using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class GameObjectException : Exception
    {
        public override string Message => String.Format($"Неверно задан параметр \"{0}\" в объекте \"{1}\"\nОписание: {2}", ParamName, sourceObject.ToString(), description);
        public string ParamName => paramName;
        private string paramName;
        public string Description => description;
        private string description;
        public object SourceObject => sourceObject;
        private object sourceObject;

        public GameObjectException(string paramName, string description, object sourceObject) : base()
        {
            this.paramName = paramName;
            this.description = description;
            this.sourceObject = sourceObject;
        }
    }
}
