using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Deform
{
    public class ToolController : MonoBehaviour
    {
        

        public GameObject CutTool;
        public GameObject SpongTool;
        public GameObject woodParticle;
        public GameObject SpongParticle;
        public PixelChecker pixelChecker;

        public float vector_v;

        public float Speed;

        private bool isCutting;

        public float topValue;
        public float lowValue;

        public float spongyValue;

        public string currentTool;

        Vector3 mouseScreenPosition;

        Ray ray;
        RaycastHit hit;

        private Vector3 ClickedPosition;
        private bool isHolding;
        private int layer_mask;

        void Start()
        {
            layer_mask = LayerMask.GetMask("Ground");
        }

        
        void FixedUpdate()
        {
            mouseScreenPosition = Input.mousePosition;

            mouseScreenPosition.z = transform.position.z;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, layer_mask))
                {
                    if (currentTool == "Spong")
                    {
                        topValue = GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().nativeCurve.returnTopValue();
                        lowValue = GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().nativeCurve.returnLowValue();

                        if (hit.point.x < topValue + spongyValue) hit.point = new Vector3(topValue + spongyValue, hit.point.y, hit.point.z);

                        transform.position = new Vector3(hit.point.x, 0f, hit.point.z);
                    }
                    else
                    {
                        if(!isHolding)
                        {
                            ClickedPosition = new Vector3(hit.point.x, 0f, hit.point.z);
                            isHolding = true;
                        }

                        Vector3 vec_dir = new Vector3(hit.point.x, 0f, hit.point.z) - ClickedPosition;

                        if (GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().nativeCurve.isCutting)
                        {
                            pixelChecker.isChecking = true;
                            vec_dir = vec_dir / 2.7f;
                            Speed = 0.8f;
                            transform.position = Vector3.Lerp(this.transform.position, this.transform.position + vec_dir, Speed * Time.deltaTime);
                            vector_v = 0.007f;
                        }
                        else
                        {

                            pixelChecker.isChecking = false;
                            vec_dir = vec_dir / 2f;
                            Speed = Mathf.Lerp(1f, 1.4f, Vector3.Distance(this.transform.position, this.transform.position + vec_dir));

                            if (vector_v < 0.15f)
                            {
                                vector_v += 0.01f;
                            }
                            
                            vec_dir = new Vector3(Mathf.Clamp(vec_dir.x,-0.12f,0.12f), Mathf.Clamp(vec_dir.y, -0.2f, 0.2f), Mathf.Clamp(vec_dir.z, -0.15f, 0.15f));
                            transform.position = this.transform.position + (vec_dir * vector_v);
                        }
                    }
                }
            }
            else
            {
                isHolding = false;
            }
        }

        public void SetSpongPhase()
        {
            if (PhaseManager.instance.currentPhase != "Cut") return;

            GameObject.Find("ShapeOnTopOfModel").GetComponent<RawImage>().enabled = false;
            GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().Bias = 2f;
            currentTool = "Spong";

            CutTool.SetActive(false);
            woodParticle.SetActive(false);

            SpongTool.SetActive(true);
            SpongParticle.SetActive(true);

            this.transform.position = new Vector3(2.9f, 0f, 1.8f);
        }
    }

}