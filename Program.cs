using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Text;

namespace CodeTest
{
    class Program
    {
        static List<Record> list_rec;
        static Record[] array_rec;

        static List<Record> list_rec2;
        static Record[] array_rec2;

        static void Main(string[] args)
        {
            read_data();

            // sort first
            sort_data();

            Console.WriteLine("Part 1");
            part1();
            Console.WriteLine("Enter to continue."); Console.ReadLine();

            Console.WriteLine("Part 2");
            part2();
            Console.WriteLine("Enter to continue."); Console.ReadLine();

            Console.WriteLine("Part 3");
            part3();
            Console.ReadLine();
        }

        private static void sort_data()
        {
            list_rec.Sort(comp_func);
            array_rec = list_rec.ToArray();

            int size = array_rec.Length;
            for (int i = 0; i < size; i++)
            {
                //Console.WriteLine(array_rec[i].date);
            }
            //Console.ReadLine();
        }

        private static int comp_func(Record x, Record y)
        {
            if (x.date > y.date)  return 1;
            if (x.date == y.date) return 0;
            if (x.date < y.date) return -1;

            return -1;
        }


        private static void part3()
        {
            //Console.WriteLine("not implemented, but mean for each record is calclued");
            double std;
            int N = array_rec2.Length;
            double sum_N = 0;
            

            // calc sum of all records
            for (int i = 0; i < N; i++)
            {
                sum_N += array_rec2[i].ave_temp;
            }
            double mean = sum_N / N;

            // calc std
            double sum_sqare = 0;
            for (int i = 0; i < N; i++)
            {
                sum_sqare += Math.Pow(array_rec2[i].ave_temp - mean,2);
            }
            std = Math.Sqrt(sum_sqare / N);

            
            // calc diff_std
            for (int i = 0; i < N; i++)
            {
                array_rec2[i].diff_std = Math.Abs(mean - array_rec2[i].ave_temp);
                if (array_rec2[i].diff_std > 2*std)
                {
                    Console.WriteLine(array_rec2[i].date.ToString("dd-MMM-yyyy") + "," + array_rec2[i].ave_temp.ToString() + "," + array_rec2[i].diff_std.ToString());

                }
            }

            Console.WriteLine("date,average_temp,diff_std");
            Console.WriteLine("mean is {0}", mean);
            Console.WriteLine("std is {0}", std);

        }

        private static void part2()
        {
            int size = array_rec2.Length;
            for (int i = 0; i < size; i++)
            {
                if (array_rec2[i].is_missing)
                {
                    Console.WriteLine(array_rec2[i].date.ToString("dd-MMM-yyyy") + "," + array_rec2[i].high_temp.ToString() + "," + array_rec2[i].low_temp.ToString());
                }
            }
        }

        private static void part1()
        {
            list_rec2 = new List<Record>();            
            int size = array_rec.Length;
            for (int i = 0; i < size; i++)
            {
                list_rec2.Add(array_rec[i]);
                if (i != size - 1)
                {
                    DateTime next_date = array_rec[i].date.AddDays(1);
                    if (next_date != array_rec[i + 1].date)
                    {
                        
                        
                        // insert extrapolate element
                        Record new_rec = new Record();
                        new_rec.date = next_date;
                        new_rec.high_temp = (array_rec[i].high_temp+array_rec[i+1].high_temp)/2.0;
                        new_rec.low_temp = (array_rec[i].low_temp+array_rec[i+1].low_temp)/2.0;
                        new_rec.is_missing = true;

                        Console.WriteLine(next_date.ToString("dd-MMM-yyyy"));

                        list_rec2.Add(new_rec);
                    }
                    
                }
                
            }
            array_rec2 = list_rec2.ToArray();
        }

        private static void read_data()
        {
            list_rec = new List<Record>();
            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("NYCTemps.csv"))
                {
                    String line;
                    // bypass first line
                    sr.ReadLine();

                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] line_array = line.Split(',');

                        //create Record Object for each entry

                        Record rec = new Record();
                        rec.date = Convert.ToDateTime(line_array[0]);
                        rec.high_temp = Convert.ToDouble(line_array[1]);
                        rec.low_temp = Convert.ToDouble(line_array[2]);
                        rec.ave_temp = (rec.high_temp + rec.low_temp) / 2.0;
                        rec.is_missing = false;

                        /*
                        Console.WriteLine(line);
                        Console.WriteLine(rec.date);
                        Console.WriteLine(rec.high_temp);
                        Console.WriteLine(rec.low_temp);
                        Console.WriteLine(rec.ave_temp);

                        //*/
                        list_rec.Add(rec);
                    }
                }                
            }
            catch (Exception e)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

        }
    }

    class Record
    {
        public DateTime date;
        public double high_temp;
        public double low_temp;
        public double ave_temp;
        public double diff_std;
        public bool is_missing;
    }
}
