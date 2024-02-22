///ETML
///Auteur : Dany Carneiro
///Date : 22.02.2024
///Description : Classe des ennemis de spicy invaders

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace spicy_invaders
{
    internal class Enemies
    {
        //sprite des ennemis 
        private const string ENEMY = "+-+";

        //la position de l'ennemi
        private int _positionX = 0;
        private int _positionY = 0;

        //vie de l'ennemi
        private int _enemyLife = 1;

        /// <summary>
        /// constructeur 
        /// </summary>
        public Enemies()
        {

        }

        /// <summary>
        /// constructeur de l'objet des ennemis
        /// </summary>
        /// <param name="positionX">position de l'ennemi sur l'axe x</param>
        /// <param name="positionY">position de l'ennemi sur l'axe y</param>
        public Enemies(int positionX, int positionY)
        {
            _positionX = positionX;
            _positionY = positionY;
        }

        /// <summary>
        /// dessine les vaisseaux ennemis
        /// </summary>
        public void drawEnemy()
        {
            Console.SetCursorPosition(_positionX, _positionY);
            Console.WriteLine(ENEMY);
        }

        public int EnemyX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        public int EnemyY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }
    }
}
