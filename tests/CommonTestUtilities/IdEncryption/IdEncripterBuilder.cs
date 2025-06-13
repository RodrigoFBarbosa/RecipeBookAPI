using Sqids;

namespace CommonTestUtilities.IdEncryption;

public class IdEncripterBuilder
{
    public static SqidsEncoder<long> Build()
    {
        return new SqidsEncoder<long>(new()
        {
            MinLength = 3,
            Alphabet = "yN8Xwpqk3WPmeDdIbSOR9uzC6JHMEf2h7BxiZYVc4sLQ0AnUKvGoF1rla5tgjT",
        });
    }
}
