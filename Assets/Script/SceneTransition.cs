using UnityEngine;
using UnityEngine.UI;  // Necessário para controlar a UI
using UnityEngine.SceneManagement;  // Necessário para carregar novas cenas
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public Image fadeImage;  // A Image usada para o fade
    public float fadeDuration = 1f;  // Duração do fade em segundos

    void Start()
    {
        // Iniciar com o fade in (a tela aparece)
        StartCoroutine(FadeIn());
    }

    public void TransitionToScene(string sceneName)
    {
        // Iniciar o fade out e carregar a cena quando o fade terminar
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 1f;
        fadeImage.color = color;

        while (fadeImage.color.a > 0f)
        {
            color.a -= Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator FadeOut(string sceneName)
    {
        fadeImage.gameObject.SetActive(true);
        Color color = fadeImage.color;
        color.a = 0f;
        fadeImage.color = color;

        while (fadeImage.color.a < 1f)
        {
            color.a += Time.deltaTime / fadeDuration;
            fadeImage.color = color;
            yield return null;
        }

        // Carregar a nova cena quando o fade out terminar
        SceneManager.LoadScene("Final");
    }
}
