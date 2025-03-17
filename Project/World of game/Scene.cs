using System.Collections.Generic;

namespace GameExample
{
    public class Scene
    {
        // Название сцены
        public string Name { get; set; }
        
        // Список объектов на сцене
        public List<GameObject> Objects { get; set; }
        
        // Список активных игроков на сцене
        public List<Player> Players { get; set; }
        
        // События, происходящие на сцене
        public Dictionary<string, Event> Events { get; set; }
        
        // Конструктор класса
        public Scene(string name)
        {
            Name = name;
            Objects = new List<GameObject>();
            Players = new List<Player>();
            Events = new Dictionary<string, Event>();
        }
        
        // Метод добавления объекта на сцену
        public void AddObject(GameObject obj)
        {
            Objects.Add(obj);
        }
        
        // Метод удаления объекта со сцены
        public void RemoveObject(GameObject obj)
        {
            Objects.Remove(obj);
        }
        
        // Метод добавления игрока на сцену
        public void AddPlayer(Player player)
        {
            Players.Add(player);
        }
        
        // Метод удаления игрока со сцены
        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
        
        // Метод запуска событий на сцене
        public void TriggerEvent(string eventName)
        {
            if (Events.ContainsKey(eventName))
            {
                Events[eventName].Execute();
            }
        }
        
        // Внутренние классы для поддержки сцены
        public class GameObject
        {
            public string Name { get; set; }
            public Vector Position { get; set; }
            
            public GameObject(string name, Vector position)
            {
                Name = name;
                Position = position;
            }
        }
        
        public struct Vector
        {
            public float X { get; set; }
            public float Y { get; set; }
            
            public Vector(float x, float y)
            {
                X = x;
                Y = y;
            }
        }
        
        public class Event
        {
            public string Description { get; set; }
            public Action ExecuteAction { get; set; }
            
            public Event(string description, Action action)
            {
                Description = description;
                ExecuteAction = action;
            }
            
            public void Execute()
            {
                ExecuteAction?.Invoke();
            }
        }
    }
}