using JaVisitei.Brasil.Business.ViewModels.Request.Email;

namespace JaVisitei.Brasil.Business.Validation.Test.Mocks
{
    public static class EmailMock
    {
        public static SendEmailUserManagerRequest ReturnSendEmailRequestMock()
        {
            return new SendEmailUserManagerRequest
            {
                Id = 1,
                UserManagerId = 1,
                Email = "teste@teste.com.zz",
                ManagerCode = "ABC123AB"
            };
        }

        public static SendEmailUserManagerRequest ReturnSendEmailRequestInvalidMock()
        {
            return new SendEmailUserManagerRequest
            {
                Id = 0,
                UserManagerId = 0,
                Email = "testetestecomzz",
                ManagerCode = "#$"
            };
        }

        public static SendEmailUserManagerRequest ReturnSendEmailRequestEmptyMock()
        {
            return new SendEmailUserManagerRequest
            {
                Email = "",
                ManagerCode = ""
            };
        }
    }
}
