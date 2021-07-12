using INACALCPROLib;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NcalUnitTest
{
    public class NcalTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateAndInit()
        {
            InaCalcProClass m_pInaCalc = new InaCalcProClass();

            m_pInaCalc.CheckCustomFunction += delegate (string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType)
            {
                int count = argTypes.Count;
                for (int i = 0; i < count; i++)
                {
                    var arg = argTypes[i];
                }
                valType = EInaValueType.inaValNumber;
            };


            m_pInaCalc.EvalCustomFunction += delegate (string strFunc, IInaCalcFuncArgVals argVals, out object vntValue)
            {
                if (string.Compare(strFunc, "add", true) == 0)
                {
                    List<int> values = new List<int>();
                    int count = argVals.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var arg = argVals[i];
                        values.Add(Convert.ToInt32(arg));
                    }
                    vntValue = values.Sum();
                }
                else if (string.Compare(strFunc, "area", true) == 0)
                {
                    List<int> values = new List<int>();
                    int count = argVals.Count;
                    for (int i = 0; i < count; i++)
                    {
                        var arg = argVals[i];
                        values.Add(Convert.ToInt32(arg));
                    }

                    vntValue = (values[2] - values[1]) * values[0];
                }
                else
                {
                    vntValue = null;
                }
            };

            m_pInaCalc.AutoCalc = 0;
            m_pInaCalc.RestrictVariables = 1;

            var pInaCalcFuncs = m_pInaCalc.Funcs;
            var nFuncs = pInaCalcFuncs.Count;
            pInaCalcFuncs.Add("add", EInaFuncCategory.inaFuncCustom, "plus", "plus test");
            pInaCalcFuncs.Add("area", EInaFuncCategory.inaFuncCustom, "get area", "get area");

            var spAtoms = m_pInaCalc.Atoms;


            var spAtomLg = spAtoms["condition"];

            spAtoms["multiplication"].Formula = "12*2";
            spAtomLg.Formula = "logical(multiplication > 3)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(Convert.ToBoolean(spAtomLg.Value) == true);



            var cusAtom = spAtoms["myCustom"];
            cusAtom.Formula = "add(1,2)";
            m_pInaCalc.Recalc();
            var cusAtomValue = cusAtom.Value;
            Assert.IsTrue(cusAtomValue.ToString() == "3");

            Assert.IsTrue(m_pInaCalc.Eval("2+3").ToString() == "5");
            Assert.IsTrue(m_pInaCalc.Eval("2*3").ToString() == "6");
            Assert.IsTrue(m_pInaCalc.Eval("3/2").ToString() == "1.5");

            Assert.IsTrue(m_pInaCalc.Eval("add(3,4)").ToString() == "7");

            spAtoms["ymax"].Value = 12;
            Assert.IsTrue(m_pInaCalc.Eval("1+ymax").ToString() == "13");
            Assert.IsTrue(m_pInaCalc.Eval("1+[ymax]").ToString() == "13");
            Assert.IsTrue(m_pInaCalc.Eval("1+{ymax}").ToString() == "13");

            Assert.IsTrue(m_pInaCalc.Eval("add(ymax,4)").ToString() == "16");


            Assert.IsTrue(m_pInaCalc.Eval("1+myCustom").ToString() == "4");
            Assert.IsTrue(m_pInaCalc.Eval("add(ymax,myCustom)").ToString() == "15");

            var allAtom = spAtoms["all"];
            allAtom.Value = 8;
            var spAtomEq = spAtoms["getarea"];
            spAtomEq.Formula = "area([all],200,400)";
            spAtomLg.Formula = "logical(([getarea] - (4))*(7 - ([getarea])) >= 0)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "1600");
            Assert.IsTrue(Convert.ToBoolean(spAtomLg.Value) == false);

            spAtomEq.Formula = "Log(100,10)"; //log不行
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "2");


            spAtomLg.Formula = "logical(1<>2)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(Convert.ToBoolean(spAtomLg.Value) == true);
            spAtomLg.Formula = "logical(1<>1)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(Convert.ToBoolean(spAtomLg.Value) == false);
            spAtomLg.Formula = "logical(1=1)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(Convert.ToBoolean(spAtomLg.Value) == true);
        }

        [Test]
        public void ErrorTest()
        {
            InaCalcProClass m_pInaCalc = new InaCalcProClass();
            m_pInaCalc.Eval("(1+1(");
            Assert.IsTrue(m_pInaCalc.LastError == EInaErrorValue.inaErrSyntax);
        }

        [Test]
        public void ComplexFunctions()
        {
            InaCalcProClass m_pInaCalc = new InaCalcProClass();

            var pInaCalcFuncs = m_pInaCalc.Funcs;
            pInaCalcFuncs.Add("area", EInaFuncCategory.inaFuncCustom, "get area", "get area");
            pInaCalcFuncs.Add("start", EInaFuncCategory.inaFuncCustom, "", "");
            pInaCalcFuncs.Add("end", EInaFuncCategory.inaFuncCustom, "", "");

            m_pInaCalc.CheckCustomFunction += delegate (string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType)
            {
                int count = argTypes.Count;
                for (int i = 0; i < count; i++)
                {
                    var arg = argTypes[i];
                }
                valType = EInaValueType.inaValNumber;
            };

            m_pInaCalc.EvalCustomFunction += delegate (string strFunc, IInaCalcFuncArgVals argVals, out object vntValue)
            {
                //假定值都是number

                List<int> values = new List<int>();
                int count = argVals.Count;
                for (int i = 0; i < count; i++)
                {
                    var arg = argVals[i];
                    values.Add(Convert.ToInt32(arg));
                }

                if (string.Compare(strFunc, "area", true) == 0)
                {
                    vntValue = (values[2] - values[1]) * values[0];
                }
                else if (string.Compare(strFunc, "start", true) == 0)
                {
                    vntValue = values[0];
                }
                else if (string.Compare(strFunc, "end", true) == 0)
                {
                    vntValue = values[values.Count - 1];
                }
                else
                {
                    vntValue = null;
                }
            };

            var spAtoms = m_pInaCalc.Atoms;

            spAtoms["All"].Value = "10";

            var spAtomEq = spAtoms["getarea"];
            spAtomEq.Formula = "Area([All],Start(20,30),End(40,50))";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "300");

            spAtomEq.Formula = "sqr(Area([All],Start(2,3),End(4,5)))";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "900");

            spAtomEq.Formula = "(1 <> 2) and(2 < 3)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "True");

            spAtomEq.Formula = "(1 = 2) or (2 < 3)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "True");

            spAtomEq.Formula = "not ((1 <> 2) and(2 < 3))";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "False");

        }


        [Test]
        public void MathFunctionTest()
        {
            InaCalcProClass m_pInaCalc = new InaCalcProClass();
            var spAtoms = m_pInaCalc.Atoms;
            var spAtomEq = spAtoms["ee"];

            spAtomEq.Formula = "log(100,10)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "2");

            spAtomEq.Formula = "exp(2)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString().IndexOf("7.389") >= 0);

            spAtomEq.Formula = "sqr(3)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "9");

            spAtomEq.Formula = "sqrt(16)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString() == "4");

            spAtomEq.Formula = "ln(9)";
            m_pInaCalc.Recalc();
            Assert.IsTrue(spAtomEq.Value.ToString().IndexOf("2.197") >= 0);
        }

        [Test]
        public void ParamterTypeTest()
        {
            InaCalcProClass m_pInaCalc = new InaCalcProClass();

            m_pInaCalc.CheckCustomFunction += delegate (string strFunc, IInaCalcFuncArgTypes argTypes, out EInaValueType valType)
            {
                int count = argTypes.Count;
                for (int i = 0; i < count; i++)
                {
                    var arg = argTypes[i];
                }
                valType = EInaValueType.inaValNumber;
            };

            var pInaCalcFuncs = m_pInaCalc.Funcs;
            var nFuncs = pInaCalcFuncs.Count;
            pInaCalcFuncs.Add("getarea", EInaFuncCategory.inaFuncCustom, "", "");
            pInaCalcFuncs.Add("ymax", EInaFuncCategory.inaFuncCustom, "", "");
            pInaCalcFuncs.Add("start", EInaFuncCategory.inaFuncCustom, "", "");
            pInaCalcFuncs.Add("end", EInaFuncCategory.inaFuncCustom, "", "");

            var spAtoms = m_pInaCalc.Atoms;

            spAtoms["area"].Formula = "getarea(3,5)";

            spAtoms["all"].Value = "800";
            var spAtomEq = spAtoms["getymax"];
            spAtomEq.Formula = "ymax(([all]),start(([all])),end(([all])))";

            Assert.IsTrue(m_pInaCalc.LastError == EInaErrorValue.inaErrNone);

            spAtomEq.Formula = "ymax(([all]),start(([all])),end(([all]))))";

            Assert.IsTrue(m_pInaCalc.LastError != EInaErrorValue.inaErrNone);

        }
    }
}
