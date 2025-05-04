using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Game.Models;

namespace Game.Utils
{
    public class Hint // Класс Hint представляет подсказку в игре
    {
        public Vector pos; // Позиция подсказки в игровом пространстве
        public Vector target; // Целевая точка, к которой указывает подсказка
        public string message; // Сообщение или описание, связанное с подсказкой
        public const int radius = Game.size * 4; // Радиус действия подсказки, зависит от размера блока

        public Hint(Vector pos, Vector target, string message) // Конструктор для инициализации подсказки с позицией, целью и сообщением
        {
            this.pos = pos; // Установка позиции
            this.target = target; // Установка целевой точки
            this.message = message; // Установка сообщения
        }
    }
}
