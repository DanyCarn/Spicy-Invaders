///ETML
///Auteur : Dany Carneiro
///Date : 25.01.2024
///Description : Classe concernant le missile tiré par le vaisseau
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace spicy_invaders
{
    internal class Missile
    {
        //sprite du missile
        private const string MISSILE = "|";

        //position du missile dans l'axe X
        private int _missileX = 0;
        public int MissileX { get { return _missileX; } set { _missileX = value; } }

        //position du missile dans l'axe Y
        private int _missileY = 0;
        public int MissileY { get { return _missileY; } set { _missileY = value; } }

        //ancienne position du missile
        private int _oldMissilePosition = 0;

        //indique si le missile est en vie ou pas
        private bool _isMissile = false;
        public bool IsMissile { get { return _isMissile; } set { _isMissile = value; } }

        //instancie le vaisseau
        SpaceShip ship = new SpaceShip();

        /// <summary>
        /// dessine le missile lorsqu'il est tiré
        /// </summary>
        public void DrawMissile()
        {
            if (_isMissile)
            {
                Console.SetCursorPosition(_missileX, _missileY);
                Console.WriteLine(MISSILE);
            }
        }

        /// <summary>
        /// actualise la position du missile
        /// </summary>
        public void UpdateMissile()
        {
            if (_isMissile)
            {

                //garde la position du vaisseau pour l'effacer par la suite
                _oldMissilePosition = _missileY;

                //décrémente la position Y du missile pour monter
                _missileY--;
            }

            //si le missile arrive en haut
            if(_missileY == 0)
            {
                ClearMissile();
                _isMissile = false;
            }
        }

        /// <summary>
        /// efface le missile
        /// </summary>
        public void ClearMissile()
        {
            if(_oldMissilePosition == 0 && _isMissile)
            {
                Console.SetCursorPosition(_missileX, ship.PositionY-1);
                Console.WriteLine(" ");
            }
            else if(_isMissile)
            {
            Console.SetCursorPosition(_missileX, _oldMissilePosition);
            Console.WriteLine(" ");

            }
        }

        /// <summary>
        /// tir du missile
        /// </summary>
        /// <param name="shipX">Position du vaisseau dans l'axe X lors du tir.</param>
        /// <param name="shipY">Position du vaisseau dans l'axe Y lors du tir.</param>
        public void FireMissile(int shipX, int shipY)
        {
            //donne l'emplacement du missile à sa création
            _missileX = shipX + 2;
            _missileY = shipY - 1;

            //indique qu'il y a un missile
            _isMissile = true;
        }
    }
}
