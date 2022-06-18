using JaVisitei.Brasil.Business.ViewModels.Request.Visit;

namespace JaVisitei.Brasil.Business.Validation.Test.Mocks
{
    public static class VisitMock
    {
        public static VisitKeyRequest VisitKeyRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = 1
            };
        }

        public static VisitKeyRequest VisitKeyInvalidRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 0,
                RegionId = "ce aquiraz",
                UserId = 0
            };
        }

        public static VisitKeyRequest VisitKeyEmptyRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 0,
                RegionId = "",
                UserId = 0
            };
        }

        public static InsertVisitRequest CreateVisitRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,100,50",
                VisitationDate = "02-07-2022",
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = 1
            };
        }

        public static InsertVisitRequest CreateVisitInvalidRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,10050",
                VisitationDate = "02-072022",
                RegionTypeId = 0,
                RegionId = "sdfas sdf",
                UserId = 0
            };
        }

        public static InsertVisitRequest CreateVisitEmptyRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "",
                VisitationDate = "",
                RegionTypeId = 0,
                RegionId = "",
                UserId = 0
            };
        }
    }
}
