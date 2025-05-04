using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Node : ICloneable // Реализует интерфейс клонирования
    {
        public int[] children = Array.Empty<int>();  // Массив целых чисел, представляющий идентификаторы дочерних узлов
        public Vector pos; // Позиция узла в пространстве 
        public bool isActivated = false; // Флаг, указывающий, активен ли узел в текущий момент
        public string? gateName; // Название (например, "AND", "OR" и т.д.)

        public static Dictionary<string, (Color color, Func<bool, bool, bool> eval)> gates = new()  // Статический словарь, содержащий описание логических ворот с цветом и функциями eval
        {
            {
                "AND",
                (Color.Blue, (a, b) => a && b) // ИЛИ (AND), с синим цветом
            },
            {
                "OR",
                (Color.Red, (a, b) => a || b) // ИЛИ (OR), с красным цветом
            },
            {
                "XOR",
                (Color.Lime, (a, b) => a ^ b) // исключающее ИЛИ (XOR), с лаймовым цветом
            },
            {
                "NAND",
                (Color.Cyan, (a, b) => !(a && b))  // NAND — отрицание AND, циан
            },
            {
                "NOR",
                (Color.Magenta, (a, b) => !(a || b))  // NOR — отрицание OR, магента
            },
            {
                "XNOR",
                (Color.Yellow, (a, b) => !(a ^ b)) // XNOR — отрицание XOR, желтый
            },
        };
        public static int size = 32;  // Размер узла в пикселях (статический размер)
        public Vector dim = new(size, size);  // Размер узла (ширина и высота), основанный на size

        public Node(Vector pos, int[] children)  // Конструктор для создания узла с позицией и дочерними узлами
        {
            this.pos = pos; // Установка позиции
            this.children = children; // Установка дочерних узлов
        }

        public Node(Vector pos, bool isActivated)  // Конструктор для создания узла с позицией и состоянием активированности
        {
            this.pos = pos; // Установка позиции
            this.isActivated = isActivated; // Установка активного состояния
        }

        public Node(Vector pos, string gateName, int[] children) // Конструктор для создания узла с позицией, названием логической воротки и дочерними узлами
        {
            this.pos = pos;  // Установка позиции
            this.gateName = gateName; // Название 
            this.children = children; // Дочерние узлы
        }

        public object Clone()  // Реализация метода Clone из интерфейса ICloneable
        {
            return MemberwiseClone();  // Создает поверхностную копию объекта
        }
    }
}
