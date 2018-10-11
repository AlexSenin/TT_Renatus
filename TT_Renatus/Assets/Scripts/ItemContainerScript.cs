using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace UI
{
    [System.Serializable]
    public class CoinSprite
    {
        public int price;
        public Sprite sprite;
        public Vector3 scale = new Vector3 (1,1,1);
    }


    public class ItemContainerScript : MonoBehaviour
    {

        public StoreItemSO[] storeItemsSO;

        public CoinSprite[] coinSprites; 

        public GameObject storeItemsPrefab;

        
        public Sprite [] aditionalSprites;

        public GameObject window;

        public string itemcontainerLocation = "ScroleBar/StoreItems";
        GameObject itemContainerGO;

        string errorTextElementNotFound = "Component needed to set item was not found: ";


        void SetItem(GameObject item, StoreItemSO itemSO)
        {   
            // Set coin amount
            if (item.transform.Find("CoinsAmount_Text")) item.transform.Find("CoinsAmount_Text").gameObject.GetComponent<Text>().text = itemSO.coinAmount.ToString();
            else Debug.LogError(errorTextElementNotFound + "CoinsAmount_Text");

            //set coin sprite
            if (item.transform.Find("Coins_Image"))
            {
                CoinSprite coinSprite = SetCoinSprite(itemSO.coinAmount);
                item.transform.Find("Coins_Image").gameObject.GetComponent<Image>().sprite = coinSprite.sprite;
                item.transform.Find("Coins_Image").gameObject.GetComponent<RectTransform>().localScale = coinSprite.scale;
            }

            else Debug.LogError(errorTextElementNotFound + "Coins_Image");

            //Set bonus

            if (item.transform.Find("Bonus_Image/Text"))
            {

                if (itemSO.bonus > 0)
                    item.transform.Find("Bonus_Image/Text").gameObject.GetComponent<Text>().text = itemSO.bonus.ToString();
                else item.transform.Find("Bonus_Image").gameObject.SetActive(false);
            }

            else Debug.LogError(errorTextElementNotFound + "Bonus_Image/Text");

            // set sticer and button color
            if (item.transform.Find("Buy_Button").gameObject.GetComponent<BuyButtonScript>())
                item.transform.Find("Buy_Button").gameObject.GetComponent<BuyButtonScript>().parent = gameObject.transform;

            if ((item.transform.Find("Sticker_Image"))&& (item.transform.Find("Buy_Button")))
            {
                GameObject sticker = item.transform.Find("Sticker_Image").gameObject;
                GameObject button = item.transform.Find("Buy_Button").gameObject;
                SetSticker(sticker, button, itemSO.stiker);


                //set Price
                if (button.transform.Find("Text")) button.transform.Find("Text").gameObject.GetComponent<Text>().text = "$" + itemSO.price.ToString();
            }



        }


        CoinSprite SetCoinSprite(int coins)
        {
            foreach (CoinSprite cs in coinSprites)
            {
                if (cs.price >= coins) return cs;
            }
            return coinSprites[coinSprites.Length-1];
        }

        void SetSticker(GameObject sticker, GameObject button, StickerType stickerType)
        {
            switch (stickerType)
            {
                case StickerType.MostPopular:
                    sticker.GetComponent<Image>().sprite = aditionalSprites[0];
                    break;
                case StickerType.BestDeal:
                    sticker.GetComponent<Image>().sprite = aditionalSprites[1];
                    button.GetComponent<Image>().sprite = aditionalSprites[2];
                    button.transform.Find("Text").gameObject.GetComponent<Outline>().enabled = false;
                    break;
                default:
                    sticker.SetActive(false);
                    break;
                    



            }
        }


        void Start()
        {
            itemContainerGO = transform.Find(itemcontainerLocation).gameObject;
            foreach (StoreItemSO itemSO in storeItemsSO)
            {
                GameObject newItem = Instantiate(storeItemsPrefab);
                newItem.transform.parent = itemContainerGO.transform;
                newItem.GetComponent<RectTransform>().localScale = new Vector3 (1, 1, 1);
                SetItem(newItem, itemSO);
            }
        }

        public void CloseWindow()
        {
            window.GetComponent<Animator>().SetBool("Close", true);
            Destroy(gameObject, 0.5f);
        }

    }


 
}
