using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] AudioSource clickSound;

    public void StartGame()
    {
        StartCoroutine(StartGameCo());
    }

    IEnumerator StartGameCo()
    {
        clickSound.Play();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Game");
    }
}