#pragma once

#include "SpellInterface.h"

class GameBoard;
class AddLineToBoardBottomSpellDelegate;

class AddLineToBoardBottomSpell :public SpellInterface
{
public:
	AddLineToBoardBottomSpell(void);
	~AddLineToBoardBottomSpell(void);

	virtual bool spellAvailable(void);
	virtual void castSpell();

	void setDelegate(AddLineToBoardBottomSpellDelegate *aDelegate);

private:

	GameBoard *_gameBoard;
	AddLineToBoardBottomSpellDelegate *_delegate;

	void riseAllTetraminos();
	void riseTetraminosLineInBoard(int aLineIndex);
	void riseTetraminoInBoardOnXY(int xPosition, int yPosition);
	void addRowToBottom();
	void fillBottomWithRandomTetraminos();
	void makeGapInBottom();
	bool checkEmptyTetraminosInBottom();
	void separateTetraminos();

	void sendMessageToDelegate();

};

