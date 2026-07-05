using System; 
using UnityEngine;

public class Core : MonoBehaviour
{
    // Quit script that's all this is
    void Update()
    {
      if (Input.GetKey("q"))
      {
        Application.Quit();
      }
    }

}
