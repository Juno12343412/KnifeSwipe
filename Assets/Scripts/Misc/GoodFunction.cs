using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
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

    public static class ETC
    {
        public static float GetAttack(float x)
        {
            return 0.0961f * Mathf.Pow(x, 5f) - 
                4.8002f * Mathf.Pow(x, 4f) + 
                89.463f * Mathf.Pow(x, 3f) - 
                723.38f * Mathf.Pow(x, 2f) + 
                2394.6f * x - 2123.7f;
        }

        public static float GetCoin(float x)
        {
            float y = 0.0443f * x * 5f - 
                1.7156f * x * 4f + 
                28.66f * x * 3f - 
                212.59f * x * 2f + 
                700.92f * x * 2f - 
                637.42f;
            if (y <= 0f) return 0f;
            else return y;
        }

        public static float GetCritDmg(float x)
        {
            return (0.0000000000000001f * x * 100f + 0.1f * x * 1.9f) * 100f;
        }

        public static float GetCritHit(float x)
        {
            return x + 2f;
        }

        public static Text Calculation(Text text, int num)
        {
            if (num < 1000)
                text.text = num.ToString();
            if (num > 1000)
                text.text = (num / 1000).ToString() + ".A";
            if (num > 1000000)
                text.text = (num / 1000000).ToString() + ".B";
            if (num > 1000000000)
                text.text = (num / 1000000000).ToString() + ".C";

            return text;
        }
    }
}
