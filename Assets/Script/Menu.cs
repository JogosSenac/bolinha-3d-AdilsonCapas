using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour

{
    public void Jogo()
{
   SceneManager.LoadScene("Jogo");
}

public void Fase2()
{
    SceneManager.LoadScene("Fase2");
}

public void Menuu()
{
    SceneManager.LoadScene("Menu");
}

public void final()
{
    SceneManager.LoadScene("Final");
}

}
