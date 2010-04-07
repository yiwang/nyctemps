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
        static DateTime start_date;
        static DateTime end_date;

        static void Main(string[] args)
        {
            read_data();
            start_date = list_rec.First().date;
            end_date = list_rec.Last().date;
            Console.WriteLine(start_date + "," + end_date);

            Console.WriteLine("Part 1");
            part1(false);

            Console.WriteLine("Part 2");
            part2();

            Console.WriteLine("Part 3");
            part3();

            Console.ReadLine();
        }

        private static void part3()
        {
            Console.WriteLine("not implemented, but mean for each record is calclued");
        }

        private static void part2()
        {
            part1(true);
        }

        private static void part1(bool print_value_for_part2)
        {
            list_rec2 = new List<Record>();
            array_rec = list_rec.ToArray();
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
                        if (print_value_for_part2)
                        {
                            Console.WriteLine(next_date.ToString("dd-MMM-yyyy") + "," + new_rec.high_temp.ToString() + "," + new_rec.low_temp.ToString());
                        }
                        else
                        {
                            Console.WriteLine(next_date.ToString("dd-MMM-yyyy"));
                        }
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
        public int std_status;
    }



}
