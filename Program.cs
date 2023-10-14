using System.Security.Cryptography;

namespace Ежедневник
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DateOnly current_date = DateOnly.FromDateTime(DateTime.Now);
            List<Event> current_events = new List<Event>();
            List<Event> event_list = EventsStandart();
            current_events = Events(current_date, event_list);
            while (true)
            {
                ConsoleKeyInfo Keys = Console.ReadKey(true);
                switch (Keys.Key)
                {
                    case ConsoleKey.LeftArrow:
                        current_date = Dates(current_date,-1);
                        current_events = Events(current_date, event_list);
                        break;
                    case ConsoleKey.RightArrow:
                        current_date = Dates(current_date,1);
                        current_events = Events(current_date, event_list);
                        break;
                    case ConsoleKey.UpArrow:
                        Arrows(-1, current_events);
                        break;
                    case ConsoleKey.DownArrow:
                        Arrows(1, current_events);
                        break;
                    case ConsoleKey.Enter:
                        WriteEvent(current_events);
                        break;
                    case ConsoleKey.A:
                        event_list = AddEvent(event_list);
                        Datebook(current_date, current_events);
                        break;
                    case ConsoleKey.Escape:
                        Console.SetCursorPosition(0, current_events.Count + 1);
                        return;
                }
            }
        }
        static List<Event> EventsStandart()
        {
            List<Event> events = new List<Event>();
            Event CodeFuture = new Event()
            {
                Name = "Занятие 'Код будущего'",
                Description = "С 9 до 13",
                Location = "Дистанционнно из дома",
                EventDate = DateOnly.Parse("09.10.2023")
            };
            Event Couples2 = new Event()
            {
                Name = "Сходить на пары",
                Description = "С первой по четвертую",
                Location = "МПТ Нежинская",
                EventDate = DateOnly.Parse("10.10.2023")
            };
            Event Homework2 = new Event()
            {
                Name = "Сделать домашнюю работу на завтра",
                Description = "Практическая по философии, ИТ и опрос по ААС",
                Location = "Дом",
                EventDate = DateOnly.Parse("10.10.2023")
            };
            Event Pickup = new Event()
            {
                Name = "Забрать посылку",
                Description = "Книга 'Секреты успеха программистов'",
                Location = "Почта",
                EventDate = DateOnly.Parse("11.10.2023")
            };
            Event Couples3 = new Event()
            {
                Name = "Сходить на пары",
                Description = "С первой по четвертую",
                Location = "МПТ Нахимовский",
                EventDate = DateOnly.Parse("11.10.2023")
            };
            Event BZHD3 = new Event()
            {
                Name = "Сделать БЖД",
                Description = "План эвакуации",
                Location = "Дом",
                EventDate = DateOnly.Parse("11.10.2023")
            };
            events.AddRange([CodeFuture, Couples2, Homework2, Pickup, Couples3, BZHD3]);
            return events;
        }
        static List<Event> Events(DateOnly current_date, List<Event> events)
        {
            List<Event> current_events = new List<Event>();
            foreach (var item in events)
            {
                if (item.EventDate == current_date)
                {
                    current_events.Add(item);
                }
            }
            Datebook(current_date, current_events);
            return current_events;
        }
        static void Datebook(DateOnly current_date, List<Event> current_events)
        {
            Console.Clear();
            Console.WriteLine($"Задачи на {current_date}");
            for (int i = 0; i < current_events.Count; i++)
            {
                Console.WriteLine($"  {i + 1}. {current_events[i].Name}");
            }
            if (current_events.Count > 0)
            {
                Console.SetCursorPosition(0, 1);
                Console.WriteLine("->");
            }
        }
        static DateOnly Dates(DateOnly current_date,int change)
        {
            return current_date.AddDays(change);
        }
        static void Arrows(int change, List<Event> current_events)
        {
            int count = current_events.Count;
            if (count > 0)
            {
                int pos = Console.GetCursorPosition().Item2 - 1;
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");
                if (pos + change > 0 && pos + change <= count)
                {
                    pos += change;
                }
                else if (pos + change == 0)
                {
                    pos = count;
                }
                else
                {
                    pos = 1;
                }
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");
            }
        }
        static void WriteEvent(List<Event> current_events)
        {
            int pos = Console.GetCursorPosition().Item2 - 2;
            Console.Clear();
            Event current = current_events[pos];
            Console.WriteLine($"Название: {current.Name}");
            Console.WriteLine($"Описание: {current.Description}");
            Console.WriteLine($"Место: {current.Location}");
            Console.WriteLine($"Дата: {current.EventDate}");
            Console.WriteLine("Для возвращения нажмите любую клавишу");
            Console.ReadKey(true);
            Datebook(current.EventDate,current_events);
        }
        static List<Event> AddEvent(List <Event> event_list)
        {
            Console.Clear();
            Event item = new Event();
            Console.Write("Название: ");
            item.Name = Console.ReadLine();
            Console.Write("Описание: ");
            item.Description = Console.ReadLine();
            Console.Write("Место: ");
            item.Location = Console.ReadLine();
            bool x = false;
            while (x == false)
            {
                Console.Write("Дата (ДД.ММ.ГГГГ): ");
                try
                {
                    item.EventDate = DateOnly.Parse(Console.ReadLine());
                    x = true; break;
                }
                catch { }
            }
                
            Console.WriteLine("Добавлено");
            event_list.Add(item);
            return event_list;
        }

    }
}