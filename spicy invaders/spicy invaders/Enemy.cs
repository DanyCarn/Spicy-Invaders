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
    internal class Enemy
    {
        /// <summary>
        /// sprite de l'ennemi
        /// </summary>
        private const string ENEMY = "+-+";

        /// <summary>
        /// taille de l'ennemi
        /// </summary>
        private const int ENEMY_SIZE = 3;


        /// <summary>
        /// position de l'ennemi dans l'axe x
        /// </summary>
        private int _positionX = 0;
        public int EnemyX { get { return _positionX; } set { _positionX = value; } }

        /// <summary>
        /// position dans l'axe y de l'ennemi
        /// </summary>
        private int _positionY = 0;
        public int EnemyY{ get { return _positionY; } set { _positionY = value; } }


        /// <summary>
        /// Vie de l'ennemi
        /// </summary>
        private int _enemyLife = 1;
        public int Life { get { return _enemyLife; } set { _enemyLife = value; } }        
            
    
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
        /// générateur d'aléatoire qui détermine si l'ennemi tire ou pas
        /// </summary>
        private Random _random;

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
            _positionX = positionX;
            _positionY = positionY;
            _enemySpeed = 0;
            _game = game;
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
            if(_enemySpeed == 1)
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

                _enemySpeed = 0;
            }
            else
            {
                _enemySpeed++;
            }

            Shoot();
        }

        public void Shoot()
        {
            _random = new Random();

            if(_random.Next(70) == 1)
            {
                _missile = new Missile(this);

                _game.Missiles.Add(_missile);

                _missile.FireMissile(_positionX, _positionY);
            }
        }

    }
}
