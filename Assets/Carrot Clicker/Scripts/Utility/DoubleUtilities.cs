using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DoubleUtilities 
{
    private static string[] units = { "", "K", "M", "B", "T", "Qa", "Qi", "Sx", "Sp", "Oc", "No", "Dc" };

    public static string ToScientificNotation(double value)
    {
        int unitIndex = 0;

        
        while (value >= 1000 && unitIndex < units.Length - 1)
        {
            value /= 1000;
            unitIndex++;
        }

        
        return string.Format("{0:F2}{1}", value, units[unitIndex]);
    }
}
