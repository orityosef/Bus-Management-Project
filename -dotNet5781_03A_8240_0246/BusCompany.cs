
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
        private List<BusLine> busLines = new List<BusLine>();
        public List<BusLine> BusLiness
        {
            get => busLines;
        }
        public BusLine this[int lineNumber]//indexrt
        {
            get
            {
                int index1 = FindIndex(lineNumber);
                if (index1 != -1)
                {
                    return busLines[index1];
                }
                else throw new IndexOutOfRangeException("not valid index");
            }
            set
            {
                int index1 = FindIndex(lineNumber);
                if (index1 == -1)
                {
                    busLines.Add(value);
                }
            }
        }
        
        static int counterLine// num of the bus
        {
            get;
            set;
        }
  
        public void addbus(BusLine x)//add bus line
        {
            if (busLines.Count== 0)
            {
                busLines.Add(x);
                counterLine++;
                return;

            }
            int counter = 0;
            foreach (BusLine y in busLines)
            {
                if (y.Number == x.Number)
                {
                    counter++;
                }
            }
            if (counter == 1)
            {
                BusLine temp = new BusLine();
                temp.Number = x.Number;
                foreach (BusStation p in x.BusStations)
                {
                    temp.BusStations.Add(p);
                }
                busLines.Add(temp);
                
                return;
            }
            if (counter > 1)
            {
                throw new ArgumentException("The bus already exists");
            }
            busLines.Add(x);
            
        }
        public void remove(BusLine x)//remove bus
        {
            foreach (BusLine y in busLines)
            {
                if (x.Number == y.Number)
                {
                    if (x.BusStations == y.BusStations)
                    {
                        BusLiness.Remove(x);
                        counterLine--;
                        return;
                    }
                }
            }
            throw new ArgumentException("The bus line does not exist");
        }
        public int Find(int x)//Check the index of the busline
        {
            int counter = 0;
            foreach (BusLine y in busLines)
            {
                counter++;
                if (x == y.Number)
                {

                    return (counter-1);

                }
            }
            throw new ArgumentException("The bus line does not exist");
        }
        public void addStationToLine(int bus, BusStation s)
        {//add station to line
            foreach (BusLine item in BusLiness)
            {
                if (item.Number == bus)
                {
                    item.AddLast(s);
                    return;
                }
            }
        }

        public void searchStation(int w)
        {

            foreach (BusLine y in busLines)
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
        public BusLine searchLine(int key)
        {//return the bus line by the line number 
            foreach (BusLine item in BusLiness)
            {
                if (item.Number == key)
                    return item;
            }
            return null;
        }
        public BusCompany check(BusStation x, BusStation y)
        {
            BusCompany sub_list = new BusCompany();
            foreach (BusLine i in BusLiness)
            {
                x = i.SearchStationKey(x.BusStationKey);//find index
                y = i.SearchStationKey(y.BusStationKey);//find index
                if (i.searchStation(x) && i.searchStation(y))
                {
                    sub_list.addbus(i);
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


        
        private static BusStation MewStation()
        {
            BusStation x = new BusStation();
            Random r = new Random();
            x.Latitude = r.NextDouble() * (33.3 - 31 - 1) + 31;
            x.Longitude = r.NextDouble() * (35.5 - 34.3 - 1) + 34.3;
            x.BusStationKey = r.Next(0, 1000000);
            return x;
        }


        private int FindIndex(int lineNumber)//chek if the bus line found
        {
            var index = BusLiness.FindIndex((BusLine line) => { return line.Number == lineNumber; });
            try
            {
                if (index == -1)
                {
                    throw new Exception("Error: not found");//an valid index
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return index;
        }

        public IEnumerator GetEnumerator()
        {
            return busLines.GetEnumerator();
        }
    }
}