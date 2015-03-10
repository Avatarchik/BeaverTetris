#include "WinGameSystem.h"
#include "GameBoard.h"
#include "Tetramino.h"
#include "ServiceLocator.h"
#include "GameServicesKeys.h"
#include "GameTimeStepController.h"
#include "CurrentVictoryConditionDataSource.h"
#include "VictoryConditionInterface.h"
#include "GameStatesHelper.h"

using namespace std;

WinGameSystem::WinGameSystem()
{
	_gameBoard = (GameBoard*)ServiceLocator::getServiceForKey(gameBoardKey);
	_gameEnded = false;
	_playerWin = false;
	CurrentVictoryConditionDataSource *victoryConditionDataSource = (CurrentVictoryConditionDataSource*)ServiceLocator::getServiceForKey(currentVictoryConditionDataSourceKey);
	_currentVictoryCondition = victoryConditionDataSource->getCurrentVictoryCondition();
	_gameTimeStepController = (GameTimeStepController*)ServiceLocator::getServiceForKey(gameTimeStepControllerKey);
}


WinGameSystem::~WinGameSystem(void)
{
}


void WinGameSystem::updateSystem(float deltaTime)
{
	_gameTimeStepController->setUpdateAvailable(false);
	_gameEnded = true;
	_playerWin = true;
	GameStatesHelper::goToPopUp(kEndGamePopUp);
	/*
	checkWinGameState();
	if (_gameEnded == false)
	{
		checkLoseGameState();
	}
	*/
}

void WinGameSystem::checkWinGameState()
{
	if (_currentVictoryCondition->playerWin())
	{
		_gameTimeStepController->setUpdateAvailable(false);
		cocos2d::log("Win game");
		_gameEnded = true;
		_playerWin = true;
		GameStatesHelper::goToPopUp(kEndGamePopUp);
	}
	else
	{
		_currentVictoryCondition->update();
	}
}

void WinGameSystem::checkLoseGameState()
{
	if (loseGameChecker())
	{
		_gameTimeStepController->setUpdateAvailable(false);
		cocos2d::log("Lose game");
		_gameEnded = true;
		GameStatesHelper::goToPopUp(kEndGamePopUp);
	}
}

bool WinGameSystem::loseGameChecker()
{
	bool isThereTetraminoInLoseZone = false;
	int loseHeight = _gameBoard->getGameBoardHeight() - 1;
	
	for (int xIndex = 0; xIndex < _gameBoard->getGameBoardWidth(); xIndex++)
	{
		Tetramino *tetraminoIngameBoard = _gameBoard->getTetraminoForXYposition(xIndex, loseHeight);
		if (tetraminoIngameBoard->getTetraminoType() > kTetraminoEmpty)
		{
			isThereTetraminoInLoseZone = true;
			break;
		}
	}
	return isThereTetraminoInLoseZone;
}

bool WinGameSystem::gameEnded()
{
	return _gameEnded;
}

bool WinGameSystem::playerWin()
{
	return _playerWin;
}