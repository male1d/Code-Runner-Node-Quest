using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Vector // Класс Vector представляет двумерный вектор с компонентами x и y
    {
        public float x = 0; // Координата по горизонтали
        public float y = 0;  // Координата по вертикали

        public Vector(float x, float y)  // Конструктор, инициализирующий вектор по компонентам x и y
        {
            this.x = x;
            this.y = y;
        }

        public Vector() { } // Пустой конструктор, создающий вектор по умолчанию (0,0)

        public float SquaredDistance()  // Метод для вычисления квадрата расстояния от начала до точки (x,y)
        {
            return x * x + y * y;  // Используется для сравнения расстояний без извлечения корня
        }

        public static Vector operator +(Vector a, Vector b) // Перегрузка оператора сложения векторов
        {
            return new Vector(a.x + b.x, a.y + b.y);
        }

        public static Vector operator -(Vector a, Vector b)  // Перегрузка оператора вычитания векторов
        {
            return new Vector(a.x - b.x, a.y - b.y);
        }

        public static Vector operator *(Vector a, Vector b) // Перегрузка оператора поэлементного умножения векторов
        {
            return new Vector(a.x * b.x, a.y * b.y);
        }

        public static Vector operator /(Vector a, Vector b)  // Перегрузка оператора поэлементного деления векторов
        {
            return new Vector(a.x / b.x, a.y / b.y);
        }

        public static Vector operator *(Vector a, float b) // Перегрузка умножения вектора на число (слева)
        {
            return new Vector(a.x * b, a.y * b);
        }

        public static Vector operator *(float a, Vector b)  // Перегрузка умножения числа на вектор (справа)
        {
            return new Vector(a * b.x, a * b.y);
        }

        public static Vector operator /(Vector a, float b) // Перегрузка деления вектора на число
        {
            return new Vector(a.x / b, a.y / b);
        }
    }
}
