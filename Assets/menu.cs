using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
}