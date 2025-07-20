using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Hàm chuyển sang SelectLevelScene
    public void GoToSelectLevel()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    // Hàm chuyển sang GuideScene
    public void GoToGuide()
    {
        SceneManager.LoadScene("Guide");
    }
    public void Edit()
    {
        SceneManager.LoadScene("Start");
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level4");
    }
    public void Level5()
    {
        SceneManager.LoadScene("Level5");
    }
    public void Level6()
    {
        SceneManager.LoadScene("Level6");
    }
    public void Level7()
    {
        SceneManager.LoadScene("Level7");
    }
    public void Level8()
    {
        SceneManager.LoadScene("Level8");
    }
    public void Level9()
    {
        SceneManager.LoadScene("Level9");
    }
    public void Level10()
    {
        SceneManager.LoadScene("Level10");
    }
    public void Level11()
    {
        SceneManager.LoadScene("Level11");
    }
    public void Level12()
    {
        SceneManager.LoadScene("Level12");
    }
    public void Level13()
    {
        SceneManager.LoadScene("Level13");
    }
    public void Level14()
    {
        SceneManager.LoadScene("Level14");
    }
    public void Level15()
    {
        SceneManager.LoadScene("Level15");
    }
    public void ResetLevel()
    {
        // Load lại scene hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
