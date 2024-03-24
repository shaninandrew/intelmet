using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlabMovement
{


    /// <summary>
    /// Модель Json
    /// </summary>
    public class DataModel
    {

       public string Filename { get; set; }
        public int AvgCountProfilePoints { get; set; }
        public string CompleteDesc { get; set; }
        public DateTime DateMeasuring { get; set; }
        public DateTime DateReceipt { get; set; }
        public DateTime DateTrace { get; set; }
        public float DeltaSpeed { get; set; }
        public float DistanceFactor { get; set; }

        /// <summary>
        /// Расстояния
        /// </summary>
        public _Distance[] Distances { get; set; }

        public int ExpositionAdapting { get; set; }
        public float Height { get; set; }
        public float HeightCalculate { get; set; }
        public float HeightLeft { get; set; }
        public float HeightRight { get; set; }
        public string Id { get; set; }
        public string IdTrace { get; set; }
        public object ImageTagRef { get; set; }
        public bool IsByPass { get; set; }
        public bool IsComplete { get; set; }
        public bool IsFeed { get; set; }
        public bool isPlanFind { get; set; }
        public bool IsRecord { get; set; }
        public bool IsRollback { get; set; }
        public bool isSeriesSlabsFind { get; set; }


        /// <summary>
        /// Последние измерение
        /// </summary>
        public Metering LastMetering { get; set; }

        public float Length { get; set; }
        public float LengthCalculate { get; set; }
        public float LengthPlan { get; set; }
        public float LengthSeries { get; set; }
        public int MaxCountProfilePoints { get; set; }


        /// <summary>
        /// Измерения
        /// </summary>
        public Metering[] Meterings { get; set; }


        public long MinCountProfilePoints { get; set; }
        public float OpticLengthFactor { get; set; }
        public long ProfileCount { get; set; }

        /// <summary>
        /// Bpvtht
        /// </summary>
        public Profilometerstatistic[] ProfilometerStatistics { get; set; }

        public int SlabType { get; set; }
        public float SNR { get; set; }
        public float SNRThreshold { get; set; }
        public float SpeedThreshold { get; set; }
        public State[] States { get; set; }
        public Swcyclogram swCyclogram { get; set; }
        public int TempSlab { get; set; }
        public float TempVelocimeter { get; set; }
        public int TraceXMax { get; set; }
        public int TraceXMin { get; set; }
        public int VideoWriterId { get; set; }
        public float Volume { get; set; }
        public float Weight { get; set; }
        public float Width { get; set; }
        public float WidthCalculate { get; set; }
        public float WidthLeft { get; set; }
        public float WidthPlan { get; set; }
        public float WidthRight { get; set; }
    }

   

    public class Meteringprofile
    {
        public int Id { get; set; }
        public _Line[] Lines { get; set; }
        public Point[] Points { get; set; }
        public string ProfilometerId { get; set; }
        public int RangePointsCount { get; set; }
        public int TotalLengthVerticalLines { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class _Line
    {
        public float Angle { get; set; }
        public float AngleHorizontal { get; set; }
        public Line Line { get; set; }  //была 1 линия   []                 
    }

    public class Line
    {
        public Direction Direction { get; set; }
        public float Length { get; set; }
        public string P1 { get; set; }
        public string P2 { get; set; }
    }

    public class Direction
    {
        public bool IsEmpty { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Point
    {
        public int Spot { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Swcyclogram
    {
        public string Elapsed { get; set; }
        public long ElapsedMilliseconds { get; set; }
        public long ElapsedTicks { get; set; }
        public bool IsRunning { get; set; }
    }

    public class _Distance
    {
        public float Distance { get; set; }
        public int ElapsedTime { get; set; }
        public int Id { get; set; }
        public float SNR1 { get; set; }
        public float Speed { get; set; }
        public DateTime Time { get; set; }
    }


    public class Metering
    {
        public DateTime DateTime { get; set; }
        public float Distance { get; set; }
        public float DistanceBegin { get; set; }
        public long ElapsedTime { get; set; }
        public float Height { get; set; }
        public float HeightLeft { get; set; }
        public float HeightRight { get; set; }
        public bool IsExclude { get; set; }
        public Meteringprofile[] MeteringProfiles { get; set; }
        public float Speed { get; set; }
        public int Tag1 { get; set; }
        public int Tag2 { get; set; }
        public float Width { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
   



    public class Profilometerstatistic
    {
        public string AnalogGain { get; set; }
        public int Exposition { get; set; }
        public int Gain { get; set; }
        public string Ip { get; set; }
        public string IpShort { get; set; }
        public string IpSide { get; set; }
        public float PointsAverage { get; set; }
        public int Strength { get; set; }
    }

    /// <summary>
    /// Уровень звука?
    /// </summary>
    public class State
    {
        public long ElapsedTime { get; set; }
        public int Sample { get; set; }
        public string Series { get; set; }
        public int Tag { get; set; }
        public int Value { get; set; }
    }




}
