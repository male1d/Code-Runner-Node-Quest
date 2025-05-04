using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class Circuit // Класс Circuit представляет собой схему или цепь из узлов (например, логическую схему или электрическую цепь)
    {
        public List<Node> nodes = new(); // Список узлов, входных точек схемы
        public List<Node> outputs = new(); // Список выходных узлов схемы

        public Circuit() { }  // Конструктор класса, по умолчанию ничего не делает
    }
}
