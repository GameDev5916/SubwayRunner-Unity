using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof (Button))]
public class ButtonStatus : MonoBehaviour {

    public Image myBackground;
    public Text myText;
    public Image myIcon;
    public Color enableColorBackground;
    public Color disableColorBackground;
    public Color enableColorText;
    public Color disableColorText;

    public void Disable()
    {
        GetComponent<Button>().interactable = false;
        myBackground.color = disableColorBackground;
        if (myText != null) myText.color = disableColorBackground;
        if (myIcon != null) myIcon.color = disableColorBackground;
    }

    public void Enable()
    {
        GetComponent<Button>().interactable = true;
        myBackground.color = enableColorBackground;
        if (myText != null) myText.color = enableColorText;
        if (myIcon != null) myIcon.color = enableColorText;
    }
}
