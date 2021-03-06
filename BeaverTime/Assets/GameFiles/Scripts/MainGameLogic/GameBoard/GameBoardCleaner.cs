﻿using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using DG.Tweening;

public class GameBoardCleaner : MonoBehaviour {

    public int rewardForLine;
    public float moveTimeForOneBlock;
    public RemoveBlocksCondition removeBlocksCondition;
    public AudioSource removeBlockSoundEffect;

    GameBoard _gameBoard;
    GamePlayerDataController _playerData;

    void Start () {

        _gameBoard = ServicesLocator.getServiceForKey(typeof(GameBoard).Name) as GameBoard;
        _playerData = ServicesLocator.getServiceForKey(typeof(GamePlayerDataController).Name) as GamePlayerDataController;

    }
	
	void Update () {

        List<int> linesForDeliting = getLinesForDeliting();
        if(linesForDeliting.Count > 0 && _gameBoard.gameBoardLocked == false)
        {
            _gameBoard.gameBoardLocked = true;
            Sequence explosionSequence = DOTween.Sequence();

            GameObject block = _gameBoard.getObjectForXY(0, linesForDeliting[0]);
            ParticleSystem particle = block.GetComponent<ParticleSystem>();
            float explosionDutation = particle.startLifetime;

            explosionSequence.AppendCallback(()=> showExplosionsInLines(linesForDeliting));
            explosionSequence.AppendInterval(explosionDutation);
            explosionSequence.AppendCallback(() => deleteAndDownLinesUpDeletedLines(linesForDeliting));
            explosionSequence.AppendCallback(() => _gameBoard.gameBoardLocked = false);
        }

    }

    List<int> getLinesForDeliting()
    {
        List<int> linesForDeliting = new List<int>();

        for (int yIndex = 0; yIndex < _gameBoard.getBoardHeight(); yIndex++)
        {
            if (lineFilledForYIndex(yIndex))
            {
                linesForDeliting.Add(yIndex);
            }
        }

        return linesForDeliting;
    }

    bool lineFilledForYIndex(int yIndex)
    {
        bool empty = true;

        for (int xIndex = 0; xIndex < _gameBoard.getBoardWidth(); xIndex++)
        {
            if (_gameBoard.getObjectForXY(xIndex, yIndex) == null)
            {
                empty = false;
                break;
            }
        }

        return empty;
    }

    void showExplosionsInLines(List<int> aLines)
    {
        foreach (int aLineIndex in aLines)
        {
            showExplosionEffectInLineForIndex(aLineIndex);
        }
    }

    void deleteAndDownLinesUpDeletedLines(List<int> aLines)
    {
        foreach (int aLineIndex in aLines)
        {
            deleteLineForIndex(aLineIndex);
            _playerData.playerScore = _playerData.playerScore + rewardForLine;
        }

    }

    void showExplosionEffectInLineForIndex(int aLineIndex)
    {

        removeBlockSoundEffect.Play();

        for (int xIndex = 0; xIndex < _gameBoard.getBoardWidth(); xIndex++)
        {
            GameObject block = _gameBoard.getObjectForXY(xIndex, aLineIndex);
            ParticleSystem particle = block.GetComponent<ParticleSystem>();
            particle.Clear();
            particle.Play();
        }
    }

    void deleteLineForIndex(int aLineIndex)
    {
        for (int xIndex = 0; xIndex < _gameBoard.getBoardWidth(); xIndex++)
        {
            GameObject block = _gameBoard.getObjectForXY(xIndex, aLineIndex);
            BlockLiveController blockLives = block.GetComponent<BlockLiveController>();
            blockLives.removeOneBlockLive();
            if (blockLives.blockLivesCount <= 0)
            {
                BlockType type = block.GetComponent<BlockType>();
                removeBlocksCondition.blockWasRemoovedWithType(type.blockType);
                _playerData.playerScore = _playerData.playerScore + type.blockReward;
                Destroy(block);
                _gameBoard.deleteObjectForXY(xIndex, aLineIndex);
            }
        }
    }

}
