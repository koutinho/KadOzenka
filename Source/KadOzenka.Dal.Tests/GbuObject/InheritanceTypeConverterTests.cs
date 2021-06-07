using System;
using Core.Register;
using KadOzenka.Dal.GbuObject;
using NUnit.Framework;

namespace KadOzenka.Dal.UnitTests.GbuObject
{
    public class InheritanceTypeConverterTests
    {
        #region Тип родителя - "Строка"

        [Test]
        public void Can_Convert_String_To_String()
        {
            var value = "Test";
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.STRING, RegisterAttributeType.STRING);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Can_Convert_String_To_Date()
        {
            var value = DateTime.Now;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.STRING, RegisterAttributeType.DATE);

            Assert.That(result, Is.EqualTo(value).Within(TimeSpan.FromMilliseconds(50000)));
        }

        [Test]
        public void If_CanNot_Convert_String_To_Date_Return_Error_Message()
        {
            var value = "asd";
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.STRING, RegisterAttributeType.DATE);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        [Test]
        public void Can_Convert_String_To_Number()
        {
            var value = 1.2;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value.ToString(), RegisterAttributeType.STRING, RegisterAttributeType.DECIMAL);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void If_CanNot_Convert_String_To_Number_Return_Error_Message()
        {
            var value = "asd";
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.STRING, RegisterAttributeType.DECIMAL);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        [TestCase("True", ExpectedResult = true)]
        [TestCase("False", ExpectedResult = false)]
        public bool Can_Convert_String_To_Boolean(string parentAttributeStr)
        {
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(parentAttributeStr, RegisterAttributeType.STRING, RegisterAttributeType.BOOLEAN);

            return (bool)result;
        }

        [Test]
        public void If_CanNot_Convert_String_To_Boolean_Return_Error_Message()
        {
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy("Test", RegisterAttributeType.STRING, RegisterAttributeType.BOOLEAN);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        #endregion


        #region Тип родителя - "Дата"

        [Test]
        public void Can_Convert_Date_To_Date()
        {
            var value = DateTime.Now;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DATE, RegisterAttributeType.DATE);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Can_Convert_Date_To_String()
        {
            var value = DateTime.Now;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DATE, RegisterAttributeType.STRING);

            Assert.That(result, Is.EqualTo(value.ToString()));
        }

        [Test]
        public void CanNot_Convert_Date_To_Number()
        {
            var value = DateTime.Now;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DATE, RegisterAttributeType.DECIMAL);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        [Test]
        public void CanNot_Convert_Date_To_Boolean()
        {
            var value = DateTime.Now;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DATE, RegisterAttributeType.BOOLEAN);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        #endregion


        #region Тип родителя - "Число"

        [Test]
        public void Can_Convert_Number_To_Number()
        {
            var value = 1;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DECIMAL, RegisterAttributeType.DECIMAL);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Can_Convert_Number_To_String()
        {
            var value = 1;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DECIMAL, RegisterAttributeType.STRING);

            Assert.That(result, Is.EqualTo(value.ToString()));
        }

        [Test]
        public void CanNot_Convert_Number_To_Date()
        {
            var value = 1;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DECIMAL, RegisterAttributeType.DATE);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        [Test]
        public void CanNot_Convert_Number_To_Boolean()
        {
            var value = 1;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.DECIMAL, RegisterAttributeType.BOOLEAN);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        #endregion


        #region Тип родителя - "Логическое значение"

        [Test]
        public void Can_Convert_Bool_To_Bool()
        {
            var value = true;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.BOOLEAN, RegisterAttributeType.BOOLEAN);

            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void Can_Convert_Bool_To_String()
        {
            var value = true;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.BOOLEAN, RegisterAttributeType.STRING);

            Assert.That(result, Is.EqualTo(value.ToString()));
        }

        [Test]
        public void CanNot_Convert_Bool_To_Date()
        {
            var value = true;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.BOOLEAN, RegisterAttributeType.DATE);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        [Test]
        public void CanNot_Convert_Bool_To_Number()
        {
            var value = true;
            var result = GbuObjectInheritanceAttribute.GetAttributeValueToCopy(value, RegisterAttributeType.BOOLEAN, RegisterAttributeType.DECIMAL);

            Assert.That(result, Contains.Substring(GbuObjectInheritanceAttribute.ErrorMessageForChildConverting));
        }

        #endregion
    }
}
