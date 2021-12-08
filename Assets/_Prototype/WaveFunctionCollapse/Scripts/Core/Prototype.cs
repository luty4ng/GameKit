using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using GameKit;
[System.Serializable]
public class Prototype : MonoBehaviour
{
    public float Probability = 1.0f;
    public bool Spawn = true;
    public HorizontalFaceDetails Left;
    public VerticalFaceDetails Down;
    public HorizontalFaceDetails Back;
    public HorizontalFaceDetails Right;
    public VerticalFaceDetails Up;
    public HorizontalFaceDetails Forward;
    public FaceDetails[] Faces
    {
        get
        {
            return new FaceDetails[] {
                this.Left,
                this.Down,
                this.Back,
                this.Right,
                this.Up,
                this.Forward
            };
        }
    }

    public Mesh GetMesh(bool createEmptyFallbackMesh = true)
    {
        var meshFilter = this.GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.sharedMesh != null)
        {
            return meshFilter.sharedMesh;
        }
        if (createEmptyFallbackMesh)
        {
            var mesh = new Mesh();
            return mesh;
        }
        return null;
    }

    public void SaveConfig()
    {
        JsonManager.instance.SaveJsonData<Prototype>("WFC/" + this.name + "_Config", this);
    }

    public bool CompareRotatedVariants(int r1, int r2)
    {
        if (!(this.Faces[Orientations.UP] as VerticalFaceDetails).Invariant || !(this.Faces[Orientations.DOWN] as VerticalFaceDetails).Invariant)
        {
            return false;
        }

        for (int i = 0; i < 4; i++)
        {
            var face1 = this.Faces[Orientations.Rotate(Orientations.HorizontalDirections[i], r1)] as HorizontalFaceDetails;
            var face2 = this.Faces[Orientations.Rotate(Orientations.HorizontalDirections[i], r2)] as HorizontalFaceDetails;

            if (face1.Socket != face2.Socket)
            {
                return false;
            }

            if (!face1.Symmetric && !face2.Symmetric && face1.Flipped != face2.Flipped)
            {
                return false;
            }
        }

        return true;
    }

#if UNITY_EDITOR

    // private static PrototypeEditor editorData;
    private static GUIStyle style;

    [DrawGizmo(GizmoType.InSelectionHierarchy | GizmoType.NotInSelectionHierarchy)]
    static void DrawGizmo(Prototype prototype, GizmoType gizmoType)
    {

        // if (Prototype.editorData == null || Prototype.editorData.Prototype != prototype) {
        // 	Prototype.editorData = new PrototypeEditor(prototype);
        // }

        // Gizmos.color = new Color(1f, 1f, 1f, 0.3f);
        // if ((gizmoType & GizmoType.Selected) != 0) {
        // 	for (int i = 0; i < 6; i++) {
        // 		var hint = Prototype.editorData.GetConnectorHint(i);
        // 		if (hint.Mesh != null) {
        // 			Gizmos.DrawMesh(hint.Mesh,
        // 				position + rotation * Orientations.Direction[i].ToVector3() * AbstractMap.BLOCK_SIZE,
        // 				rotation * Quaternion.Euler(Vector3.up * 90f * hint.Rotation));
        // 		}
        // 	}
        // }

        for (int i = 0; i < 6; i++)
        {
            var face = prototype.Faces[i];
            Handles.Label(prototype.transform.position + Orientations.Rotations[i] * AbstractMap.BLOCK_SIZE / 2f, face.ToString());
        }
    }

#endif
}