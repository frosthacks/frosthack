using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class TowerHoverHighlight : MonoBehaviour
{
    Renderer renderer;
    Shader outlineShader;
    Shader defaultShader;

    void Start() {
        renderer = GetComponent<Renderer>();
        outlineShader = Shader.Find("Shader Graphs/OutlineShader");
        defaultShader = Shader.Find("Sprites/Default");
    }

    void OnMouseOver() {
        renderer.material.shader = outlineShader;
    }

    void OnMouseExit() {
        renderer.material.shader = defaultShader;
    }

}
