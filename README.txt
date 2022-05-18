Gradebook console application

This application has been developed following the 'C# Fundamentals' course by Scott Allen on Pluralsight.

Course link: https://www.pluralsight.com/courses/csharp-fundamentals-dev

The application has been developed in C# coding language using the .NET framework on MVS 2019.

Functionality:
	- Stores grades from a class of students and computes statistics on these grades. 

Validation has been added to ensure that the user inputs grades of the right type (double) and format and that exceptions that might arise are handled appropriately.

Tests have been written in order to ensure that the application computes statistics correctly.

Files:
	Under src:
	 - Book.cs: defines a book interface and book classes for a disk book (stores data on disk) and an in-memory book (stores data on memory).
	 - Statistics.cs: defines the class 'Statistics' and methods for basic statistics to be computed (lowest grade, highest grade, average grade and letter grade).
	 - Program.cs: contains the Main() method (enter point for running the app). 

	Under test:
	 - BookTest.cs: contains a test to check if the application computes statistics correctly.
	 - TypeTests.cs: contains general practise test for learning C#. Should be removed.

Instructions for running the application:
	- Start MVS
	- Open the 'gradebook.sln' solution.
	- Set up 'Program.cs' as the Startup Project.
	- Make sure 'Debug' is selected as the Solution Configuration in MVS.
	- Click 'Start' to start running the application.
	- Follow the instructions that appear on the console.
