using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_8240_0246
{
    class Program
    {
        static public Random r = new Random();
        static void Main(string[] args)
        {
            List<Bus> buses = new List<Bus>();
            ACTION action;
            bool success;
            SartEgged(buses, out action, out success);
        }

        private static void SartEgged(List<Bus> buses, out ACTION action, out bool success)
        {
            do
            {
                do
                {
                    Console.WriteLine("choose an action");
                    Console.WriteLine("ADD_BUS, PICK_BUS, MAINTENANCE, REFUELLING, EXIT = -1");
                    success = Enum.TryParse(Console.ReadLine(), out action);

                } while (success == false);
                switch (action)
                {

                    //Introducing a bus to the list of buses in the company
                    case ACTION.ADD_BUS:
                        try
                        {
                            buses.Add(new Bus());
                        }
                        catch (Exception exception)
                        {
                            Console.WriteLine(exception.Message);
                        }
                        //print all buses
                        foreach (Bus bus in buses)
                        {
                            Console.WriteLine(bus);
                        }
                        break;
                    //Choosing a bus for travel

                    case ACTION.PICK_BUS:
                        Console.WriteLine("enter a license number");
                        string license = Console.ReadLine().Replace("-", String.Empty);
                        Bus foundBus = null;
                        foreach (Bus bus in buses)
                        {
                            if (bus.License == license)
                            {
                                foundBus = bus;
                                break;
                            }
                        }
                        if (foundBus != null)
                        {
                            Console.WriteLine(foundBus);
                            int i = r.Next(0, 1200);
                            if (i > 1200)
                            {
                                Console.WriteLine("Travel too long");
                            }
                            else
                            {
                                foundBus.Km += i;
                            }

                        }
                        else
                        {
                            Console.WriteLine("The bus does not exist in the system");
                        }
                        break;

                    //Performing refueling or handling of a bus:

                    case ACTION.MAINTENANCE:
                        Console.WriteLine("enter a license number");
                        string license1 = Console.ReadLine().Replace("-", String.Empty);
                        foreach (Bus bus in buses)
                        {
                            if (bus.License == license1)
                            {
                                Console.WriteLine("for Refuelling enter 1 for Maintenance enter 0");

                                int num = Convert.ToInt32(Console.ReadLine());
                                if (num == 0)
                                {
                                    DateTime currentDate = DateTime.Now;
                                    bus.Maintenance(currentDate);
                                    Console.WriteLine("enter mileage ");
                                    int mileage = Convert.ToInt32(Console.ReadLine());
                                    try
                                    {
                                        bus.Km = mileage;
                                    }
                                    catch (Exception exception)
                                    {
                                        Console.WriteLine(exception.Message);
                                    }

                                }
                                else
                                {
                                    if (num == 1)
                                    {
                                        bus.Refuelling(bus.Fuel);
                                    }
                                    else
                                        Console.WriteLine("Invalid value");
                                }
                                break;
                            }
                        }
                        break;
                    //Introducing the Km since the last treatment for all vehicles in the company.
                    case ACTION.REFUELLING:
                        foreach (Bus bus in buses)
                        {

                            foundBus = bus;
                            Console.WriteLine(foundBus);
                            Console.WriteLine("km:" + bus.Km);
                        }
                        break;
                    case ACTION.EXIT:
                        break;
                    default:
                        break;
                }
            } while (action != ACTION.EXIT);
        }
    }
}

