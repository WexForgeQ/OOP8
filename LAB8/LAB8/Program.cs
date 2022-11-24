using System;

namespace LAB8
{

    class Boss
    {
        public delegate void BossDelegate(string message);
        public event BossDelegate UpgradeEv;
        public event BossDelegate TurnOnEV;
        public event BossDelegate TurnOffEV;



        public string name;
        public int TechMaxvoltage;
        public int StartVoltage = 0;
        static int Max_Voltage = 220;

        public Boss(string nm, int vlt)
        {
            name = nm;
            if (vlt < Max_Voltage)
                TechMaxvoltage = vlt;
            else
            {
                Console.WriteLine("Стандартное напряжение 220+В не может существовать");
                TechMaxvoltage = 0;
            }
        }
        public Boss()
        {
            name = "Default";
            TechMaxvoltage = 10;
        }

        public override string ToString()
        {
            return $"{name}({TechMaxvoltage}V);";
        }

        public void TurnOn()
        {
                StartVoltage = 1;
                TurnOnEV?.Invoke($"Техника {ToString()} начала работу");
                Console.WriteLine($"Текущее напряжение: {StartVoltage}V");
        }
        public void TurnOff()
        {
            StartVoltage = 0;
            TurnOffEV?.Invoke($"Техника {ToString()} закончила работу");
            Console.WriteLine($"Текущее напряжение: {StartVoltage}V");
        }

        public void Upgrade(int vol)
        {
            if (StartVoltage == 0)
            {
                Console.WriteLine($"Для повышения напряжения техника {ToString()} должна начать работу");
            }
            else
            {
                if(StartVoltage + vol > Max_Voltage)
                {
                    UpgradeEv?.Invoke($"Переизбыток напряжения:{name} сгорел(-ла)");
                }
                else
                {
                    StartVoltage += vol;
                    UpgradeEv?.Invoke($"Напряжение повышено: Текущее напряжение {name} : {StartVoltage}V");
                }
            }

        }






    }

 

    class Program
    {
        static void Main(string[] args)
        {
            Boss B = new Boss("Грелка", 100);
            B.TurnOnEV += Message;
            B.UpgradeEv += Message;
            B.TurnOffEV += Message;
            B.TurnOn();
            B.Upgrade(10);
            B.TurnOff();
            B.Upgrade(10);
            B.TurnOn();
            B.Upgrade(230);
        }
        public static void Message(string message) => Console.WriteLine(message);
    }
}
