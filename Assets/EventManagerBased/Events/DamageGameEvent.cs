namespace EventManagerBased.Events
{
	public class DamageGameEvent : GameEvent
	{
		public int DamageDealt { get; private set; }

		public DamageGameEvent(int damageDealt) => DamageDealt = damageDealt;
	}
}