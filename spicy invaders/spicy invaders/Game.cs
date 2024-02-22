using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace spicy_invaders
{
    internal class Game
    {
            //nombre d'ennemis
            const int MAX_ENEMIES = 24;

            //tableau contenant les ennemis
            Enemies[] enemies = new Enemies[MAX_ENEMIES];

            //instancie le vaisseau
            SpaceShip ship = new SpaceShip();

            //instancie l'objet missile
            Missile missile = new Missile();

        public void GameMethod()
        {
            //efface le curseur
            Console.CursorVisible = false;

            //intsancie les ennemis
            for (int i = 0; i < 24; i++)
            {
                if (i < 8)
                {
                    Enemies enemy = new Enemies(1 + (i * 4), 0);
                    enemies[i] = enemy;
                }
                else if (i < 16)
                {
                    Enemies enemy = new Enemies(1 + ((i - 8) * 4), 1);
                    enemies[i] = enemy;
                }
                else if (i < MAX_ENEMIES)
                {
                    Enemies enemy = new Enemies(1 + ((i - 16) * 4), 2);
                    enemies[i] = enemy;
                }
            }

            //nettoie la console et affiche le vaisseau et les ennemis
            Console.Clear();
            ship.DrawShip();
            foreach (Enemies enemy in enemies)
            {
                enemy.drawEnemy();
            }

            do
            {
                //actualise le missile
                missile.UpdateMissile();
                missile.ClearMissile();
                missile.DrawMissile();

                Thread.Sleep(25);

                //mouvement du joueur
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    ship.MoveLeft();

                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        if (!missile.IsMissile)
                        {
                            missile.FireMissile(shipX: ship.PositionX, shipY: ship.PositionY);
                        }
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Right))
                {
                    ship.MoveRight();

                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        if (!missile.IsMissile)
                        {
                            missile.FireMissile(shipX: ship.PositionX, shipY: ship.PositionY);
                        }
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (!missile.IsMissile)
                    {
                        missile.FireMissile(shipX: ship.PositionX, shipY: ship.PositionY);
                    }
                }

                foreach(Enemies enemy in enemies)
                {
                    if(enemy.EnemyY == missile.MissileY && enemy.EnemyX == missile.MissileX || enemy.EnemyY == missile.MissileY && enemy.EnemyX + 1 == missile.MissileX || enemy.EnemyY == missile.MissileY && enemy.EnemyX + 2 == missile.MissileX)
                    {
                        missile.IsMissile = false;
                    }
                }

                //efface ce qu'il y a à l'écran puis redessine
                ship.ClearShip();
                missile.ClearMissile();

                ship.DrawShip();
                missile.DrawMissile();

            } while (ship.ShipAlive);

        }
    }
}
