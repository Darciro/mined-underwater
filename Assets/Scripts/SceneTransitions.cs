using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitions : MonoBehaviour
{
    private Animator transitionAnim;

    private void Start()
    {
        transitionAnim = GetComponent<Animator>();
        StartCoroutine(HideFirstTransiton());
    }

    public void LoadScene(string sceneName) {
        this.gameObject.SetActive(true);
        StartCoroutine(Transition(sceneName));
    }

    IEnumerator Transition(string sceneName) {
        transitionAnim.SetTrigger("TransitionEnd");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator HideFirstTransiton() {
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
