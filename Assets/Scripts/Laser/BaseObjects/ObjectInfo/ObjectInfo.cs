using UnityEngine;

[CreateAssetMenu(fileName = "ObjectInfo", menuName = "ScriptableObjects/ObjectInfo", order = 1)]
public class ObjectInfo : ScriptableObject
{
    public string objectName;
    public string description;
    public ObjectInfoTag tag;
}

public enum ObjectInfoTag
{
    Expander,
    Mirror,
    Goal,
    BeamSplitter,
    LaserEmitter,
    ObjectHolder,
    Other,


}