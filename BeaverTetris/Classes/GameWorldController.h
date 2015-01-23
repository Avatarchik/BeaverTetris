#pragma once

#include "cocos2d.h"

class GameBoardController;
class GameTimeStepController;

class GameWorldController : public cocos2d::Node
{
public:
	GameWorldController(void);
	~GameWorldController(void);

	virtual void onEnterTransitionDidFinish();
	virtual void update(float delta);

private:

	GameBoardController *_gameBoardController;
	GameTimeStepController *_gameTimeStepController;

	void makeControllerView();

};

