﻿namespace Hovercast.Core.Input {

	/*================================================================================================*/
	public interface IInputProvider {
		

		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		void UpdateInput();

		/*--------------------------------------------------------------------------------------------*/
		IInputSide GetSide(bool pIsLeft);

	}

}
