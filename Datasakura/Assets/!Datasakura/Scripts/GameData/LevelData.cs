using System;

namespace DATASAKURA
{
	/// <summary>
	/// Данные уровня, не сохраняются между сессиями.
	/// </summary>
	public class LevelData
	{
		private int _victimsDeaths = 0;
		private int _predatorDeaths = 0;
		
		public static event Action OnVictimDeath;
		public static event Action OnPredatorDeath;

		/// <summary>
		/// Инициализация данных нового уровня.
		/// </summary>
		public void Reset()
		{
			_victimsDeaths = 0;
			_predatorDeaths = 0;
		}

		public int GetVictimsDeaths()
		{
			return _victimsDeaths;
		}

		public int GetPredatorDeaths()
		{
			return _predatorDeaths;
		}

		public void ModifyVictimsDeaths(int value)
		{
			_victimsDeaths = Math.Clamp(_victimsDeaths + value, 0, int.MaxValue);

			OnVictimDeath?.Invoke();
		}

		public void ModifyPredatorDeaths(int value)
		{
			_predatorDeaths = Math.Clamp(_predatorDeaths + value, 0, int.MaxValue);

			OnPredatorDeath?.Invoke();
		}
	}
}
