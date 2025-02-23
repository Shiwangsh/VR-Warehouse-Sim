using UnityEngine;
using System;

[Serializable]
public class BoxType
{
    [SerializeField] public Type type;
    [SerializeField] public Color color;
    [SerializeField] public Sprite stamp;

    public enum Type
    {
        RED,
        BLUE,
        GREEN,
        YELLOW
    }
}
