using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Musica : MonoBehaviour
{
    private AudioSource audioSource;

    void Update()
    {
        // Obtém o componente AudioSource do GameObject
        audioSource = GetComponent<AudioSource>();
        
        // Verifica se o audioSource não é nulo
        if (audioSource != null)
        {
            // Inicia a reprodução da música
            audioSource.Play();
        }
        else
        {
            Debug.LogError("AudioSource não encontrado no GameObject.");
        }
    }
}
