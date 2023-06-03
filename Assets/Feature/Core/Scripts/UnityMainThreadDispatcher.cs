using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace FoolCardGame.Core
{
	/// <summary>
	/// Диспетчер Unity потока
	/// </summary>
	public class UnityMainThreadDispatcher : MonoBehaviour
	{
		private static readonly Queue<Action> _executionQueue = new Queue<Action>();
		private static UnityMainThreadDispatcher _instance = null;

		private void Awake()
		{
			if (_instance == null)
			{
				_instance = this;
				DontDestroyOnLoad(gameObject);
			}
		}
		
		private void Update()
		{
			lock (_executionQueue)
			{
				while (_executionQueue.Count > 0)
				{
					_executionQueue.Dequeue().Invoke();
				}
			}
		}

		/// <summary>
		/// Синглтон
		/// </summary>
		/// <returns>Диспетчер</returns>
		/// <exception cref="Exception">Ошибка если не найден экземпляр</exception>
		public static UnityMainThreadDispatcher Instance()
		{
			if (!Exists())
			{
				throw new Exception(
					"UnityMainThreadDispatcher could not find the UnityMainThreadDispatcher object. Please ensure you have added the MainThreadExecutor Prefab to your scene.");
			}

			return _instance;
		}
		
		/// <summary>
		/// Блокирует очередь и добавляет в нее IEnumerator
		/// </summary>
		/// <param name="action">Функция IEnumerator, которая будет выполняться из основного потока.</param>
		public void Enqueue(IEnumerator action)
		{
			lock (_executionQueue)
			{
				_executionQueue.Enqueue(() => { StartCoroutine(action); });
			}
		}

		/// <summary>
		/// Блокирует очередь и добавляет действие в очередь
		/// </summary>
		/// <param name="action">Функция, которая будет выполняться из основного потока</param>
		public void Enqueue(Action action)
		{
			Enqueue(ActionWrapper(action));
		}

		/// <summary>
		/// Блокирует очередь и добавляет действие в очередь, возвращая Task, которая завершается после завершения действия.
		/// </summary>
		/// <param name="action">Функция, которая будет выполняться из основного потока.</param>
		/// <returns>Task, которую можно ожидать до завершения действия</returns>
		public Task EnqueueAsync(Action action)
		{
			var tcs = new TaskCompletionSource<bool>();

			void WrappedAction()
			{
				try
				{
					action();
					tcs.TrySetResult(true);
				}
				catch (Exception ex)
				{
					tcs.TrySetException(ex);
				}
			}

			Enqueue(ActionWrapper(WrappedAction));
			return tcs.Task;
		}
		
		private IEnumerator ActionWrapper(Action a)
		{
			a();
			yield return null;
		}
		
		public static bool Exists()
		{
			return _instance != null;
		}
		
		private void OnDestroy()
		{
			_instance = null;
		}
	}
}