using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class SpicyInvaders
    {
        /// <summary>
        /// ETML
        /// Auteur : Dany Carneiro
        /// Description : Jeu space invaders sur la console
        /// Date : 18.01.2024
        /// </summary>
        static void Main(string[] args)
        {
            ConsoleKey _choice;
            bool _quit = false;

            //TODO mettre chaque truc dans une méthode avec des return pour utiliser les choix pour le switch

            do
            {
                //affiche le menu du jeu
                Console.WriteLine("**********************\n" +
                                  "    SPICY INVADERS    \n" +
                                  "**********************\n");

                Console.WriteLine("(1) Jouer");
                Console.WriteLine("(2) Options");
                Console.WriteLine("(3) Highscore");
                Console.WriteLine("(4) A propos");
                Console.WriteLine("(5) Quitter");

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
                }

            } while (_quit == false);

            void Game()
            {

            }

            ///Page d'options 
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

            void About()
            {

            }
        }
    }
}
