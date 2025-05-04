using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Game.Models
{
    public enum EntityState  // Перечисление состояний 
    {
        Idle,
        Walk,
        Die,
        Attack,
    }

    public class Entity : Box  // Класс Entity — это игровой объект, который наследует свойства от Box
    {
        public Vector vel = new(0, 0); // Скорость перемещения по осям X и Y

        public bool isGrounded = false;  // Флаг, указывающий, находится ли объект на земле
        public bool isDying = false;  // Флаг, указывающий, умирает ли объект

        public float movePower;  // Мощность перемещения (скорость прыжка или бега)
        public float jumpHeight;  // Высота прыжка

        public string name; // Имя объекта (например, "player" или "enemy")

        public EntityState state = EntityState.Idle;   // Текущее состояние анимации или поведения
        public Dictionary<EntityState, Bitmap[]> stateFrames = new(); // Словарь, хранящий массивы кадров для каждого состояния
        public int frameIndex = 0; // Индекс текущего кадра анимации
        public bool isFacingLeft = false;  // Направление, лицом ли объект влево

        public Entity(Vector pos, Vector dim, float movePower, float jumpHeight, string name) : base(pos, dim) // Конструктор, инициализирующий позицию, размер, мощность прыжка, скорость и имя
        {
            this.movePower = movePower; // Установка скорости перемещения
            this.jumpHeight = jumpHeight; // Установка высоты прыжка
            this.name = name; // Установка имени
        }

        public void GetFrames()   // Метод для загрузки кадров анимации для каждого состояния
        {
            foreach (var stateValue in Enum.GetValues(typeof(EntityState)).Cast<EntityState>())  // Перебираем все возможные значения из перечисления EntityState
            {
                try
                {
                    List<Bitmap> frames = new(); // Временный список для кадров
                    foreach (var file in Directory.EnumerateFiles($"{Game.prefix}img/{name}/{stateValue.ToString().ToLower()}").OrderBy(file => int.Parse(Regex.Match(file, @"\d+").Value)))
                    {
                        Bitmap bitmap = new(file); // Загружаем изображение
                        frames.Add(bitmap); // Добавляем его в список кадров
                        Bitmap reflected = (Bitmap)bitmap.Clone(); // Создаем отраженную (зеркальную) копию изображения
                        reflected.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        frames.Add(reflected); // Добавляем отраженную копию для анимации "поворота"
                    }

                    stateFrames[stateValue] = frames.ToArray(); // Запоминаем все кадры для текущего состояния
                }
                catch (Exception e)
                {
                    // В случае ошибки — пропускаем загрузку для этого состояния
                }
            }
        }
    }
}
