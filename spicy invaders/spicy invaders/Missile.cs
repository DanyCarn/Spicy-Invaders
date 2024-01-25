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
            for(int i = Console.WindowHeight; i > 0; i--)
            {
                ClearMissile();
                Console.SetCursorPosition(_missileX, _missileY);
                Console.WriteLine(MISSILE);

                //garde la position du vaisseau pour l'effacer par la suite
                _oldMissilePosition = _missileY;

                //incrémente la position Y du missile
                _missileY--;

                //
                Thread.Sleep(100);
            }

            //lorsque le missile arrive tout en haut de la page, il disparaît
            ClearMissile();
        }

        private void ClearMissile()
        {
            Console.SetCursorPosition(_missileX, _oldMissilePosition);
            Console.WriteLine(" ");
        }

        //permet d'accéder à la position X du missile pendant le jeu
        public int MissileX
        {
            get { return _missileX; }
            set { _missileX = value; }  
        }

        //permet d'accéder à la position Y du missile pendant le jeu
        public int MissileY
        {
            get { return _missileY; }
            set { _missileY = value; }
        }
    }
}
