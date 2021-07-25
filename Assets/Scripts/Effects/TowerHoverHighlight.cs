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
        if (isSelected) return; 
        rend.material.shader = outlineShader;
        // rend.material.SetColor("_OutlineColor", Color.cyan);
        rend.material.SetFloat("_Thickness", 0.015f);
    }

    void OnMouseExit() {
        if (isSelected) return; 
        rend.material.shader = defaultShader;
    }

    public void setSelected() {
        isSelected = true;
        rend.material.shader = outlineShader;
        // rend.material.SetColor("_OutlineColor", Color.cyan);
        rend.material.SetFloat("_Thickness", 0.05f);
    }

    public void setUnselected() {
        isSelected = false;
        rend.material.shader = defaultShader;
    }

}
