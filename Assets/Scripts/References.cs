using UnityEngine;

public class References : MonoBehaviour
{
    private static References _instance;
    
    public PlayerMovement playerMovement;

    public static PlayerMovement Player => _instance.playerMovement;

    private void OnEnable()
    {
        _instance = this;
    }
}