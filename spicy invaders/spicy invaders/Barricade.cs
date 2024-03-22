using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class Barricade
    {
        /// <summary>
        /// sprite de la barricade
        /// </summary>
        private const char _fullLifeSprite = '█';

        /// <summary>
        /// position de la barricade
        /// </summary>
        private int _x;
        public int X { get { return _x; } set { _x = value; } }

        private int _y;
        public int Y { get { return _y; } set { _y = value; } }

        /// <summary>
        /// constructeur
        /// </summary>
        /// <param name="x">position x de la barricade</param>
        /// <param name="y">position y de la barricade</param>
        public Barricade(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// dessine la barricade
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_fullLifeSprite);
        }

        /// <summary>
        /// efface la barricade
        /// </summary>
        public void Clear()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(' ');
        }
    }
}
