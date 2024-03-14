using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class Barricade
    {
        //sprites de la barricade lorsque'elle est pas touchée et lorsqu'elle s'est fait toucher une fois
        private const char _fullLifeSprite = '█';
        private const char _hitSprite = '▙';

        //vie de la barricade
        private int _life = 2;
        public int Life { get { return _life; } set { _life = value; } }

        //position de la barricade
        private int _x;
        private int _y;

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
        /// dessine la barricade pleine de vie
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_fullLifeSprite);
        }

        /// <summary>
        /// Dessine la barricade endomagée
        /// </summary>
        public void DrawDamaged()
        {
            Console.SetCursorPosition(_x, _y);
            Console.Write(_hitSprite);
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
