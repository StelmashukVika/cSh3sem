using System;
using System.IO;
using System.IO.Compression;

namespace lab.rab
{
    class Program
    {
        static void Compress(string sourceFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(sourceFile.Substring(0, sourceFile.LastIndexOf('.')) + ".gz"))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
            FileInfo fi1 = new FileInfo(sourceFile);
            fi1.Delete();
        }

        public static void Decompress(string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile.Substring(0, compressedFile.LastIndexOf('.')) + ".txt"))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", compressedFile);
                    }
                }
            }
            File.Delete(compressedFile);
        }


        static void Menu()
        {
            Console.WriteLine("\t\t\t\t\t\tМеню");
            Console.WriteLine("\t\t\t\t\t1.Создание папки");
            Console.WriteLine("\t\t\t\t\t2.Создание файла");
            Console.WriteLine("\t\t\t\t\t3.Удаление папки");
            Console.WriteLine("\t\t\t\t\t4.Удаление файла");
            Console.WriteLine("\t\t\t\t\t5.Поиск папки");
            Console.WriteLine("\t\t\t\t\t6.Запись в файл");
            Console.WriteLine("\t\t\t\t\t7.Чтение из файла");
            Console.WriteLine("\t\t\t\t\t8.Копирование файла");
            Console.WriteLine("\t\t\t\t\t9.Перемещение файла");
            Console.WriteLine("\t\t\t\t\t10.Переименование файла");
            Console.WriteLine("\t\t\t\t\t11.Архивация файла");
            Console.WriteLine("\t\t\t\t\t12.Разархивация файла");
            Console.WriteLine("\t\t\t\t\t13.Выход из программы");
        }


        static void Main()
        {
            Console.WriteLine("Здравствуйте!)\nКликните любую клавишу, вам буудет показано меню, которое осуществляет программа с вашей папкой:D ");
            Console.ReadKey();
            Console.Clear();
            string folderName = @"d:\Причины отменить 2020 год";
            DirectoryInfo di = new DirectoryInfo("d:\\Причины отменить 2020 год\\");
            string pathString;
            do
            {
                Menu();
                Console.WriteLine("Ваш выбор: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Введите название папки: ");
                        string papkaname = Console.ReadLine();
                        pathString = Path.Combine(folderName, papkaname);
                        Directory.CreateDirectory(pathString);
                        Console.WriteLine("\nСписок папок в корневом каталоге: ");
                        DirectoryInfo[] diArr = di.GetDirectories();
                        foreach (DirectoryInfo dri in diArr)
                        Console.WriteLine(dri.Name);
                        break;
                    case "2":
                        Console.WriteLine("Введите название файла, через точку, указав расширение(например: text.txt)\nЕсли вы не введете расширение, то файл создастся без него): ");
                        string fileName = Console.ReadLine();
                        pathString = Path.Combine(folderName, fileName);
                        File.Create(pathString);
                        FileInfo[] fileNames = di.GetFiles("*.*");
                        Console.WriteLine("Список файлов в корневом каталоге: ");
                        foreach (FileInfo fi in fileNames)
                        Console.WriteLine("{0}: {1}", fi.Name, fi.CreationTime);
                        break;
                    case "3":
                        DirectoryInfo[] diArr1 = di.GetDirectories();
                        int countD = 0;
                        foreach (DirectoryInfo dri in diArr1)
                            countD++;
                        if (countD == 0) { Console.WriteLine("Нет доступных папок для удаления :с"); break; }
                        Console.WriteLine("Доступные папки для удаления: ");
                        foreach (DirectoryInfo dri in diArr1)
                            Console.WriteLine(dri.Name);
                        Console.WriteLine("Введите название папки: ");
                        string folderDelete = Console.ReadLine();
                        string deleteFolder = Path.Combine(folderName, folderDelete);
                        try
                        {
                            if (!(Directory.Exists(deleteFolder))) Console.WriteLine("Такой папки не существует :с");
                            else Directory.Delete(deleteFolder);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        break;
                    case "4":
                        DirectoryInfo dirInfo1 = di;
                        FileInfo[] fileNames1 = dirInfo1.GetFiles("*.*");
                        int counter = 0;
                        foreach (FileInfo fi in fileNames1)
                            counter++;
                        if (counter == 0) { Console.WriteLine("Нет доступных файлов для удаления :с"); break; }
                        Console.WriteLine("Доступные файлы для удаления: ");
                        foreach (System.IO.FileInfo fi in fileNames1)
                            Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите название файла с его расширением(если оно есть) через точку: ");
                        string fileDelete = Console.ReadLine();
                        string deleteString = Path.Combine(folderName, fileDelete);
                        FileInfo fi1 = new FileInfo(deleteString);
                        try
                        {
                            if (File.Exists(deleteString)) fi1.Delete();
                            else Console.WriteLine("Файла не существует :с");
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                        break;
                    case "5":
                        try
                        {
                            string mySearch = Console.ReadLine();
                            DirectoryInfo[] dirs = di.GetDirectories(mySearch);
                            Console.WriteLine("Найдено папок с названием {0} - {1}.", mySearch, dirs.Length);
                            if (dirs.Length == 0) break;
                            foreach (DirectoryInfo diNext in dirs)
                                Console.WriteLine("Данная папка содержит {0} файлов", diNext.GetFiles().Length);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The process failed: {0}", e.ToString());
                        }
                        break;
                    case "6":
                        FileInfo[] fileNames2 = di.GetFiles("*.*");
                        foreach (FileInfo fi in fileNames2)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("\nВведите файл(без расширения, оно автоматически будет считаться '.txt'), в который хотите записать текст\n!!!Если файл с таким названием не будет найдет, то он создастся автоматически с тем именем, которое вы ввели(и текст запишется туда): ");
                        string writeFile = Console.ReadLine() + ".txt";
                        pathString = Path.Combine(folderName, writeFile);
                        Console.WriteLine("Введите текст, который хотите записать в файл: ");
                        string text = Console.ReadLine();
                        File.WriteAllText(pathString, text);
                        break;
                    case "7":
                        FileInfo[] fileNames3 = di.GetFiles("*.*");
                        foreach (FileInfo fi in fileNames3)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите название файла без расширения(принимаются файлы только txt формата): ");
                        string readFile = Console.ReadLine() + ".txt";
                        Console.WriteLine(readFile);
                        string readFile1 = Path.Combine(folderName, readFile);
                        if (File.Exists(readFile1))
                        {
                            string pathString3 = Path.Combine(folderName, readFile);
                            string[] lines = File.ReadAllLines(pathString3);
                            Console.WriteLine("Содержимое {0} = ", readFile);
                            foreach (string line in lines)
                                Console.WriteLine("\t" + line);
                        }
                        else Console.WriteLine("Такого файла txt формата не существует :с");
                        break;
                    case "8":
                        FileInfo[] fileNames8 = di.GetFiles("*.*");
                        foreach (FileInfo fi in fileNames8)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите имя файла, который хотите копировать, с его разрешением: ");
                        string fileName2 = Console.ReadLine();
                        string sourceFile = Path.Combine(folderName, fileName2);
                        if (!(File.Exists(sourceFile))) { Console.WriteLine("Файла не существует :с"); break; }
                        Console.WriteLine("Введите папку, в которую хотите копировать(если такой папки не будет, то она создастся автоматически): ");
                        string targetPath = Path.Combine(folderName, Console.ReadLine());
                        string destFile = Path.Combine(targetPath, fileName2);
                        Directory.CreateDirectory(targetPath);
                        File.Copy(sourceFile, destFile, true);
                        break;
                    case "9":
                        FileInfo[] fileNames4 = di.GetFiles("*.*");
                        foreach (FileInfo fi in fileNames4)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите файл, который хотите переместить, с расширением:");
                        string sf = Console.ReadLine();
                        string sourceFile2 = Path.Combine(folderName, sf);
                        if (!(File.Exists(sourceFile2))) { Console.WriteLine("Такого файла не существует :с"); break; }
                        Console.WriteLine("Введите имя папки, в которую хотите переместить файл(если папки такой не существует, она создастся автоматически): ");
                        string destinationFile = Path.Combine(folderName, Console.ReadLine());
                        Directory.CreateDirectory(destinationFile);
                        destinationFile = Path.Combine(destinationFile, sf);
                        File.Move(sourceFile2, destinationFile);
                        break;
                    case "10":
                        FileInfo[] fileNames5 = di.GetFiles("*.*");
                        foreach (FileInfo fi in fileNames5)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите файл, который хотите переименовать, с расширением: ");
                        string pf = Console.ReadLine();
                        string sourceFile3 = Path.Combine(folderName, pf);
                        if (!(File.Exists(sourceFile3))) { Console.WriteLine("Такого файоа не существует :с"); break; }
                        Console.WriteLine("Введите новое имя: ");
                        string deFile = Path.Combine(folderName, Console.ReadLine() + ".txt");
                        File.Move(sourceFile3, deFile);
                        break;
                    case "11":
                        FileInfo[] fileNames6 = di.GetFiles("*.*");
                        Console.WriteLine("Список файлов в корневом каталоге: ");
                        foreach (FileInfo fi in fileNames6)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите название файла, который хотите архивировать(сжать), вместе с расширением: ");
                        string comFile = Console.ReadLine();
                        pathString = Path.Combine(folderName, comFile);
                        if (!(File.Exists(pathString))) { Console.WriteLine("Файла не существует :с"); break; }
                        Compress(pathString);
                        break;
                    case "12":
                        FileInfo[] fileNames7 = di.GetFiles("*.*");
                        Console.WriteLine("Список файлов в корневом каталоге: ");
                        foreach (FileInfo fi in fileNames7)
                        Console.WriteLine(fi.Name);
                        Console.WriteLine("Введите название файла, который хотите разархивировать, без расширения: ");
                        string coFile = Console.ReadLine() + ".gz";
                        string compreFile = Path.Combine(folderName, coFile);
                        if (!(File.Exists(compreFile))) { Console.WriteLine("Файла не существует :с"); break; }
                        Decompress(compreFile);
                        break;
                    case "13":
                        return;
                    default:
                        Console.WriteLine("Меню содержит 12 пунктов. Вам нужно выбрать число от 1 до 12 ;)\nЕсли не хотите работать с программой, выберите пункт 13 :C");
                        break;
                }
                Console.WriteLine("\nНажмите любую клавишу, чтоб продолжить");
                Console.ReadKey();
                Console.Clear();
            } while (true);
        }
    }
}
