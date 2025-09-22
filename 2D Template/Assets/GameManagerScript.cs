using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    public Vector2 boxSize;
    public float castDistance;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void LoadScene()
    {
        SceneManager.LoadScene("Binary Star");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
