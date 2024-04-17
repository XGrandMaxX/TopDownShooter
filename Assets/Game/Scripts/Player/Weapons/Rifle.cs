namespace Game.Scripts.Player.Weapons
{
    public class Rifle : Weapon
    {
        private void Update() => RotateRifle();

        private void FixedUpdate()
        {
            if(_attacking)
                Shoot();
        }
    }
}
