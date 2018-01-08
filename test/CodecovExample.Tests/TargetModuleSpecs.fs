namespace CodecovExample.Tests

open CodecovExample.TargetProject
open Xunit

module TargetTest =
    let private intFirstNumber = 15;
    let private intsecondNumber = 10;

    [<Fact>]
    let ``when verifing add numbers``() =
        let intResult = Target.AddNumbers intFirstNumber intsecondNumber
        Assert.Equal(15 + 10, intResult)

    [<Fact>]
    let ``when verifing substract numbers``() =
        let intResult = Target.SubractNumbers intFirstNumber intsecondNumber
        Assert.Equal(15 - 10, intResult)
