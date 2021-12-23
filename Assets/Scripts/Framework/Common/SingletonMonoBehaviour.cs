using System;
using UnityEngine;

namespace Framewark.Common
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
 		static T _instance;
		
		/// <summary>
		/// 
		/// </summary>
		public static T I {
			get {
				if (_instance == null) {
					_instance = FindObjectOfType<T>();
					//if (_instance == null) {
					//	Debug.LogError($"SingletonMonobehaviour継承のインスタンスが見つかりません：{typeof(T)}");
					//}
				}
				return _instance;
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Type t = typeof(T);

                    _instance = (T)FindObjectOfType(t);
                }

                return _instance;
            }
        }
		
		public static bool HasInstance() { return I != null; }
		
		/// <summary>
		/// 
		/// </summary>
		protected virtual void Awake()
		{	
			if ( Application.isPlaying ) {
				if ( this != I ) {
					Destroy( gameObject );
					//Debug.LogError($"SingletonMonobehaviourのインスタンスが重複したので削除：{typeof(T)}");
				}
			}
		}
		
		/// <summary>
		/// 
		/// </summary>
		protected virtual void OnDestroy()
		{
			_instance = null;
		}
		
    }
}