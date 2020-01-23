using battleship.common;
using battleship.models;
using battleship.services;
using NUnit.Framework;
using System;

namespace battleshiptest
{
    [TestFixture]
    public class BoardServiceTest
    {
        BoardService boardService;

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void InvalidBoardSizeTest()
        {
            boardService = new BoardService(new Board(-10, 10));

            Assert.Throws<Exception>(() => boardService.SetupBoard());
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
            boardService = new BoardService(new Board(10, 10));
            boardService.SetupBoard();

            Position start = new Position(posX1, posY1);
            Position end = new Position(posX2, posY2);

            Ship ship = new Ship(start, end);

            boardService.AddShip(ship);

            bool isHit = boardService.Attack(new Position(attackX, attackY));

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
            boardService = new BoardService(new Board(10, 10));
            boardService.SetupBoard();

            Position start = new Position(posX1, posY1);
            Position end = new Position(posX2, posY2);

            Ship ship = new Ship(start, end);

            boardService.AddShip(ship);

            bool isHit = boardService.Attack(new Position(attackX, attackY));

            Assert.AreEqual(isHit, false);
        }

        [Test]
        public void SinkTestForOneShipOnTheBoard()
        {
            boardService = new BoardService(new Board(10, 10));
            boardService.SetupBoard();

            Position start = new Position(0, 0);
            Position end = new Position(0, 1);

            Ship ship = new Ship(start, end);

            boardService.AddShip(ship);

            boardService.Attack(start);
            boardService.Attack(start);
            boardService.Attack(new Position(100, 100));
            Assert.AreEqual(boardService.AllShipsSunk, false);

            boardService.Attack(end);

            Assert.AreEqual(boardService.AllShipsSunk, true);
        }
    }
}