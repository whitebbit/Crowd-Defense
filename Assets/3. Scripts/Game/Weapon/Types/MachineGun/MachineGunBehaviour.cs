namespace _3._Scripts.Game.Weapon.Types.MachineGun
{
    public class MachineGunBehaviour : WeaponBehaviour
    {
        protected override WeaponFSM GetWeaponFSM()
        {
            return new MachineGun(Config, weaponObject);
        }
    }
}