#pragma once

enum LoadingSreenZOrdes
{
	kLoadingBackgroundZOrder,
	kLoadingBeaverZOrder,
	kLoadingClockZOrder,
};

enum StartGameSreenZOrdes
{
	kStartGameBackgroundZOrder,
	kStartGameBeaverZOrder,
	kStartGameMenuZOrder,
	kBeaverClockArrowZOrder,
	kStartGameRegulateSoundPopUpZOrder,
};

enum StartGameRegulateSoundPopUpZOrders
{
	kStartGameRegulateSoundPopUpPadZOrder,
	kStartGameRegulateSoundSliderZOrder,
	kStartGameRegulateSoundEffectsSliderZOrder,
	kStartGameRegulateSoundPopUpCloseButtonZOrder,
};

enum SelectGameLevelScreenZOrders
{
	kSelectLevelMenuZOrder,
	kSelectLevelScenePlayerStatusControllerZOrder,
	kPlayerCreatorControllerZOrder,
};

enum SelectGameLevelPlayerStatusZOrders
{
	kSelectLevelPlayerStatusPadZOrder,
	kSelectLevelPlayerStatusNameZOrder,
	kSelectLevelPlayerStatusScoreZOrder,
};

enum PlayerCreatorZOrders
{
	kPlayerCreatorPadZOrder,
	kPlayerCreatorTextZOrder,
	kPlayerCreatorInputZOrder,
};