using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TowerHoverHighlight : MonoBehaviour
{
    Renderer rend;
    Shader outlineShader;
    Shader defaultShader;
    bool isSelected = false;

    void Start() {
        rend = GetComponent<Renderer>();
        outlineShader = Shader.Find("Shader Graphs/OutlineShader");
        defaultShader = Shader.Find("Sprites/Default");
    }

    void OnMouseOver() {
        if (isSelected || rend == null) return; 
        rend.material.shader = outlineShader;
        // rend.material.SetColor("_OutlineColor", Color.cyan);
        rend.material.SetFloat("_Thickness", 0.02f);
    }

    void OnMouseExit() {
        if (isSelected || rend == null) return; 
        rend.material.shader = defaultShader;
    }

    public void setSelected() {
        if (rend == null) { return;  }
        isSelected = true;
        rend.material.shader = outlineShader;
        // rend.material.SetColor("_OutlineColor", Color.cyan);
        rend.material.SetFloat("_Thickness", 0.04f);
    }

    public void setUnselected()
    {
        if (rend == null) { return; }
        isSelected = false;
        rend.material.shader = defaultShader;
    }

}
