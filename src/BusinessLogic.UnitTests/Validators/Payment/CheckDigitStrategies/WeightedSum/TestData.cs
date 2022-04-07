using BusinessLogic.Enums;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Validators.Payment.CheckDigitStrategies.WeightedSum
{
    public static class TestHelpers
    {
        public static IEnumerable<object[]> InvalidTestData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "98",
                            ResultSubstitutions = "10:Z",
                            Weightings = "2,0,3,0,4,0,5,0,0,0",
                            ApplySubtraction = true
                        },
                        "AB1234566",
                        '6',
                    },
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "",
                            ResultSubstitutions = "10:Z",
                            Weightings = "9,8,7,6,5,4,3,2,1,0,0",
                            ApplySubtraction = true
                        },
                        "9634561384",
                        '4',
                    },
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "",
                            ResultSubstitutions = "10:Z",
                            Weightings = "9,8,7,6,5,4,3,2,1,0,0",
                            ApplySubtraction = false
                        },
                        "9634561381",
                        '1',
                    }
                };
            }
        }

        public static IEnumerable<object[]> ValidTestData
        {
            get
            {
                return new[]
                {
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "98",
                            ResultSubstitutions = "10:Z",
                            Weightings = "2,0,3,0,4,0,5,0,0,0",
                            ApplySubtraction = true
                        },
                        "AB1234562",
                        '2',
                    },
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "",
                            ResultSubstitutions = "10:Z",
                            Weightings = "9,8,7,6,5,4,3,2,1,0,0",
                            ApplySubtraction = true
                        },
                        "963456138Z",
                        'Z',
                    },
                    new object[]
                    {
                        new Entities.CheckDigitConfiguration()
                        {
                            Type = (int)CheckDigitType.WeightedSum,
                            Modulus = 10,
                            SourceSubstitutions = "",
                            ResultSubstitutions = "10:Z",
                            Weightings = "9,8,7,6,5,4,3,2,1,0,0",
                            ApplySubtraction = false
                        },
                        "9634561380",
                        '0',
                    }
                };
            }
        }
    }
}
