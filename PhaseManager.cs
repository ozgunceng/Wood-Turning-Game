using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Deform {

    public class PhaseManager : MonoBehaviour
    {
        public static PhaseManager instance;

        public string currentPhase;

        public GameObject PaintTool;

        public GameObject Spray;

        public List<GameObject> CutPhasUI;

        public GameObject ColorsPanel;
        public GameObject ResultsPanel;
        public GameObject ResultLight;

        public Text accuracy;
        public RectTransform accuracyBar;

        public GameObject FinalModel;

        void Awake()
        {
            PhaseManager.instance = this;
        }

        void ShowResultsPanel()
        {
            ResultsPanel.SetActive(true);
        }

        // Go To next phase based on the curretPhase , there are 4 phases . "cut" - "spong" - "paint" - "result"
        public void NextPhase()
        {
            if(currentPhase == "Results")
            {
                int levelName = int.Parse(SceneManager.GetActiveScene().name) + 1;

                // if there the player finished all the levels , repeat from level 1
                if (levelName <= SceneManager.sceneCountInBuildSettings)
                {
                    SceneManager.LoadScene(levelName.ToString());
                }
                else
                {
                    SceneManager.LoadScene("1");
                }
            }

            if(currentPhase == "Paint")
            {
                currentPhase = "Results";

                ColorsPanel.SetActive(false);
                Spray.SetActive(false);

                ResultLight.SetActive(true);
                accuracyBar.sizeDelta = new Vector2((int)GameManager.Instance.Accuracy, 100);

                FinalModel.transform.parent.GetComponent<Animator>().enabled = true;
                FinalModel.transform.parent.GetComponent<Animator>().Play("Results");

                accuracy.text = "ACCURACY: " + Mathf.Clamp(GameManager.Instance.Accuracy,0,100) + "%";
                Invoke("ShowResultsPanel", 0.5f);
            }

            if (currentPhase == "Spong")
            {
                currentPhase = "Paint";
                GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().Bias = 5f;
                GameObject.Find("Tool").SetActive(false);

                Camera.main.orthographic = false;

                foreach (GameObject g in CutPhasUI) g.SetActive(false);

                Deformable deformable = GameObject.Find("Model").GetComponent<Deformable>();
                Mesh mesh = (Mesh)Instantiate(deformable.GetMesh());
                mesh.name = "FinalMesh";
                FinalModel.GetComponent<MeshFilter>().mesh = mesh;
                FinalModel.GetComponent<MeshCollider>().sharedMesh = mesh;

                Destroy(GameObject.Find("Model"));

                Invoke("PaintAnimation", 0.2f);
                
            }

            if(currentPhase == "Cut")
            {
                GameObject.Find("ResultCamera").GetComponent<Camera>().enabled = false;
                GameManager.Instance.CalculateFinalAccuracy();

                GameObject.Find("Tool").GetComponent<ToolController>().SetSpongPhase();
                currentPhase = "Spong";
            }
        }

        void PaintAnimation()
        {
            FinalModel.transform.parent.GetComponent<Animator>().Play("PaintRotation");
            PaintTool.SetActive(true);
            ColorsPanel.SetActive(true);
            Spray.SetActive(true);
        }
    }
}
