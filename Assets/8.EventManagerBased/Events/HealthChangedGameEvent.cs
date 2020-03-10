namespace _8.EventManagerBased.Events
{
	public class HealthChangedGameEvent : GameEvent
	{
		public int CurrentHealth { get; private set; }

		public HealthChangedGameEvent(int currentHealth) => CurrentHealth = currentHealth;
	}
}