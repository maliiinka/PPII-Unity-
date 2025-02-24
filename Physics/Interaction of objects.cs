using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;

namespace GameExample.Physics
{
    public class PhysicsEngine
    {
        // Объект физического мира
        private World _world;

        // Размер шага симуляции
        private const float TimeStep = 1f / 60f;

        // Конструктор класса
        public PhysicsEngine()
        {
            // Инициализируем физический мир
            _world = new World();
            _world.Gravity = new JVector(0, -9.81f, 0);
        }

        // Добавление объекта в физический мир
        public RigidBody AddRigidBody(JShape shape, float mass, JVector position)
        {
            // Создаем тело
            var body = new RigidBody(shape, mass);
            body.Position = position;
            _world.AddBody(body);
            return body;
        }

        // Обновление состояния физического мира
        public void UpdatePhysics()
        {
            _world.Step(TimeStep);
        }

        // Удаление тела из физического мира
        public void RemoveRigidBody(RigidBody body)
        {
            _world.RemoveBody(body);
        }

        // Освобождение ресурсов
        public void Dispose()
        {
            _world.Clear();
        }
    }
}