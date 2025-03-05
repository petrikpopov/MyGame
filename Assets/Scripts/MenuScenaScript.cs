using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScenaScript : MonoBehaviour
{
    [SerializeField]
    private GameObject menu;
    void Start()
    {
        DontDestroyOnLoad(menu);
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
