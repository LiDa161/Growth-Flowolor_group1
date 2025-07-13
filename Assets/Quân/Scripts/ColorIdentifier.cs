using UnityEngine;

public enum ColorName
{
    None,       // Cho phép để trống
    Red,
    Yellow,
    Blue,
    Green,
    Orange,
    Purple
}

public class ColorIdentifier : MonoBehaviour
{
    public ColorName colorName;
}
