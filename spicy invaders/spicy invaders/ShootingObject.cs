///ETML
///Auteur : Dany Carneiro
///Date : 03.05.24
///Descripiton : classe réprésentant les objets qui tirent des missiles. Elle possède de la vie ainsi que la position de l'objet

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spicy_invaders
{
    internal class ShootingObject
    {

        public int PositionY {  get; set; }

        public int PositionX { get; set; }

        public int Life {  get; set; }
    }
}
