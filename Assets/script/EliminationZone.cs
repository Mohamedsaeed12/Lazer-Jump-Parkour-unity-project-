using UnityEngine;

public class EliminationZone : MonoBehaviour
{
    public Transform resetPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                // It's important to disable the CharacterController before moving it to avoid any interference
                controller.enabled = false;
                other.transform.position = resetPosition.position;
                controller.enabled = true; // Re-enable the CharacterController
            }
            else
            {
                other.transform.position = resetPosition.position; // Fallback if no CharacterController is found
            }
        }
    }
}
