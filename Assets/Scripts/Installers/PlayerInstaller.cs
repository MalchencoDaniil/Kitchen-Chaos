using UnityEngine;
using Zenject;

namespace KitchenChaos.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private Player.Movement _playerPrefab;

        public override void InstallBindings()
        {
            Player.Movement _playerToSpawn = Instantiate(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity);
            Container.Bind<PlayerPickUp>().FromComponentInNewPrefab(_playerToSpawn.GetComponent<PlayerPickUp>()).AsTransient();
        }
    }
}