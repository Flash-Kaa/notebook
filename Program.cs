using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace Notebook
{
    class Program
    {
        // Путь к создаваемому редактору (блокноту)
        const string EditorPathFile = @"C:\Users\admin\OneDrive\Рабочий стол\FWorld\FiLeS\notebook\notebookEdit.txt";

        // Все пути к файлам (без имени самого файла)
        const string DiskLocalPath = @"C:\Users\admin\OneDrive\Рабочий стол\FWorld\FiLeS\notebook\";
        const string DiskOneDrivePath = @"C:\Users\admin\OneDrive\notebook\";
        const string DiskYandexPath = @"C:\Users\admin\YandexDisk\notebook\";

        static void Main()
        {
            // Создание файла редактора
            var editor = new FileInfo(EditorPathFile);
            editor.Create().Close();

            // Открытие редактора и ожидание его закрытия
            var p = Process.Start(EditorPathFile);
            while (!p.HasExited) 
            { 
                Thread.Sleep(500);
            }

            // Перемещение файлов по необходимым папкам
            MoveFiles(editor);
            editor.Delete();
        }

        static string NamingFile(string diskLocalPath)
        {
            // Формат даты
            var time = DateTime.Today;
            var date = time.Day + "-" + time.Month + "-" + time.Year;

            // Ожидаемое название файла
            var fileName = new StringBuilder();
            fileName.Append("blog_");
            fileName.Append(date);
            fileName.Append(".txt");

            // Определение итогового названия файла (если файл с таким названием уже есть)
            var diskLocal = new FileInfo(diskLocalPath + fileName.ToString());
            while (diskLocal.Exists)
            {
                fileName.Insert(fileName.Length - 4, "{New}");
                diskLocal = new FileInfo(diskLocalPath + fileName.ToString());
            }

            return fileName.ToString();
        }

        static void MoveFiles(FileInfo editor)
        {
            // Определение названия файла
            var fileName = NamingFile(DiskLocalPath);

            // Итоговое расположение файлов
            var diskLocalPathFile = DiskLocalPath + fileName.ToString();
            var diskOneDrivePathFile = DiskOneDrivePath + fileName.ToString();
            var diskYandexPathFile = DiskYandexPath + fileName.ToString();

            // Перемещение файлов по необходимым папкам
            editor.CopyTo(diskOneDrivePathFile);
            editor.CopyTo(diskYandexPathFile);
            editor.CopyTo(diskLocalPathFile);
        }
    }
}