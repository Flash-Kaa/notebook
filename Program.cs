using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Notebook
{
    class Program
    {
        // Путь к создаваемому редактору (блокноту)
        static string editorPathFile = @"C:\Users\admin\OneDrive\Рабочий стол\FWorld\FiLeS\notebook\notebookEdit.txt";

        // Все пути к файлам (без имени самого файла)
        static string diskLocalPath = @"C:\Users\admin\OneDrive\Рабочий стол\FWorld\FiLeS\notebook\";
        static string diskOneDrivePath = @"C:\Users\admin\OneDrive\notebook\";
        static string diskYandexPath = @"C:\Users\admin\YandexDisk\notebook\";

        static void Main()
        {
            // Определение названия файла
            var fileName = NamingFile(diskLocalPath);

            // Создание файла редактора
            FileInfo editor = new FileInfo(editorPathFile);
            editor.Create().Close();

            // Открытие редактора и ожидание его закрытия
            var p = Process.Start(editorPathFile);
            while (!p.HasExited) 
            { 
            }

            // Перемещение файлов по необходимым папкам
            MoveFiles(editor, fileName);
        }

        static string NamingFile(string diskLocalPath)
        {
            // Формат даты
            DateTime time = DateTime.Today;
            var date = time.Day + "-" + time.Month + "-" + time.Year;

            // Ожидаемое название файла
            var fileName = new StringBuilder();
            fileName.Append("blog_");
            fileName.Append(date);
            fileName.Append(".txt");

            // Определение итогового названия файла (если файл с таким названием уже есть)
            FileInfo diskLocal = new FileInfo(diskLocalPath + fileName.ToString());
            while (diskLocal.Exists)
            {
                fileName.Insert(fileName.Length - 4, "{New}");
                diskLocal = new FileInfo(diskLocalPath + fileName.ToString());
            }

            return fileName.ToString();
        }

        static void MoveFiles(FileInfo editor, string fileName)
        {
            // Итоговое расположение файлов
            var diskLocalPathFile = diskLocalPath + fileName.ToString();
            var diskOneDrivePathFile = diskOneDrivePath + fileName.ToString();
            var diskYandexPathFile = diskYandexPath + fileName.ToString();

            // Перемещение файлов по необходимым папкам
            editor.CopyTo(diskOneDrivePathFile);
            editor.CopyTo(diskYandexPathFile);
            editor.CopyTo(diskLocalPathFile);
            editor.Delete();
        }
    }
}