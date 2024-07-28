using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformation : MonoBehaviour
{
    private Vector3 parentPosition;
    private Quaternion parentRotation;
    private Vector3 parentScale;

    private Vector3 scaleRelativeToParent;
    private Vector3 scale;

    private Quaternion rotation;
    private Quaternion rotationRelativeToParent;

    private Vector3 position;
    private Vector3 positionRelativeToParent;

    void Start()
    {
        parentPosition = transform.parent.position;
        parentRotation = transform.parent.rotation;
        parentScale = transform.parent.lossyScale;

        rotation = transform.rotation;
        scale = transform.lossyScale;
        position = transform.position;

        //ScalingWorld(1);
        //RotationWorld(90,new Vector3(0,1,0));
        //TranslationWorld(new Vector3(0,0,0));

        ScalingWorld(1);
        RotationParent(90, new Vector3(0, 1, 0));
        TranslationWorld(new Vector3(0, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        scaleRelativeToParent = new Vector3(scale.x / parentScale.x, scale.y / parentScale.y, scale.z/ parentScale.z);
        positionRelativeToParent = (position - parentPosition);
        positionRelativeToParent.x /= parentScale.x;
        positionRelativeToParent.y /= parentScale.y;
        positionRelativeToParent.z /= parentScale.z;

        rotationRelativeToParent = Quaternion.Inverse(parentRotation) * rotation;

        transform.localScale = scaleRelativeToParent;
        transform.localRotation = rotationRelativeToParent;
        transform.localPosition = positionRelativeToParent;
    }
    private void ScalingWorld(float scaleFactor)
    {
        scale *= scaleFactor;
    }

    private void ScalingParent(float scaleFactor)
    {
        scaleRelativeToParent = new Vector3(scale.x / parentScale.x, scale.y / parentScale.y, scale.z / parentScale.z);
        scale = scaleRelativeToParent * scaleFactor;
    }
    private void TranslationWorld(Vector3 translation)
    {
        position += translation;
    }

    private void TranslationParent(Vector3 translation)
    {
        positionRelativeToParent = position - parentPosition;
        position = positionRelativeToParent + translation;
    }

    private void RotationWorld(float angle, Vector3 axis)
    {
        angle = angle * Mathf.PI / 180; //to radians
        Quaternion q = new Quaternion(Mathf.Sin(angle / 2) * axis.x, Mathf.Sin(angle / 2) * axis.y, Mathf.Sin(angle / 2) * axis.z, Mathf.Cos(angle/2));
        rotation = q;
    }

    private void RotationParent(float angle, Vector3 axis)
    {
        angle = angle * Mathf.PI / 2;
        rotationRelativeToParent = Quaternion.Inverse(parentRotation) * rotation;

        Quaternion q = new Quaternion(Mathf.Sin(axis.x / 2), Mathf.Sin(axis.y / 2), Mathf.Sin(axis.z / 2), Mathf.Cos(angle / 2));
        rotation = q;
    }
}
