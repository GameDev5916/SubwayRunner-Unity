using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductButton : MonoBehaviour {
    public string Name; //Ten trung voi ten co trong Name list product IAP
    string productID;
    public Text title, description, price, modification, textButton;
    public Image image, iconSale;

    void OnEnable()
    {
        int iLang = Modules.indexLanguage;
        title.font = AllLanguages.listFontLangA[iLang];
        description.font = AllLanguages.listFontLangB[iLang];
        textButton.font = AllLanguages.listFontLangA[iLang];
        if (Name != "") UpdateData(Name);
    }

    void UpdateData(string productName)
    {
        foreach (var item in Purchaser.Instance.productIAP)
        {
            if (item.Name == productName)
            {
                if (textButton != null) textButton.text = AllLanguages.shopButtonBuy[Modules.indexLanguage];
                if (title != null) title.text = AllLanguages.shopPackage[item.indexTitle][Modules.indexLanguage];
                if (description != null) description.text =
                    AllLanguages.shopStartPurchase[Modules.indexLanguage] + " " + item.Count + " " +
                    (item.Type == ModificationType.Coin ? AllLanguages.shopEndCoinPurchase[Modules.indexLanguage] : AllLanguages.shopEndKeysPurchase[Modules.indexLanguage]);
                if (modification != null)
                {
                    if (item.Count > 1)
                        modification.text = item.Count + " " + item.Type + "s";
                    else
                        modification.text = item.Count + " " + item.Type;
                }
                if (price != null && Purchaser.Instance.IsInitialized())
                {
                    string priceStr = Purchaser.m_StoreController.products.WithID(item.ID).metadata.localizedPriceString.ToString();
                    if (priceStr != "")
                        price.text = priceStr;
                    else price.text = "????";
                }

                if (image != null && item.Image != null) image.sprite = item.Image;
                if (item.iconSale != null)
                {
                    iconSale.sprite = item.iconSale;
                    iconSale.color = new Color(1, 1, 1, 1);
                }
                else iconSale.color = new Color(1, 1, 1, 0);
                productID = item.ID;
                break;
            }
        }
    }

    public void UpdateData(ProductIAP data) {
        if (textButton != null) textButton.text = AllLanguages.shopButtonBuy[Modules.indexLanguage];
        if (title != null) title.text = AllLanguages.shopPackage[data.indexTitle][Modules.indexLanguage];
        if (description != null) description.text = AllLanguages.shopStartPurchase[Modules.indexLanguage] + " " + data.Count + " " +
                    (data.Type == ModificationType.Coin ? AllLanguages.shopEndCoinPurchase[Modules.indexLanguage] : AllLanguages.shopEndKeysPurchase[Modules.indexLanguage]);
        if (modification != null)
        {
            if (data.Count > 1)
                modification.text = data.Count + " " + data.Type + "s";
            else
                modification.text = data.Count + " " + data.Type;
        }
        if (price != null && Purchaser.Instance.IsInitialized())
        {
            string priceStr = Purchaser.m_StoreController.products.WithID(data.ID).metadata.localizedPriceString.ToString();
            if (priceStr != "")
                price.text = priceStr;
            else price.text = "????";
        }

        if (image != null && data.Image != null) image.sprite = data.Image;
        if (data.iconSale != null)
        {
            iconSale.sprite = data.iconSale;
            iconSale.color = new Color(1, 1, 1, 1);
        }
        else iconSale.color = new Color(1, 1, 1, 0);
        productID = data.ID;
        Name = data.Name;
    }

    public void OnClick() {
        if (productID != "")
            Purchaser.Instance.BuyProductID(productID);
    }
}
