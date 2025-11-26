using UnityEngine;

public class CollectableController : MonoBehaviour
{
    public CollectableItem objectSO;          //objectSO is the name for referencing the scriptable object in Collectable Controller
    private Renderer theRenderer;             //reference for renderer for color changing in play mode

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theRenderer = GetComponent<Renderer>();
        ApplyColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyColor()
    {
        if (objectSO != null)          //check if scriptable object is not null
        {
            theRenderer.material.SetColor("Base Color", objectSO.color);         //for the renderer on the mesh of selected object, set the base color as the one we have added to the item in SO
        }
    }
}
