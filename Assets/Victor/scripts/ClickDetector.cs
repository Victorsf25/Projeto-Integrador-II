using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ClickDetector : MonoBehaviour
{
    private Camera mainCamera;

    [Header("Configurações de UI")]
    [Tooltip("Arraste o objeto de Texto (TextMeshPro) do Canvas para cá")]
    public TextMeshProUGUI textoInteragir; 
    
    [Header("Configurações de Interação")]
    [Tooltip("Selecione a(s) Layer(s) que contém os objetos interativos")]
    public LayerMask layerInterativa;
    
    [Tooltip("Distância máxima que o raio alcança (em unidades do Unity)")]
    public float distanciaMaxima = 100f;

    void Awake()
    {
        mainCamera = Camera.main;
        
        if (textoInteragir != null)
        {
            textoInteragir.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Mouse.current == null) return;

        // Pega a posição atual do mouse na tela e cria o raio
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        RaycastHit hit;

        // Dispara o raio uma única vez
        if (Physics.Raycast(ray, out hit, distanciaMaxima, layerInterativa))
        {
            // O mouse está sobre um objeto da Layer interativa! Mostra o texto.
            if (textoInteragir != null) textoInteragir.gameObject.SetActive(true);

            // Verifica se o jogador CLICOU enquanto o texto está aparecendo
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                GameObject clickedObject = hit.collider.gameObject;
                
                // Tenta pegar o componente que usa a interface IInterativo
                IInterativo objetoInterativo = clickedObject.GetComponent<IInterativo>();

                if (objetoInterativo != null)
                {
                    // Passa o próprio jogador (ou a câmera) como quem iniciou a interação
                    objetoInterativo.Interagir(this.gameObject); 
                }
            }
        }
        else
        {
            // O raio não bateu em nada interativo ou bateu no chão/parede. Esconde o texto.
            if (textoInteragir != null) textoInteragir.gameObject.SetActive(false);
        }
    }
}