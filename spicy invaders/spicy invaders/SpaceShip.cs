///ETML
///Auteur : Dany Carneiro
///Date : 25.01.2024
///Description : Classe concernant le vaisseau jouable par le joueur
using System;

namespace spicy_invaders
{
    internal class SpaceShip : ShootingObject
    {
        /// <summary>
        /// sprite du vaisseau controlé par le joueur
        /// </summary>
        private const string SPRITE = "<^>";

        /// <summary>
        /// la taille du vaisseau
        /// </summary>
        private const int SHIPSIZE = 3;

        /// <summary>
        /// La position du vaisseau avant le déplacement
        /// </summary>
        public int OldPosition { get; set; } = 0;


        /// <summary>
        /// indique si le joueur est en vie
        /// </summary>
        public bool ShipAlive { get; set; } = true;


        public SpaceShip()
        {
            Life = 3;
            PositionX = Console.WindowWidth / 2;
            PositionY = Console.WindowHeight - 3;
        }


        /// <summary>
        /// affiche le vaisseau
        /// </summary>
        public void DrawShip()
        {
            Console.SetCursorPosition(PositionX, PositionY);
            Console.WriteLine(SPRITE);
        }

        /// <summary>
        /// efface le vaisseau
        /// </summary>
        public void ClearShip()
        {
            //remplace les symboles du vaisseau pour les remplacer par du vide afin d'effacer uniquement le vaisseau et laisser le reste de la console
            for(int i = 0; i < SHIPSIZE; i++)
            {
                Console.SetCursorPosition(OldPosition + i, PositionY);
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// déplace le vaisseau à droite
        /// </summary>
        public void MoveRight()
        {
            //stocke la position du vaisseau avant le déplacement
            OldPosition = PositionX;

            //empêche le joueur d'aller plus loin que la taille de la page
            if (PositionX < Console.WindowWidth - 4)
            {
                PositionX++;
            }
        }

        /// <summary>
        /// déplace le vaisseau à gauche
        /// </summary>
        public void MoveLeft() 
        {
            //stocke la position du vaisseau avant le déplacement
            OldPosition = PositionX;

            //empêche le joueur d'aller plus loin que la taille de la page
            if (PositionX > 0)
            {
                PositionX--;
            }
        }

        /// <summary>
        /// dessine les vies du joueur sur la console
        /// </summary>
        public void DrawLife()
        {
            Console.SetCursorPosition(3, Console.WindowHeight - 2);
            Console.WriteLine($"Vies : {Life}");
        }
    }
}
