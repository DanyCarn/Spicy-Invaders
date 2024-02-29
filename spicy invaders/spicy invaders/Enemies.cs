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

        //taille des ennemis
        private const int ENEMY_SIZE = 3;

        //la position de l'ennemi
        private int _positionX = 0;
        private int _positionY = 0;

        //vie de l'ennemi
        private int _enemyLife = 1;

        //direction de l'ennemi
        private bool _goingLeft = false;
        private bool _goingRight = true;

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
        public void DrawEnemy()
        {
            if(_enemyLife > 0)
            {
                Console.SetCursorPosition(_positionX, _positionY);
                Console.WriteLine(ENEMY);
            }
            else
            {

            }
        }

        /// <summary>
        /// efface le sprite de l'ennemi
        /// </summary>
        public void ClearEnemy()
        {
            for(int i = 0; i < ENEMY_SIZE; i++)
            {
                Console.SetCursorPosition(_positionX + i, _positionY);
                Console.WriteLine(' ');
            }
        }

        public void UpdateEnemy()
        {
            //déplacement de l'ennemi si il se dirige à droite
            if (_goingRight)
            {
                //actualise la position de l'ennemi
                _positionX += 1;
            }

            //déplacement de l'ennemi si il se dirige à gauche
            else if (_goingLeft)
            {
                //actualise la position de l'ennemi
                _positionX -= 1;
            }
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

        public bool GoingLeft
        {
            get { return _goingLeft; }
            set { _goingLeft = value; }
        }

        public bool GoingRight
        {
            get { return _goingRight; }
            set { _goingRight = value; }
        }

        public int Life
        {
            get { return _enemyLife; }
            set { _enemyLife = value; }
        }
    }
}
