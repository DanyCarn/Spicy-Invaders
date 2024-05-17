///ETML
///Auteur : Dany Carneiro
///Date : 14.03.24
///Description : Classe des bunkers. Elle permet de déssiner les bunkers ainsi que de gérer leur collision

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class Bunker
    {
        /// <summary>
        /// sprite de la barricade
        /// </summary>
        private const char _fullLifeSprite = '█';

        /// <summary>
        /// position de la barricade
        /// </summary>
        private int _x;
        public int X { get { return _x; } set { _x = value; } }

        private int _y;
        public int Y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="x">position x de la barricade</param>
        /// <param name="y">position y de la barricade</param>
        public Bunker(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// dessine la barricade
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_fullLifeSprite);
        }

        /// <summary>
        /// efface la barricade
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(' ');
        }


        /// <summary>
        /// vérifie si un missile a touché un bunker
        /// </summary>
        /// <returns>retourne vrai pour indiquer que le bunker est détruit et pour empêcher le jeu de planter</returns>
        public bool CheckBunkerHit(List<Bunker> bunkers, List<Missile> missiles, SpaceShip ship)
        {
            foreach (Missile missile in missiles)
            {
                if (X == missile.MissileX && Y == missile.MissileY)
                {
                    //enlève le missile
                    missile.ClearMissile();

                    //déplace le missile si c'est celui du joueur
                    if (missile == missiles[0])
                    {
                        missile.MissileX = ship.PositionX;
                        missile.MissileY = ship.PositionY-1;
                    }

                    missile.IsMissile = false;

                    //enlève la barricade touchée
                    bunkers.Remove(this);
                    Clear();

                    return true;
                }
            }
            return false;
        }
    }
}
