namespace Pixelworld.Utilities;

internal class Generator
{
    private static readonly Random rand = new();

    /// <summary>
    /// Also includes the max value.
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>A random value in the specified range.</returns>
    public static int Number(int min, int max)
    {
        return rand.Next(min, max + 1);
    }

    /// <summary>
    /// Generates a value which is slightly greater or less than the current value.
    /// </summary>
    /// <param name="currentNumber"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns>A random value in the specified range.</returns>
    public static int NextNumber(int currentNumber, int min, int max)
    {
        int nextNumber;
        do
        {
            nextNumber = rand.Next(min, max + 1);
        } while (nextNumber == currentNumber || nextNumber > currentNumber + 1 || nextNumber < currentNumber - 1);

        return nextNumber;
    }

    /// <summary>
    /// Returns true with a probability equal to the provided chance, and false otherwise.
    /// This method can be used to simulate random events with a specific probability.
    /// </summary>
    /// <param name="chance">A float value between 0 and 1 representing the probability of returning true.</param>
    /// <returns>true if the random event occurs, false otherwise.</returns>
    public static bool Chance(float chance)
    {
        return rand.NextDouble() < chance;
    }
}
