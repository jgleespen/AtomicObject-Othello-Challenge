# AO-Othello-Challenge

## Overview
This project contains three implementations of a bot that will play moves in othello.
Each bot depends on functions inside `ai.LegalMoveFunctions` to acquire a list of playable moves for that turn. 

## Bot Details

**Random Bot**
`ai.Players.PlayerPrioritizeCornersRandom`
* Will acquire list of legal moves and will play corners if available. 
* If no corners are available a side move will be checked for. 
* If there is no side move, then a random move will be picked and played.

**Minimax Bot**
`ai.Players.PlayerMinimax`
* Will acquire lists of legal moves for opponent and player and recurse to a desired search depth (in this case 6). 
* Throughout recursion, our player will be the maximizing player, and the opponent will be minimized. 
* The `MinimaxValue()` Board extension function will return the best value for a score based on a heuristic,
* The `ChooseMinimax()` Board extension function will test every possible move to find the greatest heuristic for our player

**Minimax Bot**
`ai.Players.PlayerMinimaxPrioritizeCorners`
* Same as above but corner spots will always be taken. 