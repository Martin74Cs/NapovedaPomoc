namespace NapovedaPomoc
{
    public class Singleton
    {
        //konstruktor
        //Vytvořené Konstruktoru z venku musí být zakázáno.
        private Singleton() { }
        public int Cislo { get; set; }
        //instalce
        private static Singleton? instance;
        

        public static Singleton Get()
        { 
            if(instance == null)
                instance = new Singleton();
            return instance;
        }
    }

    public class Pouziti
    {
        public void Priklad()
        { 
            var s1 = Singleton.Get();
            s1.Cislo = 12345;
            var s2 = Singleton.Get();
            //v s2.Cislo - je číslo s S1 protože je to stejná instance
            Console.WriteLine(s2.Cislo);

            new Pouziti2().Priklad();
        }
    }

    public class Pouziti2
    {
        public void Priklad()
        { 
            var s1 = Singleton.Get();
            //v jiné třídě stíle zustává hodnota s2.Cislo - je číslo s S1
            Console.WriteLine(s1.Cislo);
            //Samostatně musí být konstruktor zakázán
            //var s2 = new Singleton();
            Console.WriteLine(Singleton.Get().Cislo);
        }
    }




}
