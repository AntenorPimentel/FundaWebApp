using System.ComponentModel;

namespace Funda.Business.Models
{
    public class Enums
    {
        public enum Cities : int
        {
            [Description("Amsterdam")]
            Amsterdam = 0,
            [Description("Delft")]
            Delft = 1,
            [Description("Leiden")]
            Leiden = 2,
            [Description("Rotterdam")]
            Rotterdam = 3,
            [Description("Utrecht")]
            Utrecht = 4,
        }

        public enum Buitenruimte : int
        {
            [Description("Balkon")]
            Balkon = 0,
            [Description("Dakterras")]
            Dakterras = 1,
            [Description("Tuin")]
            Tuin = 2,
        }
    }
}