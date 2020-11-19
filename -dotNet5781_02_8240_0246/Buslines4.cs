
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Targil02_Tal_DotNetLab
{
    public class Buslines4 : BusLine
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
                BusLines.Add((Buslines4)x);
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
                BusLines.Add((Buslines4)temp);
                counterLine++;
                return;
            }
            if (counter > 1)
            {
                throw new ArgumentException("The bus already exists");
            }
            BusLines.Add((Buslines4)x);
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
                        BusLiness.Remove((Buslines4)x);
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
        public Buslines4 SortTime(Buslines4 x)
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
        public Buslines4 check(BusStation x, BusStation y)
        {
            Buslines4 sub_list = new Buslines4();
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
        public void print(Buslines4 x)
        {
            foreach (BusLine i in BusLiness)
            {
                Console.WriteLine("line bus:" + i.Number);

            }
        }
        public void printS(Buslines4 x)
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



