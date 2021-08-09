using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PixelChecker : MonoBehaviour
{
    public Image Shape;

    int[,] pixelsArray;

    public bool isChecking;
    
    // Start is called before the first frame update
    void Start()
    {
        pixelsArray = new int[Shape.sprite.texture.width + 1, Shape.sprite.texture.height + 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (!isChecking) return;

        if (this.transform.position.x < -1f || this.transform.position.x > 1f) return;
        if (this.transform.position.z < 0.4f || this.transform.position.z > 2.2f) return;

        int w = (int)Mathf.Lerp(0, 88, Mathf.InverseLerp(-1, 1, this.transform.position.x));
        int h = (int)Mathf.Lerp(0, 94, Mathf.InverseLerp(0.4f, 2.2f, this.transform.position.z));

        w = Mathf.Clamp(w, 0, 88);
        h = Mathf.Clamp(h, 0, 94);

        //Debug.Log(Shape.sprite.texture.GetPixel(w,h));

        if(Shape.sprite.texture.GetPixel(w, h).a == 1 && pixelsArray[w, h] != 1)
        {
            Debug.Log("BAD CUT");
            pixelsArray[w, h] = 1;

            GameManager.Instance.Accuracy -= 1f;
        }
    }
}
