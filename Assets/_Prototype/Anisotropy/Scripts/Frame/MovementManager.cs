using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameKit;
public class MovementManager : MonoBehaviour
{
    public static MovementManager instance { get; private set; }
    public LayerMask checkLayers;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public bool CheckAccessibility(Vector3 position)
    {
        // Debug.Log(position - 2 * Vector3.up + " ");
        Debug.DrawLine(position - 2 * Vector3.up, position - Vector3.up, Color.red, 1f);
        if (Physics.Raycast(position - 2 * Vector3.up, Vector3.up, out RaycastHit hitInfo, 3f, checkLayers))
        {
            if (hitInfo.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                return true;
        }
        return false;
    }

    public void CheckAccessibility(Vector3 position, EntityLevel level)
    {
        for (int i = 0; i < EntityLevelPriority.priority.Length; i++)
        {
            if (EntityLevelPriority.priority[i] == level)
            {
                
            }
        }
    }

}