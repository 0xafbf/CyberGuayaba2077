using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iCollisionController
{
    public void collisionEnter(Collider x);
    public void collisionStay(Collider x);
    public void collisionExit(Collider x);
}
public class CollisionControler : MonoBehaviour
{
    public List<Collider> currFrameCollisions = new List<Collider>();
    public List<Collider> lastFrameCollisions = new List<Collider>();
    Collider[] collisionsDetected = new Collider[10];
    public LayerMask layerMask;
    public Collider myCollider;
    List<iCollisionController> users = new List<iCollisionController>(10);
    bool foundOnCurr = false;
    Collider currCollision;

    public void Start()
    {
        if (myCollider == null)
        {
            TryGetComponent(out myCollider);
        }

        Init();
    }



    public void Init()
    {
        lastFrameCollisions.Clear();
        currFrameCollisions.Clear();
        var myUsers = GetComponentsInChildren<iCollisionController>();

        for (int i = 0; i < myUsers.Length; i++)
        {
            if (!users.Contains(myUsers[i]))
            {
                users.Add(myUsers[i]);
            }

        }
    }



    public void AddNewUser(iCollisionController user)
    {
        if (!users.Contains(user))
        {
            users.Add(user);
        }
    }

    public void RemoveUser(iCollisionController user)
    {
        users.Remove(user);
    }


    public void Update()
    {
        lastFrameCollisions = currFrameCollisions.GetRange(0, currFrameCollisions.Count);
        currFrameCollisions.Clear();
        int objsHittedAmount = 0;

        if (myCollider is CapsuleCollider myCapsuleCollider)
        {
            objsHittedAmount = Physics.OverlapSphereNonAlloc(transform.position, myCapsuleCollider.radius, collisionsDetected, layerMask);
        }

        if (myCollider is BoxCollider myBoxCollider)
        {
            objsHittedAmount = Physics.OverlapBoxNonAlloc(transform.position, myBoxCollider.size / 2, collisionsDetected, transform.rotation, layerMask);
        }
        else if (myCollider is CharacterController myCharacterCollider)
        {
            var center = myCharacterCollider.transform.position;
            var point1 = (myCharacterCollider.height / 2) * Vector3.up + center;
            var point0 = (myCharacterCollider.height / 2) * Vector3.down + center;

            objsHittedAmount = Physics.OverlapCapsuleNonAlloc(point0, point1, myCharacterCollider.radius, collisionsDetected, layerMask);
        }
        else if (myCollider is SphereCollider sphereCollider)
        {
            var pos = sphereCollider.transform.position;
            var rad = sphereCollider.radius;

            objsHittedAmount = Physics.OverlapSphereNonAlloc(pos, rad, collisionsDetected, layerMask);
        }



        for (int i = 0; i < objsHittedAmount; i++)
        {
            var collidedObj = collisionsDetected[i];
            if (collidedObj == myCollider) continue;
            currFrameCollisions.Add(collidedObj);
            var wasAlreadyColliding = lastFrameCollisions.Contains(collidedObj);
            if (!wasAlreadyColliding)
            {

                for (int k = 0; k < users.Count; k++)
                {
                    users[k].collisionEnter(collidedObj);
                }


            }
        }
        for (int i = 0; i < lastFrameCollisions.Count; i++)
        {
            var lastCollision = lastFrameCollisions[i];
            foundOnCurr = false;
            for (int j = 0; j < currFrameCollisions.Count; j++)
            {
                currCollision = currFrameCollisions[j];
                if (currCollision == lastCollision)
                {
                    foundOnCurr = true;
                    break;
                }
            }
            if (foundOnCurr)
            {
                //Debug.Log("CollisionSaty");
                for (int k = 0; k < users.Count; k++)
                {
                    users[k].collisionStay(currCollision);
                }
            }
            else
            {
                //Debug.Log("CollisionExit");
                for (int k = 0; k < users.Count; k++)
                {
                    users[k].collisionExit(lastCollision);
                }
            }
        }
    }

    public void GetCollisionsOfType<T>(List<T> results)
    {
        results.Clear();
        for (int i = 0; i < currFrameCollisions.Count; i++)
        {
            if (currFrameCollisions[i].TryGetComponent<T>(out T TypeObject))
            {
                results.Add(TypeObject);
            }

        }

    }


}

