using System;
using System.Collections.Generic;
using System.Linq;

public class Util<T>
{
    public T[] Shuffle(T[] array)
    {
        T[] shuffled = array;
        for (int i = 0; i < array.Length - 2; i++)
        {
            var rand = new Random();
            int j = rand.Next(0, array.Length);
            (shuffled[i], shuffled[j]) = (shuffled[j], shuffled[i]);
        }

        return shuffled;
    }
}