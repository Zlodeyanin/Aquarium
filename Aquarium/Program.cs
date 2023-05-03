using System;
using System.Collections.Generic;

namespace Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium();
            aquarium.Work();
        }
    }

    class Aquarium
    {
        private List<Fish> _aquarium = new List<Fish>();
        private int _capacity  = 10;

        public void Work()
        {
            const string CommandLaunchFish = "1";
            const string CommandCatchFish = "2";
            const string CommandExit = "3";

            bool isWork = true;
            Random random = new Random();

            while (isWork)
            {
                ShowAllFish();
                Console.WriteLine();
                Console.WriteLine($"Нажмите {CommandLaunchFish}, чтобы запустить новую рыбку в аквариум.");
                Console.WriteLine($"Нажмите {CommandCatchFish}, чтобы выловить рыбку из аквариума.");
                Console.WriteLine($"Нажмите {CommandExit}, чтобы завершить работу.");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case CommandLaunchFish:
                        LaunchFish(CreateNewFish(random));
                        break;

                    case CommandCatchFish:
                        CatchFish();
                        break;

                    case CommandExit:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Такого пункта в меню нет.");
                        break;
                }

                SpendOneFishHelth();
                Console.ReadKey();
                Console.Clear();
            }
        }

        private void LaunchFish(Fish fish)
        {
            if (_capacity > _aquarium.Count)
            {
                _aquarium.Add(fish);
            }
            else
            {
                Console.WriteLine("В аквариуме не осталось места для новых рыбок.");
            }
        }

        private void CatchFish()
        {
            Console.WriteLine("Выберите рыбку, которую хотите достать из аквариума.");
            string userInput = Console.ReadLine();

            if(int.TryParse(userInput, out int fishIndex))
            {
                if(fishIndex > 0 && fishIndex <= _aquarium.Count)
                {
                    _aquarium.RemoveAt(fishIndex - 1);
                }
                else
                {
                    Console.WriteLine("Такой рыбки в аквариуме нет.");
                }
            }
            else
            {
                Console.WriteLine("Я такой команды не знаю.");
            }
        }

        private void ShowAllFish()
        {
            if (_aquarium.Count > 0)
            {
                for (int i = 0; i < _aquarium.Count; i++)
                {
                    Console.Write(i + 1 + ") ");
                    _aquarium[i].ShowStats();
                }
            }
            else
            {
                Console.WriteLine("- Пусто -");
            }
            Console.WriteLine($"В аквариуме осталось {_capacity - _aquarium.Count} мест.");
        }

        private Fish CreateNewFish(Random randomFishName)
        {
            string[] fishNames = { "Скалярия", "Гуппи", "Голубой неон" };
            int minIndex = 0;
            return new Fish(fishNames[randomFishName.Next(minIndex,fishNames.Length)]);
        }

        private void SpendOneFishHelth()
        {
            for (int i = 0; i < _aquarium.Count; i++)
            {
                _aquarium[i].SpendOneHealth();

                if (_aquarium[i].Health == 0)
                {
                    Console.WriteLine($"{i + 1}) Рыбка {_aquarium[i].Name} умерла. Вы убираете её из аквариума.");
                    _aquarium.RemoveAt(i);
                }
            }
        }
    }

    class Fish
    {
        public Fish(string name) 
        {
            Name= name;
        }

        public string Name { get; private set; }
        public int Health { get; private set; } = 20;

        public void SpendOneHealth()
        {
            Health--;
        }

        public void ShowStats()
        {
            Console.WriteLine($"Рыбка - {Name}. Остаток жизней - {Health}.");
        }
    }
}
