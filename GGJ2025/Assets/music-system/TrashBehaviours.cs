using UnityEngine;

public class TrashBehaviours : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InputManager.Instance.SetInputType("player");
        AudioManager.Instance.Play();
    }

    public void Deb(string message)
    {
        Debug.Log($"{message}");
    }
}
