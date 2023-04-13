using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private void Update()
    {
        transform.position = player.position + offset;

        //rotation
        Vector3 rotation = new Vector3(90, player.eulerAngles.y, 0);

        transform.rotation = Quaternion.Euler(rotation);
    }
}
