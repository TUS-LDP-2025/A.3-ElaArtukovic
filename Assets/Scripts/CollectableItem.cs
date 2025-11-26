using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItem", menuName = "Scriptable Objects/CollectableItem")]
public class CollectableItem : ScriptableObject
{
    public string ElementName;
    public int value;
    public Color color;
    public Sprite icon;
}
