using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

   public GameObject PaintTool;

    public List<GameObject> Colors;

    public float Accuracy = 100f;

    private int currentShapeMax;

    void Awake()
    {
        GameManager.Instance = this;
    }

    void Start()
    {
        Invoke("SetShapeMax", 0.5f);
    }

    
    /// the shape max value is how much wood compared to the goal shape , "white shape" . and this value is used for calculating the accuracy .
    
    public void SetShapeMax()
    {
        RenderTexture renderTexture = GameObject.Find("ResultCamera").GetComponent<Camera>().targetTexture;

        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        for (int i = 0; i < tex2d.height; i++)
        {
            for (int j = 0; j < tex2d.width; j++)
            {
                if (tex2d.GetPixel(i, j) != new Color(1, 1, 1, 1))
                {
                    if (ColorUtility.ToHtmlStringRGB(tex2d.GetPixel(i, j)) == "38424F")
                    {
                        currentShapeMax++;
                    }
                }
            }
        }
    }

   
    /// Calculate FinalAccuracy by checking how much wood left by the player .
   
    public void CalculateFinalAccuracy()
    {
        RenderTexture renderTexture = GameObject.Find("ResultCamera").GetComponent<Camera>().targetTexture;

        Texture2D tex2d = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        RenderTexture.active = renderTexture;
        tex2d.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        tex2d.Apply();

        int WoodLeft = 0;

        for(int i = 0; i < tex2d.height; i++)
        {
            for(int j = 0; j < tex2d.width; j++)
            {
                if (tex2d.GetPixel(i, j) != new Color(1, 1, 1, 1))
                {
                    if (ColorUtility.ToHtmlStringRGB(tex2d.GetPixel(i, j)) == "38424F")
                    {
                        WoodLeft++;
                    }
                }
            }
        }

        Accuracy -= (100 * WoodLeft) / currentShapeMax;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
