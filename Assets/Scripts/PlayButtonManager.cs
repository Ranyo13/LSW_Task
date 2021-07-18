using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonManager : MonoBehaviour
{
    public Animator transition;
    // Start is called before the first frame update
    public void PlayButtonClicked()
    {
        StartCoroutine(PlayButton());
        
    }

    IEnumerator PlayButton()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("World");
    }
}
