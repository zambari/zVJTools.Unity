using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

// data conatiner below

//[ExecuteInEditMode]
public class TransformList : MonoBehaviour
{

    public TransformListSerializable transforms;

    void Reset()
    {
        checkID();
        targetTransform = transform;
        transforms = new TransformListSerializable();
    }

    public string id;
    public Transform targetTransform;
    void OnValidate()
    {
        if (targetTransform == null) targetTransform = transform;
    }
    [Range(0, 1)]
    public float speed = 0.3f;

    [Range(0.4f, 10)]
    public float interval = 5f;
    public bool usePosition;
    public bool useRotation;
    public bool useScale;
    public bool autoSave;
    public bool isExpanded;
    public bool randomSteps;
    public int currentStep;
    public int nextStep;
    public int jumpOrAnimate;
    [Range(0, 0.9f)]
    public float randomFactor = 0.1f;
    float nextChangeTime;
    Vector3 targetPosition;
    Quaternion targetRotation;
    Vector3 targetScale;
    Vector3 positionVelocity;
    Vector3 scaleVelocity;
    Quaternion rotationVelocity;
    void Start()
    {
        if (autoSave) loadSet();
    }
    public void storeCurrent()
    {
        int current = currentStep;
        for (int i = current; i < transforms.count; i++)
        {
            transforms.positions[i + 1] = transforms.positions[i];
            transforms.rotations[i + 1] = transforms.rotations[i];
            transforms.scales[i + 1] = transforms.scales[i];
            transforms.labels[i + 1] = transforms.labels[i];
        }
        transforms.positions[current] = targetTransform.localPosition;
        transforms.rotations[current] = targetTransform.localRotation;
        transforms.scales[current] = targetTransform.localScale;
        transforms.labels[current] = zExtensions.RandomString(4);
        transforms.count++;
    }
    public void swap(int source, int target)
    {
        Vector3 vector = transforms.positions[target];
        transforms.positions[target] = transforms.positions[source];
        transforms.positions[source] = vector;

        vector = transforms.scales[target];
        transforms.scales[target] = transforms.scales[source];
        transforms.scales[source] = vector;

        string s = transforms.labels[source];
        transforms.labels[source] = transforms.labels[target];
        transforms.labels[target] = s;
        Quaternion q = transforms.rotations[target];
        transforms.rotations[target] = transforms.rotations[source];
        transforms.rotations[source] = q;
    }
    public void remove(int index)
    {
        if (currentStep > index) currentStep--;
        for (int i = index; i < transforms.count; i++)
        {
            transforms.positions[i] = transforms.positions[i + 1];
            transforms.rotations[i] = transforms.rotations[i + 1];
            transforms.scales[i] = transforms.scales[i + 1];
            transforms.labels[i] = transforms.labels[i + 1];
        }
        transforms.count--;
    }
    public void updateCurrent()
    {
        int current = currentStep;
        transforms.positions[current] = targetTransform.localPosition;
        transforms.rotations[current] = targetTransform.localRotation;
        transforms.scales[current] = targetTransform.localScale;
    }

    IEnumerator randomizer()
    {
        while (true)
        {
            yield return new WaitForSeconds(interval + interval * (Random.value - 0.5f) * randomFactor);
            if (isActiveAndEnabled && jumpOrAnimate!=2)
                GoToStep(Random.Range(0, transforms.count - 1));
        }
    }

    public void GoToStep(int step)
    {
        Debug.Log("goint to " + step);
        if (step < 0 || step >= transforms.count) return;
         currentStep = step;
        if (jumpOrAnimate == 1 || !Application.isPlaying)
        {
            if (usePosition)
                targetTransform.localPosition = transforms.positions[currentStep];
            if (usePosition)
                targetTransform.localRotation = transforms.rotations[currentStep];
            if (usePosition)
                targetTransform.localScale = transforms.scales[currentStep];
        }
        else
        {
            targetPosition = transforms.positions[currentStep];
            targetRotation = transforms.rotations[currentStep];
            targetScale = transforms.scales[currentStep];
        }
    }

    void Update()
    {
        if (jumpOrAnimate == 0)
        {
            if (usePosition)
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref positionVelocity, speed);
            if (useRotation)
              transform.localRotation= zExtensions.SmoothDamp(transform.localRotation,targetRotation,ref rotationVelocity,speed);
            if (useScale)
                transform.localScale = Vector3.SmoothDamp(transform.localScale, targetScale, ref scaleVelocity, speed);
        }
    }
public void gotoRandom()
{
    GoToStep(Random.Range(0,transforms.count-1));
}
    string getPath()
    {
        return Application.streamingAssetsPath + "/json/" + id + ".json";
    }
    void checkID()
    {
        if (string.IsNullOrEmpty(id))
            id = name + "_" + zExtensions.RandomString(6);
    }


    public void loadSet()
    {
        if (File.Exists(getPath()))
            transforms = transforms.loadJson(getPath());
    }
    public void saveSet()
    {
        transforms.saveJson(getPath());
    }
    void OnApplicationQuit()
    {
        Debug.Log("onquit");
        if (autoSave) saveSet();
    }

}

/// <summary>
/// Data Container class
/// </summary>


[System.Serializable]
public class TransformListSerializable
{
    public int count;
    public const int maxCount = 40;
    public Vector3[] positions;
    public Vector3[] scales;
    public Quaternion[] rotations;
    public string[] labels;
    public TransformListSerializable()
    {
        positions = new Vector3[maxCount];
        scales = new Vector3[maxCount];
        rotations = new Quaternion[maxCount];
        labels = new string[maxCount];
    }
}
