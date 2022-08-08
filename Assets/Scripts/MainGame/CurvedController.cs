using UnityEngine;
using System.Collections;
using VacuumShaders.CurvedWorld;

public class CurvedController : MonoBehaviour
{
    private CurvedWorld_Controller myCurved;
    public float maxXBend, minXBend, maxXBias, minXBias, maxYBend, minYBend, maxYBias, minYBias;
    public Vector2 speedXBend = new Vector2(-0.01f, 0.01f);
    public Vector2 speedYBend = new Vector2(-0.01f, 0.01f);
    public Vector2 speedXBias = new Vector2(-0.01f, 0.01f);
    public Vector2 speedYBias = new Vector2(-0.01f, 0.01f);
    private bool mesRunXBend = false, mesRunYBend = false, mesRunXBias = false, mesRunYBias = false;
    private float valueXBend = 0, valueYBend = 0, valueXBias = 0, valueYBias = 0;

	void Start () {
        if (Modules.curvedWorld == 0) gameObject.SetActive(false);
        myCurved = transform.GetComponent<CurvedWorld_Controller>();
	}

    void FixedUpdate()
    {
        if (Modules.statusGame != StatusGame.play) return;
        //xu ly XBEND
        if (mesRunXBend)
        {
            myCurved._V_CW_Bend_X += valueXBend;
            if (myCurved._V_CW_Bend_X > maxXBend)
            {
                myCurved._V_CW_Bend_X = maxXBend;
                mesRunXBend = false;
            }
            else if (myCurved._V_CW_Bend_X < minXBend)
            {
                myCurved._V_CW_Bend_X = minXBend;
                mesRunXBend = false;
            }
        }
        else
        {
            valueXBend = Random.Range(speedXBend.x, speedXBend.y) * Modules.speedGame;
            mesRunXBend = true;
        }
        //xu ly YBEND
        if (mesRunYBend)
        {
            myCurved._V_CW_Bend_Y += valueYBend;
            if (myCurved._V_CW_Bend_Y > maxYBend)
            {
                myCurved._V_CW_Bend_Y = maxYBend;
                mesRunYBend = false;
            }
            else if (myCurved._V_CW_Bend_Y < minYBend)
            {
                myCurved._V_CW_Bend_Y = minYBend;
                mesRunYBend = false;
            }
        }
        else
        {
            valueYBend = Random.Range(speedYBend.x, speedYBend.y) * Modules.speedGame;
            mesRunYBend = true;
        }
        //xu ly XBIAS
        if (mesRunXBias)
        {
            myCurved._V_CW_Bias_X += valueXBias;
            if (myCurved._V_CW_Bias_X > maxXBias)
            {
                myCurved._V_CW_Bias_X = maxXBias;
                mesRunXBias = false;
            }
            else if (myCurved._V_CW_Bias_X < minXBias)
            {
                myCurved._V_CW_Bias_X = minXBias;
                mesRunXBias = false;
            }
        }
        else
        {
            valueXBias = Random.Range(speedXBias.x, speedXBias.y) * Modules.speedGame;
            mesRunXBias = true;
        }
        //xu ly YBIAS
        if (mesRunYBias)
        {
            myCurved._V_CW_Bias_Y += valueYBias;
            if (myCurved._V_CW_Bias_Y > maxYBias)
            {
                myCurved._V_CW_Bias_Y = maxYBias;
                mesRunYBias = false;
            }
            else if (myCurved._V_CW_Bias_Y < minYBias)
            {
                myCurved._V_CW_Bias_Y = minYBias;
                mesRunYBias = false;
            }
        }
        else
        {
            valueYBias = Random.Range(speedYBias.x, speedYBias.y) * Modules.speedGame;
            mesRunYBias = true;
        }
    }
}
