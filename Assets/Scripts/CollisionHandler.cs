using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    SceneManager scene;
    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "fuel": 
                Debug.Log("fuel add");
                break;
            case "Finish":
                Debug.Log("Congrats, lets go to next step");
                break;
            case "friendly":
                Debug.Log("am still have'nt took off");
                break;
            default:
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        int currentScenceIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScenceIndex);
    }
}
