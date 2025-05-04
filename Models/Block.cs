using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Block : Box
    {
        public bool isDangerous = false;  // Поле, указывающее, является ли блок опасным (например, ловушка или враг)
        public bool isEnd = false;  // Поле, указывающее, является ли блок финишной точкой или концом уровня
        public bool isSolid = true;  // Поле, определяющее, является ли блок твердым препятствием
        public Bitmap image;  // Поле для хранения изображения блока (например, текстуры или спрайта)

        public Block(Vector pos, Vector dim, string imagePath) : base(pos, dim)   // Конструктор, создающий блок с заданной позицией, размерами и изображением
        {
            image = new(imagePath);
        }

        public Block(Vector pos, Vector dim, string imagePath, bool isDangerous) : base(pos, dim) // Перегруженный конструктор, позволяющий также указать, является ли блок опасным
        {
            this.isDangerous = isDangerous;  // Устанавливаем значение isDangerous в зависимости от параметра
            image = new(imagePath);
        }
    }
}
