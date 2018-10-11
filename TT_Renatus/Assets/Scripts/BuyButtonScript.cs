using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace UI
{
    public class BuyButtonScript : MonoBehaviour
    {

        public GameObject Spineboy;
        static bool canCallSpineboy = true;
        public Transform parent;

        public void CallForSpineboy()
        {
            if (canCallSpineboy)
            {
                GameObject newboy = Instantiate(Spineboy);
                newboy.transform.parent = parent;
                newboy.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
                canCallSpineboy = false;
            }
        }
    }
}
