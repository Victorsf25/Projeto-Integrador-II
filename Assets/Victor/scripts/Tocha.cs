using UnityEngine;
using System;


public class Tocha : MonoBehaviour, IInterativo
{
    [Header("Identificação")]
    public string simboloDaTocha; 
    
    [Header("Visual")]
    public GameObject fogoVisual; 

    public bool estaAcesa { get; private set; }
    
    public event Action<Tocha> AoSerAcesa;

    private void Start()
    {
        Apagar();
    }

    public void Interagir(GameObject quemInteragiu)
    {
        if (!estaAcesa)
        {
            Acender();
            Debug.Log($"{quemInteragiu.name} acendeu a tocha {simboloDaTocha}");
        }
    }

    private void Acender()
    {
        estaAcesa = true;
        if (fogoVisual != null) fogoVisual.SetActive(true);
        
        AoSerAcesa?.Invoke(this);
    }

    public void Apagar()
    {
        estaAcesa = false;
        if (fogoVisual != null) fogoVisual.SetActive(false);
    }
}