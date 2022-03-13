using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PiResultPanel : Scenegleton<PiResultPanel>
{
    [SerializeField] private TextMeshProUGUI piResultText;
    [SerializeField] private TextMeshProUGUI dotsCounter;

    public static char FLOAT_SEPARATOR => 1.1f.ToString()[1];
    public const char INT_PART = '3';
    public const string DECIMAL_PART = "1415926535897932";
    public const string WRONG_HEX_COLOR = "FF0000";
    public const string CORRECT_HEX_COLOR = "00FF00";

    private static string SubColoredText(string text, string hexColor)
    {
        return "<color=#" + hexColor + ">" + text + "</color>";
    }

    private static string ColoredPI
    {
        get
        {
            string[] parts = PiCalculator.Instance.closestPI.ToString().Split(FLOAT_SEPARATOR);

            string intPart = parts[0];
            string decimalPart = parts[1];

            string intPartColored = SubColoredText(intPart, intPart[intPart.Length-1] == INT_PART ? CORRECT_HEX_COLOR : WRONG_HEX_COLOR);

            string decimalPartColored = "";
            for (int i = 0; i < decimalPart.Length; i++)
            {
                decimalPartColored += SubColoredText(decimalPart[i].ToString(), decimalPart[i] == DECIMAL_PART[i] ? CORRECT_HEX_COLOR : WRONG_HEX_COLOR);
            }

            return "PI: " + intPartColored + FLOAT_SEPARATOR + decimalPartColored;
        }
    }

    private void OnEnable()
    {
        EventSystem.OnNewRecord += UpdatePI;
    }

    private void OnDisable()
    {
        EventSystem.OnNewRecord -= UpdatePI;
    }

    private void UpdatePI()
    {
        piResultText.text = ColoredPI;
    }

    public void UpdateDotsCounter()
    {
        dotsCounter.text = "Dots: " + PiCalculator.dotCount;
    }
}
