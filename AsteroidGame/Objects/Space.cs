using AsteroidGame.GameLogic;
using AsteroidGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.Objects
{
    class Space
    {
        private int starsAmount;
        private int asteroidsAmount;
        public Size SpaceSize => spaceSize;
        private Size spaceSize;
        private List<ObjectInSpace> backgroundObjects;
        private List<ObjectInSpace> foregroundObjects;
        public SpaceShip PlayerSpaceShip;

        public Space(Size size, int starsAmount, int asteroidsAmount)
        {
            this.spaceSize = size;
            this.starsAmount = starsAmount;
            this.asteroidsAmount = asteroidsAmount;
            FillInSpace();
        }

        private void FillInSpace()
        {
            backgroundObjects = new List<ObjectInSpace>();
            foregroundObjects = new List<ObjectInSpace>();
            CreateStars();
            CreateAsteroids();
            CreateEnergyKit();
            CreateSpaceShip();
        }

        private void CreateStars()
        {
            for (int i = 0; i < starsAmount; i++)
            {
                StarType starType = (StarType)StaticRandom.GetRandom(1, 6);
                int starBrightness = StaticRandom.GetRandom(1, 6);
                //StarType starType = StarType.Shiny;
                backgroundObjects.Add(new Star(
                    new Point(StaticRandom.GetRandom(1, SpaceSize.Width), StaticRandom.GetRandom(1, SpaceSize.Height)),
                    new Point(-StaticRandom.GetRandom(1, 20), 0),
                    10, 
                    starType, 
                    starBrightness, 
                    this, ObjectIdGenerator.GenerateId()));
            }

        }

        private void CreateAsteroids()
        {
            for (int i = 0; i < asteroidsAmount; i++)
            {
                CreateNewAsteroid();
            }
        }

        private void CreateNewAsteroid()
        {
            Asteroid astr = new Asteroid(
                    new Point(SpaceSize.Width/*rand.Next(10, size.Width-10)*/, StaticRandom.GetRandom(10, SpaceSize.Height - 10)),
                    new Point(-StaticRandom.GetRandom(1, 20), StaticRandom.GetRandom(20)),
                    20,
                    this, ObjectIdGenerator.GenerateId());
            astr.Collisionated += OnAsteroidCollision;
            foregroundObjects.Add(astr);
            Logger.Log(String.Format("[History] [ObjId:{0}] Объект {1} Position ({2}, {3}) создан.", astr.Id, astr.ToString(), astr.Position.X, astr.Position.Y));
        }

        internal void CreateLaserBeem()
        {
            ObjectInSpace[] bullets = foregroundObjects.Where(fo => fo is Bullet && !fo.Enabled).ToArray();
            if (bullets.Count() > 0)
            {
                foreach(Bullet bullet in bullets)
                {
                    bullet.MoveInSpace(new Point(0, PlayerSpaceShip.Position.Y));
                    bullet.Enabled = true;
                }
            }
            else
            {
                foregroundObjects.Add(new Bullet(PlayerSpaceShip.Position.Y, this, ObjectIdGenerator.GenerateId()));
            }
        }

        internal void ApplyScoreRecorderOnAsteroids(TDelegate<int> RecordScore)
        {
            foreach (ObjectInSpace foreObj in foregroundObjects)
            {
                if(foreObj is Asteroid astr && astr.recordScore == null)
                {
                    astr.recordScore = RecordScore;
                }
            }
        }

        private void CreateEnergyKit()
        {
            EnergyKit enKit = new EnergyKit(
                    new Point(StaticRandom.GetRandom(1, SpaceSize.Width), StaticRandom.GetRandom(1, SpaceSize.Height)),
                    new Point(-StaticRandom.GetRandom(1, 20), 0),
                    this, ObjectIdGenerator.GenerateId());
            foregroundObjects.Add(enKit);
        }

        private void CreateSpaceShip()
        {
            PlayerSpaceShip = new SpaceShip(
                    new Point(10, 400),
                    new Point(5, 5),
                    new Size(20, 10),
                    this, ObjectIdGenerator.GenerateId());
            PlayerSpaceShip.Destroyed += OnPlayerSpaceShipDestroyed;
            foregroundObjects.Add(PlayerSpaceShip);
        }

        private void OnAsteroidCollision(object sender, ObjectInSpaceEventArgs e)
        {
            Logger.Log(String.Format("Астероид {0} столкнулся c объектом {1}", (sender as ObjectInSpace).Id, (e.CollisionObject as ObjectInSpace).Id));
        }

        private void OnPlayerSpaceShipDestroyed(object sender, EventArgs e)
        {
            Game.GameOver = true;
        }

        public void MoveSpaceObjects()
        {
            MoveBackgroundObjects();
            MoveForegroundObjects();
            FindCollisionsDoCollisionConsequences();
        }

        public void ResetAsteroids()
        {
            ObjectInSpace[] astrds = foregroundObjects.Where(fo => fo is Asteroid).ToArray();
            foreach(Asteroid astrd in astrds)
            {
                astrd.MoveInSpace(new Point(SpaceSize.Width, StaticRandom.GetRandom(10, SpaceSize.Height - 10)));
                astrd.Enabled = true;
            }
        }

        public void IncreaseAsteroidsAmount(int amount)
        {
            if (amount > 0)
            {
                asteroidsAmount++;
                CreateNewAsteroid();
            }
        }

        public ObjectInSpace[] GetSpaceObjects()
        {
            List<ObjectInSpace> objsInSpace = new List<ObjectInSpace>();
            //ObjectInSpace[] objsInSpace = new ObjectInSpace[backgroundObjects.Count + foregroundObjects.Count];
            //int objsCounter = 0;
            //for(int i = 0; i < backgroundObjects.Count; i++)
            //{
            //    objsInSpace[objsCounter] = backgroundObjects[i];
            //    objsCounter++;
            //}

            //for (int i = 0; i < foregroundObjects.Count; i++)
            //{
            //    objsInSpace[objsCounter] = foregroundObjects[i];
            //    objsCounter++;
            //}

            objsInSpace.AddRange(GetBackgroundObjects());
            objsInSpace.AddRange(GetForegroundObjects());        

            return objsInSpace.ToArray();
        }

        public ObjectInSpace[] GetBackgroundObjects()
        {
            ObjectInSpace[] objsInSpace = new ObjectInSpace[backgroundObjects.Count];
            for (int i = 0; i < backgroundObjects.Count; i++)
            {
                objsInSpace[i] = backgroundObjects[i];
            }

            return objsInSpace;
        }

        public ObjectInSpace[] GetForegroundObjects()
        {
            ObjectInSpace[] objsInSpace = new ObjectInSpace[foregroundObjects.Count];
            for (int i = 0; i < foregroundObjects.Count; i++)
            {
                objsInSpace[i] = foregroundObjects[i];
            }

            return objsInSpace;
        }

        private void MoveBackgroundObjects()
        {
            foreach (var backgroundObject in backgroundObjects)
                backgroundObject.MoveInSpace();
        }

        private void MoveForegroundObjects()
        {
            foreach (var foregroundObject in foregroundObjects.Where(fo => fo.Enabled).ToArray())
            {
                foregroundObject.MoveInSpace();
            }
        }

        private void FindCollisionsDoCollisionConsequences()
        {
            List<ICollisionable> ListOfCollisionableObjects = GetCollisionableObjects();
            foreach (var foregroundObject in foregroundObjects.Where(fo => fo.Enabled))
            {
                if (ListOfCollisionableObjects.Count > 0)
                {
                    if (foregroundObject is ICollisionable)
                    {
                        ICollisionable collisionableForegroundObject = foregroundObject;
                        ListOfCollisionableObjects.Remove(collisionableForegroundObject);
                        DoCollisions(collisionableForegroundObject, ListOfCollisionableObjects);
                    }
                }
            }
        }

        private List<ICollisionable> GetCollisionableObjects()
        {
            List<ICollisionable> ListOfCollisionableObjects = new List<ICollisionable>();
            foreach (var foregroundObject in foregroundObjects.Where(fo=>fo.Enabled))
            {
                if(foregroundObject is ICollisionable)
                {
                    ListOfCollisionableObjects.Add(foregroundObject);
                }
            }
            return ListOfCollisionableObjects;
        }

        private void DoCollisions(ICollisionable foregroundObject, List<ICollisionable> ListOfObjects)
        {
            foreach (ICollisionable anotherForegroundObject in ListOfObjects)
            {
                if (anotherForegroundObject.CheckCollision(foregroundObject))
                {
                    ICollisionable temp;
                    if(foregroundObject is Asteroid)
                    {
                        temp = new Asteroid(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size.Width, this, foregroundObject.Id);
                    }
                    else if(foregroundObject is SpaceShip)
                    {
                        temp = new SpaceShip(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size, this, foregroundObject.Id);
                    }
                    else if (foregroundObject is EnergyKit)
                    {
                        temp = new EnergyKit(foregroundObject.Rect.Location, foregroundObject.Direction, this, foregroundObject.Id);
                    }
                    else
                    {
                        temp = new ObjectInSpace(foregroundObject.Rect.Location, foregroundObject.Direction, foregroundObject.Rect.Size, this, foregroundObject.Id);
                    }
                    foregroundObject.DoCollisionConsequences(anotherForegroundObject);
                    anotherForegroundObject.DoCollisionConsequences(temp);
                }
            }
        }

    }
}
