using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KickableInteractable : PickUpInteractable
{
    public delegate void KickHandler();
    public event KickHandler HandleKicked;
    
    public void Kick(Vector3 velocityChange)
    {
        GetComponent<Rigidbody>().AddForceAtPosition(velocityChange, transform.position, ForceMode.VelocityChange);
        HandleKicked?.Invoke();
    }

    public void Kick(GameObject kicker, float kickSpeed, float kickUpwardAngle)
    {
        Kick(CalculateKickVelocityChange(kicker, kickSpeed, kickUpwardAngle));
    }

    private Vector3 CalculateKickVelocityChange(GameObject kicker, float kickSpeed, float kickUpwardAngle)
    {
        var kickOrigin = kicker.transform.position;
        var lowY = kicker.GetComponent<Collider>().bounds.min.y;
        kickOrigin.y = lowY;
        // Get direction of kick, rotate upward by KICK_UPWARD_ANGLE degrees
        var kickOriginDirection = gameObject.transform.position - kickOrigin;
        var x = kickOriginDirection.x;
        var z = kickOriginDirection.z;
        var y = Mathf.Sqrt(x * x + z * z) * Mathf.Tan(Mathf.Deg2Rad * kickUpwardAngle);
        var forceDirection = new Vector3(x, y, z);
        forceDirection.Normalize();
        return forceDirection * kickSpeed;
    }
}
