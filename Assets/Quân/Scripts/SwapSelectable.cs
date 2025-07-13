using UnityEngine;

public class SelectableSwapper : MonoBehaviour
{
    [Header("Con sẽ bay lên khi chọn")]
    public Transform childToMove;
    public float moveOffsetY = 1f;

    [Header("Khối hiển thị marker khi chọn (có thể để trống)")]
    public GameObject markerObject;

    private Vector3 originalPos;
    private static SelectableSwapper selected = null;

    void Start()
    {
        if (childToMove != null)
            originalPos = childToMove.localPosition;

        SafeSetMarker(false); // Ẩn marker nếu có
    }

    void OnMouseDown()
    {
        // Click lại chính nó → hủy chọn
        if (selected == this)
        {
            ResetState();
            selected = null;
            return;
        }

        // Chưa chọn gì → chọn nó
        if (selected == null)
        {
            selected = this;
            MoveChildUp();
            SafeSetMarker(true);
            return;
        }

        // Đã chọn A, bấm vào B → hoán đổi
        if (selected != null)
        {
            SwapWith(selected);

            selected.ResetState();
            this.ResetState();

            selected = null;
        }
    }

    void Update()
    {
        if (selected == this && Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit) || hit.transform != transform)
            {
                ResetState();
                selected = null;
            }
        }
    }

    void MoveChildUp()
    {
        if (childToMove != null)
            childToMove.localPosition += new Vector3(0, moveOffsetY, 0);
    }

    void MoveChildDown()
    {
        if (childToMove != null)
            childToMove.localPosition = originalPos;
    }

    void SafeSetMarker(bool show)
    {
        if (markerObject != null && markerObject.activeSelf != show)
            markerObject.SetActive(show);
    }

    public void ResetState()
    {
        MoveChildDown();
        SafeSetMarker(false);
    }

    void SwapWith(SelectableSwapper other)
    {
        Vector3 myPos = transform.position;
        Vector3 otherPos = other.transform.position;

        float speed = 10f;

        StopAllCoroutines();
        other.StopAllCoroutines();

        StartCoroutine(MoveTo(transform, otherPos, speed));
        StartCoroutine(MoveTo(other.transform, myPos, speed));
    }

    System.Collections.IEnumerator MoveTo(Transform t, Vector3 target, float speed)
    {
        while (Vector3.Distance(t.position, target) > 0.01f)
        {
            t.position = Vector3.MoveTowards(t.position, target, speed * Time.deltaTime);
            yield return null;
        }

        t.position = target;
    }
}
