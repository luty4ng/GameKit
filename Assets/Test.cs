using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


public class Test : MonoBehaviour {
    public Rigidbody rigidbody;
    private void Start() {
        rigidbody =  this.GetComponent<Rigidbody>();
        // rigidbody.AddForce(Vector3.one * 100, ForceMode.Force);
        rigidbody.AddForce(Vector3.one, ForceMode.Impulse);
        // rigidbody.AddForce(Vector3.one, ForceMode.VelocityChange);
    }
    
}