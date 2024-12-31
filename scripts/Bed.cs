public class Bed 
{
    public static void Sleep(Person person, bool print)
    {
        for (int i = 0; i < person.GetSpiritsCount(); i++) 
        {
                int maxHp = person.spirits[i].getHP()[1];
                person.spirits[i].setHP(0, maxHp);
        }
        
        if(print)
        {
            Util.ClearBoard();
            Console.WriteLine(person.name + "'s Spirits have been completely healed!");
            
            Console.WriteLine("Continue...");
            Console.ReadLine();
            //new java.util.Scanner(System.in).next();
        }
    }
}
