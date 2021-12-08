using UnityEngine;
using UnityEditor;

public class MeshTest : MonoBehaviour
{
    public MeshFilter GlobalMeshFilter;
    private void Start()
    {
        // Draw();
    }

    public void Draw()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Mesh mesh = meshFilter.mesh;
        Debug.Log("Bounds: " + mesh.bounds);
        // Debug.Log(mesh.vertexCount);
        // Debug.Log(mesh.normals.Length);
        // foreach (var vec in mesh.vertices)
        // {
        //     Debug.Log("Vertice: " +vec);
        // }
        // foreach (var uv in mesh.uv)
        // {
        //     Debug.Log("UV: " +uv);
        // }
        for (int i = 0; i < mesh.normals.Length; i++)
        {
            if (mesh.normals[i] == new Vector3(0, 1, 0))
            {
                // if (mesh.vertices[i].x < 0.2f && mesh.vertices[i].x > -0.2f)
                // {
                Debug.Log(mesh.vertices[i]);
                Debug.DrawLine(this.transform.localPosition, this.transform.localPosition + mesh.vertices[i], Color.red, 10f);

                // }

            }
        }
    }

#if UNITY_EDITOR

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGizmo(MeshTest meshTest, GizmoType gizmoType)
    {
        Gizmos.color = Color.red;
        MeshFilter meshFilter = meshTest.GlobalMeshFilter;
        Mesh mesh = meshFilter.mesh;
        Gizmos.DrawWireCube(meshTest.transform.position + mesh.bounds.center, mesh.bounds.size);


        // if ((gizmoType & GizmoType.Selected) != 0)
        // {
        //     for (int i = 0; i < 6; i++)
        //     {
        //         var hint = ModulePrototype.editorData.GetConnectorHint(i);
        //         if (hint.Mesh != null)
        //         {
        //             Gizmos.DrawMesh(hint.Mesh,
        //                 position + rotation * Orientations.Direction[i].ToVector3() * AbstractMap.BLOCK_SIZE,
        //                 rotation * Quaternion.Euler(Vector3.up * 90f * hint.Rotation));
        //         }
        //     }
        // }
        // for (int i = 0; i < 6; i++)
        // {
        //     if (modulePrototype.Faces[i].Walkable)
        //     {
        //         Gizmos.color = Color.red;
        //         Gizmos.DrawLine(position + Vector3.down * 0.1f, position + rotation * Orientations.Rotations[i] * Vector3.forward * AbstractMap.BLOCK_SIZE * 0.5f + Vector3.down * 0.1f);
        //     }
        //     if (modulePrototype.Faces[i].IsOcclusionPortal)
        //     {
        //         Gizmos.color = Color.blue;

        //         var dir = rotation * Orientations.Rotations[i] * Vector3.forward;
        //         Gizmos.DrawWireCube(position + dir, (Vector3.one - new Vector3(Mathf.Abs(dir.x), Mathf.Abs(dir.y), Mathf.Abs(dir.z))) * AbstractMap.BLOCK_SIZE);
        //     }
        // }

        // if (ModulePrototype.style == null)
        // {
        //     ModulePrototype.style = new GUIStyle();
        //     ModulePrototype.style.alignment = TextAnchor.MiddleCenter;
        // }

        // ModulePrototype.style.normal.textColor = Color.black;
        // for (int i = 0; i < 6; i++)
        // {
        //     var face = modulePrototype.Faces[i];
        //     Handles.Label(position + rotation * Orientations.Rotations[i] * Vector3.forward * InfiniteMap.BLOCK_SIZE / 2f, face.ToString(), ModulePrototype.style);
        // }
    }

#endif
}

