    using System;
    using System.Collections.Generic;
    using System.Drawing.Text;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Diagnostics;
    using Game.Models;

    namespace Game.UI
    {
        public class PauseMenu
        {
            public PrivateFontCollection pfc = new(); // Коллекция шрифтов
            public FontFamily ff;
            public Font font;
            public Font big;
            public static Color defaultColor = Color.White; // Цвет по умолчанию для текста и линий
            public SolidBrush brush = new(defaultColor);
            public Pen pen = new(defaultColor, 2);
            public Vector scroll = new(); // Вектор для скроллинга меню
            public Vector vel = new();
            public Dictionary<string, string> movement = new() // Карта управления движением
            {
                { "W", "Прыжок" },
                { "A", "Влево" },
                { "D", "Вправо" },
            };
            public Dictionary<string, string> toggles = new()  // Карта переключателей
            {
                { "G", "Графика" },
                { "M", "Управление мышкой" },
            };

            private const int spacing = 4;  // Расстояние между элементами

            public PauseMenu()
            {
                pfc.AddFontFile("../../../font/font.ttf");  // Загружаем шрифт из файла
                ff = new("Pixel Operator 8", pfc); // Создаем семейство шрифтов
                font = new(ff, 16, FontStyle.Regular, GraphicsUnit.Pixel);
                big = new(ff, 32, FontStyle.Regular, GraphicsUnit.Pixel);
                pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            }

            public void DrawTimer(Graphics g, Stopwatch stopwatch)  // Метод для отображения таймера на экране
            {
                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Far,
                    LineAlignment = StringAlignment.Near
                };
                g.DrawString($"{stopwatch.ElapsedMilliseconds} ms", big, brush, Game.view.x, font.Size / 2, stringFormat);  // Отрисовка времени в миллисекундах в верхнем правом углу
            }

            public void Show(Graphics g)  // Метод для отображения всего меню
            {
                StringFormat stringFormat = new()
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("[ESC] Продолжить\n\n[Вверх] [Вниз] Скролл", font, brush, Game.view.x / 2, Game.view.y / 2, stringFormat); // Основной текст меню по центру экрана
                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;

                int x = (int)((int)(Game.view.x / 8) - scroll.x); // Вычисляем координаты для блока "Ноды" с учетом скролла
                int y = (int)((int)(Game.view.y / 8) + spacing - scroll.y);

                g.DrawString("Ноды", font, brush, x, y);
                y += (int)font.Size + spacing * 2;

                bool a;
                bool b;
                foreach (var gate in Node.gates)
                {
                    a = false;
                    b = false;

                    brush.Color = gate.Value.color;

                    g.DrawString(gate.Key, font, brush, x, y, stringFormat); // Отрисовка входных линий и их логических значений
                    y += 16 + spacing;

                    brush.Color = Color.White;

                    DrawRow(g, x, ref y, new bool[3] { a, b, gate.Value.eval(a, b) }); 
                    a = true;

                    DrawRow(g, x, ref y, new bool[3] { a, b, gate.Value.eval(a, b) });
                    b = true;

                    DrawRow(g, x, ref y, new bool[3] { a, b, gate.Value.eval(a, b) });

                    y += spacing;
                }
                brush.Color = defaultColor;

                x = (int)((int)(Game.view.x * 6 / 8) - scroll.x);  // Координаты для блока "Движение"
                y = (int)((int)(Game.view.y / 8) + spacing - scroll.y);

                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;

                g.DrawString("Движение", font, brush, x, y, stringFormat);

                y += Game.size / 2 + spacing;

                foreach (var bind in movement)  // Отрисовка управлений (W, A, D)
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawRectangle(pen, x, y, Game.size / 2, Game.size / 2);
                    g.DrawString(bind.Key, font, brush, x + Game.size / 4, y + Game.size / 4, stringFormat);

                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawString(bind.Value, font, brush, x + Game.size / 2 + spacing, y + Game.size / 4, stringFormat);

                    y += Game.size / 2 + spacing;
                }

                y += Game.size / 2 + spacing;

                stringFormat.Alignment = StringAlignment.Near;
                stringFormat.LineAlignment = StringAlignment.Near;

                g.DrawString("Ноды", font, brush, x, y, stringFormat);

                y += Game.size / 2 + spacing;

                foreach (var bind in toggles)
                {
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawRectangle(pen, x, y, Game.size / 2, Game.size / 2);
                    g.DrawString(bind.Key, font, brush, x + Game.size / 4, y + Game.size / 4, stringFormat);

                    stringFormat.Alignment = StringAlignment.Near;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawString(bind.Value, font, brush, x + Game.size / 2 + spacing, y + Game.size / 4, stringFormat);

                    y += Game.size / 2 + spacing;
                }
            }

            public void DrawRow(Graphics g, int x, ref int y, bool[] bools)  // Метод для отрисовки строки логических элементов (например, AND, OR)
            {
                for (int i = 0; i < bools.Length; i++)
                {
                    bool b = bools[i];
                    if (i == bools.Length - 1)
                    {
                        int btm = y + Game.size / 2;
                        int mid = (y + btm) / 2;
                        g.DrawLine(pen, x, mid, x + Game.size / 2, mid);
                        g.DrawLine(pen, x + Game.size / 2, mid, x + Game.size / 4, y);
                        g.DrawLine(pen, x + Game.size / 2, mid, x + Game.size / 4, btm);
                        x += Game.size / 2 + spacing;
                    }
                    if (b)
                    {
                        g.FillRectangle(brush, x, y, Game.size / 2, Game.size / 2);
                    }
                    else
                    {
                        g.DrawRectangle(pen, x, y, Game.size / 2, Game.size / 2);
                    }
                    x += Game.size / 2 + spacing;
                }
                y += Game.size / 2 + spacing;
            }
        }
    }
