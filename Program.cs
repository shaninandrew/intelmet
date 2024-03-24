namespace SlabMovement
{
    internal class Program
    {

        
        static void Main(string[] args)
        { 


            

            Console.WriteLine("Считывается json файл ...");

            string[] files =  System.IO.Directory.GetFiles("data\\", "*.json", SearchOption.AllDirectories);


            List<DataModel> dataModels = new List<DataModel>();
            foreach (string file in files)
            {
                Console.Write ($" \r {file,40}"); 
                DataModel model = new FileReader().Read(file);
                model.Filename = file;
                dataModels.Add(model);
            }

            Console.WriteLine($"\rСчитано {dataModels.Count,30} моделей.");

            
            

            foreach (DataModel m in dataModels) 
            {
                Joiner joiner = new Joiner (m);

                StatisticalMaster stat_master = new StatisticalMaster(joiner);
                Console.WriteLine($"Средний уровень шума: {stat_master.stat_Sonar.GetMean,5}  +/- {stat_master.stat_Sonar.GetSKO,5}  ");
                Console.WriteLine($"Средняя скорость:     {stat_master.stat_Speed.GetMean,5}  +/- {stat_master.stat_Speed.GetSKO,5}  ");

                var silent_speed = stat_master.stat_Speed.GetMean - stat_master.stat_Speed.GetSKO;
                if (silent_speed < 0)
                {
                    //если отклонение выше среднего
                    // то берем среднее
                    silent_speed = stat_master.stat_Speed.GetMean;
                }

                Console.WriteLine($"Самый тихий режим работы на тихой скорости  от 0... {silent_speed}  м/с ");

                Console.Write ($"Отбираем данные, у которых скорость менее {silent_speed}  м/с ");

                List<JoinedData> selected = joiner.joined_data.Where(x=>x.distance.Speed<=silent_speed).ToList();
                
                Console.WriteLine($" ок. Отобрано {selected.Count} показаний.");

                StatisticalMaster stat_selected = new StatisticalMaster(selected);
                Console.WriteLine($"Средний уровень шума: {stat_selected.stat_Sonar.GetMean,5}  +/- {stat_selected.stat_Sonar.GetSKO,5}  ");
                Console.WriteLine($"                      {stat_selected.stat_Sonar.GetMin,5} ...   {stat_selected.stat_Sonar.GetMax,5}  ");

                Console.WriteLine($"Средняя скорость:     {stat_selected.stat_Speed.GetMean,5}  +/- {stat_selected.stat_Speed.GetSKO,5}  ");

                Console.WriteLine("");

                float silence = stat_selected.stat_Sonar.GetMean;
                float min_silence = stat_selected.stat_Sonar.GetMin;


                Console.Write($"Предельное значение шума MIN {min_silence} ... MAX {silence}. Отберем данные, где работа  ведется границах указанных значений.   ");

                List<JoinedData> silent_process = joiner.joined_data.Where(x => x.Sonar <= silence).ToList();
                StatisticalMaster stat_silent = new StatisticalMaster(silent_process);

                Console.WriteLine($" Отобрано {silent_process.Count} показаний.");

                Console.WriteLine($"Средний уровень шума в данной выборке: {stat_silent.stat_Sonar.GetMean,5}  +/- {stat_silent.stat_Sonar.GetSKO,5}  ");
                Console.WriteLine($"                                       {stat_silent.stat_Sonar.GetMin,5} ...  {stat_silent.stat_Sonar.GetMax,5}  ");

                float MaxDB =  (float) Math.Log10(stat_silent.stat_Sonar.GetMax)*10;
                float MeanDB = (float)Math.Log10(stat_silent.stat_Sonar.GetMean)*10;
                float LowDB = (float)Math.Log10(stat_silent.stat_Sonar.GetMin)* 10;

                // определяем в ДБ какое значение среднее - шумное -1 нужно понижать или тихое 1 нужно повышать
                float NearToSilence = (MaxDB - MeanDB) / (MaxDB - LowDB) < 0.5 ? -1 : 1;


                Console.WriteLine($"  Перевод в дцБ:  MAX - Среднее = {MaxDB} - {MeanDB} = {MaxDB-MeanDB}    Среднее - Мин = {MeanDB} - {LowDB} = {MeanDB - LowDB} дБА   ");

                Console.WriteLine("");

                //переводим назад в средние сонара 
                //если среднее завышено то новое среднее будет средним от Дб среднего и малого или от максимума и среднего
                float MeanDBA = NearToSilence < 0 ? (MeanDB + LowDB) / 2 : (MeanDB + MaxDB) / 2;
                float NewMeanSonar = (float) Math.Pow(10,MeanDBA / 10);
                Console.WriteLine($"  Cреднее {MeanDB} дБ [ {(NearToSilence<0?"Завышено":"Занижено")} ] относительно значений");

                Console.WriteLine($"Принимаем в дцБ: среднее {MeanDBA} дБ [ {NearToSilence} ]-> Сонар {NewMeanSonar} < тишина");
                Console.WriteLine("");

                //Окончательно отбираем данные в тишине
                selected = joiner.joined_data.Where(x => x.Sonar <= NewMeanSonar).ToList();
                Console.WriteLine($"Отобрано тихих {selected.Count} данных... Дистанции и из ID для ");


                Console.WriteLine($"Файл: {m.Filename} ===================== [НАЧАЛО]");
                foreach (JoinedData data in selected)
                    Console.WriteLine(data.distance.Distance.ToString()+" "+data.distance.Id);


                Console.WriteLine($"Файл: {m.Filename} =====================  [КОНЕЦ]");
                Console.WriteLine("");

                //PrintData.Print(data);

                //  PrintData.Print (m);
                //break;
            }



        }
    }
}
