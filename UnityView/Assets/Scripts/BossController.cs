using Model.BossesAttacks;
using System.Collections;
using UnityEngine;


public class BossController : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private BossView _bossView;
    [SerializeField] private BattleManager _battleManager;
    [SerializeField] private Transform _leftPlate;
    [SerializeField] private Transform _rightPlate;

    [Header("Префабы атак")]
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private GameObject _shockwavePrefab;

    [Header("Настройки Камнепада")]
    [SerializeField] private int _rockCount = 5;
    [SerializeField] private float _spawnRadius = 5f;
    [SerializeField] private float _spawnHeight = 10f;

    [Header("Настройки Ударной волны")]
    [SerializeField] private float _shockwaveSpeed = 8f;
    [SerializeField] private float _shockwaveMaxSize = 12f;

    [Header("Настройки Пластин")]
    [SerializeField] private float _plateSpeed = 15f;

    private Transform _playerTransform;
    private bool _isAttacking = false;

    private void Start()
    {
        var playerView = FindFirstObjectByType<PlayerView>();
        if (playerView != null) _playerTransform = playerView.transform;
    }

    public void LaunchVisualAttack(AttackType type)
    {
        if (_isAttacking) return;

        switch (type)
        {
            case AttackType.RockRain:
                StartCoroutine(RockRainRoutine());
                break;
            case AttackType.Shockwave:
                StartCoroutine(ShockwaveRoutine());
                break;
            case AttackType.LaserPlates:
                StartCoroutine(PlateThrowRoutine());
                break;
        }
    }

    private IEnumerator RockRainRoutine() 
    { 
        _isAttacking = true; 
        for (int i = 0; i < _rockCount; i++) 
        { 
            if (_playerTransform == null) break; 

            Vector3 playerPos = _playerTransform.position; 

            float randomX = Random.Range(-_spawnRadius, _spawnRadius); 
            float randomZ = Random.Range(-_spawnRadius, _spawnRadius); 

            Vector3 spawnPosition = new Vector3(playerPos.x + randomX, playerPos.y + _spawnHeight, playerPos.z + randomZ); 
            GameObject rock = Instantiate(_rockPrefab, spawnPosition, Quaternion.identity); 
            Projectile proj = rock.GetComponent<Projectile>(); 

            if (proj != null) 
            { 
                proj.Init(_bossView.gameObject, _battleManager); 
            } 

            Destroy(rock, 3.5f);

            yield return new WaitForSeconds(0.3f); } _isAttacking = false; 
    }

    private IEnumerator ShockwaveRoutine()
    {
        _isAttacking = true;

        Vector3 spawnPos = new Vector3(transform.position.x, 0.2f, transform.position.z);
        GameObject wave = Instantiate(_shockwavePrefab, spawnPos, Quaternion.identity);

        if (wave.TryGetComponent<Projectile>(out var proj))
        {
            proj.Init(_bossView.gameObject, _battleManager);
        }

        float currentSize = 0f;
        wave.transform.localScale = Vector3.zero;

        while (currentSize < _shockwaveMaxSize)
        {
            if (wave == null) break;
            currentSize += _shockwaveSpeed * Time.deltaTime;
            wave.transform.localScale = new Vector3(currentSize, wave.transform.localScale.y, currentSize);
            yield return null;
        }

        if (wave != null) Destroy(wave);
        _isAttacking = false;
    }

    private IEnumerator PlateThrowRoutine()
    {
        _isAttacking = true;

        if (_playerTransform != null && _leftPlate != null && _rightPlate != null)
        {
            Transform chosenPlate = Random.Range(0, 2) == 0 ? _leftPlate : _rightPlate;
            Vector3 originalLocalPos = chosenPlate.localPosition;

            Projectile proj = chosenPlate.gameObject.AddComponent<Projectile>();
            proj.Init(_bossView.gameObject, _battleManager);

            Vector3 targetPosition = _playerTransform.position;

            while (chosenPlate != null && Vector3.Distance(chosenPlate.position, targetPosition) > 0.3f)
            {
                chosenPlate.position = Vector3.MoveTowards(chosenPlate.position, targetPosition, _plateSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(0.2f);

            if (chosenPlate != null)
            {
                while (Vector3.Distance(chosenPlate.localPosition, originalLocalPos) > 0.05f)
                {
                    chosenPlate.localPosition = Vector3.MoveTowards(chosenPlate.localPosition, originalLocalPos, _plateSpeed * Time.deltaTime);
                    yield return null;
                }
                chosenPlate.localPosition = originalLocalPos;
            }
        }

        _isAttacking = false;
    }
}