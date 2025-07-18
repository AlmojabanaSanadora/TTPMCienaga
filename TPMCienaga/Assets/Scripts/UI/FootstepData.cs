using UnityEngine;

[System.Serializable]
public class FootstepData
{
    public string layerName;             // Nombre del Layer: "Ground", "Wood", "Water", etc.
    public AudioClip[] footstepClips;    // Sonidos asociados a ese tipo de suelo
}
