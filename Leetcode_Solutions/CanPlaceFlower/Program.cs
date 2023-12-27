// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

public class Solution
{
    public bool CanPlaceFlowers(int[] flowerbed, int n)
    {
        int count = 0;

        for (int i = 0; i < flowerbed.Length; i++)
        {
            if (flowerbed[i] == 0 && (i == 0 || flowerbed[i - 1] == 0) &&
                (i == flowerbed.Length - 1 || flowerbed[i + 1] == 0))
            {
                count++;
                i++; 
            }

            if (count >= n)
            {
                return true;
            }
        }

        return false;
    }
}