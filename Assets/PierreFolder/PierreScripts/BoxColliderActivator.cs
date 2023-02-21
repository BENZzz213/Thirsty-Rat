using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class BoxColliderActivator : MonoBehaviour
{
    public CharacterMovement characterMvt;
    public GameObject hideZoneCollider;

    void Start()
    {
        hideZoneCollider.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (characterMvt.isHiding)
            hideZoneCollider.SetActive(true);
        else
            hideZoneCollider.SetActive(false);
    }
}
