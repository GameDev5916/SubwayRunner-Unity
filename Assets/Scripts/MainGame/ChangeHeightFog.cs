using UnityEngine;
using System.Collections;

public class ChangeHeightFog : MonoBehaviour {

    public float maxHeight = 50f;
    public Vector2 fogOrigin = new Vector2(70, 130);
    public Vector2 fogMinValue = new Vector2(0, 30);
    public Material matBGFakeCity;
    public Color colorOrigin = Color.white;

    void FixedUpdate()
    {
        if (Modules.mainCharacter == null || Modules.statusGame != StatusGame.flyScene) return;
        float percentX = Modules.mainCharacter.transform.position.y / maxHeight;
        float percentA = fogOrigin.x - percentX * fogOrigin.x;
        float percentB = fogOrigin.y - percentX * fogOrigin.y;
        float percentC = 1 - percentX;
        if (percentA <= fogMinValue.x) percentA = fogMinValue.x;
        if (percentB <= fogMinValue.y) percentB = fogMinValue.y;
        if (percentC < 0) percentC = 0;
        RenderSettings.fogStartDistance = percentA;
        RenderSettings.fogEndDistance = percentB;
        matBGFakeCity.color = new Color(colorOrigin.r, colorOrigin.g, colorOrigin.b, percentC);
    }

    public void ResetValue()
    {
        RenderSettings.fogStartDistance = fogOrigin.x;
        RenderSettings.fogEndDistance = fogOrigin.y;
        matBGFakeCity.color = colorOrigin;
    }
}
