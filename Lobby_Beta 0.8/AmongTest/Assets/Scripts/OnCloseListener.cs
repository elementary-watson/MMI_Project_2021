using UnityEngine;
using UnityEngine.UI;

public class OnCloseListener : MonoBehaviour
{
    public Image backgroundImage;
    public Network network;

    /// <summary>
    /// Called when the Browser is closed.
    /// </summary>
    public void OnClose()
    {
        // Uncomment this if you set up Interop
        BrowserJS.Warn("This warning was called from Unity!");

        // Randomize the background image color
        this.backgroundImage.color = new Color(Random.value, Random.value, Random.value);
        network.WebGLQuit();
        //TriggerApplicationQuit();

    }
    private void TriggerApplicationQuit()
    {
        // Get every $$anonymous$$onoBehaviour
        var allGameObjects = GameObject.FindObjectsOfType<GameObject>();

        for (int k = 0; k < allGameObjects.Length; k++)
        {
            allGameObjects[k].SendMessage("OnApplicationQuit", SendMessageOptions.DontRequireReceiver);//Send$$anonymous$$essage("OnApplicationQuit", Send$$anonymous$$essageOptions.DontRequireReceiver);
        }
    }
}