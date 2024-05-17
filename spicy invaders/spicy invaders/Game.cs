///ETML
///Auteur : Dany Carneiro
///Date : 22.02.24
///Description : Classe où le jeu se déroule

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Input;

namespace spicy_invaders
{
    internal class Game
    {
        /// <summary>
        /// nombre d'ennemis
        /// </summary>
        private const int MAX_ENEMIES = 24;

        /// <summary>
        /// chemin d'accès au fichier contenant les scores
        /// </summary>
        private const string PATH = @".\highscores.txt";

        /// <summary>
        /// score de l'utilisateur
        /// </summary>
        private int _score = 0;

        /// <summary>
        /// liste des listes d'ennemis
        /// </summary>
        private List<List<Enemy>> _allEnemies;

        /// <summary>
        /// listes contenant les ennemis de chaque ligne
        /// </summary>
        private List <Enemy> _enemiesRow1;
        private List <Enemy> _enemiesRow2;
        private List <Enemy> _enemiesRow3;


        /// <summary>
        /// liste des listes de barricades
        /// </summary>
        private List<Bunker> _allBunkers;
        

        /// <summary>
        /// liste des missiles
        /// </summary>
        private List<Missile> _missiles;
        public List <Missile> Missiles { get { return _missiles; } set { _missiles = value; } }

        /// <summary>
        /// vaisseau
        /// </summary>
        private SpaceShip _ship;

        /// <summary>
        /// missile du joueur
        /// </summary>
        private Missile _missile;

        /// <summary>
        /// classe permet d'écrire dans le fichier des scores le score final
        /// </summary>
        private FileManager _fileManager;

        /// <summary>
        /// fin de partie
        /// </summary>
        private bool _endGame = false;

        /// <summary>
        /// l'aléatoire utilisé pour déterminer si un ennemi va tirer
        /// </summary>
        private Random _random = new Random();

        /// <summary>
        /// l'index de l'ennemi qui tire
        /// </summary>
        private int _enemyToShoot;

        /// <summary>
        /// la probailité qu'un ennemi tire
        /// </summary>
        private int _shootRate = 350;

        /// <summary>
        /// nom que l'utilisateur choisi à la fin de la partie
        /// </summary>
        private string _username;

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="fileManager">le manager de fichiers qui permetra d'écrire le score dans le fichier</param>
        public Game(FileManager fileManager)
        {
            _ship = new SpaceShip();

            _missile = new Missile(_ship);

            _allBunkers = new List<Bunker>();

            _enemiesRow1 = new List<Enemy>();
            _enemiesRow2 = new List<Enemy>();
            _enemiesRow3 = new List<Enemy>();

            _allEnemies = new List<List<Enemy>>();

            //isntancie la liste de missiles et y ajoute le missile du joueur
            _missiles = new List<Missile>
            {
                _missile
            };

            _fileManager = fileManager;
        }

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


            DrawAllEnemies();

            //instancie les bunker
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {
                    if(j>15 && j<32)
                    {
                        _allBunkers.Add(new Bunker(j, Console.WindowHeight - 5 - i));
                    }
                    else if(j>47 && j < 64)
                    {
                        _allBunkers.Add(new Bunker(j, Console.WindowHeight - 5 - i));
                    }
                    else if(j>79 && j< 96)
                    {
                        _allBunkers.Add(new Bunker(j, Console.WindowHeight - 5 - i));
                    }
                }
            }

            //nettoie la console et affiche le vaisseau, les ennemis et les bunkers
            Console.Clear();
            DrawAll();
            DrawAllBunkers();
            _ship.DrawLife();

            do
            {
                Thread.Sleep(25);

                //affiche le score
                ShowScore(0,0);


                //actualise les missiles
                UpdateMissiles();

                //mouvement du joueur
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    _ship.MoveLeft();

                    if (Keyboard.IsKeyDown(Key.Space))
                    {
                        //si le joueur bouge et tire en même temps tire un missile sans arrêter la course
                        if (!_missile.IsMissile)
                        {
                            _missile.FireMissile(posX: _ship.PositionX, posY: _ship.PositionY);
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
                            _missile.FireMissile(posX: _ship.PositionX, posY: _ship.PositionY);
                        }
                    }
                }

                //tir de missile à l'arrêt
                else if (Keyboard.IsKeyDown(Key.Space))
                {
                    if (!_missile.IsMissile)
                    {
                        _missile.FireMissile(posX: _ship.PositionX, posY: _ship.PositionY);
                    }
                }

                //vérifie les collisions avec les bunkers
                foreach(Bunker b in _allBunkers)
                {
                    if(b.CheckBunkerHit(_allBunkers, _missiles, _ship))
                    {
                        break;
                    }
                }

                EnemiesChangeDirection();

                ClearAll();

                //conditions de fin de jeu
                //fin si le joueur n'a plus de vie ou que les ennemis sont arrivés à la hauteur du joueur
                if(_ship.Life == 0 || (_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.First().PositionY == _ship.PositionY - 1) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.First().PositionY == _ship.PositionY - 1) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.First().PositionY == _ship.PositionY - 1))
                {
                    _endGame = true;
                }

                //Si tous les ennemis ont été tués en crée d'autres
                if(!_endGame && _enemiesRow1.Count == 0 && _enemiesRow2.Count == 0 && _enemiesRow3.Count == 0)
                {
                    DrawAllEnemies();
                }

                if (!_endGame)
                {
                    UpdateAllEnemies();

                    DrawAll();

                    _ship.DrawLife();
                }
                else
                {
                    Console.Clear();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 5, Console.WindowHeight / 2);
                    Console.WriteLine("Game Over !");
                    ShowScore(Console.WindowWidth/2 - 5, Console.WindowHeight/2 + 1);
                    Console.ReadLine();
                    Console.Clear();

                    //enregistrement du score dans le fichier texte
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 10, Console.WindowHeight / 2);
                    Console.Write("Enter your name : ");
                    _username = Console.ReadLine();
                    

                }

            } while (!_endGame);

        }

        /// <summary>
        /// Efface tout ce qui est a l'écran.
        /// </summary>
        private void ClearAll()
        {
            _ship.ClearShip();
            foreach (Missile missile in _missiles)
            {
                missile.ClearMissile();
            }
            foreach (List<Enemy> enemyList in _allEnemies)
            {
                foreach (Enemy enemy in enemyList)
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
            foreach(Missile missile in _missiles)
            {
                missile.DrawMissile();
            }
            foreach(List<Enemy> enemyList in _allEnemies)
            {
                foreach (Enemy enemy in enemyList)
                {
                    enemy.DrawEnemy();
                }
            }

        }

        /// <summary>
        /// change la direction des ennemis lorsqu'ils arrivent au bord de l'écran
        /// </summary>
        private void EnemiesChangeDirection()
        {
            //Si le premier ou le dernier de chaque rangée atteint le bord droit du jeu, change la direction de leur déplacement
            if ((_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.Last().PositionX == Console.WindowWidth - 3) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.Last().PositionX == Console.WindowWidth - 3) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.Last().PositionX == Console.WindowWidth - 3))
            {
                foreach (List<Enemy> enemyList in _allEnemies)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        if(enemy.EnemySpeed == 1)
                        {
                            enemy.GoingRight = false;
                            enemy.GoingLeft = true;

                            enemy.ClearEnemy();
                            enemy.PositionY++;
                        }
                    }
                }
            }

            //Si le premier ou le dernier de chaque rangée atteint le bord gauche du jeu, change la direction de leur déplacement
            else if ((_enemiesRow1 != null && _enemiesRow1.Any() && _enemiesRow1.First().PositionX == 1) || (_enemiesRow2 != null && _enemiesRow2.Any() && _enemiesRow2.First().PositionX == 1) || (_enemiesRow3 != null && _enemiesRow3.Any() && _enemiesRow3.First().PositionX == 1))
            {
                foreach(List<Enemy> enemyList in _allEnemies)
                {
                    foreach (Enemy enemy in enemyList)
                    {
                        if(enemy.EnemySpeed == 1)
                        {
                            enemy.GoingRight = true;
                            enemy.GoingLeft = false;

                            enemy.ClearEnemy();
                            enemy.PositionY++;
                        }
                    }
                }
            }
        
        }

        /// <summary>
        /// met à jour tous les ennemis
        /// </summary>
        private void UpdateAllEnemies()
        {
            foreach(List<Enemy> enemyList in _allEnemies)
            {
                foreach (Enemy enemy in enemyList)
                {
                    _enemyToShoot = _random.Next(_shootRate);

                    if(_enemyToShoot < enemyList.Count && enemy == enemyList[_enemyToShoot])
                    {
                        enemy.UpdateEnemy(true);
                    }
                    else
                    {
                        enemy.UpdateEnemy(false);
                    }

                    //vérifie si l'ennemi s'est fait touché
                    if (_missile.CheckPlayerHit(enemy))
                    {
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
        /// Affiche le score du joueur
        /// </summary>
        /// <param name="posX">Position dans l'axe X</param>
        /// <param name="posY">Position dans l'axe Y</param>
        private void ShowScore(int posX, int posY)
        {
            Console.SetCursorPosition(posX, posY);
            Console.WriteLine($"SCORE : {_score}");
        }

        /// <summary>
        /// dessine tous les bunkers créés
        /// </summary>
        private void DrawAllBunkers()
        {
            foreach (Bunker barricade in _allBunkers)
            {
                barricade.Draw();
            }
        }

        /// <summary>
        /// met a jour tous les missiles
        /// </summary>
        private void UpdateMissiles()
        {
            foreach(Missile missile in _missiles)
            {
                missile.UpdateMissile();

                //si un missile touche le joueur l'enlève de la liste et met a jour l'affichage des vies
                if (missile.CheckPlayerHit(_ship))
                {
                    _missiles.Remove(missile);
                    break;
                }
            }
        }

        /// <summary>
        /// crée les trois rangées d'ennemis
        /// </summary>
        private void DrawAllEnemies()
        {
            //intsancie tous les ennemis et les place dans les listes
            for (int i = 0; i < 24; i++)
            {
                if (i < 8)
                {
                    Enemy enemy = new Enemy(1 + (i * 4), 1, this);
                    _enemiesRow1.Add(enemy);
                }
                else if (i < 16)
                {
                    Enemy enemy = new Enemy(1 + ((i - 8) * 4), 2, this);
                    _enemiesRow2.Add(enemy);
                }
                else if (i < MAX_ENEMIES)
                {
                    Enemy enemy = new Enemy(1 + ((i - 16) * 4), 3, this);
                    _enemiesRow3.Add(enemy);
                }
            }
        }
    }
}
