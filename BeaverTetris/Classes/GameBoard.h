#pragma once

#include "ServiceInterface.h"
#include <vector>
#include "GameStructures.h"

class Tetramino;
class GameBoardObserverInterface;
class TetraminosPositionsDelegate;

class GameBoard : public ServiceInterface
{
public:
	GameBoard(int width, int height);
	~GameBoard(void);

	void setTetraminoXYposition(Tetramino *aTetramino, int xPosition, int yPosition);

	int getGameBoardWidth();
	int getGameBoardHeight();

	Tetramino* getTetraminoForXYposition(int xPosition, int yPosition);
	GamePositionOnBoard getTetraminoPosition(Tetramino *aTetramino);

	std::vector <GamePositionOnBoard> getAvailableTetraminis();
	std::vector <GamePositionOnBoard> getTetraminosForType(TetraminoType aTetraminoType);

	void removeTetraminoForXYposition(int xPosition, int yPosition);
	void removeTetraminos(std::vector <GamePositionOnBoard> aNeedToRemoveTetraminos);
	void cleanGameBoard(void);

	void addObserver(GameBoardObserverInterface *aGameBoardObserver);
	void removeObserver(GameBoardObserverInterface *aGameBoardObserver);

private:

	int _gameBoardWidth;
	int _gameBoardHeight;
	std::vector< std::vector<Tetramino*> > _tetramins;
	GameBoardObserverInterface *_gameBoardObserver;
	TetraminosPositionsDelegate *_tetraminosSourceDelegate;

};

