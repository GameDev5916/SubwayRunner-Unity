using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class Purchaser : MonoBehaviour, IStoreListener
{
    public static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    public static Purchaser Instance;
    public string AndroidKey;
    public List<ProductIAP> productIAP;

    void Awake()
    {
        Instance = this;
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        // If we have already connected to Purchasing ...
        if (IsInitialized())
        {
            // ... we are done here.
            return;
        }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.Configure<IGooglePlayConfiguration>().SetPublicKey(AndroidKey);
        //KHOI TAO CAC TEN MAT HANG TREN CAC STORE
        foreach(var item in productIAP)
        {
            builder.AddProduct(item.ID, item.productType, new IDs(){
                {item.IdIOS, AppleAppStore.Name},//dinh nghia them cho ro rang cac store, cung khong can thiet + IOS
                {item.IdAndroid, GooglePlay.Name},
                {item.ID, WindowsStore.Name},
                {item.ID, FacebookStore.Name},
            });
        }
        UnityPurchasing.Initialize(this, builder);
    }

    public bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        // If we are running on an Apple device ... 
        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        foreach (var item in productIAP) {
            if (String.Equals(args.purchasedProduct.definition.id, item.ID, StringComparison.Ordinal)) {
                switch (item.Type) { 
                    case ModificationType.StarterPack:
                        AddStarterPack();
                        break;
                    case ModificationType.DoubleCoin:
                        AddDoubleCoin();
                        break;
                    case ModificationType.Coin:
                        AddCoin(item.Count);
                        break;
                    case ModificationType.Key:
                        AddKey(item.Count);
                        break;
                }
            }
        }
        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void AutoBuy(ProductType type, int quantity) {
        foreach (var item in productIAP) {
            if (item.productType == type && item.Count >= quantity) {
                BuyProductID(item.ID);
                break;
            }
        }
    }

    //Xu ly mua double coin
    void AddDoubleCoin() {
        print("Xử lý cộng Double Coin");
    }

    //Xu ly mua starter pack
    void AddStarterPack() {
        print("Xử lý thêm Starter Pack");
    }

    //Xu ly them coin
    void AddCoin(int quantity) {
        print("Xử lý cộng " + quantity + " Coins");
        Modules.totalCoin += quantity;
        Modules.SaveCoin();
        if (Modules.containShopItem.activeSelf)
            Camera.main.GetComponent<PageShopItems>().UpdateCoins();
    }

    //Xu ly them key
    void AddKey(int quantity) {
        print("Xử lý cộng " + quantity + " Keys");
        Modules.totalKey += quantity;
        Modules.SaveKey();
        if (Modules.containShopItem.activeSelf)
            Camera.main.GetComponent<PageShopItems>().UpdateKeys();
        if (Modules.containMainGame.activeSelf)
            Camera.main.GetComponent<PageMainGame>().UpdateKeys();
    }

}

[System.Serializable]
public class ProductIAP
{
    public string Name;
    public string ID;
    public string IdAndroid;
    public string IdIOS;
    public ProductType productType;
    [Header("Information")]
    public int indexTitle;

    [Header("Modification")]
    public ModificationType Type;
    public int Count;
    public Sprite Image;
    public Sprite iconSale;
}

public enum ModificationType
{ 
    Coin,
    Key,
    DoubleCoin,
    StarterPack
}