using JaVisitei.Brasil.Business.Validation.Test.Mocks;
using JaVisitei.Brasil.Business.Validation.Validators;
using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace JaVisitei.Brasil.Business.Validation.Test.Validators
{
    [TestClass]
    public class VisitValidatorTest
    {
        private readonly VisitValidator _visitValidator;

        public VisitValidatorTest()
        {
            _visitValidator = new VisitValidator();
        }

        #region Visit creation

        [TestMethod("Visit creation validation Correct return")]
        public void ValidatesVisitCreation_ShouldCorrectReturn_Validation()
        {
            var request = VisitMock.CreateVisitRequestMock();

            _visitValidator.ValidatesVisitCreation(request);

            Assert.IsTrue(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Visit creation validation Invalid return empty")]
        public void ValidatesVisitCreation_ShouldInvalidReturn_Empty()
        {
            var request = VisitMock.CreateVisitEmptyRequestMock();

            _visitValidator.ValidatesVisitCreation(request);

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(5));
        }

        [TestMethod("Visit creation validation Invalid return nullable")]
        public void ValidatesVisitCreation_ShouldInvalidReturn_Nullable()
        {
            _visitValidator.ValidatesVisitCreation(new InsertVisitRequest());

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(5));
        }

        [TestMethod("Visit creation validation Invalid return excpetion")]
        public void ValidatesVisitCreation_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _visitValidator.ValidatesVisitCreation(It.IsAny<InsertVisitRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Visit delete

        [TestMethod("Visit delete validation Correct return")]
        public void ValidatesVisitDelete_ShouldCorrectReturn_Validation()
        {
            var request = VisitMock.VisitKeyRequestMock();

            _visitValidator.ValidatesVisitDelete(request);

            Assert.IsTrue(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Visit delete validation Invalid return empty")]
        public void ValidatesVisitDelete_ShouldInvalidReturn_Empty()
        {
            var request = VisitMock.VisitKeyEmptyRequestMock();

            _visitValidator.ValidatesVisitDelete(request);

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Visit delete validation Invalid return nullable")]
        public void ValidatesVisitDelete_ShouldInvalidReturn_Nullable()
        {
            _visitValidator.ValidatesVisitDelete(new VisitKeyRequest());

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Visit delete validation Invalid return excpetion")]
        public void ValidatesVisitDelete_ExpresionIsInvalid_Exception()
        {
            var ex = Assert.ThrowsException<ArgumentNullException>(() => _visitValidator.ValidatesVisitDelete(It.IsAny<VisitKeyRequest>()));

            Assert.AreEqual(ex.Message, "Value cannot be null. (Parameter 'request')");
        }

        #endregion

        #region Visit key

        [TestMethod("Visit key validation Correct return")]
        public void ValidatesVisitKey_ShouldCorrectReturn_Validation()
        {
            var request = VisitMock.VisitKeyRequestMock();

            _visitValidator.ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);

            Assert.IsTrue(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(0));
        }

        [TestMethod("Visit key validation Invalid return empty")]
        public void ValidatesVisitKey_ShouldInvalidReturn_Empty()
        {
            var request = VisitMock.VisitKeyEmptyRequestMock();

            _visitValidator.ValidatesVisitKey(request.UserId, request.RegionId, request.RegionTypeId);

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(3));
        }

        [TestMethod("Visit key validation Invalid return nullable")]
        public void ValidatesVisitKey_ShouldInvalidReturn_Nullable()
        {
            _visitValidator.ValidatesVisitKey(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<short>());

            Assert.IsFalse(_visitValidator.IsValid);
            Assert.IsTrue(_visitValidator.Errors.Count.Equals(3));
        }

        #endregion
    }
}