using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PaintIn3D
{
    public class PaintColor : MonoBehaviour
    {
        // is the current color is the one selected or not
        public bool isSelected;

        // Color
        public Color color;
        public Color notSelectedColor;

        void Start()
        {
            color = GetComponent<Image>().color;
            notSelectedColor = this.transform.parent.GetComponent<Image>().color;

            if (isSelected) Select();
        }

       
        /// Triggerd onClick event , this function set the clicked color into the "3d paint sphere".
       
        public void Select()
        {
            isSelected = true;

            // set current color to current clicked one and deselect all the others
            GameManager.Instance.PaintTool.GetComponent<P3dPaintSphere>().Color = color;

            //set the border to white (selected state)
            this.transform.parent.GetComponent<Image>().color = Color.white;

            Deselect();
        }

        public void Deselect()
        {
            foreach (GameObject g in GameManager.Instance.Colors)
            {
                if (g != this.gameObject)
                {
                    g.GetComponent<PaintColor>().isSelected = false;
                    g.transform.parent.GetComponent<Image>().color = notSelectedColor;
                }
            }
        }
    }
}
