# .NET Framework vs .NET Core

The only difference between these two techs is that the .NET Core is an actual open project that can be installed in all SOs, like Windows, Linux and MAC.

# .NET CLI

The .NET CLI is a command line prompt for .NET that give to the developer a cmd interface do create, build, run and test any of the .NET languages supported. For example, to create a c# project, the only thing to do is:

```
dotnet new console 
```

`dotnet run command`

The first thing that happens when execute the _dotnet run_ command in the project folder, is to execute another command that is _dotnet restore_. This command uses the `NuGet` package manager of .NET to get all the dependencies packages that my application need. The second command to execute is the _dotnet build_, that compile all the files that we have generated with c# and create a .dll file containing all this code.

After that, the .NET runtime search for a method _Main_ in all the application to execute the code. This method, is called a entrypoint for the application.

Ej:
```
C:\Users\luiz.g.duarte.ramos\training\pluralsight\dotnet\gradebook\src\GradeBook> dotnet restore
Restore completed in 38.13 ms for C:\Users\luiz.g.duarte.ramos\training\pluralsight\dotnet\gradebook\src\GradeBook\GradeBook.csproj.

C:\Users\luiz.g.duarte.ramos\training\pluralsight\dotnet\gradebook\src\GradeBook> dotnet build
Microsoft (R) Build Engine version 16.2.32702+c4012a063 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  Restore completed in 38.82 ms for C:\Users\luiz.g.duarte.ramos\training\pluralsight\dotnet\gradebook\src\GradeBook\GradeBook.csproj.
  GradeBook -> C:\Users\luiz.g.duarte.ramos\training\pluralsight\dotnet\gradebook\src\GradeBook\bin\Debug\netcoreapp2.2\GradeBook.dll

Build succeeded.
    0 Warning(s)
    0 Error(s)

Time Elapsed 00:00:00.94
```

`unit tests`

To test our application we will install another library named xunit, to help us with our unit tests. This lib is not a .NET Core lib, but is a NuGet lib. To create a project with xunit, we need first execute the command `dotnet new test`. This create a new project inside our folder. To run our unit tests, in visual studio code we have many extensions to do that. However the dotnet already have a task for that. Is only to execute `dotnet test`.

In unit test, the method that we want to test, needs a decorator before it named `Factor`, to the test runtime undestand the method like a test unit. For example:

```
namespace GradeBook.Tests
{
    public clas UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }
    }
}
```

In unit test we have a standard way that most people use when creating unit tests, that is called triple A (arrange, act, assert). Ej:

```
namespace GradeBook.Tests
{
    public clas UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // arrange
            var x = 5;
            var y = 2;
            var expected = 7;

            // act
            var actual = x + y;

            // assert
            Assert.Equal(expected, actual);
        }
    }
}
```

`Solution file`

In .NET we have a file that is used to create a unique solution that contains all our projects. This solution file is used by visual studio 2015/2017 to build all the projects. In .NET CLI we have an option to create a solution file that is `dotnet new sln`. To add projects to this solution file, the command `dotnet sln add [pathFile.csproj]`. The `.csproj` is the file that contains all the dependencies of our projects. Remember that the project and the test project are two different projects. After that, to build this sln file all we need to do is execute the command `dotnet build`.

# C# language

`Overloading methods`

In C#, like in Java, is posible to right different method with the same name. Because in this language, it consider not the name of the method but it signature, that consist in the method name and the parameter passed. So, is posible to right the two method below:

NOTE: The method signature involve the method name and the parameters, not the return type

```
public void AddGrade(char letter)
{
    // ..
}

public void AddGrade(int grade)
{
    // ..
}
```

`Make variable readonly`

Exist many way of doing a variable readonly.

- Using getter and setters;
- Using readonly property;
- Using const members;

`Events and delegate`

Delegate are method that we can generate like an attribute to be passed in a parameter.

Events are used to listen for a specific action and tell to everbody who need to know that the action was completed.

Events are most common used in Xamarin and WPF applications, the .NET Core rarely use this.

`Base class`

Base class is a class that we can use to don't need to repeat code. For example, a studant and a teacher, both has a name, so with a base class I don't need to repeat the same property name for both, I can use a base class to inheritance the property name to both classes.

```
public class NamedObject
{
    public NamedObject(string name)
    {
        Name = name;
    }
    public string Name
    {
        get;
        // private set;
        set;
    }
}

// to access the base class from our classes

public class Book : NamedObject
{
    // the base c# notation is informing that the base class NamedObject need a parameter that is a name.
    public Book(string name) : base(name)
    {
        grades = new List<double>();
        Name = name;
    }
}
```

In C# every class inherit a base class, even if we don't explicit write it. When we don't especify a class for inheritance, by default this class have the base class Object inherit.

```
public class NamedObject : Object { }
// is the same as
public class NamedObject { }
```
`Abstract class`

Abstract class contains abstract method that don't need to be implemented. Example:

```
public abstract Statistics GetStatistics();
```

`Interface type`

Another way to insert polymorphism and encapsulation on C# is defining a Interface type. It contains no implementation details. All members inside a interface had the public implementation.

`Using IDisposable`

When we try to access a resource like a filesystem or access a database, the C# give to us some libraries to do that, like the System.IO or the System.Data (this last one to access the database). To use these two we need to open the resource and close it, to don't keep a resource consuming the memory of the program. However, this can be difficult to close when we have a trow exception while the method are executing. C# provide to us an interface to solve this problem. The IDisposable implement a single method that is dispose() (similarly to Close()). To use this interface, we need to use the `using` key word from C#, that provide to us a pattern that always perform a dispose() after the all code inside it had finished.

```
var writer = File.AppendText($"{Name}.txt");
writer.WriteLine(grade);
// If we had an error before this line, the close will not be called and the resource will keep open consuming memory.
writer.Close();
```

```
using(var writer = File.AppendText($"{Name}.txt"))
{
    writer.WriteLine(grade);
} // The using statement performs a dispose after all code inside it had finished.
```