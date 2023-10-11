using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "ScriptableObjects/ObjectInfo", order = 1)]
public class ObjectInfo : ScriptableObject
{
    public string objectName;
    public string description;
    public Tag tag;
}

public enum Tag
{
    Expander,
    Mirror,
    Goal,
    BeamSplitter,
    LaserEmitter,
    Other,


}