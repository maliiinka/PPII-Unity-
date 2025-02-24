using System;

namespace GameExample
{
    public class Player
    {
        // Поля класса
        private string _name;            // Имя игрока
        private int _healthPoints;       // Здоровье игрока
        private int _experiencePoints;   // Опыт игрока
        private int _level;              // Уровень игрока
        private DateTime _lastLogin;     // Время последнего входа в игру
        private Inventory _inventory;    // Инвентарь игрока

        // Конструктор класса
        public Player(string name)
        {
            _name = name;
            _healthPoints = 100;
            _experiencePoints = 0;
            _level = 1;
            _lastLogin = DateTime.Now;
            _inventory = new Inventory();
        }

        // Свойства для чтения и записи полей
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int HealthPoints
        {
            get { return _healthPoints; }
            set { _healthPoints = Math.Max(0, value); }  // Ограничиваем здоровье минимумом 0
        }

        public int ExperiencePoints
        {
            get { return _experiencePoints; }
            set { _experiencePoints = Math.Max(0, value); }  // Ограничиваем опыт минимумом 0
        }

        public int Level
        {
            get { return _level; }
            set { _level = Math.Max(1, value); }  // Ограничиваем уровень минимумом 1
        }

        public DateTime LastLogin
        {
            get { return _lastLogin; }
            set { _lastLogin = value; }
        }

        // Методы класса
        public void GainExperience(int amount)
        {
            _experiencePoints += amount;
            CheckLevelUp();  // Проверяем, достиг ли игрок нового уровня
        }

        private void CheckLevelUp()
        {
            if (_experiencePoints >= 100 * _level)  // Условие перехода на новый уровень
            {
                _level++;
                Console.WriteLine($"Поздравляем! Вы достигли уровня {_level}.");
            }
        }

        public void TakeDamage(int damage)
        {
            _healthPoints -= damage;
            if (_healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            Console.WriteLine($"{_name} погиб.");
            ResetHealth();
        }

        private void ResetHealth()
        {
            _healthPoints = 100;
        }

        public override string ToString()
        {
            return $"Имя: {_name}, Здоровье: {_healthPoints}, Уровень: {_level}";
        }

        // Внутренний класс инвентаря
        public class Inventory
        {
            private List<Item> _items;

            public Inventory()
            {
                _items = new List<Item>();
            }

            public void AddItem(Item item)
            {
                _items.Add(item);
            }

            public Item GetItemByName(string name)
            {
                return _items.Find(i => i.Name == name);
            }

            public void RemoveItem(Item item)
            {
                _items.Remove(item);
            }
        }

        // Пример внутреннего класса предмета
        public class Item
        {
            public string Name { get; set; }
            public int Value { get; set; }

            public Item(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }
    }
}
