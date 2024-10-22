using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class Video : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Referência ao componente VideoPlayer

    private void Start()
    {
        // Garantir que o vídeo não toque automaticamente
        videoPlayer.playOnAwake = false;
        videoPlayer.Stop();  // Para garantir que o vídeo não comece automaticamente

        // Inicia o vídeo após 2 segundos
        StartCoroutine(PlayVideoAfterDelay(0.5f));
    }

    private IEnumerator PlayVideoAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Espera 2 segundos

        // Começa a rodar o vídeo
        videoPlayer.Play();

        // Aguarda o vídeo terminar
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        // Desativa o GameObject que contém o vídeo após o término
        gameObject.SetActive(false);
    }
}