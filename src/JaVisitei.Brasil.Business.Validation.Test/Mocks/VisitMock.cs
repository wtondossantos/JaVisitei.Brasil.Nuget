using JaVisitei.Brasil.Business.ViewModels.Request.Visit;
using System;

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
                UserId = Guid.NewGuid().ToString()
        };
        }

        public static VisitKeyRequest VisitKeyInvalidRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 0,
                RegionId = "ce aquiraz",
                UserId = "1234567890123456789012345678901234567"
            };
        }

        public static VisitKeyRequest VisitKeyEmptyRequestMock()
        {
            return new VisitKeyRequest
            {
                RegionTypeId = 0,
                RegionId = "",
                UserId = ""
            };
        }

        public static InsertVisitRequest CreateVisitRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,100,50",
                VisitationDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Note = "Annotation",
                RegionTypeId = 6,
                RegionId = "ce_aquiraz",
                UserId = Guid.NewGuid().ToString()
            };
        }

        public static InsertVisitRequest CreateVisitInvalidRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = "200,10050",
                VisitationDate = "02-072022",
                Note = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum eu eros sagittis, laoreet sem sed, dapibus ipsum. Proin efficitur tincidunt ex, ac fringilla nisl malesuada sed. Morbi sagittis augue at libero condimentum, eu dictum ante venenatis donec.",
                RegionTypeId = 0,
                RegionId = "sdfas sdf",
                UserId = null
            };
        }

        public static InsertVisitRequest CreateVisitEmptyRequestMock()
        {
            return new InsertVisitRequest
            {
                Color = string.Empty,
                VisitationDate = string.Empty,
                RegionTypeId = 0,
                Note = string.Empty,
                RegionId = string.Empty,
                UserId = string.Empty
            };
        }
    }
}
