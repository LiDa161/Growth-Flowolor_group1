//using UnityEngine;
//using System.Collections.Generic;
//using DG.Tweening;

//public class GameManagerMove : MonoBehaviour
//{
//    public static GameManagerMove Instance;

//    [Header("Các slot sẽ hiển thị khi click block")]
//    public List<GameObject> hiddenObjects = new List<GameObject>();
//    [Header("Parent của các slot")]
//    public List<Transform> hiddenParents = new List<Transform>();

//    private ClickableBlock currentBlock;
//    private Transform currentBlockParent;

//    [Header("Lượt chơi")]
//    public int maxTurns = 10;
//    private int currentTurn;

//    public GameObject loseCanvas; // Canvas hiện khi hết lượt
//    public TMPro.TextMeshProUGUI turnText; // Hiển thị lượt còn lại

//    void Awake()
//    {
//        Instance = this;
//        //currentTurn = maxTurns;
//        //UpdateTurnText();
//    }

//    public void ShowHiddenObjects()
//    {
//        foreach (var obj in hiddenObjects)
//            if (obj != null) obj.SetActive(true);
//    }

//    public void HideHiddenObjects()
//    {
//        foreach (var obj in hiddenObjects)
//            if (obj != null) obj.SetActive(false);
//    }

//    public void SetCurrentBlock(ClickableBlock block)
//    {
//        currentBlock = block;
//        currentBlockParent = block?.GetParent();
//    }

//    public ClickableBlock GetCurrentBlock() => currentBlock;

//    public void SwapWithSlot(Transform slotParent)
//    {
//        if (currentBlockParent == null || slotParent == null) return;

//        Vector3 posA = currentBlockParent.position;
//        Quaternion rotA = currentBlockParent.rotation;

//        Vector3 posB = slotParent.position;
//        Quaternion rotB = slotParent.rotation;

//        float duration = currentBlock.GetMoveDuration();

//        Collider colA = currentBlockParent.GetComponentInChildren<Collider>();
//        Collider colB = slotParent.GetComponentInChildren<Collider>();

//        if (colA != null) colA.enabled = false;
//        if (colB != null) colB.enabled = false;

//        Tweener tweenA = currentBlockParent.DOMove(posB, duration);
//        Tweener tweenB = slotParent.DOMove(posA, duration);

//        Tween rotA_Tween = currentBlockParent.DORotateQuaternion(rotB, duration);
//        Tween rotB_Tween = slotParent.DORotateQuaternion(rotA, duration);

//        Sequence seq = DOTween.Sequence();
//        seq.Append(tweenA);
//        seq.Join(tweenB);
//        seq.Join(rotA_Tween);
//        seq.Join(rotB_Tween);

//        seq.OnComplete(() =>
//        {
//            tweenA.Kill();
//            tweenB.Kill();
//            rotA_Tween.Kill();
//            rotB_Tween.Kill();

//            currentBlockParent.position = posB;
//            currentBlockParent.rotation = rotB;

//            slotParent.position = posA;
//            slotParent.rotation = rotA;

//            if (colA != null) colA.enabled = true;
//            if (colB != null) colB.enabled = true;

//            currentBlock.ResetToOriginalState();
//            HideHiddenObjects();
//            currentBlock = null;
//            currentBlockParent = null;

//            GameManager.Instance.IncrementMoveCount();
//        });
//    }
//}
using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;

public class GameManagerMove : MonoBehaviour
{
    public static GameManagerMove Instance;

    [Header("Các slot sẽ hiển thị khi click block")]
    public List<GameObject> hiddenObjects = new List<GameObject>();

    [Header("Parent của các slot")]
    public List<Transform> hiddenParents = new List<Transform>();

    private ClickableBlock currentBlock;
    private Transform currentBlockParent;

    [Header("Lượt chơi")]
    public int maxTurns = 10;                     // Số lượt tối đa
    private int currentTurn;                      // Lượt còn lại
    private bool isOutOfTurn = false;             // Trạng thái hết lượt

    public GameObject loseCanvas;                 // Canvas khi thua
    public TextMeshProUGUI turnText;              // Text hiển thị lượt

    void Awake()
    {
        Instance = this;
        currentTurn = maxTurns;
        isOutOfTurn = false;
        UpdateTurnText();
    }

    private void UpdateTurnText()
    {
        if (turnText != null)
            turnText.text = "Lượt: " + currentTurn;
    }

    public void ShowHiddenObjects()
    {
        foreach (var obj in hiddenObjects)
            if (obj != null) obj.SetActive(true);
    }

    public void HideHiddenObjects()
    {
        foreach (var obj in hiddenObjects)
            if (obj != null) obj.SetActive(false);
    }

    public void SetCurrentBlock(ClickableBlock block)
    {
        currentBlock = block;
        currentBlockParent = block?.GetParent();
    }

    public ClickableBlock GetCurrentBlock() => currentBlock;

    public void SwapWithSlot(Transform slotParent)
    {
        if (isOutOfTurn || currentTurn <= 0)
            return;

        if (currentBlockParent == null || slotParent == null) return;

        Vector3 posA = currentBlockParent.position;
        Quaternion rotA = currentBlockParent.rotation;

        Vector3 posB = slotParent.position;
        Quaternion rotB = slotParent.rotation;

        float duration = currentBlock.GetMoveDuration();

        Collider colA = currentBlockParent.GetComponentInChildren<Collider>();
        Collider colB = slotParent.GetComponentInChildren<Collider>();

        if (colA != null) colA.enabled = false;
        if (colB != null) colB.enabled = false;

        Tweener tweenA = currentBlockParent.DOMove(posB, duration);
        Tweener tweenB = slotParent.DOMove(posA, duration);

        Tween rotA_Tween = currentBlockParent.DORotateQuaternion(rotB, duration);
        Tween rotB_Tween = slotParent.DORotateQuaternion(rotA, duration);

        Sequence seq = DOTween.Sequence();
        seq.Append(tweenA);
        seq.Join(tweenB);
        seq.Join(rotA_Tween);
        seq.Join(rotB_Tween);

        seq.OnComplete(() =>
        {
            tweenA.Kill();
            tweenB.Kill();
            rotA_Tween.Kill();
            rotB_Tween.Kill();

            currentBlockParent.position = posB;
            currentBlockParent.rotation = rotB;

            slotParent.position = posA;
            slotParent.rotation = rotA;

            if (colA != null) colA.enabled = true;
            if (colB != null) colB.enabled = true;

            currentBlock.ResetToOriginalState();
            HideHiddenObjects();
            currentBlock = null;
            currentBlockParent = null;

            currentTurn--;
            UpdateTurnText();

            if (currentTurn <= 0)
            {
                isOutOfTurn = true;
                if (loseCanvas != null)
                    loseCanvas.SetActive(true);
            }

            GameManager.Instance.IncrementMoveCount(); // Nếu có quản lý lượt di chuyển
        });
    }

    public void ResetTurn()
    {
        currentTurn = maxTurns;
        isOutOfTurn = false;
        UpdateTurnText();

        if (loseCanvas != null)
            loseCanvas.SetActive(false);
    }
}

