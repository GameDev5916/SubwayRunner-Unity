using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeImageClick : MonoBehaviour {

    public string codeObject = "";
    public Sprite spriteFrontButton;
    public Color colorFontButton;
    public Sprite spriteOriginButton;
    public Color colorOriginButton;

    public void ButtonClick(string nameFunction)
    {
        Camera.main.SendMessage(nameFunction, codeObject);
        OpenImageFront();
    }

    public void OpenImageFront()
    {
        transform.GetComponent<Image>().sprite = spriteFrontButton;
        transform.GetComponent<Image>().color = colorFontButton;
        Modules.SetLayer(transform.GetChild(0).gameObject, "Default");
        if (transform.GetChild(1).GetComponent<RotateZModels>() != null)
            transform.GetChild(1).GetComponent<RotateZModels>().StartRotate();
    }

    public void CloseImageFront()
    {
        transform.GetComponent<Image>().sprite = spriteOriginButton;
        transform.GetComponent<Image>().color = colorOriginButton;
        Modules.SetLayer(transform.GetChild(0).gameObject, "MCG-Night");
        if (transform.GetChild(1).GetComponent<RotateZModels>() != null)
            transform.GetChild(1).GetComponent<RotateZModels>().StopRotate();
    }
}
