namespace Game.Scripts.Player.Weapons
{
    public sealed class Rifle : Weapon
    {
        private void Update() => RotateWeapon();

        private void FixedUpdate()
        {
            if(_attacking)
                Shoot();
        }
    }
}
