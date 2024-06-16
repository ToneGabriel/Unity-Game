using UnityEngine;

public abstract class StateMB : MonoBehaviour
{
    [SerializeField]
    [ReadOnlyField]
    public StateBehaviour Target;   // FSMMB will set it;

    protected virtual void Awake()
    {
        
    }

    protected virtual void OnEnable()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected virtual void FixedUpdate()
    {
        
    }

    protected virtual void OnDisable()
    {
        
    }
}
