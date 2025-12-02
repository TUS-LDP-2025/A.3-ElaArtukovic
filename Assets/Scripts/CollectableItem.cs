using UnityEngine;

[CreateAssetMenu(fileName = "CollectableItem", menuName = "Scriptable Objects/CollectableItem")]
public class CollectableItem : ScriptableObject
{
    public string ElementName;             //name of the element
    public int value;                      //the value
    public Color color;                    //the color
    public Sprite icon;                    //icon that will appear
    public GameObject elementPrefab;       //prefab I will need to instantite when dropping items

}                                          //to make a collectable item go to Assets - Create - Scriptable Objects at the bottom - CollectableItem 
                                           //in it I can change its data - name, value, color etc
