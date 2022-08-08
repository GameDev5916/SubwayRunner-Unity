using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializePurchaserButton : MonoBehaviour {
    public ModificationType Type;
    public GameObject itemButton;

	void Start () {
        CreateButton();
	}
	
	void CreateButton () {
        foreach (var item in Purchaser.Instance.productIAP) {
            if (item.Type == Type) {
                GameObject temp = Instantiate(itemButton, transform, false) as GameObject;
                temp.GetComponent<ProductButton>().UpdateData(item);
            }
        }

        itemButton.SetActive(false);
	}
}
