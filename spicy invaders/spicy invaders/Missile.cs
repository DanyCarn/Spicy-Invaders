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

        //position du missile dans l'axe Y
        private int _missileY = 0;

        //ancienne position du missile
        private int _oldMissilePosition = 0;



        /// <summary>
        /// dessine le missile lorsqu'il est tiré
        /// </summary>
        public void DrawMissile()
        {
            Console.SetCursorPosition(_missileX, _missileY);
            Console.WriteLine(MISSILE);
        }

        /// <summary>
        /// efface et redessine le missile lors de son avancement
        /// </summary>
        public void UpdateMissile()
        {
            //tant que le missile est dans le missile, continue à le mettre à jour
            for(int i = Console.WindowHeight - 2; i > 0; i--)
            {

                ClearMissile();
                Console.SetCursorPosition(_missileX, _missileY);
                Console.WriteLine(MISSILE);

                //garde la position du vaisseau pour l'effacer par la suite
                _oldMissilePosition = _missileY;

                //incrémente la position Y du missile
                _missileY--;

                //donne la vitesse au missile
                Thread.Sleep(25);
            }

            //lorsque le missile arrive tout en haut de la page, il disparaît
            ClearMissile();
        }

        /// <summary>
        /// efface le missile
        /// </summary>
        private void ClearMissile()
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
            _missileX = shipX;
            _missileY = shipY;
            //dessine le missile lorsqu'il est tiré, puis le met à jour
            DrawMissile();
            UpdateMissile();
        }

    }
}
