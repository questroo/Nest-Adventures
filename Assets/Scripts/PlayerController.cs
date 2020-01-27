using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpHeight = 5.0f;
    public float horizontalLuanchSpeed = 30.0f;
    private bool isDodging = false;

    void Update()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, 1.05f))
        {
            isDodging = false;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(x, 0.0f, z) * moveSpeed * Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isDodging)
        {
            DodgeRoll(direction);
        }
        if(!isDodging)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
    }

    void DodgeRoll(Vector3 dir)
    {
        isDodging = true;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(dir.x * horizontalLuanchSpeed, jumpHeight, dir.z * horizontalLuanchSpeed));
    }
}
