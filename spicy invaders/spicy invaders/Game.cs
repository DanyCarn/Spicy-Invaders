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

        //liste contenant les ennemis
        List <Enemies> enemies = new List <Enemies>();

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
                    enemies.Add(enemy);
                }
                else if (i < 16)
                {
                    Enemies enemy = new Enemies(1 + ((i - 8) * 4), 1);
                    enemies.Add(enemy);
                }
                else if (i < MAX_ENEMIES)
                {
                    Enemies enemy = new Enemies(1 + ((i - 16) * 4), 2);
                    enemies.Add(enemy);
                }
            }

            //nettoie la console et affiche le vaisseau et les ennemis
            Console.Clear();
            ship.DrawShip();
            foreach (Enemies enemy in enemies)
            {
                enemy.DrawEnemy();
            }

            do
            {
                //actualise le missile
                missile.UpdateMissile();

                Thread.Sleep(25);

                //mouvement du joueur
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    ship.MoveLeft();

                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        //si le joueur bouge et tire en même temps tire un missile sans arrêter la course
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

                //tir de missile
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
                        enemies.Remove(enemy);
                        enemy.Life = 0;
                        enemy.ClearEnemy();
                        break;
                    }
                }

                //efface ce qu'il y a à l'écran puis redessine
                ship.ClearShip();
                missile.ClearMissile();

                ship.DrawShip();
                missile.DrawMissile();

                //lorsque la ligne d'ennemis arrive à un bord de l'écran, change de direction de déplacement
                foreach(Enemies enemy in enemies)
                {
                    if(enemies.Last().EnemyX == Console.WindowWidth - 2)
                    {
                        foreach(Enemies mob in enemies)
                        {
                            mob.GoingRight = false;
                            mob.GoingLeft = true;

                            //mob.EnemyY += 1;
                        }
                    }
                    else if (enemies.First().EnemyX == 1)
                    {
                        foreach (Enemies mob in enemies)
                        {
                            mob.GoingRight = true;
                            mob.GoingLeft = false;

                            //mob.EnemyY += 1;
                        }
                    }

                    enemy.ClearEnemy();
                    enemy.UpdateEnemy();
                    enemy.DrawEnemy();
                }

            } while (ship.ShipAlive);

        }
    }
}
