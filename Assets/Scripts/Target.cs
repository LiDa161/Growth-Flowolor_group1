using UnityEngine;

public class Target : MonoBehaviour
{
    void OnMouseDown()
    {
        Move.Instance.SwapWithSlot(transform.parent);
    }
}
