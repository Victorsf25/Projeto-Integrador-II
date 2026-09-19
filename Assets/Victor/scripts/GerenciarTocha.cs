using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GerenciadorTochas : MonoBehaviour
{
    [Header("Configuração do Puzzle")]
    [Tooltip("A ordem correta que as tochas devem ser acesas. Ex: A, S, B, D")]
    public List<string> sequenciaCorreta;
    
    [Header("Referências")]
    public List<Tocha> todasAsTochas;
    

    private int indiceAtual = 0;

    private void OnEnable()
    {
        
        foreach (Tocha tocha in todasAsTochas)
        {
            tocha.AoSerAcesa += ValidarTochaAcesa;
        }
    }

    private void OnDisable()
    {
        foreach (Tocha tocha in todasAsTochas)
        {
            tocha.AoSerAcesa -= ValidarTochaAcesa;
        }
    }

    private void ValidarTochaAcesa(Tocha tochaAcesa)
    {
        if (tochaAcesa.simboloDaTocha.ToUpper() == sequenciaCorreta[indiceAtual].ToUpper())
        {
            indiceAtual++; 
            Debug.Log($"Correto! Faltam {sequenciaCorreta.Count - indiceAtual}");

            if (indiceAtual >= sequenciaCorreta.Count)
            {
                PuzzleResolvido();
            }
        }
        else
        {
            ResetarPuzzle();
        }
    }

    private void ResetarPuzzle()
    {
        Debug.Log("Sequência Incorreta! Resetando...");
        indiceAtual = 0;
        
        foreach (Tocha tocha in todasAsTochas)
        {
            tocha.Apagar();
        }
        
    }

    private void PuzzleResolvido()
    {
        Debug.Log("Puzzle das Tochas Resolvido!");
        
    }
}