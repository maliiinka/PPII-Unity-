using System;
using System.IO;
using NAudio.Wave;

namespace GameExample.Audio
{
    public static class SoundManager
    {
        // Словарь для хранения загруженных звуков
        private static readonly Dictionary<string, AudioFileReader> LoadedSounds = new Dictionary<string, AudioFileReader>();

        // Воспроизведение звука
        public static void PlaySound(string soundFilePath)
        {
            if (!LoadedSounds.TryGetValue(soundFilePath, out var audioFileReader))
            {
                try
                {
                    audioFileReader = new AudioFileReader(soundFilePath);
                    LoadedSounds[soundFilePath] = audioFileReader;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка загрузки файла: {ex.Message}");
                    return;
                }
            }

            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFileReader);
                outputDevice.Play();
            }
        }

        // Остановка воспроизведения звука
        public static void StopSound(string soundFilePath)
        {
            if (LoadedSounds.TryGetValue(soundFilePath, out var audioFileReader))
            {
                audioFileReader.Stop();
            }
        }

        // Освобождение ресурсов после завершения работы
        public static void DisposeAll()
        {
            foreach (var reader in LoadedSounds.Values)
            {
                reader.Dispose();
            }
            LoadedSounds.Clear();
        }
    }
}