using UnityEngine;
using UnityEngine.SceneManagement;

public class CC_ConquerorFleet : MonoBehaviour
{

    public CC_Conqueror[] prefabs;
    public int rows = 5;
    public int cols = 11;

    private Vector3 _direction = Vector2.right;

    public AnimationCurve speed;

    public int totalConquerors => this.rows * this.cols;
    public int deadConquerors { get; private set; }
    public int livingConquerors => this.totalConquerors - this.deadConquerors;

    public float percentDead => (float)this.deadConquerors / (float)this.totalConquerors;

    public float missileAttackRate = 1.0f;

    public CC_Projectile missilePrefab;

    public bool cutscenePlaying;



    private void Awake() {
        for (int row = 0; row < this.rows; row++) {

            Vector3 rowPosition = new Vector3(-2.5f, row * 0.5f - 1.0f, 0.0f);
            for (int col = 0; col < this.cols; col++) {

                CC_Conqueror conqueror = Instantiate(this.prefabs[row], this.transform);
                Vector3 position = rowPosition;
                position.x += col * 0.5f;
                conqueror.transform.position = position;
                conqueror.killed += ConquerorKilled;
            }
        }
    }

    private void Start() {
        if (!cutscenePlaying) {
            InvokeRepeating(nameof(MissileAttack), this.missileAttackRate, this.missileAttackRate);
        }
    }
//updates position of conquerors
    private void Update() {
        if (!cutscenePlaying) {
            this.transform.position += _direction * this.speed.Evaluate(this.percentDead) * Time.deltaTime;

            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

            foreach(Transform conqueror in this.transform) {
                if (!conqueror.gameObject.activeInHierarchy) {
                    continue;
                }

                if (_direction == Vector3.right && conqueror.position.x >= rightEdge.x) {
                    AdvanceRow();
                } else if (_direction == Vector3.left && conqueror.position.x <= leftEdge.x) {
                    AdvanceRow();
                }
            }
        }
    }
//logic to make the fleet move down
    private void AdvanceRow() {
        _direction.x *= -1.0f;
        Vector3 position = this.transform.position;
        position.y -= 0.15f;
        this.transform.position = position;
    }
//logic for if conqueror is destroyed
    private void ConquerorKilled() {
        this.deadConquerors++;
        if (deadConquerors == totalConquerors) {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            FindObjectOfType<AK_GameManager>().LevelComplete();
        }
    }
//logic for conquerors to attack player
    private void MissileAttack() {
        foreach(Transform conqueror in this.transform) {
            if (!conqueror.gameObject.activeInHierarchy) {
                continue;
            }

            if (Random.value < 1.0f / (float)livingConquerors) {
                Instantiate(this.missilePrefab, conqueror.position, Quaternion.identity);
                break;
            }
        }
    }
}



