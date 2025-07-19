using DG.Tweening;
using UnityEngine;

public class Block : MonoBehaviour
{
    public float moveDistance = 1f;
    public float moveDuration = 0.2f;

    private bool isUp = false;
    private Vector3 originalLocalPos;

    void Start()
    {
        originalLocalPos = transform.localPosition;
    }

    void OnMouseDown()
    {
        if (DOTween.IsTweening(transform.parent)) return;

        if (isUp)
        {
            transform.DOLocalMove(originalLocalPos, moveDuration);
            Move.Instance.HideHiddenObjects();
            Move.Instance.SetCurrentBlock(null);
        }
        else
        {
            transform.DOLocalMove(originalLocalPos + Vector3.up * moveDistance, moveDuration);
            Move.Instance.ShowHiddenObjects();
            Move.Instance.SetCurrentBlock(this);
        }

        isUp = !isUp;
    }

    public void ResetToOriginalState()
    {
        transform.DOLocalMove(originalLocalPos, moveDuration);
        isUp = false;
    }

    public Transform GetParent() => transform.parent;
    public float GetMoveDuration() => moveDuration;
}
