using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{

    public void LoadLevel(string name)
    {
        Debug.Log("Loading level:" + name);
        SceneManager.LoadSceneAsync(name);
    }



    public void ResetLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }


}
