using Galaxian.Common;
using UnityEngine;

public class WinUI : MonoBehaviour
{
    [SerializeField] private Canvas _winUiCanvas;


    private void Awake()
    {
        GameEventHolder.OnAllEnemiesDestroyed.AddListener(Show);
        _winUiCanvas.gameObject.SetActive(false);
    }

    private void Show()
    {
        _winUiCanvas.gameObject.SetActive(true);
    }
}
