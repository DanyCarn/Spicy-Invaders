///ETML
///Auteur : Dany Carneiro
///Date : 25.01.2024
///Description : Classe concernant le vaisseau jouable par le joueur
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace spicy_invaders
{
    internal class SpaceShip
    {
        //sprite du vaisseau controlé par le joueur
        private const string SPACESHIP = "<^>";

        //la taille du vaisseau
        private const int SHIPSIZE = 3;

        //position de départ dans l'axe X pour le vaisseau
        private int _shipX = Console.WindowWidth / 2; 

        //position de départ dans l'axe Y pour le vaisseau
        private int _shipY = Console.WindowHeight - 2 ;

        //La position du vaisseau avant le déplacement
        private int _oldXPosition = 0;

        //indique si le joueur est en vie
        private bool _shipAlive = true;


        /// <summary>
        /// affiche le vaisseau
        /// </summary>
        public void DrawShip()
        {
            Console.SetCursorPosition(_shipX, _shipY);
            Console.WriteLine(SPACESHIP);
        }

        /// <summary>
        /// efface le vaisseau
        /// </summary>
        public void ClearShip()
        {
            //remplace les symboles du vaisseau pour les remplacer par du vide afin d'effacer uniquement le vaisseau et laisser le reste de la console
            for(int i = 0; i < SHIPSIZE; i++)
            {
                Console.SetCursorPosition(_oldXPosition + i, _shipY);
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// déplace le vaisseau à droite
        /// </summary>
        public void MoveRight()
        {
            //stocke la position du vaisseau avant le déplacement
            _oldXPosition = _shipX;

            //empêche le joueur d'aller plus loin que la taille de la page
            if (_shipX < Console.WindowWidth - 3)
            {
                _shipX++;
            }
        }

        /// <summary>
        /// déplace le vaisseau à gauche
        /// </summary>
        public void MoveLeft() 
        {
            //stocke la position du vaisseau avant le déplacement
            _oldXPosition = _shipX;

            //empêche le joueur d'aller plus loin que la taille de la page
            if (_shipX > 0)
            {
                _shipX--;
            }
        }
        
        /// <summary>
        /// permet d'accéder à la position du viasseau pendant le jeu
        /// </summary>
        public int PositionX
        {
            get { return _shipX; }
            set { _shipX = value; }
        }

        /// <summary>
        /// permet d'obtenir la position du vaisseau avant le déplacement
        /// </summary>
        public int OldPosition
        {
            get { return _oldXPosition; }
            set { _oldXPosition = value; }
        }

        /// <summary>
        /// position dans l'axe y du joueur
        /// </summary>
        public int PositionY
        {
            get { return _shipY; }
            set { _shipY = value; }
        }

        /// <summary>
        /// permet d'obtenir l'information si le joueur est en vie
        /// </summary>
        public bool ShipAlive
        {
            get { return _shipAlive; }
            set { _shipAlive = value; }
        }
    }
}
