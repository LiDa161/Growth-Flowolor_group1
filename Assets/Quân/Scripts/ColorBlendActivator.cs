using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class ColorBlendActivator : MonoBehaviour
{
    [Header("Chọn tối đa 3 màu cần kiểm tra")]
    public ColorName targetColorA;
    public ColorName targetColorB;
    public ColorName targetColorC = ColorName.None;

    [Header("Thiết lập kiểm tra")]
    public float checkRadius = 1f;
    public LayerMask boxLayer;

    [Header("Object sẽ bật nếu đúng")]
    public GameObject objectToActivate;

    void Update()
    {
        // Xây danh sách mục tiêu
        List<ColorName> expected = new List<ColorName>();
        expected.Add(targetColorA);
        expected.Add(targetColorB);
        if (targetColorC != ColorName.None)
            expected.Add(targetColorC);

        // Thu thập danh sách màu gần
        List<ColorName> found = new List<ColorName>();
        Collider[] nearbyBoxes = Physics.OverlapSphere(transform.position, checkRadius, boxLayer);
        foreach (var box in nearbyBoxes)
        {
            ColorIdentifier ci = box.GetComponent<ColorIdentifier>();
            if (ci != null)
                found.Add(ci.colorName);
        }

        // So sánh danh sách chính xác về số lượng + màu sắc
        if (found.Count == expected.Count)
        {
            var foundCount = CountColors(found);
            var expectedCount = CountColors(expected);

            bool match = true;
            foreach (var kvp in expectedCount)
            {
                if (!foundCount.ContainsKey(kvp.Key) || foundCount[kvp.Key] != kvp.Value)
                {
                    match = false;
                    break;
                }
            }

            objectToActivate.SetActive(match);
        }
        else
        {
            objectToActivate.SetActive(false);
        }
    }

    // Đếm số lượng mỗi màu trong danh sách
    Dictionary<ColorName, int> CountColors(List<ColorName> list)
    {
        Dictionary<ColorName, int> count = new Dictionary<ColorName, int>();
        foreach (var color in list)
        {
            if (!count.ContainsKey(color))
                count[color] = 0;
            count[color]++;
        }
        return count;
    }

    // Hiển thị vùng kiểm tra trong Scene
    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.3f);
        Gizmos.DrawWireSphere(transform.position, checkRadius);

#if UNITY_EDITOR
        Handles.color = new Color(1f, 0.5f, 0f, 0.15f);
        Handles.DrawSolidDisc(transform.position, Vector3.forward, checkRadius);
#endif
    }
}
