using UnityEngine;
using System.Collections;

public class ButtonRoom : MonoBehaviour {

    public string nameRoom = "Room1";
    public GameObject networkManagerBox;

    public void ClickButton()
    {
        networkManagerBox.SendMessage("ButtonJoinSelect", nameRoom);
    }
}
