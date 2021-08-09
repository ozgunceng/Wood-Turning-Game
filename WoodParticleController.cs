using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Deform {
    public class WoodParticleController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                var main = GetComponent<ParticleSystem>().main;

                main.loop = false;
            }

            if (Input.GetKey(KeyCode.X))
            {
                StartP();
            }

            if (GameObject.Find("Lathe Displace").GetComponent<LatheDisplaceDeformer>().nativeCurve.isCutting)
            {
                StartP();
            }
            else
            {
                Stop();
            }
        }

        public void Stop()
        {
            var main = GetComponent<ParticleSystem>().main;

            if (main.loop)
            {
                //main.loop = false;
                GetComponent<ParticleSystem>().Stop();
            }
        }

        public void StartP()
        {
            var main = GetComponent<ParticleSystem>().main;

            //Debug.Log(!GetComponent<ParticleSystem>().isPlaying);

            if (GetComponent<ParticleSystem>().particleCount <= 5)
            {
                GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
