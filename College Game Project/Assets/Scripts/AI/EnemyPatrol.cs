using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrol : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float idlespeed;
    [SerializeField] private float attackspeed;
    [SerializeField] private float angularspeed;
    [SerializeField] private float acceleration;
    [SerializeField] private float fieldOfViewAngle;

    [SerializeField]
    Transform _destination;

    [SerializeField]
    bool _patrolWaiting;

    [SerializeField]
    float _totalWaitTime = 3f;

    [SerializeField]
    float _switchProbability = 0.2f;

    [SerializeField]
    List<Waypoint> _patrolPoints;

    private SphereCollider col;

    public float range;
    public Transform player;

    NavMeshAgent _navMeshAgent;
    int _currentPatrolIndex;
    bool _travelling;
    bool _waiting;
    bool _patrolForward;
    float _waitTimer;


    void Start()
    {
        col = GetComponent<SphereCollider>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attatched to " + gameObject.name);
        }
        else
        {
            if (_patrolPoints != null && _patrolPoints.Count >= 2)
            {
                _currentPatrolIndex = 0;
                SetDestination();
            }
            else
            {
                Debug.Log("Insufficent patrol points for basic patrolling behaviour.");
            }
        }
    }

    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("OMG! SO SPHERE! MUCH CASTING!");
            RaycastHit sphereHit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2f, Camera.main.pixelHeight / 2f, 0f));

            if (Physics.SphereCast(ray, castingRadius, out sphereHit, castingDistance))
            {
                Debug.Log("WOW! SOMETHING HIT!");
                MGLConnector.TransformClicked(sphereHit.transform);
            }
        }
    }
    */

    void Update()
    {

        Vector3 direction = transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if(angle < fieldOfViewAngle * 0.5f)
        {
            RaycastHit hit;

            Debug.DrawRay(transform.position, direction, Color.green);

            if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
            {
                if (hit.collider.tag == "Player")
                {
                    Attack();
                    _navMeshAgent.speed = attackspeed;
                }
                else
                {
                    Idle();
                    _navMeshAgent.speed = idlespeed;
                }
            }
        }

        /*
        RaycastHit sphereHit;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 50;
        Debug.DrawRay(transform.position, forward, Color.green);

        if(Physics.SphereCast(transform.position, castingRadius, out sphereHit, castingDistance))
        {
            if(hit.collider.tag == "Player")
            {
                Attack();
                _navMeshAgent.speed = attackspeed;
            }
            else
            {
                Idle();
                _navMeshAgent.speed = idlespeed;
            }
        }

        
        if (Vector3.Distance(player.position, transform.position) <= range)
        {
            Attack();
            _navMeshAgent.speed = attackspeed;
        }
        else
        {
            Idle();
            _navMeshAgent.speed = idlespeed;
        }
        */
    }

    private void Idle()
    {

        if (_travelling && _navMeshAgent.remainingDistance <= 1.0f)
        {
            _travelling = false;

            if (_patrolWaiting)
            {
                _waiting = true;
                _waitTimer = 0f;

            }
            else
            {
                ChangePatrolPoint();
                SetDestination();
            }

            if (_waiting)
            {
                _waitTimer += Time.deltaTime;
                if (_waitTimer >= _totalWaitTime)
                {
                    _waiting = false;

                    ChangePatrolPoint();
                    SetDestination();
                }
            }
        }

    }

    private void SetDestination()
    {

        if (_patrolPoints != null)
        {
            Vector3 targetVector = _patrolPoints[_currentPatrolIndex].transform.position;
            _navMeshAgent.SetDestination(targetVector);
            _travelling = true;
        }

    }

    private void ChangePatrolPoint()
    {

        if (UnityEngine.Random.Range(0f, 1f) <= _switchProbability)
        {
            _patrolForward = !_patrolForward;

        }

        if (_patrolForward)
        {
            _currentPatrolIndex = (_currentPatrolIndex + 1) % _patrolPoints.Count;
        }
        else
        {
            if (--_currentPatrolIndex < 0)
            {
                _currentPatrolIndex = _patrolPoints.Count - 1;
            }
        }
    }

    //NavMeshAgent _navMeshAgent;

    private void Attack()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _navMeshAgent.speed = speed;
        _navMeshAgent.angularSpeed = angularspeed;
        _navMeshAgent.acceleration = acceleration;

        if (_navMeshAgent == null)
        {
            Debug.LogError("The nav mesh agent component is not attached to " + gameObject.name);
        }
        else
        {
            SetDestinations();
        }
    }

    private void SetDestinations()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

}
