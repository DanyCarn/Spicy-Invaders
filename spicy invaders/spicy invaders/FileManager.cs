﻿///ETML
///Auteur : Dany Carneiro
///Date : 17.05.24
///Description : Classe permettant de gérer le fichier texte qui contient les scores

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class FileManager
    {
        /// <summary>
        /// chemin d'accès au fichier contenant les scores
        /// </summary>
        private const string PATH = @".\highscores.txt";

        /// <summary>
        /// outil permettant de lire le fichier
        /// </summary>
        private StreamReader _reader;

        /// <summary>
        /// outil permettant d'écrire dans le fichier
        /// </summary>
        private StreamWriter _writer;



        /// <summary>
        /// constructeur
        /// </summary>
        public FileManager()
        {

        }

        /// <summary>
        /// méthode permettant d'écrire le score dans le fichier
        /// </summary>
        /// <param name="name">le nom choisi par le joueur</param>
        /// <param name="score">le score du joueur</param>
        public void AddScore(string name, int score)
        {
            _writer = File.AppendText(PATH);

            _writer.WriteLine($"{name},{score}");

            _writer.Close();
        }

        /// <summary>
        /// permet d'écrire les 5 meilleurs scores sur la console
        /// </summary>
        public void WriteScores()
        {
            _reader = new StreamReader(PATH);

            for(int i = 0; i < 5; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 2 - 8, 5 + i);
                Console.WriteLine(_reader.ReadLine());
            }
        }

        private void SortScore()
        {

        }
    }
}
