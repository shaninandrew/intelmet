using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlabMovement
{
    /// <summary>
    /// Класс склеивает данные в DataModel по общим полям и формирует единый список
    /// </summary>
    public class Joiner
    {
        /// <summary>
        /// Объединенные данные дистанций (_Distance) и положений (State)
        /// </summary>
        public List<JoinedData> joined_data { get; }

        /// <summary>
        /// Объединитель данных
        /// </summary>
        /// <param name="model"></param>
        public Joiner (DataModel model)
        {
            joined_data = new List<JoinedData> ();

            foreach (_Distance d in model.Distances)
            {
                JoinedData j = new JoinedData ();

                int glue = d.ElapsedTime;
                
                j.distance = d;
                j.states = model.States.Where(x => x.ElapsedTime == glue).ToArray();

                j.meterings = model.Meterings.Where(x => x.ElapsedTime == glue).ToArray();

                j.Sonar = d.SNR1;

                j.ProfilometerStatistics = model.ProfilometerStatistics;
                //model.ProfilometerStatistics

                //Коллекция данных
                joined_data.Add (j);
            }

        }
    }

    /// <summary>
    /// Сопоставленные данные дистаниций и положений
    /// </summary>
    public class JoinedData
    {
        /// <summary>
        /// Дистанции
        /// </summary>
        public _Distance distance { set; get; }
        
        /// <summary>
        /// Положения
        /// </summary>
        public  State[] states { set; get; }

        /// <summary>
        /// Измерения
        /// </summary>
        public Metering[] meterings { set; get; }

        /// <summary>
        /// УРовень шума
        /// </summary>
        public float Sonar { set; get; }

        public Profilometerstatistic [] ProfilometerStatistics { set; get; }


        public JoinedData()
        { 
            
            
        }
    }

}
