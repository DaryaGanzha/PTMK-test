// See https://aka.ms/new-console-template for more information

using PTMK_test;
using PTMK_test.Data;
using System;
using System.Data.SqlClient;

namespace PTMK
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DatabaseService();

            Console.WriteLine("1 - Создание таблицы с полями представляющими ФИО, дату рождения, пол");
            Console.WriteLine("2 - Создание записи");
            Console.WriteLine("3 - Вывод всех строк с уникальным значением ФИО+дата, отсортированным по ФИО");
            Console.WriteLine("4 - Заполнение автоматически 1000000 строк");
            Console.WriteLine("5 - Заполнение автоматически 100 строк в которых пол мужской и ФИО начинается с F");
            Console.WriteLine("6 - Результат выборки из таблицы по критерию: пол мужской, ФИО начинается с F. Сделать замер времени выполнения");
            Console.WriteLine("Введите номер действия:");

            string inputString = Console.ReadLine();
            while (inputString != "")
            {
                if (inputString == "1")
                {
                    Console.WriteLine(service.CreateTable());
                    Console.WriteLine();
                }
                if (inputString == "2")
                {
                    Console.WriteLine("Введите ФИО: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Введите дату рождения в формате DD/MM/YYYY: ");
                    string dateOfBirth = Console.ReadLine();
                    Console.WriteLine("Введите пол (1 - жен, 2 - муж): ");
                    string gender = "";
                    string read = Console.ReadLine();
                    while (gender == "")
                    {
                        if (read == "1" || read == "2")
                        {
                            gender = read;
                        }
                        else
                        {
                            Console.WriteLine("Пол введен не правильно! Введите еще раз.");
                            read = Console.ReadLine();
                        }
                    }
                    Console.WriteLine(service.CreateRecord(name, dateOfBirth, gender));
                    Console.WriteLine();
                }
                if (inputString == "3")
                {
                    Console.WriteLine(service.GetUniqueRows());
                    Console.WriteLine();
                }
                if (inputString == "4")
                {
                    Console.WriteLine(service.RandomLinesGeneration(1000000, "", ""));
                    Console.WriteLine();
                }
                if (inputString == "5")
                {
                    Console.WriteLine(service.RandomLinesGeneration(100, "F", "2"));
                    Console.WriteLine();
                }
                if (inputString == "6")
                {
                    Console.WriteLine(service.QueryByCriteria());
                    Console.WriteLine();
                }
                Console.WriteLine("1 - Создание таблицы с полями представляющими ФИО, дату рождения, пол");
                Console.WriteLine("2 - Создание записи");
                Console.WriteLine("3 - Вывод всех строк с уникальным значением ФИО+дата, отсортированным по ФИО");
                Console.WriteLine("4 - Заполнение автоматически 1000000 строк");
                Console.WriteLine("5 - Заполнение автоматически 100 строк в которых пол мужской и ФИО начинается с F");
                Console.WriteLine("6 - Результат выборки из таблицы по критерию: пол мужской, ФИО начинается с F. Сделать замер времени выполнения");
                Console.WriteLine("Введите номер действия:");
                inputString = Console.ReadLine();
            }
        }
    }
}

