using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Box
    {
        public Vector pos; // Вектор, указывающий позицию центра блока (например, в мировой системе координат)
        public Vector dim; // Вектор, указывающий размеры блока (ширина и высота)

        public Box(Vector pos, Vector dim) { this.pos = pos; this.dim = dim; } // Конструктор, инициализирующий позицию и размеры

        public bool IsIntersecting(Box box) // Метод для проверки пересечения с другим объектом типа Box
        {
            return pos.x + dim.x / 2 > box.pos.x - box.dim.x / 2 && pos.x - dim.x / 2 < box.pos.x + box.dim.x / 2 && pos.y + dim.y / 2 > box.pos.y - box.dim.y / 2 && pos.y - dim.y / 2 < box.pos.y + box.dim.y / 2;   // Проверка, пересекаются ли два прямоугольника по оси X и Y
        }

        public bool IsIntersecting(Vector pos, Vector dim) // Метод для проверки пересечения с произвольным положением и размерами
        {
            return this.pos.x + this.dim.x / 2 > pos.x - dim.x / 2 && this.pos.x - this.dim.x / 2 < pos.x + dim.x / 2 && this.pos.y + this.dim.y / 2 > pos.y - dim.y / 2 && this.pos.y - this.dim.y / 2 < pos.y + dim.y / 2;
        }
    }
}
