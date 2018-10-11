using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace UI
{
    [System.Serializable]
   
    public enum StickerType { Null, MostPopular, BestDeal }

    [CreateAssetMenu(fileName = "newItem", menuName = "MenueItem")]
    public class StoreItemSO : ScriptableObject
    {

        public int coinAmount;
        public int bonus;
        public float price;
        public StickerType stiker;

    }
}