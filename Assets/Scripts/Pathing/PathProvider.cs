using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathProvider: MonoBehaviour
{
    private Vector3[] path;

    private void Awake()
    {
        // Get the position of all children as the path
        path = transform.Cast<Transform>()
            .Select(transform => transform.position)
            .ToArray();
    }

    public Vector3[] GetPath()
    {
        return path;
    }

    public Vector3[] GetReversePath()
    {
        return path.Reverse().ToArray();
    }
}
