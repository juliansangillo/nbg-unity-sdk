using NaughtyBikerGames.SDK.Adapters.Interfaces;
using UnityEngine;

namespace NaughtyBikerGames.SDK.Adapters {
    /**
	* Default implementation of IInput
	*
	* @author Julian Sangillo
	* @version 3.0
	* @since 3.0
	*/
	public class InputAdapter : IInput {
        /// Default implementation of IInput.MousePosition
		public Vector3 MousePosition {
            get {
                return Input.mousePosition;
            }
        }
	}
}