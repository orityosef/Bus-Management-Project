
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _dotNet5781_03A_8240_0246
{
    public class BusCompany : BusLine
    {
        private List<BusLine> BusLines = new List<BusLine>();
        public List<BusLine> BusLiness
        {
            get
            {
                List<BusLine> temp = new List<BusLine>(BusLines);
                return temp;
            }
        }
        private int counterLine
        {
            get { return counterLine; }
            set { counterLine = 0; }
        }

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
    }
}



