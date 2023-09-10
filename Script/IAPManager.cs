using System;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;
    public FormCreator formCreator;
    public TMP_Text itemPriceLabel;
    public TMP_Text shippingFeeLabel;
    public TMP_Text totalAmount;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_ExtensionProvider;

    private readonly string removeAds = "Ads_Removal";

    //Small
    private readonly string superSmall = "Super_Small";
    private readonly string extraSmall = "Extra_Small";
    private readonly string small = "Small";

    //Medium
    private readonly string medium = "Medium";
    private readonly string extraMedium = "Extra_Medium";
    private readonly string superMedium = "Super_Medium";

    //Large
    private readonly string large = "Large";
    private readonly string extraLarge = "Extra_Large";
    private readonly string superLarge = "Super_Large";
    private readonly string doubleExtraLarge = "Double_Extra_Large";
    private readonly string tripleExtraLarge = "Triple_Extra_Large";

    // Gigantic
    private readonly string gigantic = "Gigantic";
    private readonly string extraGigantic = "Extra_Gigantic";
    private readonly string superGigantic = "Super_Gigantic";
    private readonly string doubleExtraGigantic = "Double_Extra_Gigantic";

    private float time_rate;
    private double totalWeight;

    public FirebaseDatabaseHandler databaseHandler;

    private const double conversionFactor = 0.0163871;

    [Obsolete]
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(superSmall, ProductType.Consumable);
        builder.AddProduct(extraSmall, ProductType.Consumable);
        builder.AddProduct(small, ProductType.Consumable);
        builder.AddProduct(medium, ProductType.Consumable);
        builder.AddProduct(extraMedium, ProductType.Consumable);
        builder.AddProduct(superMedium, ProductType.Consumable);
        builder.AddProduct(large, ProductType.Consumable);
        builder.AddProduct(extraLarge, ProductType.Consumable);
        builder.AddProduct(superLarge, ProductType.Consumable);
        builder.AddProduct(doubleExtraLarge, ProductType.Consumable);
        builder.AddProduct(tripleExtraLarge, ProductType.Consumable);
        builder.AddProduct(gigantic, ProductType.Consumable);
        builder.AddProduct(extraGigantic, ProductType.Consumable);
        builder.AddProduct(superGigantic, ProductType.Consumable);
        builder.AddProduct(doubleExtraLarge, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_ExtensionProvider != null;
    }

    public void BuyRemoveAds()
    {
        BuyProductID(removeAds);
    }

    public void GetTotalWeight()
    {
        float totalVolume = float.Parse(formCreator.length.text) * float.Parse(formCreator.width.text)
        * float.Parse(formCreator.height.text);

        double totalWeightByVolume = totalVolume * conversionFactor;
        totalWeight = float.Parse(formCreator.weight.text) + totalWeightByVolume;
    }

    //In Kilogram
    public void BuyProducts()
    {
        if (totalWeight <= 1.5f)
        {
            BuyProductID(superSmall);
        }
        else if (totalWeight >= 1.5f && totalWeight <= 6.5f)
        {
            BuyProductID(extraSmall);
        }
        else if (totalWeight >= 6.5f && totalWeight <= 10.5f)
        {
            BuyProductID(small);
        }
        else if (totalWeight >= 10.5f && totalWeight <= 18.5f)
        {
            BuyProductID(medium);
        }
        else if (totalWeight >= 18.5f && totalWeight <= 26.5f)
        {
            BuyProductID(extraMedium);
        }
        else if (totalWeight >= 26.5f && totalWeight <= 34.5f)
        {
            BuyProductID(superMedium);
        }
        else if (totalWeight >= 34.5f && totalWeight <= 40.0f)
        {
            BuyProductID(large);
        }
        else if (totalWeight >= 40.0f && totalWeight <= 45.5f)
        {
            BuyProductID(extraLarge);
        }
        else if (totalWeight >= 45.5f && totalWeight <= 57.0f)
        {
            BuyProductID(superLarge);
        }
        else if (totalWeight >= 57.0f && totalWeight <= 68.5f)
        {
            BuyProductID(doubleExtraLarge);
        }
        else if (totalWeight >= 68.5f && totalWeight <= 76.5f)
        {
            BuyProductID(tripleExtraLarge);
        }
        else if (totalWeight >= 76.5f && totalWeight <= 83.5f)
        {
            BuyProductID(gigantic);
        }
        else if (totalWeight >= 83.5f && totalWeight <= 90.6f)
        {
            BuyProductID(extraGigantic);
        }
        else if (totalWeight >= 90.0f && totalWeight <= 98.0f)
        {
            BuyProductID(superGigantic);
        }
        else if (totalWeight >= 100.0f)
        {
            BuyProductID(doubleExtraGigantic);
        }
    }

    public void DisplayProducts()
    {
        Debug.Log(totalWeight);

        if (totalWeight <= 1.5f)
        {
            DisplayProductPrice(superSmall, "15");
        }
        else if (totalWeight >= 1.5f && totalWeight <= 6.5f)
        {
            DisplayProductPrice(extraSmall, "20");
        }
        else if (totalWeight >= 6.5f && totalWeight <= 10.5f)
        {
            DisplayProductPrice(small, "25");
        }
        else if (totalWeight >= 10.5f && totalWeight <= 18.5f)
        {
            DisplayProductPrice(medium, "30");
        }
        else if (totalWeight >= 18.5f && totalWeight <= 26.5f)
        {
            DisplayProductPrice(extraMedium, "50");
        }
        else if (totalWeight >= 26.5f && totalWeight <= 34.5f)
        {
            DisplayProductPrice(superMedium, "70");
        }
        else if (totalWeight >= 34.5f && totalWeight <= 40.0f)
        {
            DisplayProductPrice(large, "90");
        }
        else if (totalWeight >= 40.0f && totalWeight <= 45.5f)
        {
            DisplayProductPrice(extraLarge, "120");
        }
        else if (totalWeight >= 45.5f && totalWeight <= 57.0f)
        {
            DisplayProductPrice(superLarge, "180");
        }
        else if (totalWeight >= 57.0f && totalWeight <= 68.5f)
        {
            DisplayProductPrice(doubleExtraLarge, "210");
        }
        else if (totalWeight >= 68.5f && totalWeight <= 76.5f)
        {
            DisplayProductPrice(tripleExtraLarge, "250");
        }
        else if (totalWeight >= 76.5f && totalWeight <= 83.5f)
        {
            DisplayProductPrice(gigantic, "290");
        }
        else if (totalWeight >= 83.5f && totalWeight <= 90.6f)
        {
            DisplayProductPrice(extraGigantic, "320");
        }
        else if (totalWeight >= 90.0f && totalWeight <= 98.0f)
        {
            DisplayProductPrice(superGigantic, "350");
        }
        else if (totalWeight >= 100.0f)
        {
            DisplayProductPrice(doubleExtraGigantic, "400");
        }
    }

    [Obsolete]
    public void ClickRestorePurchases()
    {
        IAPManager.instance.RestorePurchases();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, removeAds, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Remove Ads Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
        else if (String.Equals(args.purchasedProduct.definition.id, superSmall, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, extraSmall, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, small, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, medium, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, extraMedium, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, superMedium, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, large, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, extraLarge, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, superLarge, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, doubleExtraLarge, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, tripleExtraLarge, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, gigantic, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, extraGigantic, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, superGigantic, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, doubleExtraGigantic, StringComparison.Ordinal))
        {
            SSTools.ShowMessage("Purchase Successful", SSTools.Position.bottom, SSTools.Time.threeSecond);
            StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Paid"));
            formCreator.LoadForm();
        }
        else
        {
            SSTools.ShowMessage("Purchase Failed", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }

        return PurchaseProcessingResult.Complete;
    }

    [Obsolete]
    void Start()
    {
        databaseHandler = GameObject.FindGameObjectWithTag("Database").GetComponent<FirebaseDatabaseHandler>();

        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void DisplayProductPrice(string productID, string rawPrice)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productID);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("{0}", product.metadata.localizedPrice.ToString()));
            }
            else
            {
                SSTools.ShowMessage("Price Initialization Failed", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
        }
        else
        {
            itemPriceLabel.text = databaseHandler.totalAmount.ToString();
            shippingFeeLabel.text = rawPrice;
            totalAmount.text = (int.Parse(rawPrice) + databaseHandler.totalAmount).ToString();
        }
    }

    public void BuyWithCash()
    {
        StartCoroutine(databaseHandler.UploadPaymentToDatabase(databaseHandler.trackingNumber, "Unpaid"));
        formCreator.LoadForm();
    }

    private void BuyProductID(string productID)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productID);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("{0}", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                SSTools.ShowMessage("BuyProductID Fail", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
        }
        else
        {
            SSTools.ShowMessage("BuyProductID Fail", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
    }

    [Obsolete]
    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("Error Not Initialized");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
        Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("Restore Purchase started");

            var apple = m_ExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) =>
            {
                Debug.Log("RestorePurchasing Continuing: " + result);
                SSTools.ShowMessage("Restore Purchasing:" + result, SSTools.Position.bottom, SSTools.Time.threeSecond);
            });
        }
        else
        {
            Debug.Log("RestorePurchasing Continuing: " + Application.platform);
            SSTools.ShowMessage("Restore Purchasing:" + Application.platform, SSTools.Position.bottom, SSTools.Time.threeSecond);
        }

    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_ExtensionProvider = extensions;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(String.Format("Failed: {0} : {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log(error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log(error + message);
    }
}