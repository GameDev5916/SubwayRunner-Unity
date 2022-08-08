using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EffectUpScore : MonoBehaviour {

    public int timeEffect = 10;
    private int runTime = 0;
    private int timeHalfEffect = 5;
    public Color colorEffect = Color.yellow;
    public int addSizeFont = 5;
    private Text myText;
    private bool mesEffect = false;
    private Color oldColor = Color.white;
    private float oldSize = 20;
    private float stepScore = 0;
    private Color stepColor = Color.white;
    private float stepSize = 0;
    private bool runUp = true;
    private float scoreNew = 0;
    private float scoreRun = 0;

    void Start()
    {
        timeHalfEffect = timeEffect / 2;
        myText = transform.GetComponent<Text>();
        oldColor = myText.color;
        oldSize = myText.fontSize;
    }

    public void StartEffect(float score)
    {
        scoreNew = score;
        stepScore = score / (float)timeEffect;
        stepColor = new Color(colorEffect.r - oldColor.r, colorEffect.g - oldColor.g, colorEffect.b - oldColor.b, colorEffect.a - oldColor.a) / (float)timeHalfEffect;
        stepSize = (float)addSizeFont / (float)timeHalfEffect;
        scoreRun = 0;
        runTime = 0;
        mesEffect = true;
        runUp = true;
    }

    void FixedUpdate()
    {
        if (mesEffect)
        {
            scoreRun += stepScore;
            myText.text = Mathf.RoundToInt(scoreRun).ToString();
            if (runTime >= timeHalfEffect)
            {
                if (runUp)
                {
                    runUp = false;
                    runTime = 0;
                }
                else
                {
                    mesEffect = false;
                    myText.text = Mathf.RoundToInt(scoreNew).ToString();
                    myText.color = oldColor;
                    myText.fontSize = Mathf.RoundToInt(oldSize);
                }
            }
            else
            {
                runTime++;
                if (runUp)
                {
                    myText.color += stepColor;
                    myText.fontSize = Mathf.RoundToInt(myText.fontSize + stepSize);

                }
                else
                {
                    myText.color -= stepColor;
                    myText.fontSize = Mathf.RoundToInt(myText.fontSize - stepSize);
                }
            }
        }
    }
}
