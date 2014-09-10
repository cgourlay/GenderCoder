


















using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GenderCoder.Entities;

namespace GenderCoder
{
    public static class Processor
    {
        static Processor()
        {
            GenderCodingNames.AllGenderCodingNames.Refresh();
            GenderCodingNames.UnitesStatesNames.Refresh();
            GenderCodingNames.ForeignNames.Refresh();
            GenderCodingNames.WildCardNames.Refresh();
        }

      
        public delegate void ProgressChangedEventHandler(decimal PercentComplete);

        public static event ProgressChangedEventHandler ProgressChanged;

      

      
        private static object ThreadLock = new object();

        private static List<GenderCodingResult> Results;

      
        
        public static Gender GetGender(string FirstName)
        {
            return LookupName(FirstName);
        }


        private static void ConfigureGenderCodingResults(List<string> FirstNames)
        {
            Results = new List<GenderCodingResult>();

            int row = 1;

            foreach (string s in FirstNames)
            {
                Results.Add(new GenderCodingResult(s, row));

                row++;
            }
        }


        private static void ProcessInputRecords()
        {
            ConcurrentQueue<GenderCodingResult> ScanQueue = new ConcurrentQueue<GenderCodingResult>();

            foreach (GenderCodingResult result in Results)
            {
                ScanQueue.Enqueue(result);
            }

            //Upping the thread count to slightly above the processor count appears to maximize performance...?
            for (int i = 1; i <= (Environment.ProcessorCount + 2); i++)
            {
                Thread T = new Thread(() =>
                {
                    while (true)
                    {
                        GenderCodingResult name;

                        if (ScanQueue.TryDequeue(out name))
                        {
                            name.Gender = LookupName(name.FirstName);
                        }
                        else
                        {
                            break;
                        }
                    }
                });

                T.Start();
            }

            int Remaining = Results.Count;

            while (Remaining > 0)
            {
                ReportProgress(Results.Count, Remaining);

                Thread.Sleep(2000);

                lock (ThreadLock)
                {
                    Remaining = Results.Where(x => x.Processed == false).ToList().Count;
                }
            }
        }

        private static void RunThread(List<GenderCodingResult> WorkingRecords)
        {
            foreach (GenderCodingResult record in WorkingRecords)
            {
                record.Gender = LookupName(record.FirstName);

                record.Processed = true;
            }
        }

        private static Gender LookupName(string FirstName)
        {
            string workingFirstName = FirstName.Trim();

            //Remove any intials at the end of the string 
            while (workingFirstName.Contains("."))
            {
                int dotIndex = workingFirstName.IndexOf(".");

                int spaceIndex = dotIndex - 1;

                while (workingFirstName[spaceIndex] != ' ')
                {
                    spaceIndex--;

                    if (spaceIndex == -1)
                    {
                        break;
                    }
                }

                workingFirstName = workingFirstName.Remove(spaceIndex + 1, dotIndex - spaceIndex).Trim();
            }

            if (workingFirstName.Length < 1)
            {
                return Gender.Unknown;
            }

            //Trim the string, then replace any spaces or hyphens with the "+" wildcard
            workingFirstName = workingFirstName.Trim().Replace(" ", "+").Replace("-", "+");

            //If the name contains a wildcard, run it against a subset of wildcard-containing names
            if (workingFirstName.Contains("+"))
            {
                foreach (GenderCodingName name in GenderCodingNames.WildCardNames)
                {
                    if (String.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        return name.Sex;
                    }
                }
            }
            else
            {
                //Otherwise, attempt a case-insensitive compare against US-Only names
                foreach (GenderCodingName name in GenderCodingNames.UnitesStatesNames)
                {
                    if (String.Equals(workingFirstName, name.FirstName, StringComparison.OrdinalIgnoreCase))
                    {
                        return name.Sex;
                    }
                }
            }

            //If we've still got nothing, try a  deep case-insensitive compare...
            foreach (GenderCodingName name in GenderCodingNames.ForeignNames)
            {
                //Remove wildcard from the gender coding name, if it exists
                string compare = name.FirstName.Contains("+") ? name.FirstName.Replace("+", "") : name.FirstName;

                if (String.Equals(workingFirstName, compare, StringComparison.OrdinalIgnoreCase))
                {
                    return name.Sex;
                }
            }

            return Gender.Unknown;
        }

        private static void ReportProgress(int TotalRecords, int RemainingRecords)
        {
            decimal ProgressPercent = decimal.Divide((decimal)(TotalRecords - RemainingRecords), (decimal)TotalRecords);

            if (ProgressChanged != null)
            {
                ProgressChanged(ProgressPercent);
            }

            System.Diagnostics.Debug.Print("Gender Coding Process : " + ProgressPercent.ToString("0.000%") + " complete!");
        }

    }
}
