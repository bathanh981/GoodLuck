using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodLuck.Model.Material
{
    class MaterialDB
    {
        public static ObservableCollection<ConcreteProperties> ConcreteProperties = new ObservableCollection<ConcreteProperties>()
        {
            new ConcreteProperties("B15",8.5,0.75,24000,11,1.1),
            new ConcreteProperties("B20",11.5,0.9,27500,15,1.35),
            new ConcreteProperties("B22,5",13,1,28500,16.5,1.45),
            new ConcreteProperties("B25",14.5,1.1,30000,18.5,1.55),
            new ConcreteProperties("B30",17,1.2,32500,22,1.75),
            new ConcreteProperties("B35",19.5,1.3,34500,25.5,1.95),
            new ConcreteProperties("B40",22,1.4,36000,29,2.1),
            new ConcreteProperties("B45",25,1.5,37000,32,2.25),
            new ConcreteProperties("B50",27.5,1.6,38000,36,2.45),
            new ConcreteProperties("B55",30,1.7,39000,39.5,2.6),
            new ConcreteProperties("B60",33,1.8,39500,43,2.75)
        };
        public static ConcreteProperties GetConcreteProperties(string nameConcrete)
        {
            foreach (ConcreteProperties concretes in ConcreteProperties)
            {
                if (concretes.ConcreteName.Equals(nameConcrete))
                {
                    return concretes;
                }
            }

            return null;
        }
        public static ObservableCollection<SteelProperties> SteelProperties = new ObservableCollection<SteelProperties>()
       {
           new SteelProperties("CB240-T",240,210,210,170,200000),
           new SteelProperties("CB300-T",300,260,260,210,200000),
           new SteelProperties("CB300-V",300,260,260,210,200000),
           new SteelProperties("CB400-V",400,350,350,280,200000),
           new SteelProperties("CB500-V",500,435,400,300,200000),
           new SteelProperties("SD460",400,348,302,280,200000),
           new SteelProperties("SD490",490,426,371,300,200000)
       };

        public static SteelProperties GetSteelProperties(string nameSteel)
        {
            foreach (SteelProperties steels in SteelProperties)
            {
                if (steels.SteelName.Equals(nameSteel))
                {
                    return steels;
                }
            }
            return null;
        }
    }
}
