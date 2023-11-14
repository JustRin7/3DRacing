using System;
using System.IO;
using UnityEngine;


    [Serializable]
    public class Saver<T>
    {
        /// <summary>
        /// Выгрузка инфы из файла
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="complitionData"></param>
        public static void TryLoad(string filename, ref T data)
        {
            var path = FileHandler.Path(filename);

            if (File.Exists(path))
            {
                //Debug.Log($"Loading from {path}");

                var dataString = File.ReadAllText(path);

                var saver = JsonUtility.FromJson<Saver<T>>(dataString);//восстанавливает класс из Json

                data = saver.data;
            }
            /*else
            {
                Debug.Log($"No file at {path}");
            }*/
        }


        /// <summary>
        /// Запись в файл
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="data"></param>
        public static void Save(string filename, T data)
        {
            //Debug.Log($"saving to {Path(filename)}");

            var wrapper = new Saver<T> { data = data };

            var dataString = JsonUtility.ToJson(wrapper);//если в Json дается не массив, даже если в этом типе данных есть массив,
            //то он все равно не запишет в файл этот тип поэтому нужен wrapper

            //Debug.Log(dataString);

            File.WriteAllText(FileHandler.Path(filename), dataString);
        }


        public T data;
    }


    public static class FileHandler
    {
        /// <summary>
        /// путь к файлу
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string Path(string filename)
        {
            return $"{Application.persistentDataPath}/{filename}";
        }


        /// <summary>
        /// Ресетнуть карту
        /// </summary>
        /// <param name="filename"></param>
        public static void Reset(string filename)
        {
            var path = Path(filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        internal static bool HasFile(string filename)
        {
            return File.Exists(Path(filename));
        }
    }