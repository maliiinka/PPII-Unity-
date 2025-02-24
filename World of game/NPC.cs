using System;

namespace GameExample.Entities
{
    public class NPC : Entity
    {
        // Идентификатор NPC
        public Guid Id { get; set; }

        // Имя NPC
        public string Name { get; set; }

        // Текущее состояние NPC (например, idle, moving, attacking)
        public NPCState State { get; set; }

        // Цель NPC (может быть координатой или другим объектом)
        public object Target { get; set; }

        // Текущая позиция NPC
        public Vector2D Position { get; set; }

        // Скорость перемещения NPC
        public double Speed { get; set; }

        // Конструктор класса
        public NPC(string name, Vector2D position, double speed)
        {
            Id = Guid.NewGuid();
            Name = name;
            Position = position;
            Speed = speed;
            State = NPCState.Idle;
        }

        // Метод обновления состояния NPC
        public void Update(double deltaTime)
        {
            switch (State)
            {
                case NPCState.Moving:
                    Move(deltaTime);
                    break;
                case NPCState.Attacking:
                    Attack(Target as Entity);
                    break;
                default:
                    // Действия в состоянии покоя
                    break;
            }
        }

        // Метод перемещения NPC
        private void Move(double deltaTime)
        {
            if (Target is Vector2D targetPosition)
            {
                // Расчет направления и перемещения
                Vector2D direction = targetPosition - Position;
                direction.Normalize();
                Position += direction * Speed * deltaTime;

                // Проверка достижения цели
                if ((targetPosition - Position).MagnitudeSquared < 1e-6)
                {
                    State = NPCState.Idle;
                }
            }
        }

        // Метод атаки NPC
        private void Attack(Entity targetEntity)
        {
            // Логика атаки (например, уменьшение здоровья цели)
            targetEntity.Health -= Damage;
        }

        // Вспомогательные структуры и перечисления
        public struct Vector2D
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Vector2D(double x, double y)
            {
                X = x;
                Y = y;
            }

            public static Vector2D operator -(Vector2D a, Vector2D b)
            {
                return new Vector2D(a.X - b.X, a.Y - b.Y);
            }

            public void Normalize()
            {
                double magnitude = Magnitude;
                X /= magnitude;
                Y /= magnitude;
            }

            public double Magnitude
            {
                get { return Math.Sqrt(X * X + Y * Y); }
            }

            public double MagnitudeSquared
            {
                get { return X * X + Y * Y; }
            }
        }

        public enum NPCState
        {
            Idle,
            Moving,
            Attacking
        }
    }
}
