using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

[CreateAssetMenu(fileName = "New Song Data", menuName = "ScriptableObjects/Song Data")]


public class SongData : ScriptableObject
{
    public string songName;
    public string artist;
    public AudioClip songClip;
}