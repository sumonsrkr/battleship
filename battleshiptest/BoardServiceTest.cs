using battleship.common;
using battleship.models;
using battleship.services;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace battleshiptest
{
    [TestFixture]
    public class BoardServiceTest
    {
        BoardService _boardService;
        int _boardWidth;
        int _boardHeight;

        List<Position> _attackPositions;

        [SetUp]
        public void Setup()
        {
            _boardWidth = 10;
            _boardHeight = 10;

            _attackPositions = new List<Position>();

            for (int x = 0; x < _boardHeight; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    _attackPositions.Add(new Position(x, y));
                }
            }
        }

        [Test]
        public void InvalidBoardSizeTest()
        {
            _boardService = new BoardService(new Board(-10, 10));

            Assert.Throws<Exception>(() => _boardService.SetupBoard());
        }


        [TestCase(1, 1, 3, 3, 3, 3)]
        [TestCase(1, 1, 3, 3, 3, 2)]
        [TestCase(1, 1, 3, 3, 3, 1)]
        [TestCase(1, 1, 3, 3, 2, 3)]
        [TestCase(1, 1, 3, 3, 2, 2)]
        [TestCase(1, 1, 3, 3, 2, 1)]
        [TestCase(1, 1, 3, 3, 1, 3)]
        [TestCase(1, 1, 3, 3, 1, 2)]
        [TestCase(1, 1, 3, 3, 1, 1)]
        public void HitTestForOneShipInTheBoard(int posX1, int posY1, int posX2, int posY2, int attackX, int attackY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));
            _boardService.SetupBoard();

            Position start = new Position(posX1, posY1);
            Position end = new Position(posX2, posY2);

            Ship ship = new Ship(start, end);

            _boardService.AddShip(ship);

            bool isHit = _boardService.Attack(new Position(attackX, attackY));

            Assert.AreEqual(isHit, true);
        }

        [TestCase(1, 1, 3, 3, 0, 0)]
        [TestCase(1, 1, 3, 3, 0, 1)]
        [TestCase(1, 1, 3, 3, 0, 2)]
        [TestCase(1, 1, 3, 3, 9, 9)]
        [TestCase(1, 1, 3, 3, 9, 8)]
        [TestCase(1, 1, 3, 3, 9, 7)]
        [TestCase(1, 1, 3, 3, 10, 10)]
        [TestCase(1, 1, 3, 3, 20, 20)]
        [TestCase(1, 1, 3, 3, 30, 300)]
        public void MissTestForOneShipInTheBoard(int posX1, int posY1, int posX2, int posY2, int attackX, int attackY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));
            _boardService.SetupBoard();

            Position start = new Position(posX1, posY1);
            Position end = new Position(posX2, posY2);

            Ship ship = new Ship(start, end);

            _boardService.AddShip(ship);

            bool isHit = _boardService.Attack(new Position(attackX, attackY));

            Assert.AreEqual(isHit, false);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(1, 1, 3, 4)]
        [TestCase(0, 0, 9, 4)]
        public void SinkTestForOneShipOnTheBoard(int shipStartPosX, int shipStartPosY, int shipEndPosX, int shipEndPosY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));

            _boardService.SetupBoard();

            Ship ship = new Ship(new Position(shipStartPosX, shipStartPosY), new Position(shipEndPosX, shipEndPosY));

            _boardService.AddShip(ship);

            foreach (Position position in _attackPositions)
            {
                _boardService.Attack(position);
            }

            Assert.AreEqual(_boardService.AllShipsSunk, true);
        }

        [TestCase(0, 0, 0, 5)]
        [TestCase(2, 2, 2, 9)]
        [TestCase(1, 1, 3, 20)]
        [TestCase(0, 0, 100, 100)]
        public void UnsinkTestForOneShipOnTheBoard(int shipStartPosX, int shipStartPosY, int shipEndPosX, int shipEndPosY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));

            _boardService.SetupBoard();

            Ship ship = new Ship(new Position(shipStartPosX, shipStartPosY), new Position(shipEndPosX, shipEndPosY));

            _boardService.AddShip(ship);

            foreach (Position position in _attackPositions)
            {
                _boardService.Attack(position);
            }

            Assert.AreEqual(_boardService.AllShipsSunk, false);
        }

        [TestCase(0, 0, 0, 0)]
        [TestCase(2, 2, 2, 2)]
        [TestCase(1, 1, 3, 4)]
        [TestCase(0, 0, 9, 4)]
        public void ABattleShipCanBeAddedTest(int shipStartPosX, int shipStartPosY, int shipEndPosX, int shipEndPosY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));

            _boardService.SetupBoard();

            Ship ship = new Ship(new Position(shipStartPosX, shipStartPosY), new Position(shipEndPosX, shipEndPosY));

            bool added = _boardService.AddShip(ship);

            Assert.AreEqual(added, true);
        }

        [TestCase(1, 1, 3, 20)]
        [TestCase(0, 0, 100, 100)]
        public void ABattleShipCannotBeAddedTest(int shipStartPosX, int shipStartPosY, int shipEndPosX, int shipEndPosY)
        {
            _boardService = new BoardService(new Board(_boardWidth, _boardHeight));

            _boardService.SetupBoard();

            Ship ship = new Ship(new Position(shipStartPosX, shipStartPosY), new Position(shipEndPosX, shipEndPosY));

            bool added = _boardService.AddShip(ship);

            Assert.AreEqual(added, false);
        }
    }
}