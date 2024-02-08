﻿///ETML
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

        //position du missile dans l'axe Y
        private int _missileY = 0;

        //ancienne position du missile
        private int _oldMissilePosition = 0;

        //indique si le missile est en vie ou pas
        private bool _isMissile = false;

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

                //incrémente la position Y du missile
                _missileY--;
            }

            if(_missileY == -1)
            {
                _isMissile = false;
            }
        }

        /// <summary>
        /// efface le missile
        /// </summary>
        public void ClearMissile()
        {
            Console.SetCursorPosition(_missileX, _oldMissilePosition);
            Console.WriteLine(" ");
        }

        /// <summary>
        /// tir du missile
        /// </summary>
        /// <param name="shipX">Position du vaisseau dans l'axe X lors du tir.</param>
        /// <param name="shipY">Position du vaisseau dans l'axe Y lors du tir.</param>
        public void FireMissile(int shipX, int shipY)
        {
            //donne l'emplacement du missile à sa création
            _missileX = shipX + 1;
            _missileY = shipY - 1;

            //indique que le missile est en vie
            _isMissile=true;
        }

        public bool IsMissile
        {
            get { return _isMissile; }
            set { _isMissile = value; }
        }
    }
}
