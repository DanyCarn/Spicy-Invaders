﻿        /// ETML
        /// Auteur : Dany Carneiro
        /// Description : Jeu space invaders sur la console
        /// Date : 18.01.2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace spicy_invaders
{
    internal class main
    {
        static void Main(string[] args)
        {
            //largeur de la fenêtre
            const int WINDOWWIDTH = 100;

            //hauteur de la fenêtre
            const int WINDOWHEIGHT = 35;

            //choix du joueur dans les menus
            ConsoleKey _choice;

            //indique si le joueur veut quitter
            bool _quit = false;

            //fixe la taille de la fenêtre
            Console.WindowWidth = WINDOWWIDTH;
            Console.WindowHeight = WINDOWHEIGHT;

            //efface le curseur
            Console.CursorVisible = false;

            //permet de faire son choix à l'ouverture du jeu
            do
            {
                //affiche le menu
                Menu();

                //le choix du joueur
                _choice = Console.ReadKey().Key;

                //change l'affichage pour afficher ce que le joueur a décidé de faire
                switch ((_choice))
                {
                    case ConsoleKey.D1:
                        Game();
                        break;

                    case ConsoleKey.D2:
                        Options();
                        break;

                    case ConsoleKey.D3:
                        Highscore();
                        break;

                    case ConsoleKey.D4:
                        About();
                        break;

                    case ConsoleKey.D5:
                        _quit = true;
                        break;

                    case ConsoleKey.Escape:
                        _quit = true;
                        break;

                    default:
                        break;
                }

            } while (_quit == false);

            ///méthode permettant d'afficher le menu
            void Menu()
            {
                //efface tout ce qui est actuellement a l'écran
                Console.Clear();

                //affiche le menu du jeu
                Console.WriteLine("**********************\n" +
                                  "    SPICY INVADERS    \n" +
                                  "**********************\n");

                Console.WriteLine("(1) Jouer");
                Console.WriteLine("(2) Options");
                Console.WriteLine("(3) Highscore");
                Console.WriteLine("(4) A propos");
                Console.WriteLine("(5) Quitter");
            }

            ///méthode permettant de lancer le jeu
            void Game()
            {
                //efface le curseur
                Console.CursorVisible = false;

                //indique le déplacement que le joueur veut faire pendant le jeu (gauche ou droite)
                ConsoleKey _move;

                //instancie le vaisseau
                SpaceShip ship = new SpaceShip();

                //instancie l'objet missile
                Missile missile = new Missile();

                //nettoie la console et affiche le vaisseau
                Console.Clear();
                ship.DrawShip();

                do
                {
                    _move = Console.ReadKey().Key;

                    //à chaque fois que le joueur appuie sur gauche ou droite, bouge le vaisseau dans la direction voulue
                    switch (_move)
                    {
                        //déplacement à droite
                        case ConsoleKey.RightArrow:

                            ship.MoveRight();
                            break;

                        //déplacement à gauche
                        case ConsoleKey.LeftArrow:

                            ship.MoveLeft();
                            break;

                        //tire le missile
                        case ConsoleKey.Spacebar:

                            missile.FireMissile(shipX: ship.PositionX + 1, shipY: ship.PositionY - 1);
                            break;

                        default:
                            break;
                    }

                    //efface uniquement le vaisseau pouis le redessine au bon endroit
                    ship.ClearShip();
                    ship.DrawShip();

                } while (_move != ConsoleKey.Escape);
            }

            ///page d'options
            void Options()
            {
                Console.Clear();
                Console.WriteLine("**********************\n" +
                                  "        OPTIONS       \n" +
                                  "**********************\n");

                Console.WriteLine("(1) Son on/off");
                Console.WriteLine("(2) Difficulté");
                Console.WriteLine("(3) Retour");

                _choice = Console.ReadKey().Key;

                switch (_choice)
                {
                    case ConsoleKey.D1:
                        //TODO option pour le son
                        break;

                    case ConsoleKey.D2:
                        //TODO option pour la difficulté
                        break;

                    case ConsoleKey.D3:
                        Console.Clear();
                        break;

                }
            }

            void Highscore()
            {

            }

            ///affiche les règles du jeu
            void About()
            {
                Console.Clear();
                Console.WriteLine("En construction...");
                Console.ReadLine();
            }
        }
    }
}
