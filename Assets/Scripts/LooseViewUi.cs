using Galaxian.Common;
using Galaxian.Spaceship;
using UnityEngine;

public class LooseViewUi:MonoBehaviour
{
    [SerializeField] private Canvas _looseUi;
    
    private void Awake()
    {
        GameEventHolder.OnPlayerSpaceshipDestroyed.AddListener(Show);
        _looseUi.gameObject.SetActive(false);
    }

    private void Show(PlayerSpaceship arg0)
    {
        _looseUi.gameObject.SetActive(true);
    }
}