using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlabMovement
{
    public static class PrintData
    {
        /// <summary>
        /// Печатает на экране запись
        /// </summary>
        /// <param name="d"></param>
        public static void  Print (DataModel d) 
        {

            Console.WriteLine($"====== {d.Id,4} =================== {d.Height,3} x {d.Width,3} мм");
            
            foreach (var st in d.States)
            {
                
                Console.WriteLine($"{st.ElapsedTime,5} {st.Value,5}");

            }

            if (d.Distances!=null)
                foreach (var x in d.Distances)
                {
                    Console.WriteLine($"{x.Id,5} {x.Distance,5}  {x.ElapsedTime,20}  ∙ {x.SNR1} ");

                }

            if (d.Meterings!=null)
          foreach (var m in  d.Meterings)
            {
                 Console.WriteLine($" {m.Height} x {m.Width}  ");

            }
            //Console.WriteLine("========================= ");

        }


        /// <summary>
        /// Печать объедиенных данных по времени
        /// </summary>
        /// <param name="joined"></param>
        public static void Print(JoinedData joined)
        {
            ConsoleColor save = Console.ForegroundColor;
            if (joined.distance.Speed == 0)
            {
             Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine($" Измерение #{joined.distance.Id,4}  Положение {joined.distance.Distance.ToString("G5"),6} м  - -  {joined.distance.Speed.ToString("G3"),6} м/с    / Сонар {joined.Sonar}  / снр = {joined.distance.SNR1} / {joined.distance.Time}");

            Console.ForegroundColor = save;

            foreach (State s in joined.states)
            {
                Console.WriteLine($"  +STATE:  {s.Value}  - -  {s.Sample} ");
            }

            foreach (Metering m in joined.meterings)
            {
                Console.WriteLine($"  +METER:  {m.DistanceBegin}  - -  {m.MeteringProfiles.Length} -- скорость = {m.Speed,5} │ {m.Height,5} x {m.Width,5} (мм) - -  {(m.IsExclude ?"ИСКЛ" : "ОСТАВ")} ");
            }

            foreach (Profilometerstatistic p in joined.ProfilometerStatistics)
            {
                Console.WriteLine($"  +PROFILE METRIC:  {p.AnalogGain}  - -  {p.Strength}  - - {p.Ip,9} ");
            }

        }

    }
}
