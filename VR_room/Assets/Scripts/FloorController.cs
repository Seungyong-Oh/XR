using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject buildings;
    [SerializeField] GameObject terrain;

    public void floorControl(float floor)
    {
        Vector3 posB = buildings.transform.position;
        Vector3 posT = terrain.transform.position;

        posB.y = -(floor - 1) * 3f;
        posT.y = -(floor - 1) * 3f;

        buildings.transform.position = posB;
        terrain.transform.position = posT;
    }
}
