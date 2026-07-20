using EpsilonWebApp.Bonus;

namespace EpsilonWebApp.Tests
{
    public class NamePrinterTests
    {
        [Fact]
        public void Print_Employee_WritesName()
        {
            using var writer = new StringWriter();
            var printer = new NamePrinter(writer);

            printer.Print(new Employee { Name = "Alice" });

            Assert.Equal("Alice", writer.ToString().Trim());
        }

        [Fact]
        public void Print_Manager_WritesName()
        {
            using var writer = new StringWriter();
            var printer = new NamePrinter(writer);

            printer.Print(new Manager { Name = "Bob" });

            Assert.Equal("Bob", writer.ToString().Trim());
        }

        [Fact]
        public void Print_AcceptsBothViaCommonInterface()
        {
            using var writer = new StringWriter();
            var printer = new NamePrinter(writer);

            var people = new List<INamed>
            {
                new Employee { Name = "Alice" },
                new Manager { Name = "Bob" }
            };

            foreach (var person in people)
            {
                printer.Print(person);
            }

            var lines = writer.ToString()
                .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            Assert.Equal(new[] { "Alice", "Bob" }, lines);
        }

        [Fact]
        public void Print_Null_Throws()
        {
            var printer = new NamePrinter(new StringWriter());

            Assert.Throws<ArgumentNullException>(() => printer.Print(null!));
        }
    }
}
