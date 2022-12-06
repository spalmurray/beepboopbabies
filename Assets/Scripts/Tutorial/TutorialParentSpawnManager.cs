using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TutorialParentSpawnManager : ParentSpawnManager
{
    public override int NumberOfParents => 1;

    protected override void Start()
    {
        childNames = new List<string>() { "Bob", "Anna", "Gaston", "Lemmy", "Chad", "Linda", "Bruce", "Penelope", "Jillian", "Carter" };
    }

    public void SpawnParents()
    {
        StartCoroutine(SpawnMultipleParents());
    }
}
