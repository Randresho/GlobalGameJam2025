using UnityEngine;

public class SimpleSizeEffect : MonoBehaviour
{
    public void SizeDown() => transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

    public void SizeUp() => transform.localScale = new Vector3(1, 1, 1);
}
