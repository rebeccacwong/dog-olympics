using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CustomHandle))]
public class CustomHandleEditor : Editor
{
    private void OnSceneGUI()
    {
        CustomHandle handle = target as CustomHandle;
        if (Event.current.type == EventType.Repaint)
        {
            EventType eventType = EventType.Repaint;
            Handles.color = Color.white;
            //Forward
            CreateHandle(11, handle.transform.position + handle.transform.forward * handle.handleParams.offset,
                       Quaternion.FromToRotation(Vector3.forward, handle.transform.forward),
                        handle.handleParams.size, handle.handleParams.Type, eventType);

            //Right
            CreateHandle(12, handle.transform.position + handle.transform.right * handle.handleParams.offset,
                        Quaternion.FromToRotation(Vector3.forward, handle.transform.right),
                        handle.handleParams.size, handle.handleParams.Type, eventType);

            //up
            CreateHandle(13, handle.transform.position + handle.transform.up * handle.handleParams.offset,
                        Quaternion.FromToRotation(Vector3.forward, handle.transform.up),
                        handle.handleParams.size, handle.handleParams.Type, eventType);

            Handles.color = Color.red;
            if (Vector3.Distance(handle.getVelocityVector(), Vector3.zero) < 0.05f)
            {
                // if the velocity is small, just add a sphere.
                Handles.SphereHandleCap(14, handle.transform.position + handle.transform.up * handle.handleParams.offset, Quaternion.identity, handle.handleParams.size / 10, eventType);

            }
            else
            {
                // Add velocity vector
                CreateHandle(14, handle.transform.position + handle.transform.up * handle.handleParams.offset,
                    Quaternion.LookRotation(handle.getVelocityVector()),
                    handle.handleParams.size, handle.handleParams.Type, eventType);
            }

            Handles.color = Color.blue;
            if (Vector3.Distance(handle.getForceVector(), Vector3.zero) < 0.05f)
            {
                // if the velocity is small, just add a sphere.
                Handles.SphereHandleCap(
                    14,
                    handle.transform.position + (handle.transform.up * handle.handleParams.offset) - (handle.transform.forward * 4),
                    Quaternion.identity,
                    handle.handleParams.size / 10,
                    eventType);
            }
            else
            {
                // Add force vector if applied
                CreateHandle(
                    14,
                    handle.transform.position + (handle.transform.up * handle.handleParams.offset) - (handle.transform.forward * 4),
                    Quaternion.LookRotation(handle.getForceVector()),
                    handle.handleParams.size,
                    handle.handleParams.Type,
                    eventType);
            }
        }
    }
    private void CreateHandle(int id, Vector3 position, Quaternion rotation, float size, HandleTypes handleTypes, EventType eventType)
    {
        Handles.ArrowHandleCap(id, position, rotation, size, eventType);
    }
}