
public class Person
{
    public String name;
    public String mapName;
    public int[] mapPosition = new int[] { 0, 0 };
    public Spirit[] spirits = new Spirit[6];

    public int soulStones;//capture new spirit
    public int lesserHpRestores;//restore small amount of hp
    public int hpRestores;//restore some amount of hp
    public int greaterHpRestores;//restore big amount of hp
    public int ultimateHpRestores;//restore all hp

    public int GetSpiritsCount()
    {
        int count = 0;

        for (int i = 0; i < spirits.Length; i++)
        {
            try
            {
                if (spirits[i].getLevel() > 0)
                {
                    count++;
                }
            }
            catch (Exception e)
            {

            }
        }
        return count;
    }
}
