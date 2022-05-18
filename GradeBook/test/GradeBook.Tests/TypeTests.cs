using System;
using Xunit; // library?

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage); // describe wht method WOULD look like!

    public class TypeTests
    {
        int count = 0;

        [Fact] // this one is not a unit test. is checking that my method can invoke another
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            //log = new writelogdelegate(returnmessage);
            log += ReturnMessage;  // += for invoking multiple methods
            log += IncrementCount;

            var resul = log("Hello!");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message;
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void ValueTypeAlsoPassByValue()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private int GetInt()
        {
            return 3;
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }

        [Fact]
        public void CSharpCouldPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name"); // ref modifies book1 var to point to new object!

            Assert.Equal("New Name", book1.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);

        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1"); // book1 is a reference to an object created with this method
            GetBookSetName(book1, "New Name"); // Cannot modify var book1 with this method

            Assert.Equal("Book 1", book1.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name); // just makes a copy of book1 but doesnt modify this var
            //book.Name = name;
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "New Name");

            Assert.Equal("New Name", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringBehavesLikeValueType() // although is a class (reference type)
        {
            string name = "Scott";
            var upper = MakeUpperCase(name);

            Assert.Equal("Scott", name);
            Assert.Equal("SCOTT", upper);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper(); // name does not change! strings are immutable
        }

        [Fact] /*argument attached to the method that follows (Test1). It is a decorator*/
        public void GetBookReturnsDifferentObjects()
        {

            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book1.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book1, book2);
        }

        [Fact] /*argument attached to the method that follows (Test1). It is a decorator*/
        public void TwoVarCanReferenceSameObject()
        {

            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
            // var hold values, not objects so can point to same object
        }

        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
