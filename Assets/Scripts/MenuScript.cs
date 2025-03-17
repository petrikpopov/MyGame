using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScript : MonoBehaviour
{
    private GameObject content;
    private static MenuScript previousInstance = null;
    void Start()
    {
        if(previousInstance == null) {
            previousInstance = this;
        }
        else {
            Destroy(this.gameObject);
        }
        content = transform.Find("Content").gameObject;

        Time.timeScale = this.gameObject.activeInHierarchy ? 0.0f : 1.0f;
    }

    void Update()
    {
       if(Input.GetKeyDown(KeyCode.Escape)) {
            content.SetActive(!content.activeInHierarchy);
            Time.timeScale = 1.0f - Time.timeScale;
       }

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            SceneManager.LoadScene(1);
       }
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            SceneManager.LoadScene(2);
        }
    }
}
