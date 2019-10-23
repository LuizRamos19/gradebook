using System;
using Xunit;

namespace GradeBook.Tests
{

    // Defining a delegate
    public delegate string WriteLogDelegate(string logMessage);

    public class TypeTests
    {
        int count = 0;

        [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            // to use multi-cast delegate we need to initialize the delegate variable
            WriteLogDelegate log = ReturnMessage;

            // In delegate, we can pass a method to a variable
            // log = ReturnMessage;

            // We can also use multi-cast delegate to add more than an
            // method to the delegate variable
            log += ReturnMessage;
            log += IncrementCount;

            var result = log("Hello!");
            Assert.Equal("hello!", result);
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToLower();
        }
        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);

            Assert.Equal(42, x);
        }

        private void SetInt(ref int x)
        {
            x = 42;
        }
        private int GetInt()
        {
            return 3;
        }

        [Fact]
        public void CSharpCanPassByRef()
        {
            var book = GetBook("Book 1");
            // the ref atribute passed to the method and receiving in the method, inform to C#
            // the book variable was passed by reference and the method can change its value
            GetBookSetName(ref book, "New Name");

            Assert.Equal("New Name", book.Name);
        }

        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            // exist another way to pass a variable by reference in a method parameter
            // that is out (out Book book), this inform to C# that the variable needs to be
            // initialized by this method. If we comment the line below, the method will not 
            // be accepted by C#
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CSharpIsPassByValue()
        {
            var book = GetBook("Book 1");
            GetBookSetName(book, "New Name");

            Assert.Equal("Book 1", book.Name);
        }

        private void GetBookSetName(InMemoryBook book, string name)
        {
            book = new InMemoryBook(name);
        }

        [Fact]
        public void CanSetNameFromReference()
        {
            var book = GetBook("Book 1");
            SetName(book, "New Name");

            Assert.Equal("New Name", book.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            book.Name = name;
        }

        [Fact]
        public void StringsBehaveLikeValueTypes()
        {
            string name = "Scott";
            var upper = MakeUppercase(name);

            Assert.Equal("Scott", name);
            Assert.Equal("SCOTT", upper);
        }

        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]
        public void GetBookReturnsDifferentObjects()
        {
            var book = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1", book.Name);
            Assert.Equal("Book 2", book2.Name);
            Assert.NotSame(book, book2);
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
            var book = GetBook("Book 1");
            var book2 = book;

            Assert.Same(book, book2);
            Assert.True(Object.ReferenceEquals(book, book2));
        }
        InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
