using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;


namespace SeleniumTests
{

    class Tests
    {
        IWebDriver driver;
        IWebElement textboxNum1;
        IWebElement selectOperation;
        IWebElement textboxNum2;
        IWebElement buttonCalclulate;
        IWebElement buttonReset;
        IWebElement result;


        [OneTimeSetUp]
        public void Setup()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://number-calculator.nakov.repl.co/");
            textboxNum1 = driver.FindElement(By.Id("number1"));
            selectOperation = driver.FindElement(By.Id("operation"));
            textboxNum2 = driver.FindElement(By.Id("number2"));
            buttonCalclulate = driver.FindElement(By.Id("calcButton"));
            buttonReset = driver.FindElement(By.Id("resetButton"));
            result = driver.FindElement(By.Id("result"));
        }


        //valid tests with integer numbers
        [TestCase("5", "+", "3", "Result: 8")]
        [TestCase("19", "+", "3", "Result: 22")]
        [TestCase("20", "-", "3", "Result: 17")]
        [TestCase("84", "*", "2", "Result: 168")]
        [TestCase("81", "/", "9", "Result: 9")]
        [TestCase("12", "+", "12", "Result: 24")]

        //invalid tests with integer numbers
        [TestCase("5", "+", "", "Result: invalid input")]
        [TestCase("19", "", "3", "Result: invalid operation")]

        //valid tests with decimal numbers
        [TestCase("5.8", "+", "3", "Result: 8.8")]
        [TestCase("19.25", "*", "3", "Result: 57.75")]
        [TestCase("20", "-", "3", "Result: 17")]

        //invalid tests with decimal numbers
        [TestCase("5.8", "", "3", "Result: invalid operation")]
        [TestCase("", "*", "3", "Result: invalid input")]
        [TestCase("", "-", "", "Result: invalid input")]


        //infinity number testing
        [TestCase("Infinity", "+", "Infinity", "Result: Infinity")]
        [TestCase("Infinity", "-", "3", "Result: Infinity")]
        [TestCase("Infinity", "*", "2", "Result: Infinity")]

        public void TestCalclucatorWebApp(string num1, string op, string num2, string expectedResult)
        {
            //arrange                
            buttonReset.Click();
            if (num1 != "")
                textboxNum1.SendKeys(num1);
            if (op != "")
                selectOperation.SendKeys(op);
            if (num2 != "")
                textboxNum2.SendKeys(num2);


            //act
            buttonCalclulate.Click();

            //assert
            var actualresult = result.Text;
            Assert.That(actualresult, Is.EqualTo(expectedResult));
        }



        [OneTimeTearDown]
        public void TearDown()

        {
            driver.Quit();
        }

    }
}