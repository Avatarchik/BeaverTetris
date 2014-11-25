#pragma once

#include "ServiceInterface.h"
#include <string>
#include <vector>
#include "cocos2d.h"
#include "GameStructures.h"
#include "CurrentPlayerDataSource.h"
#include "GameViewElementsDataSource.h"

class StartGameMenuDataSource :public ServiceInterface
{
public:
	StartGameMenuDataSource(CurrentPlayerDataSource *aCurrentPlayerDataSource, GameViewElementsDataSource *aGameViewElementsDataSource);
	~StartGameMenuDataSource();

	int getMenuItemsCount();
	std::string getMenuItemUnselectedImageForIndex(int aIndex);
	std::string getMenuItemSelectedImageForIndex(int aIndex);
	cocos2d::Vec2 getMenuItemPositionForIndex(int aIndex);
	GameEvent getMenuItemEventForIndex(int aIndex);

	void refreshDataSource();


private:

	CurrentPlayerDataSource *_currentPlayerDataSource;
	GameViewElementsDataSource *_gameViewElementsDataSource;
	std::vector <MenuItemInformation> _menuItems;

};

