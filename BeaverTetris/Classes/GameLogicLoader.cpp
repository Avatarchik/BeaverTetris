#include "GameLogicLoader.h"
#include "ServiceLocator.h"
#include "GameServicesKeys.h"
#include "GameDesignConstants.h"

#include "GameBoard.h"
#include "GameTimeStepController.h"
#include "CurrentDetailDataSource.h"
#include "CurrentDetailController.h"
#include "FullLineCombination.h"
#include "SimilarTetraminosCombination.h"
#include "TetrisLogicSystem.h"
#include "FillingGapInBoardSystem.h"
#include "CurrentLevelDataSource.h"
#include "GameFlowSystem.h"
#include "CurrentVictoryConditionDataSource.h"
#include "WinGameSystem.h"
#include "SpellBox.h"
#include "NewTetraminoDetailDataSource.h"

GameLogicLoader::GameLogicLoader(void)
{
}


GameLogicLoader::~GameLogicLoader(void)
{
}

void GameLogicLoader::loadResources()
{
	GameBoard *gameBoard = new GameBoard(tetrisBlocksWidth, tetrisBlocksHeight);
	ServiceLocator::setServiceForKey(gameBoard, gameBoardKey);

	CurrentLevelDataSource *currentLevelDataSource = new CurrentLevelDataSource();
	ServiceLocator::setServiceForKey(currentLevelDataSource, currentLevelDataSourceKey);

	NewTetraminoDetailDataSource *newTetraminoDetailDataSource = new NewTetraminoDetailDataSource();
	ServiceLocator::setServiceForKey(newTetraminoDetailDataSource, newTetraminoDetailDataSourceKey);

	GameTimeStepController *gameTimeStepController = new GameTimeStepController();
	ServiceLocator::setServiceForKey(gameTimeStepController, gameTimeStepControllerKey);

	CurrentDetailDataSource *currentDetailDataSource = new CurrentDetailDataSource();
	ServiceLocator::setServiceForKey(currentDetailDataSource, currentDetailDataSourceKey);

	GameFlowSystem *gameFlowSystem = new GameFlowSystem();
	gameTimeStepController->addSystem(gameFlowSystem);

	CurrentVictoryConditionDataSource *currentVictoryConditionDataSource = new CurrentVictoryConditionDataSource();
	ServiceLocator::setServiceForKey(currentVictoryConditionDataSource, currentVictoryConditionDataSourceKey);

	WinGameSystem *winGameSystem = new WinGameSystem(gameBoard, currentVictoryConditionDataSource);
	gameTimeStepController->addSystem(winGameSystem);

	CurrentDetailController *currentDetailController = new CurrentDetailController(gameBoard, currentDetailDataSource);
	gameTimeStepController->addSystem(currentDetailController);
	ServiceLocator::setServiceForKey(currentDetailController, currentDetailControllerKey);

	FullLineCombination *fullLineCombinationModel = new FullLineCombination(gameBoard);
	ServiceLocator::setServiceForKey(fullLineCombinationModel, fullLineCombinationModelKey);

	SimilarTetraminosCombination *similarTetraminosCombinationModel = new SimilarTetraminosCombination(gameBoard);
	ServiceLocator::setServiceForKey(similarTetraminosCombinationModel, similarTetraminosCombinationModelKey);

	FillingGapInBoardSystem *fillingGapInBoardSystem = new FillingGapInBoardSystem(gameBoard);
	ServiceLocator::setServiceForKey(fillingGapInBoardSystem, fillingGapInBoardSystemKey);

	TetrisLogicSystem *tetrisLogicSystem = new TetrisLogicSystem(gameBoard);
	gameTimeStepController->addSystem(tetrisLogicSystem);
	ServiceLocator::setServiceForKey(tetrisLogicSystem, tetrisLogicSystemKey);

	SpellBox *spellBox = new SpellBox();
	ServiceLocator::setServiceForKey(spellBox, spellBoxKey);

}