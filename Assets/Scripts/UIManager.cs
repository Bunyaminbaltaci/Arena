using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //________________Server yönetim____________________
    [SerializeField]
    private Button startServerButton;
    [SerializeField]
    private Button startHostButton;
    [SerializeField]
    private Button startClientButton;
    [SerializeField]
    private TextMeshProUGUI playerInGameText;
    //___________________________________________________
   /* [SerializeField]
    private Button executePhysicsButton;

    private bool hasServerStarted;*/
    private void Awake()
    {
       
        Cursor.visible = true;
    }
    private void Update()
    {
      playerInGameText.text = $"Player in game :{PlayersManager.Instance.PlayerIngame}";
    }
    void Start()
    {
        startServerButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartServer())
                Debug.Log("");

            else
                Debug.Log("");

        });

        startHostButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartHost())
                Debug.Log("");

            else
                Debug.Log("");

        });

        startClientButton.onClick.AddListener(() =>
        {
            if (NetworkManager.Singleton.StartClient())
                Debug.Log("");

            else
                Debug.Log("");

        });
    }
}
