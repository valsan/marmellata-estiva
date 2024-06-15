using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Debuffs/MirroredDisplayDebuff")]
public class MirroredDisplayDebuff : Debuff
{
    public override void Apply()
    {
        base.Apply();
        Matrix4x4 mat = Camera.main.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(-0.5f, 1, 1));
        Camera.main.projectionMatrix = mat;
    }

    public override void Restore()
    {
        base.Restore();
        Matrix4x4 mat = Camera.main.projectionMatrix;
        mat *= Matrix4x4.Scale(new Vector3(1, 1, 1));
        Camera.main.projectionMatrix = mat;
    }
}
