using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFunctions : MonoBehaviour
{
    void SetTransformAndDisableAnimation()
    {
        Transform t = this.transform;

        this.GetComponent<Animator>().enabled = false;
    }
}
