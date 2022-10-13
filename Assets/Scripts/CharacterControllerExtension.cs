using UnityEngine;

public static class CharacterControllerExtension
{
    public static void SetPosition(this CharacterController controller, Vector3 position)
    {
        controller.enabled = false;
        controller.transform.position = position;
        controller.enabled = true;
    }
}
