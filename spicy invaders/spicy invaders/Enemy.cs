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
    internal class Enemy : ShootingObject
    {
        /// <summary>
        /// sprite de l'ennemi
        /// </summary>
        private const string SPRITE = "+-+";

        /// <summary>
        /// taille de l'ennemi
        /// </summary>
        private const int ENEMY_SIZE = 3;

        /// <summary>
        /// l'ennemi va à gauche
        /// </summary>
        private bool _goingLeft = false;
        public bool GoingLeft { get { return _goingLeft; } set { _goingLeft = value; } } 
  
        /// <summary>
        /// L'ennemi va à droite
        /// </summary>
        private bool _goingRight = true;
        public bool GoingRight { get { return _goingRight; } set { _goingRight = value; } }

        /// <summary>
        /// compteur pour savoir quand l'ennemi doit bouger
        /// </summary>
        private int _enemySpeed;
        public int EnemySpeed { get { return _enemySpeed; } }

        /// <summary>
        /// le jeu en cours
        /// </summary>
        private Game _game;

        /// <summary>
        /// le missile tiré par l'ennemi
        /// </summary>
        private Missile _missile;


        /// <summary>
        /// constructeur de l'objet des ennemis
        /// </summary>
        /// <param name="positionX">position de l'ennemi sur l'axe x</param>
        /// <param name="positionY">position de l'ennemi sur l'axe y</param>
        public Enemy(int positionX, int positionY, Game game)
        {
            PositionX = positionX;
            PositionY = positionY;
            _enemySpeed = 0;
            _game = game;
            Life = 1;
        }

        /// <summary>
        /// dessine les vaisseaux ennemis
        /// </summary>
        public void DrawEnemy()
        {
            if(Life > 0)
            {
                Console.SetCursorPosition(PositionX, PositionY);
                Console.WriteLine(SPRITE);
            }
        }

        /// <summary>
        /// efface le sprite de l'ennemi
        /// </summary>
        public void ClearEnemy()
        {
            if(PositionX >= 0 && PositionX <= Console.WindowWidth - 3)
            {
                for(int i = 0; i < ENEMY_SIZE; i++)
                {
                    Console.SetCursorPosition(PositionX + i, PositionY);
                    Console.WriteLine(' ');
                }
            }
        }

        /// <summary>
        /// met à jour la position de l'ennemi
        /// </summary>
        public void UpdateEnemy(bool isShooting)
        {
            if(_enemySpeed == 1)
            {
                //déplacement de l'ennemi si il se dirige à droite
                if (_goingRight)
                {
                    PositionX += 1;
                }

                //déplacement de l'ennemi si il se dirige à gauche
                else if (_goingLeft)
                {
                    PositionX -= 1;
                }

                _enemySpeed = 0;
            }
            else
            {
                _enemySpeed++;
            }

            if(isShooting)
            {
                Shoot();
            }
        }

        /// <summary>
        /// méthode permettant à l'ennemi de tirer
        /// </summary>
        public void Shoot()
        {
                _missile = new Missile(this);

                _game.Missiles.Add(_missile);

                _missile.FireMissile(PositionX, PositionY);
        }

    }
}
