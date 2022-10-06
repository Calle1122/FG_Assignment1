
using UnityEngine;

public class CameraHolderScript : MonoBehaviour
{
    [SerializeField] GameObject activePlayerManObj;

    private ActivePlayerController _activeMan;
    private Transform _transformToFocus;

    void Awake()
    {
        _activeMan = activePlayerManObj.GetComponent<ActivePlayerController>();
    }

    private void Start()
    {
        SetCameraFocus();
    }

    void Update()
    {
        if (_activeMan.activePlayer != null)
        {
            transform.position = _transformToFocus.position;
        }
        
    }
    
    public void SetCameraFocus()
    {
        _transformToFocus = _activeMan.activePlayer.GetComponent<PlayerManager>().cameraPosTransform;
    }
}
