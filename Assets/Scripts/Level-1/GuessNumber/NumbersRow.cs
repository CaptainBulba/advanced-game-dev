[System.Serializable]
public class NumbersRow
{
    public int[] values;

    public int GetLength()
    {
        return values.Length;
    }
}