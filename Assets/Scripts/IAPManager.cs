using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyMobile;
using UnityEngine;

public class IAPManager : MonoBehaviour
{
    private IAPProduct selectedProduct;
    private List<IAPProduct> ownedProducts = new List<IAPProduct>();
    public bool logProductLocalizedData;

    public GameObject removeAdsButton;


    void Awake()
    {
        // Init EM runtime if needed (useful in case only this scene is built).
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();

        if (Advertising.IsAdRemoved())
            removeAdsButton.SetActive(false);
    }

    void OnEnable()
    {
        InAppPurchasing.PurchaseCompleted += OnPurchaseCompleted;
        InAppPurchasing.PurchaseFailed += OnPurchaseFailed;
        InAppPurchasing.RestoreCompleted += OnRestoreCompleted;
    }

    void OnDisable()
    {
        InAppPurchasing.PurchaseCompleted -= OnPurchaseCompleted;
        InAppPurchasing.PurchaseFailed -= OnPurchaseFailed;
        InAppPurchasing.RestoreCompleted -= OnRestoreCompleted;
    }

    void OnPurchaseCompleted(IAPProduct product)
    {
        if (!ownedProducts.Contains(product))
            ownedProducts.Add(product);

        if (product.Name.Equals(EM_IAPConstants.Product_Remove_Ads))
        {
            Advertising.RemoveAds();
            removeAdsButton.SetActive(false);
        }

/*
        NativeUI.Alert("Purchased Completed",
            "The purchase of product " + product.Name +
            " has completed successfully. This is when you should grant the buyer digital goods.");
*/
    }

    void OnPurchaseFailed(IAPProduct product)
    {
        NativeUI.Alert("Purchased Failed", "The purchase of product " + product.Name + " has failed.");
    }

    void OnRestoreCompleted()
    {
        StartCoroutine(CROnRestoreCompleted());
    }

    IEnumerator CROnRestoreCompleted()
    {
        while (NativeUI.IsShowingAlert())
            yield return new WaitForSeconds(0.5f);

        NativeUI.Alert("Restore Completed", "Your purchases have been restored successfully.");
    }

    // Start is called before the first frame update
    void Start()
    {
        if (logProductLocalizedData)
        {
#if EM_UIAP
            foreach (IAPProduct p in EM_Settings.InAppPurchasing.Products)
            {
                UnityEngine.Purchasing.ProductMetadata data = InAppPurchasing.GetProductLocalizedData(p.Name);

                if (data != null)
                {
                    Debug.Log("Product Localized Title: " + data.localizedTitle);
                    Debug.Log("Localized Price: " + data.localizedPriceString);
                    Debug.Log("Product Localized Description: " + data.localizedDescription);
                }
                else
                {
                    Debug.Log("Localized data is null");
                }
            }
#endif
        }

        StartCoroutine(CheckOwnedProducts());
    }

    public void Purchase(string productName)
    {
        var products = EM_Settings.InAppPurchasing.Products;
        if (products == null) return;

        var items = products.ToDictionary(pd => pd.Name, pd => pd);

        InAppPurchasing.Purchase(items[productName]);
    }

    public void RestorePurchases()
    {
        InAppPurchasing.RestorePurchases();
    }

    IEnumerator CheckOwnedProducts()
    {
        // Wait until the module is initialized
        if (!InAppPurchasing.IsInitialized())
        {
            yield return new WaitForSeconds(0.5f);
        }

        // Display list of owned non-consumable products.
        var products = EM_Settings.InAppPurchasing.Products;
        if (products != null && products.Length > 0)
        {
            for (int i = 0; i < products.Length; i++)
            {
                var pd = products[i];
                if (InAppPurchasing.IsProductOwned(pd.Name) && !ownedProducts.Contains(pd))
                {
                    ownedProducts.Add(pd);
                }
            }
        }
    }
}