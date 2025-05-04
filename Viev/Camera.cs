using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Models;

namespace Game.Viev
{
    public class Camera // Класс Camera управляет отображением области игры на экране
    {
        public Vector pos = new(); // Текущая позиция камеры в игровом пространстве
        public int zoom = 1; // Уровень масштабирования (зум)

        public Camera() { } // Пустой конструктор
    }
}
