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
    internal class Squid : Enemies
    {
        #region const
        //sprite des ennemis 
        private const string ENEMY = "+-+";

        //taille des ennemis
        private const int ENEMY_SIZE = 3;
        #endregion

        #region variables
        //la position de l'ennemi
        private int _positionX = 0;
        public int EnemyX
        {
            get { return _positionX; }
            set { _positionX = value; }
        }

        private int _positionY = 0;
        public int EnemyY
        {
            get { return _positionY; }
            set { _positionY = value; }
        }

        //vie de l'ennemi
        private int _enemyLife = 1;
        public int Life { get { return _enemyLife; } set { _enemyLife = value; } }        
            
    
        //direction de l'ennemi
        private bool _goingLeft = false;
        public bool GoingLeft { get { return _goingLeft; } set { _goingLeft = value; } } 
  
        private bool _goingRight = true;
        public bool GoingRight { get { return _goingRight; } set { _goingRight = value; } }
        #endregion


        /// <summary>
        /// constructeur de l'objet des ennemis
        /// </summary>
        /// <param name="positionX">position de l'ennemi sur l'axe x</param>
        /// <param name="positionY">position de l'ennemi sur l'axe y</param>
        public Squid(int positionX, int positionY)
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
        }

        /// <summary>
        /// efface le sprite de l'ennemi
        /// </summary>
        public void ClearEnemy()
        {
            if(_positionX >= 0 && _positionX <= Console.WindowWidth - 3)
            {
                for(int i = 0; i < ENEMY_SIZE; i++)
                {
                    Console.SetCursorPosition(_positionX + i, _positionY);
                    Console.WriteLine(' ');
                }
            }
        }

        /// <summary>
        /// met à jour la position de l'ennemi
        /// </summary>
        public void UpdateEnemy()
        {
            //déplacement de l'ennemi si il se dirige à droite
            if (_goingRight)
            {
                _positionX += 1;
            }

            //déplacement de l'ennemi si il se dirige à gauche
            else if (_goingLeft)
            {
                _positionX -= 1;
            }
        }

    }
}
