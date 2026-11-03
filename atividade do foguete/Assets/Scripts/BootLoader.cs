using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    [Header("Configuração")]
    public string nomeDaCenaDoJogo = "Scene"; // Coloque aqui o nome exato da sua cena do jogo
    public float tempoDeEspera = 2.0f; // Tempo em segundos antes de carregar o jogo

    void Start()
    {
        // Chama a função de carregar a cena após o tempo definido
        Invoke("CarregarJogo", tempoDeEspera);
    }

    void CarregarJogo()
    {
        SceneManager.LoadScene(nomeDaCenaDoJogo);
    }
}