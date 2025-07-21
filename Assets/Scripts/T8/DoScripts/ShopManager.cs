using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public Material skinToSell;
    public int skinPrice = 200;
    public Button buyButton;

    private GameObject previewBlock;
    private bool skinPurchased = false;

    private void Start()
    {
        previewBlock = GameObject.FindWithTag("ShopPreviewBlock");

        buyButton.onClick.AddListener(TryBuySkin);

        // If skin was bought in this session
        if (skinPurchased && previewBlock != null)
        {
            previewBlock.GetComponent<Renderer>().material = skinToSell;
        }
    }

    private void TryBuySkin()
    {
        if (skinPurchased)
        {
            Debug.Log("You already own this skin (in this session).");
            return;
        }

        if (QuestManager.SpendFlowers(skinPrice))
        {
            skinPurchased = true;

            if (previewBlock != null)
                previewBlock.GetComponent<Renderer>().material = skinToSell;

            Debug.Log("Skin purchased!");
        }
        else
        {
            Debug.Log("Not enough flowers.");
        }
    }

    public static void ApplyEquippedSkin()
    {
        // Skins are no longer saved
    }
}