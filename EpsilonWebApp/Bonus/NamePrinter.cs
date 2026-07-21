namespace EpsilonWebApp.Bonus
{
    /// <summary>
    /// Prints the name of any named entity (Employee, Manager, ...).
    /// The output writer is injectable so the behaviour can be unit tested
    /// without capturing the real console.
    /// </summary>
    public class NamePrinter
    {
        private readonly TextWriter _output;

        public NamePrinter(TextWriter? output = null)
        {
            _output = output ?? Console.Out;
        }

        /// <summary>
        /// Accepts either a Manager or an Employee (or any INamed) and prints its name.
        /// </summary>
        public void Print(INamed named)
        {
            ArgumentNullException.ThrowIfNull(named);
            _output.WriteLine(named.Name);
        }
    }
}
