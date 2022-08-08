using UnityEngine;
using System.Collections;

public class ColorBGWinLose : MonoBehaviour {

    public Material matBackground;
    public Material matGlow;
    public Color winBackground, loseBackground;
    public Color winGlow, loseGlow;

    public void SetColor(bool winStatus)
    {
        if (winStatus)
        {
            matBackground.color = winBackground;
            matGlow.color = winGlow;
        }
        else
        {
            matBackground.color = loseBackground;
            matGlow.color = loseGlow;
        }
    }
}
