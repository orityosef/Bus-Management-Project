using System;
using System.Collections.Generic;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil02_Tal_DotNetLab
{
    public enum Options
    {
        ADD, DELETE, SEARCH, PRINT, EXIT = -1
    }

    class Program
    {
        private static BusStation MewStation()
        {
            BusStation x = new BusStation();
            Random r = new Random();
            x.Latitude = r.NextDouble() * (33.3 - 31 - 1) + 31;
            x.Longitude = r.NextDouble() * (35.5 - 34.3 - 1) + 34.3;
            x.BusStationKey = r.Next(0, 1000000);
            return x;
        }
        static void Main(string[] args)
        {
            //BusCompany egged = new BusCompany();


            Buslines4 collection_line = new Buslines4();//Create a collection of lines
            List<BusStation> stations = new List<BusStation>();//List of stations
                                                               // List<BusLine> busLines = new List<BusLine>();
            BusLine bus = new BusLine();

            for (int j = 0; j < 10; j++)
            {
                Random r = new Random();
                bus.Number = r.Next(0, 1000000);
                for (int i = 0; i < 40; i++)
                {
                    BusStation x = MewStation();
                    try
                    {
                        bus.Add(i, x);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception.Message);
                    }
                }
                try
                {
                    collection_line.add(bus);
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            bool success = false;
            Options choice;
            do
            {
                do
                {
                    Console.WriteLine("enter yor choice:");
                    Console.WriteLine("ADD, DELETE, SEARCH, PRINT, EXIT = -1:");
                    success = Enum.TryParse(Console.ReadLine(), out choice);

                } while (success == false);
                switch (choice)
                {
                    case Options.ADD:
                        Console.WriteLine("enter 1 to add bus 0 to add station");
                        string temp = Console.ReadLine();
                        int num = int.Parse(temp);
                        if (num == 1)
                        {
                            Console.WriteLine("enter bus line");
                            string numberTemp = Console.ReadLine();
                            int number = int.Parse(numberTemp);
                            bus.Number = number;
                            try
                            {
                                collection_line.add(bus);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }

                        }
                        else
                        {
                            BusStation s = MewStation();
                            Console.WriteLine("enter the bus line to which the station will be added");
                            string x = Console.ReadLine();
                            int y = int.Parse(x);
                            try
                            {
                                int index = collection_line.Find(y);
                                bus = collection_line.FindLine(index);
                                bus.Add(11, s);
                                collection_line.remove(collection_line.FindLine(index));
                                collection_line.add(bus);
                            }

                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        break;
                    case Options.DELETE:
                        Console.WriteLine("enter 1 to delete bus 0 to delete station");
                        temp = Console.ReadLine();
                        num = int.Parse(temp);
                        if (num == 1)
                        {
                            Console.WriteLine("enter bus line");
                            string numberTemp = Console.ReadLine();
                            int number = int.Parse(numberTemp);
                            try
                            {
                                int tempbus = collection_line.Find(number);
                                bus = collection_line.FindLine(tempbus);
                                collection_line.remove(bus);
                            }
                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }


                        }
                        else
                        {
                            BusStation s = MewStation();
                            Console.WriteLine("enter the bus line to which the station will be deleted");
                            string x = Console.ReadLine();
                            int y = int.Parse(x);
                            Console.WriteLine("enter the station that will be deleted");
                            x = Console.ReadLine();
                            int w = int.Parse(x);
                            s.BusStationKey = w;
                            try
                            {
                                int index = collection_line.Find(y);
                                bus = collection_line.FindLine(index);
                                bus.remove(s);
                                collection_line.remove(collection_line.FindLine(index));
                                collection_line.add(bus);
                            }

                            catch (Exception exception)
                            {
                                Console.WriteLine(exception.Message);
                            }
                        }
                        break;
                    case Options.SEARCH:
                        Console.WriteLine("enter 1 to Search for lines that pass through the station bus " +
                                              "0 to Printing the options for travel between 2 stations");
                        temp = Console.ReadLine();
                        num = int.Parse(temp);
                        if (num == 1)
                        {
                            Console.WriteLine("enter Station number");
                            string x = Console.ReadLine();
                            int y = int.Parse(x);
                            collection_line.searchStation(y);
                        }
                        else
                        {
                            //Printing the options for travel between 2 stations, without changing buses, take in a departure station and a destination station and return the results sorted by travel time.
                            Console.WriteLine("enter the number of first station:");
                            int num_start = int.Parse(Console.ReadLine());
                            Console.WriteLine(" enter the number of last station:");
                            int num_end = int.Parse(Console.ReadLine());
                            BusLine sub = new BusLine();
                            Buslines4 sub_list = new Buslines4();// list of pathes between the two stations
                            BusStation start = new BusStation();
                            start.BusStationKey = num_start;

                            BusStation end = new BusStation();
                            end.BusStationKey = num_end;
                            sub_list = collection_line.check(start, end);
                            sub_list.SortTime(sub_list);
                            sub_list.print(sub_list);
                        }
                        break;
                    case Options.PRINT:
                        Console.WriteLine("enter 1 print all the bus line 0 to Printing all the stations");
                        temp = Console.ReadLine();
                        num = int.Parse(temp);
                        if (num == 1)
                        {
                            collection_line.print(collection_line);
                        }
                        else
                        {
                            collection_line.printS(collection_line);
                        }
                        break;
                    case Options.EXIT:
                        break;
                    default:
                        break;
                }
            } while (choice != Options.EXIT);






        }

    }
}
