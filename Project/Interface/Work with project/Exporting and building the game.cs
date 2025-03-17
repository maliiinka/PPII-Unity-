using System;
using System.IO;
using System.Diagnostics;

namespace Game.BuildSystem
{
    public class BuildManager
    {
        // Директория исходников игры
        private string SourceDirectory { get; set; }

        // Директория для выходных файлов
        private string OutputDirectory { get; set; }

        // Имя исполняемого файла
        private string ExecutableName { get; set; }

        // Конструктор модуля
        public BuildManager(string sourceDir, string outputDir, string exeName)
        {
            SourceDirectory = sourceDir;
            OutputDirectory = outputDir;
            ExecutableName = exeName;
        }

        // Сборка проекта
        public void BuildProject()
        {
            // Логируем начало сборки
            LogMessage("Начало сборки проекта...");

            // Компилируем проект
            CompileProject();

            // Копируем необходимые файлы в директорию вывода
            CopyRequiredFiles();

            // Упаковываем проект в архив
            ArchiveProject();

            // Логируем завершение сборки
            LogMessage("Сборка проекта завершена.");
        }

        // Компиляция проекта
        private void CompileProject()
        {
            // Логируем начало компиляции
            LogMessage("Компиляция проекта...");

            // Запускаем компилятор (например, MSBuild)
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = "msbuild.exe",
                Arguments = Path.Combine(SourceDirectory, "MyGame.sln") + " /p:Configuration=Release"
            };

            Process compilerProcess = new Process
            {
                StartInfo = processInfo
            };

            compilerProcess.Start();
            compilerProcess.WaitForExit();

            // Логируем завершение компиляции
            LogMessage("Компиляция проекта завершена.");
        }

        // Копирование необходимых файлов
        private void CopyRequiredFiles()
        {
            // Логируем копирование файлов
            LogMessage("Копирование необходимых файлов...");

            // Указываем путь к файлам, которые нужно скопировать
            string binFolder = Path.Combine(SourceDirectory, "bin", "Release");
            string[] filesToCopy = Directory.GetFiles(binFolder, "*.*", SearchOption.AllDirectories);

            // Копируем файлы в директорию вывода
            foreach (string file in filesToCopy)
            {
                string destinationPath = Path.Combine(OutputDirectory, Path.GetFileName(file));
                File.Copy(file, destinationPath, true);
            }

            // Логируем завершение копирования
            LogMessage("Копирование необходимых файлов завершено.");
        }

        // Архивация проекта
        private void ArchiveProject()
        {
            // Логируем архивацию проекта
            LogMessage("Архивация проекта...");

            // Используем библиотеку для упаковки проекта в zip-архив
            ZipFile.CreateFromDirectory(OutputDirectory, Path.Combine(OutputDirectory, "MyGame.zip"));

            // Логируем завершение архивации
            LogMessage("Архивация проекта завершена.");
        }

        // Функция для ведения журнала событий
        private void LogMessage(string message)
        {
            Console.WriteLine(message);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Настройки сборки
            string sourceDir = @"C:\Projects\MyGame\Source";
            string outputDir = @"C:\Projects\MyGame\Output";
            string executableName = "MyGame.exe";

            // Создаем менеджер сборки
            BuildManager buildManager = new BuildManager(sourceDir, outputDir, executableName);

            // Запускаем процесс сборки
            buildManager.BuildProject();
        }
    }
}