using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace SlabMovement
{
    internal class Statistic
    {

        private long Count = 0;
        private float Mean = 0;


        private float Min = 0;
        
        public float GetMin { get { return Min; } }
        
        
        private float Max = 0;
        public float GetMax { get { return Max; } }


        /// <summary>
        /// Дисперсия
        /// </summary>
        private float Disper = 0;


        /// <summary>
        /// СКО
        /// </summary>
        private float MS = 0;

        /// <summary>
        /// Возвращает текущее значение среднего
        /// </summary>
        public float GetMean { get { return Mean; }  }
        
        /// <summary>
        /// СКО измерения
        /// </summary>
        public float GetSKO { get { return MS; } }


        /// <summary>
        /// Дисперсия
        /// </summary>
        public float GetDispersion { get { return Disper; } }

        /// <summary>
        /// Поточный вычислитель средних значения
        /// </summary>
        /// <param name="FirstValue"></param>
        public Statistic(float FirstValue)
        {
            //первое значение
            Mean = FirstValue;
            Max= FirstValue;
            Min = FirstValue;
            Count++;
            Disper = 0; MS = 0;
        }

        /// <summary>
        /// Возвращает среднее значение
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public float Next(float Value)
        { 
            Mean = (Value  + (float)Count * Mean)/ (float)(Count+1);

            Count++;

            Disper =  ( (float) Math.Pow ( (Value  - Mean) , 2) + Disper * (Count-1)) / (Count);

            MS =(float) Math.Sqrt(Disper);

            if (Value < Min) { Min = Value; }
            if (Value > Max) { Max = Value; }


            return Mean;
        }

    }


    /// <summary>
    /// Расчет статистики для потока
    /// </summary>
    class StatisticalMaster 
    {

        public Statistic stat_Sonar;
        public Statistic stat_Speed;


        /// <summary>
        /// Для расчета статистики нужен пакет данных Joiner.
        /// Вычисляет статистику
        /// </summary>
        /// <param name="joiner"></param>
        /// 
        public StatisticalMaster(Joiner joiner)
        {
            int i = 0;
            foreach (var j in joiner.joined_data)
            {

                if (i == 0)
                {
                    stat_Sonar = new Statistic(j.Sonar);
                    stat_Speed = new Statistic(j.distance.Speed);
                }
                else
                {
                    stat_Sonar.Next(j.Sonar);
                    stat_Speed.Next(j.distance.Speed);
                }

               i++;
            }    
        
        }

        /// <summary>
        /// Конструктор статистического анализатора для отдельных выборок данных.
        
        /// </summary>
      
        public StatisticalMaster(List<JoinedData> joined_data)
        {
            int i = 0;
            foreach (var j in joined_data)
            {

                if (i == 0)
                {
                    stat_Sonar = new Statistic(j.Sonar);
                    stat_Speed = new Statistic(j.distance.Speed);
                }
                else
                {
                    stat_Sonar.Next(j.Sonar);
                    stat_Speed.Next(j.distance.Speed);
                }

                i++;
            }

        }

    }

}
