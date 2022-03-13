using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiCalculator : Scenegleton<PiCalculator>
{
    public double closestPI { get; private set; }
    public int inside { get; private set; }
    public int outside { get; private set; }

    public static int dotCount => PiCalculator.Instance.inside + PiCalculator.Instance.outside;

    public static double estimatedPI
    {
        get
        {
            try
            {
                int numerator = PiCalculator.Instance.inside * 4;
                int denominator = dotCount;
                return (double)numerator / (double)denominator;
            }
            catch (System.DivideByZeroException)
            {
                return 0;
            }
        }
    }


    private void OnEnable()
    {
        EventSystem.OnSpawnedInside += OnSpawnedInside;
        EventSystem.OnSpawnedOutside += OnSpawnedOutside;
    }

    private void OnDisable()
    {
        EventSystem.OnSpawnedInside -= OnSpawnedInside;
        EventSystem.OnSpawnedOutside -= OnSpawnedOutside;
    }

    private void OnSpawnedInside()
    {
        inside++;
    }

    private void OnSpawnedOutside()
    {
        outside++;
    }

    public static void CalculatePI()
    {
        double newPI = estimatedPI;
        double recordError = System.Math.Abs(PiCalculator.Instance.closestPI - System.Math.PI);
        double newError = System.Math.Abs(newPI - System.Math.PI);
        if (newError < recordError)
        {
            PiCalculator.Instance.closestPI = newPI;
            EventSystem.NewRecord();
        }

        PiResultPanel.Instance.UpdateDotsCounter();
    }
}
