using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Vendia.Extensions;

namespace Vendia.Tests
{

  [ TestClass ]
  public class UnitTest1
  {
    [ TestMethod ]
    public void TestMethod1() => Assert.IsTrue(1.Plus(9) == 10);

    [ TestMethod ]
    public void TestMethod2() => Assert.IsTrue(12.Minus(2) == 10);

    [ TestMethod ]
    public void TestMethod3() => Assert.IsTrue(5.Times(2) == 10);

    [ TestMethod ]
    public void TestMethod4() => Assert.IsTrue(20.DividedBy(2) == 10);

    [ TestMethod ]
    public void TestMethod5()
    {
    }
  }

}