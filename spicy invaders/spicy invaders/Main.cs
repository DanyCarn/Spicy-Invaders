/// ETML
/// Auteur : Dany Carneiro
/// Description : Jeu space invaders sur la console
/// Date : 18.01.2024
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace spicy_invaders
{
   
    internal class main
    {
        [STAThread]
        static void Main(string[] args)
        {
            //largeur de la fenêtre
            const int WINDOWWIDTH = 80;

            //hauteur de la fenêtre
            const int WINDOWHEIGHT = 24;

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

                Console.WriteLine("     (1) Jouer");
                Console.WriteLine("     (2) Options");
                Console.WriteLine("     (3) Highscore");
                Console.WriteLine("     (4) A propos");
                Console.WriteLine("     (5) Quitter");
            }

            ///méthode permettant de lancer le jeu
            void Game()
            {
                Game game = new Game();
                game.GameMethod();
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
