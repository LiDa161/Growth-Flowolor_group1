using UnityEngine;

public class SelectableSwapper : MonoBehaviour
{
    [Header("Con sẽ bay lên khi chọn")]
    public Transform childToMove;
    public float moveOffsetY = 1f;

    [Header("Khối hiển thị marker khi chọn")]
    public GameObject markerObject;

    private Vector3 originalPos;
    private static SelectableSwapper selected = null;

    void Start()
    {
        if (childToMove != null)
            originalPos = childToMove.localPosition;

        if (markerObject != null)
            markerObject.SetActive(false); // Ẩn marker lúc đầu
    }

    void OnMouseDown()
    {
        // Bấm lại chính nó → hủy chọn
        if (selected == this)
        {
            ResetState();
            selected = null;
            return;
        }

        // Nếu chưa có khối nào được chọn → chọn khối này
        if (selected == null)
        {
            selected = this;
            MoveChildUp();
            ShowMarker(true);
            return;
        }

        // Nếu đang có khối khác được chọn → thực hiện hoán đổi
        if (selected != null)
        {
            // Đổi vị trí 2 khối
            SwapWith(selected);

            // Reset cả hai khối
            selected.ResetState();
            this.ResetState();

            selected = null;
        }
    }

    void Update()
    {
        // Nếu đang chọn → click ra ngoài → hủy chọn
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

    void ShowMarker(bool show)
    {
        if (markerObject != null)
            markerObject.SetActive(show);
    }

    public void ResetState()
    {
        MoveChildDown();
        ShowMarker(false);
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
