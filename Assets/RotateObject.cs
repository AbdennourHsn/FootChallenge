using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Runners
{
    public enum Axes
    {
        x,
        y,
        z
    }

    public class RotateObject : MonoBehaviour
    {
        public Axes axe;
        public float rotationSpeed;
        // Update is called once per frame
        void Update()
        {
            if (axe == Axes.x) this.transform.Rotate(Vector3.right * rotationSpeed);
            else if (axe == Axes.y) this.transform.Rotate(Vector3.up * rotationSpeed);
            else this.transform.Rotate(Vector3.forward * rotationSpeed);
        }
    }
}