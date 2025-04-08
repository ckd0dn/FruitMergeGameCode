using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool Selected { get; set; } = false;
    public Fruit currentFruit;
    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Select()
    {
        if (Selected)
        {
            Selected = false;
            spriteRenderer.color = Color.white;
            return;
        }
        spriteRenderer.color = Color.red;
        Selected = true;
    }
    
    private void Merge()
    {
        
    }

    public void SetFruit(Fruit fruit)
    {
        fruit.transform.position = transform.position;
        currentFruit = fruit;
        fruit.CurrentTile = this;
    }
    
    
}
