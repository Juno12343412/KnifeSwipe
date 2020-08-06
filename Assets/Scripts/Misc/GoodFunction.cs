using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Good
{
    public static class Transform2D
    {
        public static Vector2 SetX(float x, Transform t)
        {
            return new Vector2(x, t.position.y);
        }

        public static Vector2 SetY(float y, Transform t)
        {
            return new Vector2(t.position.x, y);
        }
    }

    public static class MathK
    {
        public static GameObject FindNearTarget(string tag, GameObject myObj)
        {
            List<GameObject> objects = new List<GameObject>(GameObject.FindGameObjectsWithTag(tag));
            if (objects.Count == 0)
            {
                return null;
            }
            if (tag == myObj.tag && objects.Count > 1)
                objects.Remove(myObj);

            GameObject nearTarget = objects[0];
            float nearDistance = Vector3.Distance(myObj.transform.position, nearTarget.transform.position);
            foreach (var obj in objects)
            {
                float distance = Vector3.Distance(myObj.transform.position, obj.transform.position);
                if (distance < nearDistance)
                {
                    nearTarget = obj;
                    nearDistance = distance;
                }
            }
            return nearTarget;
        }

        public static Quaternion LookAngle(Vector2 dir)
        {
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.back);
        }
    }
}
