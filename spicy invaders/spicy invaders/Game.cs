using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Input;

namespace spicy_invaders
{
    internal class Game
    {
        //nombre d'ennemis
        const int MAX_ENEMIES = 24;

        /// <summary>
        /// score de l'utilisateur
        /// </summary>
        private int _score = 0;

        //liste des listes d'ennemis
        private List<List<Squid>> _allEnemies = new List<List<Squid>>();

        //listes contenant les ennemis de chaque ligne
        private List <Squid> _enemiesRow1 = new List <Squid>();
        private List <Squid> _enemiesRow2 = new List <Squid>();
        private List <Squid> _enemiesRow3 = new List <Squid>();


        //liste des listes de barricades
        private List<List<Barricade>> _allBunkers = new List<List <Barricade>>();
        
        //listes contenant les barricades
        private List <Barricade> _bunker1 = new List<Barricade>();
        private List <Barricade> _bunker2 = new List<Barricade>();

        //instancie le vaisseau
        private SpaceShip _ship = new SpaceShip();

        //instancie l'objet missile
        private Missile _missile = new Missile();

        //fin de partie
        private bool _endGame = false;

        /// <summary>
        /// activité dans le jeu en direct
        /// </summary>
        public void GameMethod()
        {
            //efface le curseur
            Console.CursorVisible = false;


            _allEnemies.Add(_enemiesRow1);
            _allEnemies.Add(_enemiesRow2);
            _allEnemies.Add(_enemiesRow3);

            _allBunkers.Add(_bunker1);
            _allBunkers.Add(_bunker2);

            //intsancie les ennemis
            for (int i = 0; i < 24; i++)
            {
                if (i < 8)
                {
                    Squid enemy = new Squid(1 + (i * 4), 1);
                    _enemiesRow1.Add(enemy);
                }
                else if (i < 16)
                {
                    Squid enemy = new Squid(1 + ((i - 8) * 4), 2);
                    _enemiesRow2.Add(enemy);
                }
                else if (i < MAX_ENEMIES)
                {
                    Squid enemy = new Squid(1 + ((i - 16) * 4), 3);
                    _enemiesRow3.Add(enemy);
                }
            }

            //instancie les bunker
            for (int i = 0;i < _allBunkers.Count;i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if(j>15 && j<32)
                    {
                        _allBunkers[i].Add(new Barricade(j, Console.WindowHeight - 4 - i));
                    }
                    else if(j>47 && j < 64)
                    {
                        _allBunkers[i].Add(new Barricade(j, Console.WindowHeight - 4 - i));
                    }
                    else if(j>79 && j< 96)
                    {
                        _allBunkers[i].Add(new Barricade(j, Console.WindowHeight - 4 - i));
                    }
                }
            }

            //nettoie la console et affiche le vaisseau, les ennemis et les bunkers
            Console.Clear();
            DrawAll();
            DrawAllBunkers();

            do
            {
                Thread.Sleep(25);

                //affiche le score
                ShowScore(0,0);

                //actualise le missile
                _missile.UpdateMissile();

                //mouvement du joueur
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    _ship.MoveLeft();

                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        //si le joueur bouge et tire en même temps tire un missile sans arrêter la course
                        if (!_missile.IsMissile)
                        {
                            _missile.FireMissile(shipX: _ship.PositionX, shipY: _ship.PositionY);
                        }
                    }
                }

                else if (Keyboard.IsKeyDown(Key.Right))
                {
                    _ship.MoveRight();

                    //tir de missile en mouvement
                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        if (!_missile.IsMissile)
                        {
                            _missile.FireMissile(shipX: _ship.PositionX, shipY: _ship.PositionY);
                        }
                    }
                }

                //tir de missile à l'arrêt
                else if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (!_missile.IsMissile)
                    {
                        _missile.FireMissile(shipX: _ship.PositionX, shipY: _ship.PositionY);
                    }
                }


                CheckBunkerHit();
                CheckEnemyHit();
                EnemiesChangeDirection();

                ClearAll();

                //conditions de fin de jeu
                if(_enemiesRow1.Count == 0 && _enemiesRow2.Count == 0 && _enemiesRow3.Count == 0)
                {
                    _endGame = true;
                }

                else if((_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.First().EnemyY == _ship.PositionY - 1) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.First().EnemyY == _ship.PositionY - 1) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.First().EnemyY == _ship.PositionY - 1))
                {
                    _endGame = true;
                }

                if (!_endGame)
                {
                    UpdateAllEnemies();

                    DrawAll();
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                    Console.WriteLine("Game Over !");
                    ShowScore(Console.WindowWidth/2 - 5, Console.WindowHeight/2 + 1);
                    Console.ReadLine();
                }

            } while (!_endGame);

        }

        /// <summary>
        /// Efface tout ce qui est a l'écran.
        /// </summary>
        private void ClearAll()
        {
            _ship.ClearShip();
            _missile.ClearMissile();
            foreach(List<Squid> enemyList in _allEnemies)
            {
                foreach (Squid enemy in enemyList)
                {
                    enemy.ClearEnemy();
                }
            }
        }

        /// <summary>
        /// Dessine tout ce qu'il y a à l'écran
        /// </summary>
        private void DrawAll()
        {
            _ship.DrawShip();
            _missile.DrawMissile();
            foreach(List<Squid> enemyList in _allEnemies)
            {
                foreach (Squid enemy in enemyList)
                {
                    enemy.DrawEnemy();
                }
            }

        }

        /// <summary>
        /// vérifie si un ennemi a été touché
        /// </summary>
        private void CheckEnemyHit()
        {
            foreach(List<Squid> enemyList in _allEnemies)
            {
                foreach (Squid enemy in enemyList)
                {
                    if (enemy.EnemyY == _missile.MissileY && enemy.EnemyX == _missile.MissileX || enemy.EnemyY == _missile.MissileY && enemy.EnemyX + 1 == _missile.MissileX || enemy.EnemyY == _missile.MissileY && enemy.EnemyX + 2 == _missile.MissileX || enemy.EnemyY == _missile.MissileY && enemy.EnemyX + 3 == _missile.MissileX)
                    {
                        //enlève le missile
                        _missile.ClearMissile();
                        _missile.MissileX = _ship.PositionX;
                        _missile.MissileY = _ship.PositionY;
                        _missile.IsMissile = false;

                        //enlève l'ennemi touché
                        enemyList.Remove(enemy);
                        enemy.Life = 0;
                        enemy.ClearEnemy();

                        //ajoute des points au score
                        _score += 20;

                        break;
                    }
                }

            }
        }

        /// <summary>
        /// change la direction des ennemis lorsqu'ils arrivent au bord de l'écran
        /// </summary>
        private void EnemiesChangeDirection()
        {

            if ((_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.Last().EnemyX == Console.WindowWidth - 3) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.Last().EnemyX == Console.WindowWidth - 3) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.Last().EnemyX == Console.WindowWidth - 3))
            {
                foreach (List<Squid> enemyList in _allEnemies)
                {
                    foreach (Squid enemy in enemyList)
                    {
                        enemy.GoingRight = false;
                        enemy.GoingLeft = true;

                        enemy.ClearEnemy();
                        enemy.EnemyY++;
                    }
                }
            }

            else if ((_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.First().EnemyX == 1) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.First().EnemyX == 1) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.First().EnemyX == 1))
            {
                foreach(List<Squid> enemyList in _allEnemies)
                {
                    foreach (Squid enemy in enemyList)
                    {
                        enemy.GoingRight = true;
                        enemy.GoingLeft = false;

                        enemy.ClearEnemy();
                        enemy.EnemyY++;
                    }
                }
            }
        
        }

        /// <summary>
        /// met à jour tous les ennemis
        /// </summary>
        private void UpdateAllEnemies()
        {
            foreach(List<Squid> enemyList in _allEnemies)
            {
                foreach (Squid enemy in enemyList)
                {
                    enemy.UpdateEnemy();
                }
            }
        }

        /// <summary>
        /// Affiche le score du joueur
        /// </summary>
        /// <param name="posX">Position dans l'axe X</param>
        /// <param name="posY">Position dans l'axe Y</param>
        private void ShowScore(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine("SCORE : " + _score);
        }

        /// <summary>
        /// dessine tous les bunkers créés
        /// </summary>
        /// <param name="allBunkers">liste de liste contenant toutes les barricades</param>
        private void DrawAllBunkers()
        {
            foreach (List<Barricade> barricadeList in _allBunkers)
            {
                foreach (Barricade barricade in barricadeList)
                {
                    barricade.Draw();
                }
            }
        }

        private void CheckBunkerHit()
        {
            foreach (List<Barricade> barricadeList in _allBunkers)
            {
                foreach (Barricade barricade in barricadeList)
                {
                    if (barricade.X == _missile.MissileX && barricade.Y == _missile.MissileY)
                    {
                        //enlève le missile
                        _missile.ClearMissile();
                        _missile.MissileX = _ship.PositionX;
                        _missile.MissileY = _ship.PositionY;
                        _missile.IsMissile = false;

                        //enlève la barricade touchée
                        barricadeList.Remove(barricade);
                        barricade.Clear();

                        break;
                    }
                }

            }
        }
    }
}
