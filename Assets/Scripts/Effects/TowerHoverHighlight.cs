using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TowerHoverHighlight : MonoBehaviour
{
    Renderer rend;
    Shader outlineShader;
    Shader defaultShader;

    void Start() {
        rend = GetComponent<Renderer>();
        outlineShader = Shader.Find("Shader Graphs/OutlineShader");
        defaultShader = Shader.Find("Sprites/Default");
    }

    void OnMouseOver() {
        rend.material.shader = outlineShader;
    }

    void OnMouseExit() {
        rend.material.shader = defaultShader;
    }

}
