using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMaterialChanger : MonoBehaviour
{

    public Shader replacementShader; // the shader you want to use with this camera
    public string replacementTag;

    void OnValidate()
    {
        this.GetComponent<Camera>().SetReplacementShader(replacementShader, replacementTag);
    }
    void Update()
    {
        this.GetComponent<Camera>().SetReplacementShader(replacementShader, replacementTag);
    }
}
