
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;



namespace _dotNet5781_03A_8240_0246
{
    public class BusCompany : IEnumerable 

    {
        List<BusLine> BusLines = new List<BusLine>();
        public BusLine this[int lineNumber]
        {
            get
            {
                int index1 = FindIndex(lineNumber);
                return BusLines[index1];
            }
            set
            {
                int index1 = FindIndex(lineNumber);
                BusLines[index1] = value;
            }
        }
      /*public List<BusLine> BusLiness
        {
            get;
            set;
        }*/

        public int BusLineNum (int x)
        {
            int y = Find(x);
            return y;
         
        }
        public int counterLine
        {
            get;
            set;
        }
        public IEnumerable GetEnumerator()
            {return BusLines.GetEnumerator();}

        public void add(BusLine x)
        {
            if (BusLines == null)
            {
                BusLines.Add((BusCompany)x);
                counterLine++;
                return;

            }
            int counter = 0;
            foreach (BusLine y in BusLines)
            {
                if (y == x)
                {
                    counter++;
                }
            }
            if (counter == 1)
            {
                BusLine temp = x;
                temp.BusStations.Clear();
                foreach (Station p in x.BusStations)
                {
                    temp.BusStations.Add((BusStation)p);
                }
                BusLines.Add((BusCompany)temp);
                counterLine++;
                return;
            }
            if (counter > 1)
            {
                throw new ArgumentException("The bus already exists");
            }
            BusLines.Add((BusCompany)x);
            counterLine++;
        }
        public void remove(BusLine x)
        {
            foreach (BusLine y in BusLines)
            {
                if (x == y)
                {
                    if (x.BusStations == y.BusStations)
                    {
                        BusLiness.Remove((BusCompany)x);
                        counterLine--;
                        return;
                    }
                }
            }
            throw new ArgumentException("The bus line does not exist");
        }
        public int Find(int x)
        {
            int counter = 0;
            foreach (BusLine y in BusLines)
            {
                counter++;
                if (x == y.Number)
                {

                    return counter;

                }
            }
            throw new ArgumentException("The bus line does not exist");
        }
        public void searchStation(int w)
        {

            foreach (BusLine y in BusLines)
            {

                if (y.SearchStationKey1(w))
                {
                    Console.WriteLine(y.Number);
                }

            }
            throw new ArgumentException("There are no bus lines passing through this station");
        }
        public BusCompany SortTime(BusCompany x)
        {
            x.BusLiness.Sort();
            return x;
        }
        public BusLine FindLine(int index)
        {
            if (index < counterLine)
            {
                return BusLiness[index];
            }
            else
            {
                throw new ArgumentException("The index exceeds the limits of the list");

            }
        }
        public BusCompany check(BusStation x, BusStation y)
        {
            BusCompany sub_list = new BusCompany();
            foreach (BusLine i in BusLiness)
            {
                x = i.SearchStationKey(x.BusStationKey);//find index
                y = i.SearchStationKey(y.BusStationKey);//find index
                if (i.search(x) && i.search(y))
                {
                    sub_list.add(i);
                }
            }
            return sub_list;
        }
        public void print(BusCompany x)
        {
            foreach (BusLine i in BusLiness)
            {
                Console.WriteLine("line bus:" + i.Number);

            }
        }
        public void printS(BusCompany x)
        {
            foreach (BusLine i in BusLiness)
            {
                Console.WriteLine("line bus:" + i.Number);
                foreach (BusStation w in i.BusStations)
                {
                    Console.WriteLine("station:" + w.BusStationKey);
                }

            }
        }
       
        public BusCompany constractor()
        {
            BusCompany collection_line = new BusCompany();//Create a collection of lines
            List<BusStation> stations = new List<BusStation>();//List of stations
                                                              
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
            return collection_line;
        }
        private static BusStation MewStation()
        {
            BusStation x = new BusStation();
            Random r = new Random();
            x.Latitude = r.NextDouble() * (33.3 - 31 - 1) + 31;
            x.Longitude = r.NextDouble() * (35.5 - 34.3 - 1) + 34.3;
            x.BusStationKey = r.Next(0, 1000000);
            return x;
        }
 
       /* public BusLine this[int index]
        { 
             
            get 
            {
                if (index > -1 && index < counterLine)
                {
                    return BusLiness[index];
                }
                return null;
            } 
            set { BusLiness[index] = value; } 
        }*/

        private int FindIndex(int lineNumber)
        {
            var index = BusLiness.FindIndex((BusLine line) => { return line.Number == lineNumber; });
            try
            {
                if (index == -1)
                {
                    throw new Exception("Error: not found");//לזרוק חריגה אם האינדקס קטן מאפס
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return index;


        }
    }
}



