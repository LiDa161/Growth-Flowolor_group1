using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject canvasHome;
    public GameObject canvasGuide;
    public GameObject canvasSelectLevel;
    public GameObject canvasWin;
    public GameObject canvasLose;
    public GameObject currentLevel;
    public GameObject[] levelPrefabs;

    public Transform levelParent;

    public Button buttonPlay;
    public Button buttonGuide;
    public Button buttonExitGuide;
    public Button buttonExitSelectLevel;

    public Button[] levelButtons;
    public TextMeshProUGUI moveCountText;
    public TextMeshProUGUI scoreText;

    private int currentLevelIndex = -1;
    private List<WinBlock> finalBlocks = new List<WinBlock>();
    private int moveCount = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ShowCanvas(canvasHome);

        buttonPlay.onClick.AddListener(() =>
        {
            ShowCanvas(canvasSelectLevel);
        });

        buttonGuide.onClick.AddListener(() =>
        {
            ShowCanvas(canvasGuide);
        });

        buttonExitGuide.onClick.AddListener(() =>
        {
            ShowCanvas(canvasHome);
        });

        buttonExitSelectLevel.onClick.AddListener(() =>
        {
            ShowCanvas(canvasHome);
        });

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int level = i + 1;
            levelButtons[i].onClick.AddListener(() =>
            {
                LoadLevel(level);
            });
        }

        UpdateMoveCountText();
    }

    private void ShowCanvas(GameObject targetCanvas)
    {
        canvasHome.SetActive(targetCanvas == canvasHome);
        canvasGuide.SetActive(targetCanvas == canvasGuide);
        canvasWin.SetActive(targetCanvas == canvasWin);
        canvasLose.SetActive(targetCanvas == canvasLose);
        canvasSelectLevel.SetActive(targetCanvas == canvasSelectLevel);
        if (targetCanvas == canvasWin || targetCanvas == canvasLose)
        {
            DisableOtherButtons(targetCanvas);
        }
        else
        {
            EnableAllButtons();
        }
    }

    public void LoadLevel(int level)
    {
        HideAllCanvases();
        finalBlocks.Clear();
        moveCount = 0;
        UpdateMoveCountText();
        if (currentLevel != null)
        {
            Destroy(currentLevel);
        }

        if (level > 0 && level <= levelPrefabs.Length)
        {
            currentLevelIndex = level - 1;
            currentLevel = Instantiate(levelPrefabs[currentLevelIndex], levelParent);

            Button[] buttons = currentLevel.GetComponentsInChildren<Button>();
            foreach (Button btn in buttons)
            {
                string lowerName = btn.name.ToLower();

                if (lowerName.Contains("Home"))
                {
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => ShowCanvas(canvasHome));
                }

                if (lowerName.Contains("Restart"))
                {
                    btn.onClick.RemoveAllListeners();
                    btn.onClick.AddListener(() => LoadLevel(currentLevelIndex + 1));
                }
            }
        }
    }

    public void ShowWinCanvas()
    {
        canvasHome.SetActive(false);
        canvasGuide.SetActive(false);
        canvasLose.SetActive(false);
        canvasWin.SetActive(true);
        DisableOtherButtons(canvasWin);

        if (scoreText != null)
        {
            scoreText.text = "Score: " + moveCount;
        }

        Button[] winButtons = canvasWin.GetComponentsInChildren<Button>();
        foreach (Button btn in winButtons)
        {
            string lowerName = btn.name.ToLower();

            if (lowerName.Contains("Home"))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    HideAllCanvases();
                    ShowCanvas(canvasHome);
                });
            }
            else if (lowerName.Contains("Next"))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    HideAllCanvases();
                    if (currentLevelIndex + 1 >= levelPrefabs.Length)
                    {
                        ShowCanvas(canvasHome);
                    }
                    else
                    {
                        LoadLevel(currentLevelIndex + 2);
                    }
                });
            }
            else if (lowerName.Contains("Restart"))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    HideAllCanvases();
                    if (currentLevelIndex + 1 >= levelPrefabs.Length)
                    {
                        LoadLevel(1);
                    }
                    else
                    {
                        LoadLevel(currentLevelIndex + 1);
                    }
                });
            }
        }
    }

    public void ShowLoseCanvas()
    {
        canvasHome.SetActive(false);
        canvasGuide.SetActive(false);
        canvasWin.SetActive(false);
        canvasLose.SetActive(true);
        DisableOtherButtons(canvasLose);

        Button[] loseButtons = canvasLose.GetComponentsInChildren<Button>();
        foreach (Button btn in loseButtons)
        {
            string lowerName = btn.name.ToLower();

            if (lowerName.Contains("Home"))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    HideAllCanvases();
                    ShowCanvas(canvasHome);
                });
            }
            else if (lowerName.Contains("Restart"))
            {
                btn.onClick.RemoveAllListeners();
                btn.onClick.AddListener(() =>
                {
                    HideAllCanvases();
                    LoadLevel(currentLevelIndex + 1);
                });
            }
        }
    }

    private void DisableOtherButtons(GameObject activeCanvas)
    {
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button btn in allButtons)
        {
            bool inActiveCanvas = btn.transform.IsChildOf(activeCanvas.transform);
            btn.interactable = inActiveCanvas;
        }
    }

    private void EnableAllButtons()
    {
        Button[] allButtons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (Button btn in allButtons)
        {
            btn.interactable = true;
        }
    }

    private void HideAllCanvases()
    {
        canvasHome.SetActive(false);
        canvasGuide.SetActive(false);
        canvasSelectLevel.SetActive(false);
        canvasWin.SetActive(false);
        canvasLose.SetActive(false);

        EnableAllButtons();
    }

    public int GetCurrentLevelIndex()
    {
        return currentLevelIndex;
    }

    public void RegisterWinBlock(WinBlock block)
    {
        if (!finalBlocks.Contains(block))
        {
            finalBlocks.Add(block);
        }
    }

    public void CheckAllWinBlocksMatched()
    {
        foreach (WinBlock block in finalBlocks)
        {
            if (!block.IsMatched())
                return;
        }

        ShowWinCanvas();
    }

    public void IncrementMoveCount()
    {
        moveCount++;
        UpdateMoveCountText();
        if (moveCount > 20)
        {
            ShowLoseCanvas();
        }
    }

    private void UpdateMoveCountText()
    {
        if (moveCountText != null)
        {
            moveCountText.text = "Move: " + moveCount;
        }
    }
}