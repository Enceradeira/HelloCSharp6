using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

using NUnit.Framework;

namespace HelloCSharp6
{
    [TestFixture]
    public class HelloCSharp6Tests
    {
        public class Person
        {
            public string Name { get; set; } = "Jane";

            public bool IsPerson { get; } = true;

            public IEnumerable<Person> Children { get; set; }

            public void ThrowException<T>() where
                T : Exception, new()
            {
                throw new T();
            }

            public string SaySomething() => $"My name is {Name}";
        }

        [Test]
        public void UsingStatic()
        {
            WriteLine("Where is Console?");
        }

        [Test]
        public void AutoPropertyInitializer()
        {
            var person = new Person();

            Assert.That(person.Name, Is.EqualTo("Jane"));
            Assert.That(person.IsPerson, Is.True);
        }

        [Test]
        public void ExpressionBodiedFunctionMembers()
        {
            var person = new Person();
            
            Assert.That(person.SaySomething(), Is.EqualTo("My name is Jane"));
        }

        [Test]
        public void ExceptionFilterShouldCatch()
        {
            var doCatch = true;
            var person = new Person();

            try
            {
                person.ThrowException<NotFiniteNumberException>();
            }
            catch (Exception) when (doCatch)
            {}
        }

        [Test]
        public void IndexInitializer()
        {
            var numbers = new Dictionary<int, string>
            {
                [7] = "seven",
                [9] = "nine",
                [13] = "thirteen"
            };

            Assert.That(numbers[7], Is.EqualTo("seven"));
        }

        [Test]
        public void NameOf()
        {
            var person = new Person();
            Assert.That(nameof(person), Is.EqualTo("person"));
            Assert.That(nameof(person.Name), Is.EqualTo("Name"));
        }

        [Test]
        public void NullConditionalOperatorWithNull()
        {
            var person = new Person();

            var names = person.Children?.Select(c => c.Name) ?? new string[] { };
            var childrenName = string.Join(" and ", names);
            Assert.That(childrenName, Is.EqualTo(""));
        }

        [Test]
        public void NullConditionalOperatorWithoutNull()
        {
            var person = new Person
            {
                Children = new[]
                {
                    new Person { Name = "Maria" },
                    new Person { Name = "Peter" }
                }
            };
            var names = person.Children?.Select(c => c.Name) ?? new string[] { };
            var childrenName = string.Join(" and ", names);
            Assert.That(childrenName, Is.EqualTo("Maria and Peter"));
        }

        [Test]
        public void StringInterpolation()
        {
            var person = new Person { Name = "John" };
            Assert.That($"My name is {person.Name}", Is.EqualTo("My name is John"));
        }
    }
}