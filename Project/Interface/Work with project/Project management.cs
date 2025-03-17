using System;
using System.Collections.Generic;

namespace ProjectManagement
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Resource> Resources { get; set; }

        public Project(string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            Tasks = new List<Task>();
            Resources = new List<Resource>();
        }

        public void AddTask(Task task)
        {
            Tasks.Add(task);
        }

        public void AddResource(Resource resource)
        {
            Resources.Add(resource);
        }

        public void AssignResourceToTask(Resource resource, Task task)
        {
            if (Resources.Contains(resource) && Tasks.Contains(task))
            {
                task.AssignedResources.Add(resource);
            }
        }

        public override string ToString()
        {
            return $"{Id}: {Name} ({StartDate} - {EndDate})";
        }
    }

    public class Task
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Resource> AssignedResources { get; set; }

        public Task(string name, string description, DateTime startDate, DateTime endDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            AssignedResources = new List<Resource>();
        }

        public override string ToString()
        {
            return $"{Id}: {Name} ({StartDate} - {EndDate})";
        }
    }

    public class Resource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public Resource(string name, string role)
        {
            Id = Guid.NewGuid();
            Name = name;
            Role = role;
        }

        public override string ToString()
        {
            return $"{Id}: {Name} ({Role})";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Создаём проект
            Project project = new Project(
                "Пример проекта",
                "Описание проекта",
                DateTime.Today,
                DateTime.Today.AddDays(30));

            // Создаём задачи
            Task task1 = new Task(
                "Задача 1",
                "Описание задачи 1",
                DateTime.Today,
                DateTime.Today.AddDays(10));

            Task task2 = new Task(
                "Задача 2",
                "Описание задачи 2",
                DateTime.Today.AddDays(11),
                DateTime.Today.AddDays(20));

            // Создаём ресурсы
            Resource resource1 = new Resource("Иван Иванов", "Разработчик");
            Resource resource2 = new Resource("Анна Петрова", "Тестировщик");

            // Добавляем задачи и ресурсы в проект
            project.AddTask(task1);
            project.AddTask(task2);
            project.AddResource(resource1);
            project.AddResource(resource2);

            // Назначаем ресурсы на задачи
            project.AssignResourceToTask(resource1, task1);
            project.AssignResourceToTask(resource2, task2);

            // Выводим информацию о проекте
            Console.WriteLine(project.ToString());
            foreach (var task in project.Tasks)
            {
                Console.WriteLine($"\t{task.ToString()}");
                foreach (var resource in task.AssignedResources)
                {
                    Console.WriteLine($"\t\t{resource.ToString()}");
                }
            }
        }
    }
}