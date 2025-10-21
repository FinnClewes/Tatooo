using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToShopScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       //SceneManager.LoadScene(sceneName: "ShopScene");
    }


    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 30), "Button"))
        {
            Debug.Log("Scene2 loading: ShopScene");
            SceneManager.LoadScene(sceneName: "ShopScene", LoadSceneMode.Single);
        }
    }
}
