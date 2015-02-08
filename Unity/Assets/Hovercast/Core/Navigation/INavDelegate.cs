﻿namespace Hovercast.Core.Navigation {

	/*================================================================================================*/
	public interface INavDelegate {

		
		////////////////////////////////////////////////////////////////////////////////////////////////
		/*--------------------------------------------------------------------------------------------*/
		NavLevel GetTopLevel();

		/*--------------------------------------------------------------------------------------------*/
		string GetTopLevelTitle();

		/*--------------------------------------------------------------------------------------------*/
		void HandleItemSelection(NavLevel pLevel, NavItem pItem);

		/*--------------------------------------------------------------------------------------------*/
		void HandleLevelChange(NavLevel pNewLevel, int pDirection);

	}

}
