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
        /// <summary>
        /// sprite du missile
        /// </summary>
        private const string MISSILE = "|";

        /// <summary>
        /// position du missile dans l'axe X
        /// </summary>
        private int _missileX = 0;
        public int MissileX { get { return _missileX; } set { _missileX = value; } }

        /// <summary>
        /// position du missile dans l'axe Y
        /// </summary>
        private int _missileY = 0;
        public int MissileY { get { return _missileY; } set { _missileY = value; } }

        /// <summary>
        /// ancienne position du missile
        /// </summary>
        private int _oldMissilePosition = 0;

        /// <summary>
        /// indique si le missile est en vie ou pas
        /// </summary>
        private bool _isMissile = false;
        public bool IsMissile { get { return _isMissile; } set { _isMissile = value; } }
        
        /// <summary>
        /// indique si le missile tiré a été tiré par un ennemi ou un allié
        /// </summary>
        public bool _isAlly;

        /// <summary>
        /// le vaisseau du joueur
        /// </summary>
        private SpaceShip _ship;

        /// <summary>
        /// l'ennemi qui tire
        /// </summary>
        private Enemy _enemy;

        /// <summary>
        /// constructeur du missile
        /// </summary>
        /// <param name="ship">le vaisseau du joueur</param>
        public Missile(SpaceShip ship)
        {
            _ship = ship;
            _isAlly = true;
        }

        public Missile(Enemy enemy)
        {
            _enemy = enemy;
            _isAlly = false;
        }


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

                if (_isAlly)
                {
                    //décrémente la position Y du missile pour monter
                    _missileY--;
                }
                else
                {
                    //incrémente la position Y du missile ennemi
                    _missileY++;
                }
            }

            //si le missile arrive en haut
            if(_missileY == 0 && _isAlly)
            {
                //efface le missile et le désactive
                ClearMissile();
                _isMissile = false;
            }
            else if(_missileY == 24)
            {
                //efface le missile et le désactive
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
                if (_isAlly)
                {
                    Console.SetCursorPosition(_missileX, _ship.PositionY-1);
                }
                else
                {
                    Console.SetCursorPosition(_missileX, _enemy.PositionY + 1);
                }
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
        /// <param name="posX">Position du vaisseau dans l'axe X lors du tir.</param>
        /// <param name="posY">Position du vaisseau dans l'axe Y lors du tir.</param>
        public void FireMissile(int posX, int posY)
        {
            //donne l'emplacement du missile à sa création
            _missileX = posX + 1;
            if (_isAlly)
            {
                _missileY = posY - 1;
            }
            else
            {
                _missileY = posY + 1;
            }

            //indique qu'il y a un missile
            _isMissile = true;
        }


        /// <summary>
        /// contrôle si le missile a touché le joueur
        /// </summary>
        /// <param name="target">la cible du missile</param>
        public bool CheckPlayerHit(ShootingObject target)
        {

            //si le missile touche le vaisseau, le vaisseau perd une vie et le missile se désactive
            if (target.PositionY == MissileY && target.PositionX == MissileX || target.PositionY == MissileY && target.PositionX + 1 == MissileX || target.PositionY == MissileY && target.PositionX + 2 == MissileX || target.PositionY == MissileY && target.PositionX + 3 == MissileX)
            {

                target.Life--;
                ClearMissile();
                if (_isAlly)
                {
                    MissileX = _ship.PositionX;
                    MissileY = _ship.PositionY-1;
                }

                _isMissile = false;
                return true;
            }
            else
            {
                return false;
            }
            

           
        }
    }

}
