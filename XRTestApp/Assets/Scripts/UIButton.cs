using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIButton : MonoBehaviour
{
    public TMP_Text text;
    private int _count = 0;

    public void PushCountButton()
    {
        _count++;
        text.text = _count.ToString();
    }

    public void PushRetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
