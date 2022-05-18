using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact] /*argument attached to the methods that follows (Test1). It is a decorator*/
        public void BookCalculatesAnAverageGrade()
        {
            // arrange
            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            // act
            var result = book.GetStatistics();

            // assert
            Assert.Equal(85.6, result.Average, 1); // This is a letter B
            Assert.Equal(90.5, result.High, 1);
            Assert.Equal(77.3, result.Low, 1);
            Assert.Equal('B', result.Letter);
        }

        /*[Fact]
        public void OutOfRangeGradeNotValid()
        {
           var book = new InMemoryBook("Book 3");
            
           var result = book.AddGrade(105);

           Assert.Equal("Invalid grade", result);
            }*/
    }
}
