using UnityEngine;

public class IKSolverFABRIKS : MonoBehaviour
{
    [SerializeField] private Transform targetPos;
    [SerializeField] private int numIterations;
    [SerializeField] private float segmentLength = .75f;
    
    private JointContainer jc;
    private Vector3 initialRootPos;

    private void Awake()
    {
        jc = FindFirstObjectByType<JointContainer>();
        initialRootPos = jc.joints[0].transform.position;
    }

    private void Update()
    {
        // based on admiralra's 2D FABRIKS interpretation: https://editor.p5js.org/admiralra/sketches/FBRwrPqbF
        for (int it = 0; it < numIterations; it++)
        {
            // if we're close enough break early
            if (Vector3.Distance(jc.joints[jc.joints.Length - 1].transform.position, targetPos.position) < 0.001)
                break;

            // Start with the end effector and go backward
            jc.joints[jc.joints.Length - 1].transform.position = targetPos.transform.position;
            for (int i = jc.joints.Length-2; i >= 0; i--)
            {
                Vector3 direction = (jc.joints[i + 1].transform.position - jc.joints[i].transform.position).normalized;
                Vector3 scaledVector = direction * -segmentLength;
                jc.joints[i].transform.position = jc.joints[i + 1].transform.position + scaledVector;
            }

            //Now start at root and go forward, correct positions
            jc.joints[0].transform.position = initialRootPos;
            for (int j = 0; j < jc.joints.Length-1; j++)
            {
                Vector3 direction = (jc.joints[j + 1].transform.position - jc.joints[j].transform.position).normalized;
                Vector3 scaledVector = direction * segmentLength;
                jc.joints[j + 1].transform.position = jc.joints[j].transform.position + scaledVector; 
            }
            
        }
    }
}
